using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    public class MyProduct : Product
    {
        public string Category { get; set; }
        public MyProduct(Product product)
        {
            foreach (PropertyInfo property in product.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(this, property.GetValue(product));
                }           
            }
        }
    }
}
