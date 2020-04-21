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
    public partial class SettingsFrm : Form
    {
        private List<UI_Template.RoundButton> RadioList = new List<UI_Template.RoundButton>();

        public SettingsFrm()
        {
            InitializeComponent();

            RadioList.Add(rdoTone1);
            RadioList.Add(rdoTone2);
            RadioList.Add(rdoTone3);
        }

        public delegate void ReceiveSelectedColor(int num);
        public ReceiveSelectedColor r_event;

        private void roundButton1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < RadioList.Count; i++)
            {
                if(RadioList[i].BorderColor == Color.Orange)
                {
                    r_event(i);
                    break;
                }
            }

            this.Close();
        }

        private void ToneRadioButton_Click(object sender, EventArgs e)
        {
            foreach(UI_Template.RoundButton btn in RadioList)
            {
                btn.BorderColor = Color.Silver;

                btn.OnHoverBorderColor = Color.Gray;
            }

            UI_Template.RoundButton tBtn = (UI_Template.RoundButton)sender;

            Color tBorderColor = Color.Orange;

            tBtn.BorderColor = tBorderColor;

            tBtn.OnHoverBorderColor = tBorderColor;
        }
    }
}
