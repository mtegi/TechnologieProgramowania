using DataHandler;
using BuisnessLogic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Data;
using DefinitionLib;

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

            WrappedPurchase purchaseEvent = (WrappedPurchase)repo.GetAllEvents().Last();

            Assert.AreEqual(true, PurchaseEventRised);

            Assert.AreEqual(true, repo.ContainsCopy(101));
            Assert.AreEqual(CopyCondition.Mint, repo.GetCopy(101).Condition);
            Assert.AreEqual(repo.GetBook(1).GetBook(), repo.GetCopy(101).Book.GetBook());
            Assert.AreEqual("testtitle", repo.GetCopy(101).Book.Title);
            Assert.AreEqual("testauthor", repo.GetCopy(101).Book.Author);

            Assert.AreEqual(101, purchaseEvent.Copy.CopyId);
            Assert.AreEqual(repo.GetCopy(101).GetCopy(), purchaseEvent.Copy.GetCopy());
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
            WrappedDestruction destructionEvent = (WrappedDestruction)repo.GetAllEvents().Last();

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
            WrappedBorrowing borrowingEvent = (WrappedBorrowing)repo.GetAllEvents().Last();


            Assert.AreEqual(true, repo.ContainsCopy(101));
            Assert.AreEqual(true, repo.ContainsReader(201));

            Assert.AreEqual(true, repo.GetCopy(101).Borrowed);
            Assert.AreEqual(true, BorrowingEventRised);

            Assert.AreEqual(101, borrowingEvent.Copy.CopyId);
            Assert.AreEqual(201, borrowingEvent.Reader.Id);
        }


    }
}
