using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    class DataRepository
    {
        private DataContext _data;

        public DataRepository(IDataProvider dataProvider) // Dependency Injection w kontruktorze
        {
            _data = new DataContext();
            dataProvider.Fill(_data);
        }

        public void AddBook(Book book)
        {
            _data.Books.Add(book.Id, book);
        }

        public Book GetBook(int bookID)
        {
            return _data.Books[bookID];
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _data.Books.Values;
        }

        public void UpdateBook(int bookID, Book book)
        {
            //TODO: sprawdzanie czy mozna, wyjatki
            _data.Books[bookID] = book;
        }

        public void DeleteBook(Book book)
        {
            //TODO: sprawdzanie czy mozna, wyjatki
            _data.Books.Remove(book.Id);
        }

        public void AddReader(Reader reader)
        {
            _data.Readers.Add(reader);
        }

        public Reader GetReader(int id)
        {
            return _data.Readers[id];
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            return _data.Readers;
        }

        public void DeleteReader(Reader reader)
        {
            //TODO: sprawdzic czy klient ma wypozyczenie bo inaczej nie mozna go usunac
            _data.Readers.Remove(reader);
        }

        public void UpdateReader(int id, Reader reader)
        {
            //TODO: jakies sprawdzanie?
            _data.Readers[id] = reader;
        }
    }
}
