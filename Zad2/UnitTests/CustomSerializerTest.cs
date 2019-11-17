using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace UnitTests
{
    [TestClass]
    public class CustomSerializerTest
    {
        DataContext data;
        private CustomSerializer serializer;
        [TestMethod]
        public void GetTypeTest()
        {
            Assert.AreEqual(null, Type.GetType("Library.BorrowingEvent"));
            Assert.AreEqual(typeof(BorrowingEvent), Type.GetType("Library.BorrowingEvent,Library"));
        }

        [TestMethod]
        public void DeserializeTest()
        {
            serializer = new CustomSerializer();
            DataContext dataToSerialize = new DataContext();
            ContextFiller filler = new ContextFiller();
            filler.Fill(dataToSerialize);
            using (FileStream stream = File.Open("serialized.txt", FileMode.OpenOrCreate))
            {
                serializer.Serialize(dataToSerialize, stream);
            }
            using (FileStream stream = File.Open("serialized.txt", FileMode.Open))
            {
                data = serializer.Deserialize(stream);
            }
            DataRepository dataRepository = new DataRepository(data);

            //Sprawdz referencje
            Assert.ReferenceEquals(dataRepository.GetAllEvents().ElementAt(0), dataRepository.GetReader(1));
            Assert.ReferenceEquals(dataRepository.GetAllEvents().ElementAt(0), dataRepository.GetReader(2));
            Assert.ReferenceEquals(dataRepository.GetBook(3), dataRepository.GetCopy(6).Book);
            Assert.ReferenceEquals(dataRepository.GetBook(2), dataRepository.GetCopy(3).Book);
            Assert.ReferenceEquals(dataRepository.GetAllEvents().ElementAt(2), dataRepository.GetAllEvents().ElementAt(3));

            //Sprawdz zawartosc
            Assert.AreEqual(4, dataRepository.GetAllEvents().Count());
            Assert.AreNotEqual(null, dataRepository.GetAllEvents().ElementAt(0));
            Assert.AreNotEqual(null, dataRepository.GetAllEvents().ElementAt(1));
            Assert.AreNotEqual(null, dataRepository.GetAllEvents().ElementAt(2));
            Assert.AreNotEqual(null, dataRepository.GetAllEvents().ElementAt(3));
            Assert.AreEqual("19.10.2019 22:00:00 +02:00", dataRepository.GetAllEvents().ElementAt(0).EventDate.ToString());
            Assert.AreEqual(1, dataRepository.GetBook(1).Id);
            Assert.AreEqual("Wydra", dataRepository.GetBook(1).Title);
            Assert.AreEqual("Jan Lasica", dataRepository.GetBook(1).Author);
            Assert.AreEqual(2, dataRepository.GetBook(2).Id);
            Assert.AreEqual("Lolita", dataRepository.GetBook(2).Title);
            Assert.AreEqual("Milosz Liana", dataRepository.GetBook(2).Author);
            Assert.AreEqual("Nowek", dataRepository.GetReader(1).LastName);
            Assert.AreEqual("Rybicka", dataRepository.GetReader(2).LastName);
            Assert.AreEqual(false, dataRepository.GetCopy(1).Borrowed);
            int id = 1;
            foreach (Copy copy in dataRepository.GetAllCopies())
            {
                Assert.AreEqual(id, copy.CopyId);
                id++;
            }
            foreach (Copy copy in dataRepository.GetAllCopies())
            {
                if (copy.CopyId == 3 || copy.CopyId == 6) Assert.AreEqual(true, copy.Borrowed);
                else
                    Assert.AreEqual(false, copy.Borrowed);
            }
        }
    }
}
