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
    public partial class CreateProductUC : UserControl
    {
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;

        private List<Category> categories;
        private List<Product> products;
        private Product product;

        public CreateProductUC()
        {
            InitializeComponent();

            this._categoryService = new CategoryService();
            this._productService = new ProductService();

            SetUpUI();
            Load();
        }

        #region Methods

        new private void Load()
        {
            categories = _categoryService.GetAll();
            products = _productService.GetAll();
            product = new Product();
            product.ID = GetProductID();
            product.Category = categories[0].ID;

            cbbCategory.DataSource = categories;
            cbbCategory.DisplayMember = "Name";

            txtID.Text = product.ID;
        }

        private string GetProductID()
        {
            return $"SP{(products.Count + 1).ToString().PadLeft(3, '0')}";
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

        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (CheckValidateValue())
            {
                product.Name = txtName.Text;
                product.Description = txtDescription.Text;
                product.Price = Convert.ToDecimal(txtPrice.Text);


                var fileName = Path.GetFileName(product.Image);
                fileName = new Random().Next(0, 1000000000).ToString() + fileName;

                if (!File.Exists(Path.Combine("../../Assets/Images/", fileName)))
                {
                    File.Copy(product.Image, Path.Combine("../../Assets/Images/", fileName));
                    product.Image = fileName;
                }

                if (_productService.Insert(product))
                {
                    MessageBox.Show("Lưu thành công!");
                }
            }
            else
            {
                MessageBox.Show("Thông tin sản phẩm không hợp lệ");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnBack_Click(sender, e);
        }

        private void txtID_Enter(object sender, EventArgs e)
        {
            txtID.ForeColor = Color.FromArgb(68, 226, 255);

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

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemSlected = cbbCategory.SelectedItem as Category;

            if (itemSlected != null)
            {
                product.Category = itemSlected.ID;
            }
        }

        #endregion
    }
}
