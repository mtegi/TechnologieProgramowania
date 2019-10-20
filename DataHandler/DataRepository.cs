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
        private IdManager idManager;

        public DataRepository(IDataProvider dataProvider) // Dependency Injection w kontruktorze
        {
            _data = new DataContext();
            idManager = new DefaultIdManager();
            dataProvider.Fill(_data, idManager);
        }

        public DataRepository(IDataProvider dataProvider, IdManager idManager) // Dependency Injection w kontruktorze
        {
            _data = new DataContext();
            this.idManager = idManager;
            dataProvider.Fill(_data, idManager);
        }

        public void AddBook(Book book)
        {
            if (_data.Books.ContainsKey(book.Id))
                throw new ArgumentException("Książka o danym ID już znajduje się w katalogu");
            else
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

            if (_data.Books.ContainsKey(book.Id) && bookID!= book.Id)
                throw new ArgumentException("Książka o danym ID już znajduje się w katalogu");
            else
            _data.Books[bookID] = book;
        }

        public List<Book> FindBooks(string title)
        {
            List<Book> result = new List<Book>();
            foreach (Book entry in _data.Books.Values)
            {
                if (String.Equals(entry.Title, title, StringComparison.CurrentCultureIgnoreCase))
                    result.Add(entry);
            }
            return result;
        }

        public void DeleteBook(Book book)
        {

            //TODO: sprawdzanie czy mozna, wyjatki
            _data.Books.Remove(book.Id);
        }

        public void DeleteBook(int bookID)
        {
            _data.Books.Remove(bookID);
        }

        public void AddReader(Reader reader)
        {
            if(_data.Readers.Any(i => i.Id==reader.Id))
                throw new ArgumentException("Czytelnik o danym ID już znajduje się w wykazie");
            else
            _data.Readers.Add(reader);
        }

        public Reader GetReader(int id)
        {
            return _data.Readers.Find(x => x.Id == id);
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

        public void DeleteReader(int readerId)
        {
            _data.Readers.RemoveAll(i => i.Id == readerId);
        }

        public void UpdateReader(int id, Reader reader)
        {
            if (_data.Readers.Any(i => i.Id == reader.Id) && id != reader.Id)
                throw new ArgumentException("Książka o danym ID już znajduje się w katalogu");
            else
            _data.Readers[id] = reader;
        }

      
    }
}
