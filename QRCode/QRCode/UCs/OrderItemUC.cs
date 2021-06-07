using QRCode.Common;
using QRCode.Models;
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
    public partial class OrderItemUC : UserControl
    {
        public delegate void ClickItem(object sender, EventArgs eventArgs);
        public event ClickItem ClickItemForm;

        private OrderDetail orderDetail;

        public OrderItemUC(OrderDetail orderDetail)
        {
            InitializeComponent();

            this.orderDetail = orderDetail;

            Load();
            this.Click += OrderItemUC_Click;
            foreach (Control item in this.Controls)
            {
                item.Click += OrderItemUC_Click;
            }
        }

        public void OrderItemUC_Click(object sender, EventArgs e)
        {
            this.BackColor = Constants.COLOR_ACTIVE;
            ClickItemForm?.Invoke(this, new EventArgs());
        }

        public void RestBackgroundColor()
        {
            this.BackColor = Constants.COLOR_NO_ACTIVE;
        }

        #region Methods

        new private void Load()
        {
            lblName.Text = orderDetail.Name;
            lblPrice.Text = $"{orderDetail.Price.ToString("#,##")}";
            lblCount.Text = orderDetail.Count.ToString();
            lblTotal.Text = $"{(orderDetail.Price * orderDetail.Count).ToString("#,##")}";
            picImage.BackgroundImage = new Bitmap($"./../../Assets/Images/{orderDetail.Image}");
        }

        public int GetCurrentCount()
        {
            return Convert.ToInt32(lblCount.Text);
        }

        public decimal GetTotal()
        {
            return orderDetail.Count * orderDetail.Price;
        }

        public void UpdateCount(int count)
        {
            orderDetail.Count = count;

            lblCount.Text = orderDetail.Count.ToString();
            lblTotal.Text = $"{(orderDetail.Price * orderDetail.Count).ToString("#,##")}";
        }

        #endregion
    }
}
