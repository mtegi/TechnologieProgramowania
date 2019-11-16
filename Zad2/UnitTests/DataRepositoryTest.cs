using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Library;

namespace UnitTests
{
    [TestClass]
    public class DataRepositoryTest
    {

        internal class TraceProvider : IDataProvider
        {
            internal List<int> _callStack = new List<int>();

            public void TraceData(TraceEventType eventType, int id, object data)
            {
                _callStack.Add(id);
            }

            internal void CheckConsistency()
            {
                Assert.AreEqual<int>(1, _callStack.Count);
                Assert.AreEqual<int>("Fill".GetHashCode(), _callStack[0]);
            }

            public void Fill(DataContext data)
            {
                this.TraceData(TraceEventType.Verbose, nameof(Fill).GetHashCode(), "Filling DataContext");
            }
        }

        private DataRepository repo;

        [TestMethod]
        public void DependecyInjectionTest()
        {
            TraceProvider provider = new TraceProvider();
            Assert.AreEqual<int>(0, provider._callStack.Count);
            repo = new DataRepository(provider);
            Assert.AreEqual<int>(1, provider._callStack.Count);
            Assert.AreEqual<int>("Fill".GetHashCode(), provider._callStack[0]);
            repo = new DataRepository(provider);
            Assert.AreEqual<int>(2, provider._callStack.Count);
        }

        [TestMethod]
        public void AddBookTest()
        {
            repo = new DataRepository(new ContextFiller());
            Book book = new Book(4, "testtitle", "testautthor", new LiteraryGenre []{ LiteraryGenre.Comedy });
            Assert.AreEqual(3, repo.GetAllBooks().Count());
            repo.AddBook(book.Id,book.Title,book.Author, book.Genres);
            Assert.AreEqual(4, repo.GetAllBooks().Count());
            Assert.AreEqual(4, repo.GetBook(4).Id);
            Assert.AreEqual(book.Title, repo.GetBook(4).Title);
            Assert.AreEqual(book.Author, repo.GetBook(4).Author);
            Assert.AreEqual(true, book.Genres.SequenceEqual(repo.GetBook(4).Genres));
            Assert.ThrowsException<ArgumentException>(() => repo.AddBook(book.Id, book.Title, book.Author, book.Genres));
        }

        [TestMethod]
        public void UpdateBookTest()
        {
            repo = new DataRepository(new ContextFiller());
            repo.UpdateBook(1, "title", "testautthor", new List<LiteraryGenre>() { LiteraryGenre.SciFi });
            Assert.AreEqual("title", repo.GetBook(1).Title);
            Assert.AreEqual("testautthor",repo.GetBook(1).Author);
            Assert.ThrowsException<KeyNotFoundException>(() => repo.UpdateBook(100, "title", "testautthor", new List<LiteraryGenre>() { LiteraryGenre.SciFi }));
        }

        [TestMethod]
        public void AddReaderTest()
        {
            repo = new DataRepository(new ContextFiller());
            Assert.AreEqual(3, repo.GetAllReaders().Count());
            Reader r = new Reader(4, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);
            Assert.AreEqual(4, repo.GetAllReaders().Count());
            Assert.AreEqual(4, repo.GetReader(4).Id);
            Assert.AreEqual("t", repo.GetReader(4).FirstName);
            Assert.AreEqual("tt", repo.GetReader(4).LastName);
            Assert.ThrowsException<ArgumentException>(() => repo.AddReader(r.Id, r.FirstName, r.LastName));
        }

        [TestMethod]
        public void UpdateReaderTest()
        {
            repo = new DataRepository(new ContextFiller());
            repo.UpdateReader(1, "ttt", "t");
            Assert.AreEqual("ttt", repo.GetReader(1).FirstName);
            Assert.AreEqual("t", repo.GetReader(1).LastName);
            Assert.ThrowsException<NullReferenceException>(() => repo.UpdateReader(2000, "ttt", "t"));
        }

        [TestMethod]
        public void AddCopyTest()
        {
            repo = new DataRepository(new ContextFiller());
            Book book = new Book(4, "testtitle", "testautthor", new LiteraryGenre[] { LiteraryGenre.Horror });
            Copy copy = new Copy(7, book, CopyCondition.Mint);
            Assert.AreEqual(6, repo.GetAllCopies().Count());
            Assert.ThrowsException<KeyNotFoundException>(() => repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint));
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint);
            Assert.AreEqual(7, repo.GetAllCopies().Count());
            Assert.AreEqual(7, repo.GetCopy(7).CopyId);
            Assert.AreEqual(false, repo.GetCopy(7).Borrowed);
            Assert.AreEqual(CopyCondition.Mint, repo.GetCopy(7).Condition);
        }

        [TestMethod]
        public void UpdateCopyTest()
        {
            repo = new DataRepository(new ContextFiller());
            Assert.AreEqual(false, repo.GetCopy(1).Borrowed);
            repo.UpdateCopy(1, 2, true, CopyCondition.Mint);
            Assert.AreEqual(repo.GetBook(2).Title, repo.GetCopy(1).Book.Title);
            Assert.AreEqual(repo.GetBook(2).Author, repo.GetCopy(1).Book.Author);
            Assert.AreEqual(true, repo.GetCopy(1).Borrowed);
            Assert.ThrowsException<KeyNotFoundException>(() => repo.UpdateCopy(2000, 1, false, CopyCondition.Good));
            Assert.ThrowsException<KeyNotFoundException>(() => repo.UpdateCopy(1, 2000, false, CopyCondition.Good));
        }

        [TestMethod]
        public void AddEventsTest()
        {
            repo = new DataRepository(new ContextFiller());
            Assert.AreEqual(false, repo.GetCopy(1).Borrowed);
            repo.AddBorrowingEvent(1, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0)), 1);
            Assert.AreEqual(5, repo.GetAllEvents().Count());
            repo.AddDestructionEvent(1, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)),"botak");
            Assert.AreEqual(6, repo.GetAllEvents().Count());
            repo.AddPurchaseEvent(1, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), 200, "test");
            Assert.AreEqual(7, repo.GetAllEvents().Count());
            repo.AddReturnEvent(1, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)),1, repo.GetAllEvents().OfType<BorrowingEvent>().FirstOrDefault());
            Assert.AreEqual(8, repo.GetAllEvents().Count());
        }

        [TestMethod]
        public void DeleteBookTest()
        {
            repo = new DataRepository(new ContextFiller());
            repo.UpdateCopy(1, repo.GetBook(1).Id, true, CopyCondition.Mint);
            Assert.IsFalse(repo.DeleteBook(1));
            Assert.IsTrue(repo.DeleteBook(2));
        }

        [TestMethod]
        public void DeleteCopyTest()
        {
            repo = new DataRepository(new ContextFiller());
            repo.UpdateCopy(2, repo.GetBook(2).Id, true, CopyCondition.Mint);
            Assert.IsFalse(repo.DeleteCopy(2));
            Assert.IsTrue(repo.DeleteCopy(1));
        }

        [TestMethod]
        public void DeleteReaderTest()
        {
            repo = new DataRepository(new ContextFiller());
            Assert.IsTrue(repo.DeleteReader(1));
        }

        [TestMethod]
        public void ContainTests()
        {
            repo = new DataRepository(new ContextFiller());
            Assert.IsTrue(repo.ContainsReader(1));
            Assert.IsTrue(repo.ContainsBook(1));
            Assert.IsTrue(repo.ContainsCopy(1));
            Assert.IsFalse(repo.ContainsBook(10));
            Assert.IsFalse(repo.ContainsCopy(10));
        }
    }
}
