using FontAwesome.Sharp;
using QRCode.Common;
using QRCode.Models;
using QRCode.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCode
{
    public partial class fMain : Form
    {
        private readonly CategoryFilmService _categoryFilmService;

        private IconButton currentBtn;
        private Panel leftBorderBtn;

        public fMain()
        {
            InitializeComponent();

            Load();
        }

        new private void Load()
        {
            Constants.MainForm = this;

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 100);
            panelMenu.Controls.Add(leftBorderBtn);

            // Set logo
            imgLogo.BackgroundImage = new Bitmap("./../../Assets/Images/logo.png");

            Reset();
        }

        private void ActivateButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableButton();

                //Button transition
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = Constants.BORDER_MENU_LEFT_COLOR;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Constants.BORDER_MENU_LEFT_COLOR;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Left border button
                leftBorderBtn.BackColor = Constants.BORDER_MENU_LEFT_COLOR;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;

            var fOrder = new fOrder();
            UIHelper.ShowControl(fOrder, panelContent);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            var f = new fOrder();
            UIHelper.ShowControl(f, panelContent);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            var f = new fProduct();
            UIHelper.ShowControl(f, panelContent);
        }

        private void btnOrder1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            var f = new fAnother();
            UIHelper.ShowControl(f, panelContent);
        }

        private void btnOrder2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            var f = new fAnother();
            UIHelper.ShowControl(f, panelContent);
        }

        private void imgLogo_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
