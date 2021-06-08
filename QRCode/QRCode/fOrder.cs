using QRCode.Common;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using ZXing;
using QRCode.Services;
using QRCode.Models;
using System.Collections.Generic;
using System.Linq;
using QRCode.UCs;
using System;
using System.Data;

namespace QRCode
{
    public partial class fOrder : UserControl
    {
        private readonly ProductService _productuctService;

        private List<Product> products;
        private Product product;

        private List<OrderDetail> orderDetails;
        private OrderDetail currentOrderDetail;
        private OrderItemUC currentOrderItemUC;

        private EmptyOrderUC EmptyOrderUC = new EmptyOrderUC();

        public fOrder()
        {
            InitializeComponent();

            this._productuctService = new ProductService();

            SetUpUI();
            Load();
        }

        #region Methods 

        new private void Load()
        {
            EnableButtonControls(false, false, false);

            products = _productuctService.GetAll();
            orderDetails = new List<OrderDetail>();

            // Nếu không có sản phẩm nào thì thêm ảnh rỗng vào
            flpItems.Controls.Add(EmptyOrderUC);
            SetTotalPrice();
        }

        private void LoadDetail()
        {
            if (currentOrderDetail == null)
            {
                lblID.Text = ". . .";
                lblName.Text = ". . .";
                lblPrice.Text = ". . .";
                nudCount.Value = 1;
                picImage.BackgroundImage = null;

                EnableButtonControls(false, false, false);
                flpItems.Controls.Add(EmptyOrderUC);
                return;
            }

            lblID.Text = currentOrderDetail.ProductID;
            lblName.Text = currentOrderDetail.Name;
            lblPrice.Text = $"{currentOrderDetail.Price.ToString("#,##")} VND";
            nudCount.Value = currentOrderDetail.Count;
            picImage.BackgroundImage = new Bitmap($"./../../Assets/Images/{currentOrderDetail.Image}");
        }

        private void ResetColorItems(OrderItemUC orderItemUC = null)
        {
            // Reset BG color
            if (orderDetails.Count > 0)
            {
                foreach (OrderItemUC item in flpItems.Controls)
                {
                    if (item != orderItemUC)
                        item.RestBackgroundColor();
                }
            }
        }

        private void HandleImportFile()
        {
            ResetColorItems();

            try
            {
                // Kiểm tra  sản phẩm có trong danh sách order chưa
                var od = orderDetails.FirstOrDefault(o => o.ProductID == product.ID);
                var isExists = od != null;

                // Nếu không tồn tại
                if (!isExists)
                {
                    EnableButtonControls(true, false, true);
                    currentOrderDetail = new OrderDetail(product);
                    currentOrderItemUC = null;
                }
                // Nếu tồn tại  trong danh sách order
                else
                {
                    EnableButtonControls(true, true, false);
                    foreach (OrderItemUC item in flpItems.Controls)
                    {
                        var orDe = item.Tag as OrderDetail;
                        if (orDe.ProductID == od.ProductID)
                        {
                            currentOrderItemUC = item;
                            break;
                        }
                    }

                    currentOrderDetail = od;
                    // số  lượng trong view ++
                    currentOrderDetail.Count = currentOrderItemUC.GetCurrentCount() + 1;

                    currentOrderItemUC.OrderItemUC_Click(currentOrderItemUC, new EventArgs());
                }

                LoadDetail();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void EnableButton(Button btn, bool isEnable)
        {
            btn.Enabled = isEnable;
            btn.Cursor = isEnable ? Cursors.Hand : Cursors.No;
        }

        private void EnableButtonControls(bool isEnableRemove, bool isEnableEdit, bool isEnableAdd)
        {
            EnableButton(btnDelete, isEnableRemove);
            EnableButton(btnEdit, isEnableEdit);
            EnableButton(btnAdd, isEnableAdd);
        }

        public void SetTotalPrice()
        {
            decimal sum = 0;
            if (orderDetails.Count > 0)
            {
                foreach (OrderItemUC item in flpItems.Controls)
                {
                    sum += item.GetTotal();
                }
            }

            txtTotal.Text = sum != 0 ? $"{sum.ToString("#,##")}" : "0";
            txtTotal.TextAlign = HorizontalAlignment.Center;
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Count", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Total", typeof(string));

            foreach (var item in orderDetails)
            {
                dt.Rows.Add(item.Name, item.Count, item.Price.ToString("#,##"), (item.Count * item.Price).ToString("#,##"));
            }

            return dt;
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

        #endregion Header

        #region Events

        private void btnImport_Click(object sender, System.EventArgs e)
        {
            // import qrcode 
            var initialPath = @"C:\Desktop\HK2-năm 3\Ngôn ngữ lập trình C# - CS511.L21\BTTH\BTTH3\Exports";
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = initialPath;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var reader = new BarcodeReader();
                var imageFile = Image.FromFile(dialog.FileName) as Bitmap;
                var result = reader.Decode(imageFile);

                if (result == null)
                {
                    MessageBox.Show("Hình ảnh không hợp lệ!", "Thông báo");
                    return;
                }

                // Giaỉ mã code
                var id = result.Text;
                product = products.FirstOrDefault(p => p.ID == id);
                if (product == null)
                {
                    MessageBox.Show("Hình ảnh không hợp lệ!", "Thông báo");
                    return;
                }

                HandleImportFile();
            }
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thiết lập lại hóa đơn!", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                orderDetails = new List<OrderDetail>();
                currentOrderDetail = null;
                currentOrderItemUC = null;
                flpItems.Controls.Clear();
                flpItems.Controls.Add(EmptyOrderUC);

                LoadDetail();
            }

            SetTotalPrice();
        }

