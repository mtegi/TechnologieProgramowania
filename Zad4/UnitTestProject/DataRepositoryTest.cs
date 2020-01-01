using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Service;
using DataLayer;

namespace UnitTestProject
{
    [TestClass]
    public class DataRepositoryTest
    {
        
        [TestMethod]
        public void Add()
        {
            IDataRepository repository = new Service.DataRepository();
            int size = repository.GetAllProducts().Count();

            Product product = new Product();

            product.rowguid = new Guid();
            product.Name = "Test";
            product.ProductNumber = "TX-1111";
            product.MakeFlag = true;
            product.FinishedGoodsFlag = true;
            product.Color = null;
            product.SafetyStockLevel = 100;
            product.ReorderPoint = 100;
            product.StandardCost = 100;
            product.ListPrice = 100;
            product.Size = "S";
            product.SizeUnitMeasureCode = "CM";
            product.WeightUnitMeasureCode = "LB";
            product.Weight = 100;
            product.DaysToManufacture = 100;
            product.ProductLine = "M";
            product.Class = "H";
            product.Style = "M";
            product.ProductSubcategoryID = 1;
            product.ProductModelID = 1;
            product.SellStartDate = DateTime.Today;
            product.SellEndDate = DateTime.Today.AddDays(1);
            product.ModifiedDate = DateTime.Today;
            repository.Add(product);

            Assert.AreEqual(size + 1, repository.GetAllProducts().Count());
        }

        [TestMethod]
        public void Get()
        {
            IDataRepository repository = new Service.DataRepository();
            int id = repository.GetAllProducts().First().ProductID;
            Assert.AreEqual(repository.GetAllProducts().First().Name, repository.Get(id).Name);
        }

        [TestMethod]
        public void Delete()
        {
            IDataRepository repository = new Service.DataRepository();
            Assert.IsTrue(repository.Delete(repository.GetAllProducts().Where(p => p.Name == "Test").First()));
        }

        [TestMethod]
        public void Update()
        {
            IDataRepository repository = new Service.DataRepository();
            Product p = repository.GetAllProducts().First();
            p.Name = "test";
            int id = p.ProductID;
            repository.Update(p);
            Assert.AreEqual("test", repository.Get(id).Name);
         }
    }
}
