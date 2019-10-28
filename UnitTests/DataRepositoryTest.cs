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
            Book book = new Book(1, "testtitle", "testautthor", "testgenre");
            Assert.AreEqual(0, repo.GetAllBooks().Count());
            repo.AddBook(book.Id,book.Title,book.Author,book.Genres);
            Assert.AreEqual(1, repo.GetAllBooks().Count());
            Assert.AreEqual(1, repo.GetBook(1).Id);
            Assert.AreEqual("testtitle", repo.GetBook(1).Title);
            Assert.AreEqual("testautthor", repo.GetBook(1).Author);
            Assert.AreEqual("testgenre", repo.GetBook(1).Genres);
            Assert.ThrowsException<ArgumentException>(() => repo.AddBook(book.Id, book.Title, book.Author, book.Genres));
        }

        [TestMethod]
        public void UpdateBookTest()
        {
            repo = new DataRepository(new EmptyProvider());
            Book book = new Book(1, "testtitle", "testautthor", "testgenre");
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.UpdateBook(1, "title", "testautthor", "testgenre");
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
            Book book = new Book(1, "testtitle", "testautthor", "testgenre");
            Copy copy = new Copy(1, book, CopyCondition.Mint); //TODO: ujednolicic tworzenie copy
            Assert.AreEqual(0, repo.GetAllCopies().Count());
            Assert.ThrowsException<KeyNotFoundException>(() => repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint));
            repo.AddBook(book.Id, book.Title, book.Author, book.Genres);
            repo.AddCopy(copy.CopyId, book.Id, CopyCondition.Mint);
            Assert.AreEqual(1, repo.GetAllCopies().Count());
            Assert.AreEqual(1, repo.GetCopy(1).CopyId);
            Assert.AreEqual(false, repo.GetCopy(1).Borrowed);
            Assert.AreEqual(CopyCondition.Mint, repo.GetCopy(1).Condition);
        }

    }
}
