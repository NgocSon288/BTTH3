using QRCode.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCode.UCs
{
    public partial class UpdateProductUC : UserControl
    {
        public UpdateProductUC()
        {
            InitializeComponent();

            SetUpUI();
        }

        #region SetUpUI

        private void SetUpUI()
        {
            // Set background color for form
            this.BackColor = Constants.MAIN_BACK_COLOR;

            // Set color for button control window
            btnMinimize.BackColor = Constants.MAIN_BACK_COLOR;
            btnMinimize.ForeColor = Constants.MAIN_FORE_COLOR;
            btnMinimize.FlatAppearance.MouseOverBackColor = Constants.CONTROLS_WINDOW_MOUSE_OVER_BACK_COLOR;
            btnClose.BackColor = Constants.MAIN_BACK_COLOR;
            btnClose.ForeColor = Constants.MAIN_FORE_COLOR;
            btnClose.FlatAppearance.MouseOverBackColor = Constants.CONTROLS_WINDOW_MOUSE_OVER_BACK_COLOR;
        }

        #endregion

        #region Header

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMinimize_Click(object sender, System.EventArgs e)
        {
            Constants.MainForm.WindowState = FormWindowState.Minimized;
        }

        #endregion


        #region Events 

        private void btnBack_Click(object sender, EventArgs e)
        {
            var f = new fProduct();
            UIHelper.ShowControl(f, Constants.MainForm.panelContent);
        }

        private void btnBack_MouseEnter(object sender, EventArgs e)
        {
            btnBack.IconColor = Color.FromArgb(255, 34, 101);
            btnBack.ForeColor = Color.FromArgb(255, 34, 101);
        }

        private void btnBack_MouseLeave(object sender, EventArgs e)
        {
            btnBack.IconColor = Color.FromArgb(0, 151, 230);
            btnBack.ForeColor = Color.FromArgb(0, 151, 230);
        }

        #endregion
    }
}
