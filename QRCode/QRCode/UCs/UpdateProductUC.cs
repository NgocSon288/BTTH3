using QRCode.Common;
using QRCode.Models;
using QRCode.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCode.UCs
{
    public partial class UpdateProductUC : UserControl
    {
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;

        private List<Category> categories;
        private Product product;
        private string oldPathImage;

        public UpdateProductUC(Product product)
        {
            InitializeComponent();

            this._categoryService = new CategoryService();
            this._productService = new ProductService();
            this.product = product;
            this.oldPathImage = product.Image;

            SetUpUI();
            Load();
        }

        #region Methods

        new private void Load()
        {
            categories = _categoryService.GetAll();
            cbbCategory.DataSource = categories;
            cbbCategory.DisplayMember = "Name";

            LoadPorductUI();
        }

        private void LoadPorductUI()
        {
            txtID.Text = product.ID;
            txtName.Text = product.Name;
            txtPrice.Text = product.Price.ToString();
            txtDescription.Text = product.Description;
            cbbCategory.SelectedItem = categories.FirstOrDefault(item => item.ID == product.Category);
            picImage.BackgroundImage = new Bitmap($"./../../Assets/Images/{product.Image}");
        }

        private bool CheckValidateValue()
        {
            var check = true;

            check = !CheckValidate(txtName, pnlName) ? false : check;
            check = !CheckValidate(txtPrice, pnlPrice, true) ? false : check;
            check = !CheckValidate(txtDescription, pnlDescription) ? false : check;

            if (product.Image == null || product.Image == "")
            {
                check = false;

                pnlImage.BackColor = Color.Red;
            }

            return check;
        }

        private bool CheckValidate(TextBox txt, Control pnl, bool isNumber = false)
        {
            Color color_invalid = Color.FromArgb(255, 0, 0);

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.ForeColor = color_invalid;
                pnl.BackColor = color_invalid;

                return false;
            }
            else if (isNumber && !decimal.TryParse(txt.Text, out decimal res))
            {
                txt.ForeColor = color_invalid;
                pnl.BackColor = color_invalid;

                return false;
            }
            else
            {
                txt.ForeColor = Color.FromArgb(102, 139, 172);
                pnl.BackColor = Color.FromArgb(102, 139, 172);

                return true;
            }
        }

        #endregion

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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (CheckValidateValue())
            {
                product.Name = txtName.Text;
                product.Description = txtDescription.Text;
                product.Price = Convert.ToDecimal(txtPrice.Text);


                if (product.Image != oldPathImage)
                {
                    var fileName = Path.GetFileName(product.Image);
                    fileName = new Random().Next(0, 1000000000).ToString() + fileName;

                    if (!File.Exists(Path.Combine("../../Assets/Images/", fileName)))
                    {
                        File.Copy(product.Image, Path.Combine("../../Assets/Images/", fileName));
                        product.Image = fileName;
                    }
                }

                var a = product;

                if (_productService.UpdateWithoutGuid(product))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo!");
                }
            }
            else
            {
                MessageBox.Show("Thông tin sản phẩm không hợp lệ", "Thông báo!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnBack_Click(sender, e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            txt.ForeColor = Color.FromArgb(68, 170, 190);
            (txt.Parent.Controls[0]).BackColor = Color.FromArgb(88, 190, 210);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            var name = txt.Name;
            var isNumber = name.Contains("Price");
            CheckValidate(txt, txt.Parent.Controls[0], isNumber);
        }

        private void picImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn một hình ảnh";
            openFileDialog.Filter = "txt files (*.jpg)|*.jpg|(*.png)|*.png|(*.jpeg)|*.jpeg|(*.jfif)|*.jfif|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;

                if (File.Exists(fileName))
                {
                    product.Image = fileName;
                    pnlImage.BackColor = Color.FromArgb(68, 226, 255);
                    picImage.BackgroundImage = Image.FromFile(fileName);
                }
                else
                {
                    pnlImage.BackColor = Color.Red;
                }
            }
            else
            {
                if (product.Image == null || product.Image == "")
                {
                    pnlImage.BackColor = Color.Red;
                }
            }
        }

        #endregion
    }
}
