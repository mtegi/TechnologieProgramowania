using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace UnitTests
{
    [TestClass]
    public class DataServiceTest
    {

        private class EmptyProvider : IDataProvider
        {
            public void Fill(DataContext data)
            { }
        }

        private DataRepository repo;
        private DataService dataService;

        [TestMethod]
        public void PurchaseCopyTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));

            bool PurchaseEventRised = false;
            dataService.PurchaseHappened += PurchaseEventMethod;
            void PurchaseEventMethod (object sender, EventArgs e)
            {
                if (PurchaseEventRised == true)
                    Assert.Fail("Event rised multiple times");
                else
                    PurchaseEventRised = true;
            }

            Assert.AreEqual(false, repo.ContainsCopy(101));

            dataService.PurchaseCopy(101, 1, CopyCondition.Mint, testDate, "TestDistrib");

            PurchaseEvent purchaseEvent = (PurchaseEvent)repo.GetAllEvents().Last();

            Assert.AreEqual(true, PurchaseEventRised);

            Assert.AreEqual(true, repo.ContainsCopy(101));
            Assert.AreEqual(CopyCondition.Mint, repo.GetCopy(101).Condition);
            Assert.AreEqual(repo.GetBook(1), repo.GetCopy(101).Book);
            Assert.AreEqual("testtitle", repo.GetCopy(101).Book.Title);
            Assert.AreEqual("testauthor", repo.GetCopy(101).Book.Author);

            Assert.AreEqual(101, purchaseEvent.Copy.CopyId);
            Assert.AreEqual(repo.GetCopy(101), purchaseEvent.Copy);
            Assert.AreEqual("TestDistrib", purchaseEvent.Distributor);
            Assert.AreEqual(testDate, purchaseEvent.EventDate);
        }

        [TestMethod]
        public void DestroyCopyTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));
            repo.AddCopy(101, 1, CopyCondition.Mint);

            bool DestructionEventRised = false;
            dataService.DestructionHappened += DestructionEventMethod;
            void DestructionEventMethod(object sender, EventArgs e)
            {
                if (DestructionEventRised == true)
                    Assert.Fail("Event rised multiple times");
                else
                    DestructionEventRised = true;
            }

            Assert.AreEqual(true, repo.ContainsCopy(101));

            dataService.DestroyCopy(101, testDate, "TestReason");
            DestructionEvent destructionEvent = (DestructionEvent)repo.GetAllEvents().Last();

            Assert.AreEqual(false, repo.ContainsCopy(101));

            Assert.AreEqual(true, DestructionEventRised);

            Assert.AreEqual("TestReason", destructionEvent.Reason);
            Assert.AreEqual(101, destructionEvent.Copy.CopyId);

        }

        [TestMethod]
        public void BorrowCopyTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

            Assert.AreEqual(true, repo.ContainsBook(1));

            DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));
            DateTimeOffset testDate2 = new DateTimeOffset(2019, 10, 27, 22, 0, 0, new TimeSpan(2, 0, 0));

            repo.AddCopy(101, 1, CopyCondition.Mint);

            Assert.AreEqual(true, repo.ContainsCopy(101));

            Reader r = new Reader(201, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);

            Assert.AreEqual(true, repo.ContainsReader(201));

            bool BorrowingEventRised = false;
            dataService.BorrowingHappened += BorrowingEventMethod;
            void BorrowingEventMethod(object sender, EventArgs e)
            {
                if (BorrowingEventRised == true)
                    Assert.Fail("Event rised multiple times");
                else
                    BorrowingEventRised = true;
            }

            Assert.AreEqual(true, repo.ContainsCopy(101));
            Assert.AreEqual(false, repo.GetCopy(101).Borrowed);

            dataService.BorrowCopy(101, 201, testDate, testDate2);
            BorrowingEvent borrowingEvent = (BorrowingEvent)repo.GetAllEvents().Last();


            Assert.AreEqual(true, repo.ContainsCopy(101));
            Assert.AreEqual(true, repo.ContainsReader(201));

            Assert.AreEqual(true, repo.GetCopy(101).Borrowed);
            Assert.AreEqual(true, BorrowingEventRised);

            Assert.AreEqual(101, borrowingEvent.Copy.CopyId);
            Assert.AreEqual(201, borrowingEvent.Reader.Id);
            Assert.AreEqual(false, borrowingEvent.Completed);
        }

        [TestMethod]
        public void ReturnCopyTest()
        {
            //Set up clean repo
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);
            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

            Assert.AreEqual(true, repo.ContainsBook(1));

            //Create some dates
            DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));
            DateTimeOffset testDate2 = new DateTimeOffset(2019, 10, 27, 22, 0, 0, new TimeSpan(2, 0, 0));
            DateTimeOffset testDate3 = new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0));

            //Add copy and reader
            repo.AddCopy(101, 1, CopyCondition.Mint);
            Assert.AreEqual(true, repo.ContainsCopy(101));

            Reader r = new Reader(201, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);

            Assert.AreEqual(true, repo.ContainsReader(201));

            //Prepare event rising test
            bool ReturnEventRised = false;
            dataService.ReturnHappened += ReturnEventMethod;
            void ReturnEventMethod(object sender, EventArgs e)
            {
                if (ReturnEventRised == true)
                    Assert.Fail("Event rised multiple times");
                else
                    ReturnEventRised = true;
            }
            
            //Borrow Copy
            dataService.BorrowCopy(101, 201, testDate, testDate2);
            BorrowingEvent borrowingEvent = (BorrowingEvent)repo.GetAllEvents().Last();

            Assert.AreEqual(true, repo.GetCopy(101).Borrowed);


            //Try to return copy
            dataService.ReturnCopy(101, 201, testDate3, CopyCondition.HeavlyDamaged);
            ReturnEvent returnEvent = (ReturnEvent)repo.GetAllEvents().Last();

            Assert.AreEqual(true, repo.ContainsCopy(101));
            Assert.AreEqual(true, repo.ContainsReader(201));

            Assert.AreEqual(true, ReturnEventRised);
            Assert.AreEqual(false, repo.GetCopy(101).Borrowed);
            Assert.AreEqual(true, borrowingEvent.Completed);

            Assert.AreEqual(101, returnEvent.Copy.CopyId);
            Assert.AreEqual(201, returnEvent.Reader.Id);

        }

        [TestMethod]
        public void FindEventsTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

            Reader r = new Reader(201, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);

            DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));


            dataService.PurchaseCopy(101, 1, CopyCondition.Mint, testDate, "test");
            dataService.DestroyCopy(101, testDate, "test");
            dataService.PurchaseCopy(103, 1, CopyCondition.Mint, testDate, "test");
            dataService.BorrowCopy(103, 201, testDate, testDate);

            IEnumerable<LibEvent> findTypes = dataService.FindEvents(typeof(PurchaseEvent));

            Assert.AreEqual(2, findTypes.Count());
            foreach (LibEvent libEvent in findTypes)
                Assert.AreEqual(typeof(PurchaseEvent), libEvent.GetType());

            IEnumerable<LibEvent> findCopies = dataService.FindEvents(101);

            Assert.AreEqual(2, findCopies.Count());
            foreach (LibEvent libEvent in findCopies)
                Assert.AreEqual(101, libEvent.Copy.CopyId);

            IEnumerable<LibEvent> findCopiesTypes = dataService.FindEvents(101, typeof(PurchaseEvent));

            Assert.AreEqual(1, findCopiesTypes.Count());
            foreach (LibEvent libEvent in findCopiesTypes)
            {
                Assert.AreEqual(typeof(PurchaseEvent), libEvent.GetType());
                Assert.AreEqual(101, libEvent.Copy.CopyId);
            }


        }

        [TestMethod]
        public void FindEventsInPeriodTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

            Reader r = new Reader(201, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);

            DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));
            DateTimeOffset testDate2 = new DateTimeOffset(2019, 10, 22, 22, 0, 0, new TimeSpan(2, 0, 0));
            DateTimeOffset testDate3 = new DateTimeOffset(2019, 10, 24, 22, 0, 0, new TimeSpan(2, 0, 0));
            DateTimeOffset testDate4 = new DateTimeOffset(2019, 10, 26, 22, 0, 0, new TimeSpan(2, 0, 0));
            DateTimeOffset testDate5 = new DateTimeOffset(2019, 10, 28, 22, 0, 0, new TimeSpan(2, 0, 0));

            dataService.PurchaseCopy(101, 1, CopyCondition.Mint, testDate, "test");
            dataService.PurchaseCopy(102, 1, CopyCondition.Mint, testDate3, "test");
            dataService.PurchaseCopy(103, 1, CopyCondition.Mint, testDate4, "test");

            IEnumerable<LibEvent> findEvents = dataService.FindEventsInPeriod(testDate2, testDate5);

            Assert.AreEqual(2, findEvents.Count());
        }

        [TestMethod]
        public void FindUserBorrowingsTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

            Reader r = new Reader(201, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);

            Reader r2 = new Reader(202, "y", "yy");
            repo.AddReader(r2.Id, r2.FirstName, r2.LastName);

            DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));

            dataService.PurchaseCopy(101, 1, CopyCondition.Mint, testDate, "test");
            dataService.PurchaseCopy(102, 1, CopyCondition.Mint, testDate, "test");
            dataService.PurchaseCopy(103, 1, CopyCondition.Mint, testDate, "test");

            dataService.BorrowCopy(101, 201, testDate, testDate);
            dataService.BorrowCopy(102, 202, testDate, testDate);
            dataService.BorrowCopy(103, 201, testDate, testDate);

            IEnumerable<LibEvent> result1 = dataService.FindUserBorrowings(201);

            Assert.AreEqual(2, result1.Count());
            foreach (BorrowingEvent libEvent in result1)
            {
                Assert.AreEqual(typeof(BorrowingEvent), libEvent.GetType());
                Assert.AreEqual(201, libEvent.Reader.Id);
            }

            IEnumerable<LibEvent> result2 = dataService.FindUserBorrowings(201,101);

            Assert.AreEqual(1, result2.Count());
            foreach (BorrowingEvent libEvent in result2)
            {
                Assert.AreEqual(typeof(BorrowingEvent), libEvent.GetType());
                Assert.AreEqual(101, libEvent.Copy.CopyId);
                Assert.AreEqual(201, libEvent.Reader.Id);
            }
        }

        [TestMethod]
        public void FindUserReturnsTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

            Reader r = new Reader(201, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);

            Reader r2 = new Reader(202, "y", "yy");
            repo.AddReader(r2.Id, r2.FirstName, r2.LastName);

            DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));

            dataService.PurchaseCopy(101, 1, CopyCondition.Mint, testDate, "test");
            dataService.PurchaseCopy(102, 1, CopyCondition.Mint, testDate, "test");
            dataService.PurchaseCopy(103, 1, CopyCondition.Mint, testDate, "test");

            dataService.BorrowCopy(101, 201, testDate, testDate);
            dataService.BorrowCopy(102, 202, testDate, testDate);
            dataService.BorrowCopy(103, 201, testDate, testDate);

            dataService.ReturnCopy(101, 201, testDate, CopyCondition.Mint);
            dataService.ReturnCopy(102, 202, testDate, CopyCondition.Mint);
            dataService.ReturnCopy(103, 201, testDate, CopyCondition.Mint);

            IEnumerable<LibEvent> result1 = dataService.FindUserReturns(201);

            Assert.AreEqual(2, result1.Count());
            foreach (ReturnEvent libEvent in result1)
            {
                Assert.AreEqual(typeof(ReturnEvent), libEvent.GetType());
                Assert.AreEqual(201, libEvent.Reader.Id);
            }

            IEnumerable<LibEvent> result2 = dataService.FindUserReturns(201, 101);

            Assert.AreEqual(1, result2.Count());
            foreach (ReturnEvent libEvent in result2)
            {
                Assert.AreEqual(typeof(ReturnEvent), libEvent.GetType());
                Assert.AreEqual(101, libEvent.Copy.CopyId);
                Assert.AreEqual(201, libEvent.Reader.Id);
            }
        }

        [TestMethod]
        public void FindLastBorrowingTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

            Reader r = new Reader(201, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);

            DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));


            dataService.PurchaseCopy(101, 1, CopyCondition.Mint, testDate, "test");
            dataService.BorrowCopy(101, 201, testDate, testDate);
            dataService.PurchaseCopy(103, 1, CopyCondition.Mint, testDate, "test");
            dataService.BorrowCopy(103, 201, testDate, testDate);
            dataService.PurchaseCopy(105, 1, CopyCondition.Mint, testDate, "test");

            BorrowingEvent result = dataService.FindLastBorrowing(101);
            Assert.AreEqual(101,result.Copy.CopyId);
            Assert.AreEqual(201, result.Reader.Id);
        }

        [TestMethod]
        public void FindLastReturn()
        {
            {
                repo = new DataRepository(new EmptyProvider());
                dataService = new DataService(repo);

                Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
                repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

                Reader r = new Reader(201, "t", "tt");
                repo.AddReader(r.Id, r.FirstName, r.LastName);

                DateTimeOffset testDate = new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0));


                dataService.PurchaseCopy(101, 1, CopyCondition.Mint, testDate, "test");
                dataService.BorrowCopy(101, 201, testDate, testDate);
                dataService.PurchaseCopy(103, 1, CopyCondition.Mint, testDate, "test");
                dataService.BorrowCopy(103, 201, testDate, testDate);
                dataService.ReturnCopy(103, 201, testDate, CopyCondition.Damaged);

                ReturnEvent result = dataService.FindLastReturn(103);
                Assert.AreEqual(103, result.Copy.CopyId);
                Assert.AreEqual(201, result.Reader.Id);
            }
        }

        [TestMethod]
        public void FindBooksByTitleTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

            Book book2 = new Book(2, "testtitle2", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book2.Id, book2.Title, book2.Author, book2.Genres);

            Book book3 = new Book(3, "testtitle2", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book3.Id, book3.Title, book3.Author, book3.Genres);

            IEnumerable<Book> result = dataService.FindBooksByTitle("testtitle2");

            Assert.AreEqual(2, result.Count());
            foreach (Book wbook in result)
                Assert.AreEqual("testtitle2", wbook.Title);

        }

        [TestMethod]
        public void FindBookByAuthorTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Book book = new Book(1, "testtitle", "testauthor", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);

            Book book2 = new Book(2, "testtitle2", "testauthor2", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book2.Id, book2.Title, book2.Author, book2.Genres);

            Book book3 = new Book(3, "testtitle3", "testauthor2", new LiteraryGenre[] { LiteraryGenre.Comedy });
            repo.AddBook(book3.Id, book3.Title, book3.Author, book3.Genres);

            IEnumerable<Book> result = dataService.FindBooksByAuthor("testauthor2");

            Assert.AreEqual(2, result.Count());
            foreach (Book wbook in result)
                Assert.AreEqual("testauthor2", wbook.Author);
        }

        [TestMethod]
        public void FindReadersTest()
        {
            repo = new DataRepository(new EmptyProvider());
            dataService = new DataService(repo);

            Reader reader1 = new Reader(1, "Jakub", "Jakis");
            repo.AddReader(reader1.Id, reader1.FirstName, reader1.LastName);

            Reader reader2 = new Reader(2, "Jonasz", "test");
            repo.AddReader(reader2.Id, reader2.FirstName, reader2.LastName);

            Reader reader3 = new Reader(3, "Julian", "test");
            repo.AddReader(reader3.Id, reader3.FirstName, reader3.LastName);

            IEnumerable<Reader> result = dataService.FindReaders("test");

            Assert.AreEqual(2, result.Count());
            foreach (Reader reader in result)
                Assert.AreEqual("test",reader.LastName);
        }
    }
}
