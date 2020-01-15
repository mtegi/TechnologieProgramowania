using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;

namespace UnitTestProject
{
    [TestClass]
    public class DetailWindowTest
    {
        [TestMethod]
        public void Get()
        {
            TestService service = new TestService();
            ProductDetailsViewModel viewModel = new ProductDetailsViewModel(service.Get(1),service);
            Assert.AreEqual(service.Get(1), viewModel.ProductName);
        }
    }
}
