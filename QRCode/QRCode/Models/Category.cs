using QRCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QRCode.Models
{
    public class Category
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public Category()
        {

        }

        public Category(List<object> objValues)
        {
            try
            {
                var m = 0;
                foreach (PropertyInfo p in typeof(Category).GetProperties())
                {
                    string propertyName = p.Name;
                    var propertyType = p.PropertyType.Name;
                    var type = p.PropertyType;

                    var value = Converters<Category>.StringToValue(objValues[m++].ToString(), type);
                    var msgInfo = this.GetType().GetProperty(propertyName);

                    msgInfo.SetValue(this, value, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public Category(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
        }
    }
}
