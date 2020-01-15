using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductWrapper
    {
        private readonly Product product;

        public ProductWrapper(Product product)
        {
            this.product = product;
        }


    }
}