        private void btnCreateOrder_Click(object sender, System.EventArgs e)
        {
            if (orderDetails.Count <= 0)
            {
                MessageBox.Show("Giỏ hàng rỗng");
                return;
            }

            DataTable dt = GetData();
            double totalSub = (double)orderDetails.Sum(o => o.Count * o.Price);
            double promotion = totalSub * 0.1;
            double totalAll = totalSub - promotion;

            var f = new fReport();
            f.Report(dt, "ReportForm.rdlc", totalSub, promotion, totalAll);
            f.Show();
        }

        private void nudCount_ValueChanged(object sender, System.EventArgs e)
        {
            if (currentOrderDetail != null)
            {
                currentOrderDetail.Count = (int)nudCount.Value;
            }
        }

        //========== Controls==================

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            flpItems.Controls.Remove(EmptyOrderUC);

            // Thêm vào danh sách
            orderDetails.Add(currentOrderDetail);

            // Reset BG color
            foreach (OrderItemUC item in flpItems.Controls)
            {
                item.RestBackgroundColor();
            }

            // Thêm vào list
            var orderDetail = new OrderItemUC(currentOrderDetail);
            orderDetail.ClickItemForm += OrderDetail_Click;
            orderDetail.Tag = currentOrderDetail;
            flpItems.Controls.Add(orderDetail);

            EnableButtonControls(true, true, false);

            currentOrderItemUC = orderDetail;
            currentOrderItemUC.OrderItemUC_Click(currentOrderItemUC, new EventArgs());
            SetTotalPrice();
        }

        private void OrderDetail_Click(object sender, System.EventArgs e)
        {
            ResetColorItems(sender as OrderItemUC);

            currentOrderItemUC = sender as OrderItemUC;
            currentOrderDetail = currentOrderItemUC.Tag as OrderDetail;

            LoadDetail();
            EnableButtonControls(true, true, false);
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            // Cập nhật lại danh sách, vì tham chiếu nên danh sách đã được cập  nhật

            // Cập nhật lại list
            currentOrderItemUC.UpdateCount(currentOrderDetail.Count);
            SetTotalPrice();

            MessageBox.Show("Cập nhật thành công");
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            // Xóa ra khỏi danh sách
            orderDetails.Remove(currentOrderDetail);
            currentOrderDetail = orderDetails.Count <= 0 ? null : orderDetails[0];

            // Xóa ra khỏi list
            flpItems.Controls.Remove(currentOrderItemUC);
            currentOrderItemUC = flpItems.Controls.Count <= 0 ? null : flpItems.Controls[0] as OrderItemUC;

            EnableButtonControls(true, true, false);
            LoadDetail();
            currentOrderItemUC?.OrderItemUC_Click(currentOrderItemUC, new EventArgs());
            SetTotalPrice();
        }

        #endregion
    }
}