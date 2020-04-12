using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MemoDialog.Forms
{
    public partial class MemoPanelFrm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public string LabelText = "";
        public string LabelDateTime = "";

        public MemoPanelFrm(string pText, string pDateTime)
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 10, 15));
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
    }
}
