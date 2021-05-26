using QRCode.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace QRCode.Models
{
    public class Product
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public Guid Category { get; set; }

        public Product()
        {
        }

        public Product(List<object> objValues)
        {
            try
            {
                var m = 0;
                foreach (PropertyInfo p in typeof(Product).GetProperties())
                {
                    string propertyName = p.Name;
                    var propertyType = p.PropertyType.Name;
                    var type = p.PropertyType;

                    var value = Converters<Product>.StringToValue(objValues[m++].ToString(), type);
                    var msgInfo = this.GetType().GetProperty(propertyName);

                    msgInfo.SetValue(this, value, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public Product(string iD, string name, decimal price, string description, string image, Guid category)
        {
            ID = iD;
            Name = name;
            Price = price;
            Description = description;
            Image = image;
            Category = category;
        }
    }
}