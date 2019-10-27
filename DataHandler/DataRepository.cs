using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public class DataRepository : IDataRepository
    {
        private DataContext _data;

        public DataRepository(IDataProvider dataProvider) // Dependency Injection w kontruktorze
        {
            _data = new DataContext();
            dataProvider.Fill(_data);
        }

        public void AddBook(int id, string title, string author, string genres)
        {
            _data.Books.Add(id, new Book(id,title,author,genres));
        }

        public WrappedBook GetBook(int bookID)
        {
            return new WrappedBook(_data.Books[bookID]);
        }

        public bool ContainsBook(int id)
        {
            return _data.Books.ContainsKey(id);
        }

        public IEnumerable<WrappedBook> GetAllBooks()
        {
             List<WrappedBook> result = new List<WrappedBook>();

            foreach (KeyValuePair<int, Book> entry in _data.Books)
            {
                result.Add(new WrappedBook(entry.Value));
            }
            return result;
        }

        public void UpdateBook(int orginalId, string title, string author, string genres)
        {
                _data.Books[orginalId].Title = title;
                _data.Books[orginalId].Author = author;
                _data.Books[orginalId].Genres = genres;
            
        }

        public void DeleteBook(int bookID)
        {
            _data.Books.Remove(bookID);
        }

        public void AddReader(int id, string firstName, string lastName)
        {
            if(_data.Readers.Any(i => i.Id==id))
                throw new ArgumentException("Czytelnik o danym ID już znajduje się w wykazie");
            else
            _data.Readers.Add(new Reader(id,firstName,lastName));
        }

        public WrappedReader GetReader(int id)
        {
            return new WrappedReader( _data.Readers.Find(x => x.Id == id));
        }

        public bool ContainsReader(int id)
        {
            return _data.Readers.Any(x => x.Id == id);
        }

        public IEnumerable<WrappedReader> GetAllReaders()
        {
            List<WrappedReader> result = new List<WrappedReader>();

            foreach (Reader reader in _data.Readers)
            {
                result.Add(new WrappedReader(reader));
            }

            return result;
        }

        public void DeleteReader(int readerId)
        {
            _data.Readers.RemoveAll(i => i.Id == readerId);
        }

        public void UpdateReader(int originalId, string firstName, string lastName)
        {
            _data.Readers[originalId].FirstName = firstName;
            _data.Readers[originalId].LastName = lastName;
        }

        public void AddCopy(int copyId, int bookId, CopyCondition condition)
        {
             _data.Copies.Add(copyId, new Copy(copyId,_data.Books[bookId], (int) condition ));
        }

        public WrappedCopy GetCopy(int copyID)
        {
            return new WrappedCopy( _data.Copies[copyID]);
        }

        public bool ContainsCopy(int copyId)
        {
            return _data.Copies.ContainsKey(copyId);
        }

        public IEnumerable<WrappedCopy> GetAllCopies()
        {
            List<WrappedCopy> result = new List<WrappedCopy>();

            foreach (KeyValuePair<int, Copy> entry in _data.Copies)
            {
                result.Add(new WrappedCopy(entry.Value));
            }
            return result;
        }

        public void UpdateCopy(int id, int bookId, bool borrowed, CopyCondition condition)
        {
            _data.Copies[id].Book = _data.Books[bookId];
            _data.Copies[id].Borrowed = borrowed;
            _data.Copies[id].Condition = (int)condition;
        }


        public void DeleteCopy(int copyID)
        {
            _data.Copies.Remove(copyID);
        }

        public void AddPurchaseEvent ( int copyId, DateTimeOffset eventDate, int price, string distributor)
        {
            _data.Events.Add(new PurchaseEvent(_data.Copies[copyId], eventDate, price, distributor));
        }

        public void AddDestructionEvent (int copyId, DateTimeOffset eventDate, string reason)
        {
            _data.Events.Add(new DestructionEvent(_data.Copies[copyId], eventDate, reason));
        }

        public void AddBorrowingEvent (int copyId, DateTimeOffset eventDate, DateTimeOffset returnDate, int readerId)
        {
            _data.Events.Add(new BorrowingEvent(_data.Copies[copyId], eventDate, returnDate, _data.Readers.Find(x => x.Id == readerId)));
        }

        public void AddReturnEvent(int copyId, DateTimeOffset eventDate, int readerId)
        {
            _data.Events.Add(new ReturnEvent(_data.Copies[copyId], eventDate, _data.Readers.Find(x => x.Id == readerId)));
        }


    }
}
