using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;

namespace UnitTestProject
{
    [TestClass]
    public class ProductListTest
    {
        [TestMethod]
        public void Get()
        {
            ProductListViewModel viewModel = new ProductListViewModel(new TestService());
            Assert.AreEqual(5, viewModel.ProductsInList.Count());
        }

        [TestMethod]
        public void Select()
        {
            ProductListViewModel viewModel = new ProductListViewModel(new TestService());
            viewModel.SelectedProduct = viewModel.ProductsInList.ElementAt(0);
            Assert.AreEqual(0, viewModel.SelectedProduct.Id);
        }

        [TestMethod]
        public void Delete()
        {
            ProductListViewModel viewModel = new ProductListViewModel(new TestService());
            Assert.AreEqual(5, viewModel.ProductsInList.Count());
            viewModel.SelectedProduct = viewModel.ProductsInList.ElementAt(0);
            viewModel.Delete.Execute(null);
            Assert.AreEqual(4, viewModel.ProductsInList.Count());
        }
    }
}
