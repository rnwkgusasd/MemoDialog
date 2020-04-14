using System;
using System.Windows.Forms;

namespace MemoDialog.Forms
{
    public partial class MemoPanelFrm : Form
    {
        public string LabelText = "";
        public string LabelDateTime = "";

        public delegate void EditMemo(string tData);
        public event EditMemo mEditEvent;

        public MemoPanelFrm(string pText, string pDateTime)
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(Classes.RoundRectRgn.CreateRoundRectRgn(0, 0, Width, Height, 10, 15));
            LabelText = pText;
            label1.Text = pText;
            LabelDateTime = pDateTime;
            label2.Text = pDateTime;
        }

        private void MemoPanelFrm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Parent.Height - 5 >= 0)
            {
                this.Parent.Height -= 5;
            }

            if (this.Parent.Height <= 0) this.Close();
        }

        private void MemoPanelFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;

            this.Parent.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mEditEvent(LabelText);

            this.Close();
        }
    }
}
