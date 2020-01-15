using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DataRepository : IProductService
    {
        private DataClassesDataContext context;

        public DataRepository()
        {
            this.context = new DataClassesDataContext();
        }

        public Product Get(int id)
        {
            return context.Products.Where(p => p.ProductID == id).FirstOrDefault();
        }

        public bool Delete(Product product)
        {
            try
            {
                context.Products.DeleteOnSubmit(product);
                context.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Product product)
        {
            try
            {
                Product updatedProduct = context.Products.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
                foreach (System.Reflection.PropertyInfo property in updatedProduct.GetType().GetProperties())
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(updatedProduct, property.GetValue(product));
                    }
                }
                context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Product> GetAllProducts()
        {
            return context.Products;
        }

        public IEnumerable<Tuple<int,string>> GetDataForListView()
        {
            List<Tuple<int, string>> tuples = new List<Tuple<int, string>>();
            foreach(Product p in context.Products)
            {
                tuples.Add(new Tuple<int, string>(p.ProductID, p.Name));
            }
            return tuples;
        }

        public bool Add(Product product)
        {
            try
            {
                context.Products.InsertOnSubmit(product);
                context.SubmitChanges();
                return true;
            } catch
            {
                return false;
            }

        }

        public ProductWrapper GetDataForDetailsView(int id)
        {
            return new ProductWrapper(this.Get(id));
        }
    }
}
