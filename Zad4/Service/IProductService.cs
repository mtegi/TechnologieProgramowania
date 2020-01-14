using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public interface IProductService
    {
        bool Add(Product product);
        Product Get(int id);
        IQueryable<Product> GetAllProducts();
        IEnumerable<Tuple<int, string>> GetDataForListView();
        bool Update(Product product);
        bool Delete(Product product); 
    }
}
