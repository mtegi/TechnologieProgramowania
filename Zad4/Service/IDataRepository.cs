using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public interface IDataRepository
    {
        bool Add(Product product);
        Product Get(int id);
        IQueryable<Product> GetAllProducts();
        bool Update(Product product);
        bool Delete(Product product); 
    }
}
