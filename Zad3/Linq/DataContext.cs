using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    public class DataContext
    {
       public List<MyProduct> Products { get; private set; }

        public DataContext()
        {
            DataClassesDataContext data = new DataClassesDataContext();
            this.Products = new List<MyProduct>();
            foreach (Product p in data.Products.ToList())
            {
                MyProduct myProduct = new MyProduct(p);
                Products.Add(myProduct);
            }
        }
    }
}
