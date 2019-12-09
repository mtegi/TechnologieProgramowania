using System;
using System.Collections.Generic;
using System.Linq;
using Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class ExtensionTest
    {
        [TestMethod]
        public void GetProductsWithoutCategory()
        {
            using (DataClassesDataContext context = new DataClassesDataContext())
            {
                List<Product> products = context.Products.ToList();
                Assert.AreEqual(209, products.GetProductsWithoutCategory_D().Count);
                Assert.AreEqual(products.GetProductsWithoutCategory_I().Count, products.GetProductsWithoutCategory_D().Count);
                Assert.IsNull(products.GetProductsWithoutCategory_I().ElementAt(0).ProductSubcategory);
                Assert.IsNull(products.GetProductsWithoutCategory_D().ElementAt(0).ProductSubcategory);
            }
        }

        [TestMethod]
        public void GetProductVendorString()
        {
            using (DataClassesDataContext context = new DataClassesDataContext())
            {
                List<Product> products = context.Products.ToList();
                List<ProductVendor> vendors = context.ProductVendors.ToList();
                string[] pv1 = products.AsString_I(vendors).Split('\n');
                string[] pv2 = products.AsString_D(vendors).Split('\n');
                Assert.AreEqual(pv1.Count(), pv2.Count());
                Assert.AreEqual(461, pv1.Count());
                Assert.IsTrue(pv1[0].Contains('-'));
                Assert.IsTrue(pv2[0].Contains('-'));
                string[] pv = pv1[0].Split('-');
                Assert.AreEqual("Adjustable Race", pv[0]);
                Assert.IsTrue(pv[1].Contains(Selector.GetProductVendorByProductName("Adjustable Race")));
            }
        }

        public void PageTest()
        {
            using (DataClassesDataContext context = new DataClassesDataContext())
            {
                List<Product> products = context.Products.ToList();
                List<Product> page = products.AsPage(1, 15);
                Assert.AreEqual(15, page.Count());
                Assert.AreEqual("Adjustable Race", page.First().Name);
            }     
        }
    }
}
