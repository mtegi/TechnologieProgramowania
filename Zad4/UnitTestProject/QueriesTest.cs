using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class QueriesTest
    {
        [TestMethod]
        public void GetProductsByName()
        {
            Assert.AreEqual(2, Selector.GetProductsByName("Decal").Count);
        }

        [TestMethod]
        public void GetProductsByVendorName()
        {
            Assert.AreEqual(9, Selector.GetProductsByVendorName("Continental Pro Cycles").Count);
        }

        [TestMethod]
        public void GetProductNamesByVendorName()
        {
            List<string> p = Selector.GetProductNamesByVendorName("Continental Pro Cycles");
            Assert.AreEqual(9, p.Count);
            Assert.AreEqual("Flat Washer 1", p[0]);
        }

        [TestMethod]
        public void GetProductVendorByProductName()
        {
            Assert.AreEqual("Continental Pro Cycles", Selector.GetProductVendorByProductName("Flat Washer 1"));
        }

        [TestMethod]
        public void GetProductsWithNRecentReviews()
        {
            List<Product> products = Selector.GetProductsWithNRecentReviews(1);
            Assert.AreEqual(products.Count, 2);
            Assert.IsNotNull(products.Find(product => product.ProductID == 709));
            Assert.IsNotNull(products.Find(product => product.ProductID == 798));
        }

        [TestMethod]
        public void GetNRecentlyReviewedProducts()
        {
            List<Product> products = Selector.GetNRecentlyReviewedProducts(2);
            Assert.AreEqual(products.Count, 2);
            Assert.AreEqual("HL Mountain Pedal", products[0].Name);
            Assert.AreEqual("Road-550-W Yellow, 40", products[1].Name);
        }

        [TestMethod]
        public void GetNProductsFromCategory()
        {
            List<Product> products = Selector.GetNProductsFromCategory("Clothing", 5);
            Assert.AreEqual(products.Count, 5);
            foreach (Product product in products)
               Assert.AreEqual("Clothing", product.ProductSubcategory.ProductCategory.Name);
        }

        [TestMethod]
        public void GetTotalStandardCostByCategory()
        {
            ProductCategory components = new ProductCategory
            {
                Name = "Components"
            };
            ProductCategory bikes = new ProductCategory
            {
                ProductCategoryID = 1
            };
            Assert.AreEqual(92092,Selector.GetTotalStandardCostByCategory(bikes));
            Assert.AreEqual(35930, Selector.GetTotalStandardCostByCategory(components));
        }

    }
}
