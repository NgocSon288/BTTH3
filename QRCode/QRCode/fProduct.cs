using QRCode.Common;
using QRCode.Models;
using QRCode.Services;
using QRCode.UCs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ZXing;

namespace QRCode
{
    public partial class fProduct : UserControl
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        private List<Product> products;
        private Dictionary<Guid, string> categories;

        private ProductItemUC currentRowProduct;

        public fProduct()
        {
            InitializeComponent();

            this._productService = new ProductService();
            this._categoryService = new CategoryService();

            SetUpUI();
            Load();
        }

        #region Methods

        new private void Load()
        {
            products = _productService.GetAll();
            categories = _categoryService.GetAll().ToDictionary(item => item.ID, item => item.Name);

            // sắp xếp sản phẩm theo mã sản phẩm
            products = products.OrderBy(pro => Convert.ToInt32(pro.ID.Substring(2))).ToList();

            if (products.Count > 0)
            {
                LoadListView();
                LoadDetail();
                currentRowProduct?.OrderItemUC_Click(currentRowProduct, new EventArgs());
            }
        }

        private void LoadListView()
        {
            foreach (var item in products)
            {
                var productItemUC = new ProductItemUC(item);
                productItemUC.Tag = item;
                productItemUC.ClickItemForm += ProductItemUC_Click;

                flpItems.Controls.Add(productItemUC);
            }

            currentRowProduct = flpItems.Controls[0] as ProductItemUC;
        }

        private void ProductItemUC_Click(object sender, EventArgs e)
        {
            currentRowProduct = sender as ProductItemUC;
            ResetColorItems(currentRowProduct);
            LoadDetail();
        }

        private void ResetColorItems(ProductItemUC productItemUC = null)
        {
            // Reset BG color
            if (flpItems.Controls.Count > 0)
            {
                foreach (ProductItemUC item in flpItems.Controls)
                {
                    if (item != productItemUC)
                        item.RestBackgroundColor();
                }
            }
        }

        private void LoadDetail()
        {
            if (currentRowProduct != null)
            {
                var prod = currentRowProduct.Tag as Product;
                lblID.Text = prod.ID;
                lblName.Text = prod.Name;
                lblPrice.Text = $"{prod.Price.ToString("#,##")} VND";
                lblCategory.Text = categories[prod.Category];
                lblDescription.Text = prod.Description;
                picImageOrQR.BackgroundImage = new Bitmap($"./../../Assets/Images/{prod.Image}");
            }
            else
            {
                lblID.Text = ". . .";
                lblName.Text = ". . .";
                lblPrice.Text = ". . .";
                lblCategory.Text = ". . .";
                lblDescription.Text = ". . .";
                picImageOrQR.BackgroundImage = null;
            }

            picImageOrQR.Tag = "IMAGE";
        }

        #endregion Methods

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

        #endregion SetUpUI

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

        #endregion Header

        #region Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var f = new CreateProductUC();
            UIHelper.ShowControl(f, Constants.MainForm.panelContent);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var f = new UpdateProductUC(currentRowProduct.Tag as Product);
            UIHelper.ShowControl(f, Constants.MainForm.panelContent);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentRowProduct == null)
            {
                MessageBox.Show("Không thể xóa sản phẩm!");
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa sản phẩm này!", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var productDelete = currentRowProduct.Tag as Product;
                var pathDelete = productDelete.Image;
                _productService.DeleteWithoutGuid(productDelete);

                flpItems.Controls.Remove(currentRowProduct);

                if (flpItems.Controls.Count > 0)
                {
                    currentRowProduct = flpItems.Controls[0] as ProductItemUC;
                }
                else
                {
                    currentRowProduct = null;
                }

                LoadDetail();
                currentRowProduct.OrderItemUC_Click(currentRowProduct, new EventArgs());

                MessageBox.Show("Xóa thành công", "Thông báo");
            }
        }

        private void picImageOrQR_Click(object sender, EventArgs e)
        {
            if (picImageOrQR.Tag.ToString() == "IMAGE")
            {
                picImageOrQR.BackgroundImage = QRCodeHelper.ConvertToBitmap(lblID.Text);

                picImageOrQR.Tag = "QR";
            }
            else
            {
                var product = currentRowProduct.Tag as Product;

                picImageOrQR.BackgroundImage = new Bitmap($"./../../Assets/Images/{product.Image}");

                picImageOrQR.Tag = "IMAGE";
            }
        }

        private void btnExportQRCode_Click(object sender, EventArgs e)
        {
            try
            {
                var initialPath = @"C:\Desktop\HK2-năm 3\Ngôn ngữ lập trình C# - CS511.L21\BTTH\BTTH3\Exports";
                var imgQR = QRCodeHelper.ConvertToBitmap(lblID.Text);
                var dialog = new SaveFileDialog();
                dialog.InitialDirectory = initialPath;
                dialog.Filter = "txt files (*.jpg)|*.jpg|(*.png)|*.png|(*.jpeg)|*.jpeg|(*.jfif)|*.jfif|All files (*.*)|*.*";
                dialog.FileName = lblName.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imgQR.Save(dialog.FileName);
                    //var a = dialog.FileName;
                    MessageBox.Show("Lưu thành công!", "Thông báo");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi vui lòng thử lại!", "Thông báo!");
            }
        }

        #endregion Events
    }
}