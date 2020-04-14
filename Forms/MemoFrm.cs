using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MemoDialog.Forms
{
    public partial class MemoFrm : Form
    {
        private bool IsDraging = false;
        private bool IsDraging2 = false;

        private Point ClickPos;
        private Point CurrMousePos;

        private string memoPath = Application.StartupPath + "\\MEMO.txt";
        
        public MemoFrm()
        {
            InitializeComponent();

            this.Region = System.Drawing.Region.FromHrgn(Classes.RoundRectRgn.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void DragForm(object sender, MouseEventArgs e)
        {
            IsDraging = true;

            ClickPos.Y = e.Y;
            ClickPos.X = e.X;
            CurrMousePos = Cursor.Position;

            Thread DragForm = new Thread(DragThread);

            DragForm.Start();
        }

        private void DragOutForm(object sender, MouseEventArgs e)
        {
            IsDraging = false;
        }

        private void DragThread()
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                while (IsDraging)
                {
                    CurrMousePos = Cursor.Position;

                    CurrMousePos.X -= ClickPos.X;//X;
                    CurrMousePos.Y -= ClickPos.Y;

                    Invoke(new Action(() => this.Location = CurrMousePos));
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(MessageFrm.ShowDialog("Exit Program ?") == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MemoFrm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.AutoScroll = false;
            flowLayoutPanel1.HorizontalScroll.Enabled = false;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            flowLayoutPanel1.HorizontalScroll.Maximum = 0;
            flowLayoutPanel1.AutoScroll = true;

            LoadMemo(memoPath);
        }

        private int ClickX;
        private int MouseX;

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            IsDraging2 = true;

            ClickX = e.X;
            MouseX = Cursor.Position.X;

            Thread DragTrackBarTh = new Thread(DragTrackBar);

            DragTrackBarTh.Start();
        }

        private void DragTrackBar()
        {
            while (IsDraging2)
            {
                MouseX = Cursor.Position.X;

                MouseX -= ClickX + this.Location.X + panel6.Location.X;

                if(MouseX >= panel7.Location.X && MouseX <= panel7.Location.X + panel7.Width - panel8.Width)
                    Invoke(new Action(() => panel8.Location = new Point(MouseX, panel8.Location.Y)));

                Application.DoEvents();
            }
        }

        private void panel8_MouseUp(object sender, MouseEventArgs e)
        {
            IsDraging2 = false;
        }

        private void panel8_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                double value = (double)(panel7.Width - ((panel7.Width - panel8.Width) - panel8.Location.X)) / (double)(panel7.Width - panel8.Width);
                this.Opacity = value;

                int tIntValue = (int)(value * 100);

                if (tIntValue <= 100)
                    label3.Text = tIntValue + " %";
            }
            catch (Exception)
            {
                this.Opacity = 0;
            }
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    if (textBox1.Lines.Length > 3)
        //    {
        //        List<string> strList = textBox1.Lines.ToList();
        //        strList.RemoveAt(textBox1.Lines.Length - 1);
        //        textBox1.Lines = strList.ToArray();

        //        textBox1.SelectionStart = textBox1.TextLength;
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim() != "")
            {
                AddMemo(textBox1.Text);
            }
        }

        private void MemoFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMemo(memoPath);
        }

        private void SaveMemo(string pPath)
        {
            FileInfo fi = new FileInfo(pPath);

            if (!fi.Directory.Exists) fi.Directory.Create();
            if (!fi.Exists) File.Delete(pPath);

            if (flowLayoutPanel1.Controls.Count == 0)
            {
                FileStream f = File.Create(fi.FullName);
                f.Close();

                return;
            }

            List<string> labelTextList = new List<string>();

            foreach(Panel pan in flowLayoutPanel1.Controls)
            {
                MemoPanelFrm frm = (MemoPanelFrm)pan.Controls[0];
                labelTextList.Add(string.Format($"{frm.LabelText},{frm.LabelDateTime}"));
            }
            
            File.WriteAllLines(fi.FullName, labelTextList);
        }

        private void LoadMemo(string pPath)
        {
            FileInfo fi = new FileInfo(pPath);

            if (!fi.Directory.Exists) fi.Directory.Create();
            if (!fi.Exists)
            {
                FileStream f = File.Create(fi.FullName);
                f.Close();
            }

            Thread.Sleep(100);

            string[] readFile = File.ReadAllLines(fi.FullName);

            if(readFile.Length != 0)
            {
                foreach(string s in readFile)
                {
                    string[] tSplit = s.Split(',');
                    AddMemo(tSplit[0], tSplit[1]);
                }
            }
        }

        private void AddMemo(string pText, string pDateTime=null)
        {
            Panel tPn = new Panel();
            tPn.Size = new Size(300, 80);
            tPn.BackColor = Color.FromArgb(255, 250, 235);
            tPn.BorderStyle = BorderStyle.None;
            tPn.Region = System.Drawing.Region.FromHrgn(Classes.RoundRectRgn.CreateRoundRectRgn(0, 0, tPn.Width, tPn.Height, 10, 15));

            string tDateTime = pDateTime == null ? DateTime.Now.ToString("yyyy.MM.dd") : pDateTime;

            MemoPanelFrm frm = new MemoPanelFrm(pText, tDateTime)
            {
                Dock = DockStyle.Fill,
                TopLevel = false,
                TopMost = true,
                Parent = tPn
            };

            frm.mEditEvent += (data) => 
            {
                Invoke(new Action(() =>
                {
                    textBox1.Text = data;
                }));
            };

            tPn.Controls.Add(frm);
            frm.Show();

            textBox1.Clear();
            textBox1.Focus();

            flowLayoutPanel1.Controls.Add(tPn);
        }
    }
}
