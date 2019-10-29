using DataCreator;
using DataHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    [TestClass]
   public class FillerTest
    {
        private readonly Filler filler = new Filler();
        DataRepository dataRepository;

        [TestMethod]
        public void FillerBookTest()
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
        public void FillerReaderTest()
        {
            dataRepository = new DataRepository(filler);
            Assert.AreEqual("Nowek", dataRepository.GetReader(1).LastName);
            Assert.AreEqual("Rybicka", dataRepository.GetReader(2).LastName);
            Assert.AreEqual("Złotek", dataRepository.GetReader(3).LastName);
            Assert.AreEqual(20, dataRepository.GetAllReaders().Count());
        }

        [TestMethod]
        public void FillerCopyTest()
        {
            dataRepository = new DataRepository(filler);
            Assert.AreEqual(20, dataRepository.GetAllCopies().Count());
        }

        [TestMethod]
        public void FillerEventTest()
        {
            dataRepository = new DataRepository(filler);
            Assert.IsTrue(dataRepository.GetAllEvents().Count() <= 20);
            Assert.IsInstanceOfType(dataRepository.GetAllEvents().FirstOrDefault(), typeof(WrappedEvent));
        }

    }
}
