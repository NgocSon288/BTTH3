using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCode.Models
{
    public class OrderDetail
    {
        public Guid ID { get; set; }

        public string ProductID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public string Image { get; set; }

        public OrderDetail()
        {
            ID = Guid.NewGuid();
        }

        public OrderDetail(Product product, int count = 1)
        {
            ID = Guid.NewGuid();
            ProductID = product.ID;
            Name = product.Name;
            Price = product.Price;
            Count = count;
            Image = product.Image;
        }
    }
}
