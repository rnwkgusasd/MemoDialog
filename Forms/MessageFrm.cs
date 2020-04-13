using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoDialog.Forms
{
    public partial class MessageFrm : Form
    {
        public MessageFrm(string pMsg)
        {
            InitializeComponent();
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
