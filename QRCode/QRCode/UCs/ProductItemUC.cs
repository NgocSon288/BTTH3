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
    public partial class ProductItemUC : UserControl
    {
        public delegate void ClickItem(object sender, EventArgs eventArgs);
        public event ClickItem ClickItemForm;

        private Product product;

        public ProductItemUC(Product product)
        {
            InitializeComponent();

            this.product = product;

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

        #region Methods

        new private void Load()
        {
            lblName.Text = product.Name;
            lblPrice.Text = $"{product.Price.ToString("#,##")} VND";
            lblDescription.Text = product.Description;

            picImage.BackgroundImage = new Bitmap($"./../../Assets/Images/{product.Image}");
        }

        public void RestBackgroundColor()
        {
            this.BackColor = Constants.COLOR_NO_ACTIVE;
        }

        #endregion
    }
}
