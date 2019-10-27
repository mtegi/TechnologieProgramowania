using DataCreator;
using DataHandler;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTests
{
    [TestClass]
    public class ContextFillerText
    {
        ContextFiller filler = new ContextFiller();
        DataRepository dataRepository;
        [TestMethod]
        public void ContextFillerBookTest()
        {
            dataRepository = new DataRepository(filler);

            Assert.AreEqual(1001, dataRepository.GetBook(1001).Id);
            Assert.AreEqual("Wydra", dataRepository.GetBook(1001).Title);
            Assert.AreEqual("Jan Lasica", dataRepository.GetBook(1001).Author);

            Assert.AreEqual(1002, dataRepository.GetBook(1002).Id);
            Assert.AreEqual("Lolita", dataRepository.GetBook(1002).Title);
            Assert.AreEqual("Mi³osz Liana", dataRepository.GetBook(1002).Author);
        }

        [TestMethod]
        public void ContextFillerReaderTest()
        {
            dataRepository = new DataRepository(filler);
            Assert.AreEqual("Nowek", dataRepository.GetReader(3001).LastName);
            Assert.AreEqual("Rybicka", dataRepository.GetReader(3002).LastName);
            Assert.AreEqual("Z³otek", dataRepository.GetReader(3003).LastName);
        }

        [TestMethod]
        public void ContextFillerCopyTest()
        {
            dataRepository = new DataRepository(filler);
            Assert.AreEqual(dataRepository.GetBook(1003), dataRepository.GetCopy(6).Book);
            Assert.AreEqual(dataRepository.GetBook(1002), dataRepository.GetCopy(3).Book);
            Assert.AreEqual(false, dataRepository.GetCopy(1).Borrowed);
            int id = 1;
            foreach (WrappedCopy copy in dataRepository.GetAllCopies())
            {
                Assert.AreEqual(id, copy.copyId);
                id++;
            }
        }

        [TestMethod]
        public void ContextFillerBorrowingTest()
        {
            dataRepository = new DataRepository(filler);
            foreach(WrappedCopy copy in dataRepository.GetAllCopies())
            {
                if(copy.copyId == 3 || copy.copyId == 6) Assert.AreEqual(true, copy.Borrowed);
                else
                Assert.AreEqual(false, copy.Borrowed);

            }         
        }
    }

}
