using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;

namespace UnitTests
{
    [TestClass]
    public class ContextFillerText
    {
        private readonly ContextFiller filler = new ContextFiller();
        DataRepository dataRepository;
        [TestMethod]
        public void ContextFillerBookTest()
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
        public void ContextFillerReaderTest()
        {
            dataRepository = new DataRepository(filler);
            Assert.AreEqual("Nowek", dataRepository.GetReader(1).LastName);
            Assert.AreEqual("Rybicka", dataRepository.GetReader(2).LastName);
            Assert.AreEqual("Z³otek", dataRepository.GetReader(3).LastName);
        }

        [TestMethod]
        public void ContextFillerCopyTest()
        {
            dataRepository = new DataRepository(filler);
            Assert.ReferenceEquals(dataRepository.GetBook(3), dataRepository.GetCopy(6).Book);
            Assert.ReferenceEquals(dataRepository.GetBook(2), dataRepository.GetCopy(3).Book);
            Assert.AreEqual(false, dataRepository.GetCopy(1).Borrowed);
            int id = 1;
            foreach (Copy copy in dataRepository.GetAllCopies())
            {
                Assert.AreEqual(id, copy.CopyId);
                id++;
            }
        }

        [TestMethod]
        public void ContextFillerBorrowingTest()
        {
            dataRepository = new DataRepository(filler);
            foreach(Copy copy in dataRepository.GetAllCopies())
            {
                if(copy.CopyId == 3 || copy.CopyId == 6) Assert.AreEqual(true, copy.Borrowed);
                else
                Assert.AreEqual(false, copy.Borrowed);

            }         
        }
    }

}
