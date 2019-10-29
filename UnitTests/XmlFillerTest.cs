using DataCreator;
using DataHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
   public class XmlFillerTest
    {
        private readonly XmlFiller filler = new XmlFiller();
        DataRepository dataRepository;

        [TestMethod]
        public void XmlFillerBookTest()
        {
            dataRepository = new DataRepository(filler);

            Assert.AreEqual(1, dataRepository.GetBook(1).Id);
            Assert.AreEqual("Wydra", dataRepository.GetBook(1).Title);
            Assert.AreEqual("Jan Lasica", dataRepository.GetBook(1).Author);

            Assert.AreEqual(2, dataRepository.GetBook(2).Id);
            Assert.AreEqual("Lolita", dataRepository.GetBook(2).Title);
            Assert.AreEqual("Milosz Liana", dataRepository.GetBook(2).Author);
        }

        [TestMethod]
        public void XmlFillerReaderTest()
        {
            dataRepository = new DataRepository(filler);
            Assert.AreEqual("Nowek", dataRepository.GetReader(1).LastName);
            Assert.AreEqual("Rybicka", dataRepository.GetReader(2).LastName);
            Assert.AreEqual("Złotek", dataRepository.GetReader(3).LastName);
        }

        [TestMethod]
        public void XmlFillerCopyTest()
        {
            dataRepository = new DataRepository(filler);
            Assert.ReferenceEquals(dataRepository.GetBook(3), dataRepository.GetCopy(6).Book);
            Assert.ReferenceEquals(dataRepository.GetBook(2), dataRepository.GetCopy(3).Book);
            Assert.AreEqual(false, dataRepository.GetCopy(1).Borrowed);
            int id = 1;
            foreach (WrappedCopy copy in dataRepository.GetAllCopies())
            {
                Assert.AreEqual(id, copy.CopyId);
                id++;
            }
        }
    }
}
