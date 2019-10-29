using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataCreator;
using DataHandler;
using Data;
using System.Linq;
using System;
using System.Collections.Generic;
using DefinitionLib;

namespace UnitTests
{
    [TestClass]
    public class DataRepositoryTest
    {
        private class EmptyProvider : IDataProvider
        {
            public void Fill(DataContext data)
            {}
        }

        private DataRepository repo;
        [TestMethod]
        public void AddBookTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Book book = new Book(1, "testtitle", "testautthor", new LiteraryGenre []{ LiteraryGenre.Comedy });
            Assert.AreEqual(0, repo.GetAllBooks().Count());
            repo.AddBook(book.Id,book.Title,book.Author, book.Genres);
            Assert.AreEqual(1, repo.GetAllBooks().Count());
            Assert.AreEqual(1, repo.GetBook(1).Id);
            Assert.AreEqual(book.Title, repo.GetBook(1).Title);
            Assert.AreEqual(book.Author, repo.GetBook(1).Author);
            Assert.AreEqual(true, book.Genres.SequenceEqual(repo.GetBook(1).Genres));
            Assert.ThrowsException<ArgumentException>(() => repo.AddBook(book.Id, book.Title, book.Author, book.Genres));
        }

        [TestMethod]
        public void UpdateBookTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Book book = new Book(1, "testtitle", "testautthor", new LiteraryGenre[] { LiteraryGenre.SciFi });
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.UpdateBook(1, "title", "testautthor", book.Genres);
            Assert.AreEqual("title", repo.GetBook(1).Title);
            Assert.ThrowsException<KeyNotFoundException>(() => repo.UpdateBook(2, book.Title, book.Author, book.Genres));
        }

        [TestMethod]
        public void AddReaderTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Assert.AreEqual(0, repo.GetAllReaders().Count());
            Reader r = new Reader(1, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);
            Assert.AreEqual(1, repo.GetAllReaders().Count());
            Assert.AreEqual(1, repo.GetReader(1).Id);
            Assert.AreEqual("t", repo.GetReader(1).FirstName);
            Assert.AreEqual("tt", repo.GetReader(1).LastName);
            Assert.ThrowsException<ArgumentException>(() => repo.AddReader(r.Id, r.FirstName, r.LastName));
        }

        [TestMethod]
        public void UpdateReaderTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Reader r = new Reader(1, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);
            repo.UpdateReader(1, "ttt", "t");
            Assert.AreEqual("ttt", repo.GetReader(1).FirstName);
            Assert.AreEqual("t", repo.GetReader(1).LastName);
            Assert.ThrowsException<NullReferenceException>(() => repo.UpdateReader(2000, "ttt", "t"));
        }

        [TestMethod]
        public void AddCopyTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Book book = new Book(1, "testtitle", "testautthor", new LiteraryGenre[] { LiteraryGenre.Horror });
            Copy copy = new Copy(1, book, CopyCondition.Mint);
            Assert.AreEqual(0, repo.GetAllCopies().Count());
            Assert.ThrowsException<KeyNotFoundException>(() => repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint));
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint);
            Assert.AreEqual(1, repo.GetAllCopies().Count());
            Assert.AreEqual(1, repo.GetCopy(1).CopyId);
            Assert.AreEqual(false, repo.GetCopy(1).Borrowed);
            Assert.AreEqual(CopyCondition.Mint, repo.GetCopy(1).Condition);
        }

        [TestMethod]
        public void UpdateCopyTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Book book = new Book(1, "testtitle", "testautthor", new LiteraryGenre[] { LiteraryGenre.Horror });
            Book book2 = new Book(2, "testtitle2", "testautthor2", new LiteraryGenre[] { LiteraryGenre.Horror });
            Copy copy = new Copy(1, book, CopyCondition.Mint);
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.AddBook(book2.Id, book2.Title, book2.Author, book2.Genres);
            repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint);
            Assert.AreEqual(false, repo.GetCopy(1).Borrowed);
            repo.UpdateCopy(1, 2, true, CopyCondition.Mint);
            Assert.AreEqual(book2.Title, repo.GetCopy(1).Book.Title);
            Assert.AreEqual(book2.Author, repo.GetCopy(1).Book.Author);
            Assert.AreEqual(true, repo.GetCopy(1).Borrowed);
            Assert.ThrowsException<KeyNotFoundException>(() => repo.UpdateCopy(2000, 1, false, CopyCondition.Good));
            Assert.ThrowsException<KeyNotFoundException>(() => repo.UpdateCopy(1, 2000, false, CopyCondition.Good));
        }

        [TestMethod]
        public void AddEventsTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Book book = new Book(1, "testtitle", "testautthor", new LiteraryGenre[] { LiteraryGenre.Horror });
            Book book2 = new Book(2, "testtitle2", "testautthor2", new LiteraryGenre[] { LiteraryGenre.Horror });
            Copy copy = new Copy(1, book, CopyCondition.Mint);
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.AddBook(book2.Id, book2.Title, book2.Author, book2.Genres);
            repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint);
            Reader r = new Reader(1, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);

            Assert.AreEqual(false, repo.GetCopy(1).Borrowed);
            repo.AddBorrowingEvent(1, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0)), 1);
            Assert.AreEqual(1, repo.GetAllEvents().Count());
            repo.AddDestructionEvent(1, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)),"botak");
            Assert.AreEqual(2, repo.GetAllEvents().Count());
            repo.AddPurchaseEvent(1, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), 200, "test");
            Assert.AreEqual(3, repo.GetAllEvents().Count());
            repo.AddReturnEvent(1, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)),1, repo.GetAllEvents().OfType<WrappedBorrowing>().FirstOrDefault());
            Assert.AreEqual(4, repo.GetAllEvents().Count());
        }

        [TestMethod]
        public void DeleteBookTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Book book = new Book(1, "testtitle", "testautthor", new LiteraryGenre[] { LiteraryGenre.Horror });
            Book book2 = new Book(2, "testtitle2", "testautthor2", new LiteraryGenre[] { LiteraryGenre.Horror });
            Copy copy = new Copy(1, book, CopyCondition.Mint);
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.AddBook(book2.Id, book2.Title, book2.Author, book2.Genres);
            repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint);
            Assert.AreEqual(2, repo.GetAllBooks().Count());
            //Assert.ThrowsException<NullReferenceException>(() => repo.DeleteBook(book.Id));
            Assert.IsTrue(repo.DeleteBook(2));
        }

        [TestMethod]
        public void DeleteCopyTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Book book = new Book(1, "testtitle", "testautthor", new LiteraryGenre[] { LiteraryGenre.Horror });
            Copy copy = new Copy(1, book, CopyCondition.Mint);
            Copy copy2 = new Copy(2, book, CopyCondition.Mint);      
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint);
            repo.AddCopy(copy2.CopyId, book.Id, CopyCondition.Mint);
            repo.UpdateCopy(copy2.CopyId, book.Id, true, CopyCondition.Mint);
            Assert.IsFalse(repo.DeleteCopy(2));
            Assert.IsTrue(repo.DeleteBook(1));
        }

        [TestMethod]
        public void DeleteReaderTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Reader r = new Reader(1, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);
            Assert.IsTrue(repo.DeleteReader(1));
        }

        //TODO: Contain testy
        [TestMethod]
        public void ContainTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Reader r = new Reader(1, "t", "tt");
            repo.AddReader(r.Id, r.FirstName, r.LastName);
            Assert.IsTrue(repo.ContainsReader(1));
            Book book = new Book(1, "testtitle", "testautthor", new LiteraryGenre[] { LiteraryGenre.Horror });
            Copy copy = new Copy(1, book, CopyCondition.Mint);
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint);
            Assert.IsTrue(repo.ContainsBook(1));
            Assert.IsTrue(repo.ContainsCopy(1));
        }
    }
}
