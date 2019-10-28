using Data;
using DefinitionLib;
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

        public void AddBook(int id, string title, string author, IEnumerable<LiteraryGenre> genres)
        {
            _data.Books.Add(id, value: new Book(id,title,author,genres));
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

        public void UpdateBook(int orginalId, string title, string author, List<LiteraryGenre> genres)
        {
                _data.Books[orginalId].Title = title;
                _data.Books[orginalId].Author = author;
                _data.Books[orginalId].Genres = genres;
            
        }

        public bool DeleteBook(int bookID)
        {
            return _data.Books.Remove(bookID);
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

        public bool DeleteReader(int readerId)
        {
           Reader readertoremove = _data.Readers.Single(r => r.Id == readerId);
           return  _data.Readers.Remove(readertoremove);
        }

        public void UpdateReader(int originalId, string firstName, string lastName)
        {
            Reader reader = _data.Readers.Find(r => r.Id == originalId);
            reader.FirstName = firstName;
            reader.LastName = lastName;
        }

        public void AddCopy(int copyId, int bookId, CopyCondition condition)
        {
             _data.Copies.Add(copyId, new Copy(copyId,_data.Books[bookId],  condition ));
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
            _data.Copies[id].Condition = condition;
        }


        public bool DeleteCopy(int copyID)
        {
            bool canRemove = true;
            foreach (KeyValuePair<int, Copy> copy in _data.Copies)
            {
                if (copy.Value.Borrowed == true) canRemove = false;
            }
            if(canRemove) return _data.Copies.Remove(copyID);
            return false;
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
            _data.Events.Add(new BorrowingEvent(_data.Readers.Find(x => x.Id == readerId), _data.Copies[copyId], eventDate, returnDate));
        }

        public void CompleteBorrowingEvent (WrappedBorrowing wrappedBorrwing)
        {
            BorrowingEvent borrowing = (BorrowingEvent)wrappedBorrwing.GetEvent();
            borrowing.Completed = true;
        }

        public void AddReturnEvent(int copyId, DateTimeOffset eventDate, int readerId, WrappedBorrowing borrowing)
        {
            _data.Events.Add(new ReturnEvent(_data.Copies[copyId], eventDate, _data.Readers.Find(x => x.Id == readerId),(BorrowingEvent)borrowing.GetEvent()));
        }

        public IEnumerable<WrappedEvent> GetAllEvents()
        {
            List<WrappedEvent> result = new List<WrappedEvent>();

            foreach (LibEvent libEvent in _data.Events)
            {
                result.Add(WrapEvent(libEvent));
            }

            return result;
        }

        private WrappedEvent WrapEvent (LibEvent libEvent)
        {
            WrappedEvent result;
            switch(libEvent.Type)
            {
                case EventType.Purchase:
                    result = new WrappedPurchase((PurchaseEvent)libEvent);
                    break;
                case EventType.Destruction:
                    result = new WrappedDestruction((DestructionEvent)libEvent);
                    break;
                case EventType.Borrowing:
                    result = new WrappedBorrowing((BorrowingEvent)libEvent);
                    break;
                case EventType.Return:
                    result = new WrappedReturn((ReturnEvent)libEvent);
                    break;
                default:
                    throw new ArgumentException("Nie rozpoznano typu wydarzenia");
            }
            return result;
        }


    }
}
