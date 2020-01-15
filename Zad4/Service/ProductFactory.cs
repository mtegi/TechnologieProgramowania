using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public static class ProductFactory
    {

       public static ProductWrapper getNewWrapperProduct(String productName, String productNumber, bool makeFlag)
        {
            Product product = new Product();
            product.Name = productName;
            product.ProductNumber = productNumber;
            product.MakeFlag = makeFlag;

            return new ProductWrapper(product);
        }
    }
}
