using System;
using System.Windows.Forms;

namespace MemoDialog.Forms
{
    public partial class MessageFrm : Form
    {
        public MessageFrm(string pMsg)
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(Classes.RoundRectRgn.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
            label1.Text = pMsg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public static DialogResult ShowDialog(string pMsg)
        {
            MessageFrm frm = new MessageFrm(pMsg);

            return frm.ShowDialog();
        }
    }
}
