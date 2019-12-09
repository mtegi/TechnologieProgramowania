using System;
using System.Collections.Generic;
using System.Linq;
using Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class MyProductTest
    {
        [TestMethod]
        public void GetMyProductsByName()
        {
            Assert.AreEqual(2, Selector.GetProductsByName("Decal").Count);
        }

        [TestMethod]
        public void GetProductsWithNRecentReviews()
        {
            List<MyProduct> products = Selector.GetMyProductsWithNRecentReviews(1);
            Assert.AreEqual(products.Count, 2);
            Assert.IsNotNull(products.Find(product => product.ProductID == 709));
            Assert.IsNotNull(products.Find(product => product.ProductID == 798));
        }

        [TestMethod]
        public void GetNProductsFromCategory()
        {
            List<MyProduct> products = Selector.GetNMyProductsFromCategory("Clothing", 5);
            Assert.AreEqual(products.Count, 5);
        }
    }


}
