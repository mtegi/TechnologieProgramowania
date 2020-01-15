using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    class TestService : IProductService
    {
        private List<Product> Products;

        public TestService()
        {
            this.Products = new List<Product>();
            
            for(int i=0; i < 5; i++)
            {
                Product product = new Product();
                product.Name = "Test" + i.ToString();
                Products.Add(product);
            }
            Assert.AreEqual(Products.Count(), 5);
        }

        public bool Add(Product product)
        {
           Products.Add(product);
            return true;
        }

        public bool Delete(int ProductID)
        {
            return Products.Remove(Get(ProductID));
        }

        public Product Get(int id)
        {
            return Products.Where(p => p.ProductID == id).FirstOrDefault();
        }

        public IQueryable<Product> GetAllProducts()
        {
            return Products.AsQueryable();
        }

        public ProductWrapper GetDataForDetailsView(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<int, string>> GetDataForListView()
        {
            List<Tuple<int, string>> tuples = new List<Tuple<int, string>>();
            foreach (Product p in Products)
            {
                tuples.Add(new Tuple<int, string>(p.ProductID, p.Name));
            }
            return tuples;
        }

        public bool Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
