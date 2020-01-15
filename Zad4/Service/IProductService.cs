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
        bool Add(ProductWrapper product);
        Product Get(int id);
        IQueryable<Product> GetAllProducts();
        IEnumerable<Tuple<int, string>> GetDataForListView();
        ProductWrapper GetDataForDetailsView(int id);
        bool Update(ProductWrapper product);
        bool Delete(int ProductID); 
    }
}
