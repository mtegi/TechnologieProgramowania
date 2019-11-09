using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DataRepository : IDataRepository
    {
     
        public event EventHandler PurchaseHappened;
        public event EventHandler DestructionHappened;
        public event EventHandler BorrowingHappened;
        public event EventHandler ReturnHappened;


        private readonly DataContext _data = new DataContext();
        private readonly IDataProvider _dataProvider;

        public DataRepository(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _dataProvider.Fill(_data);
            _data.Events.CollectionChanged += EventsCollectionChanged;
        }

        private void EventsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(LibEvent libEvent in e.NewItems)
                {
                    if(libEvent is BorrowingEvent)
                    {
                        BorrowingHappened?.Invoke(this, new EventArgs());
                    }
                    else if(libEvent is ReturnEvent)
                    {
                        ReturnHappened?.Invoke(this, new EventArgs());
                    }
                    else if (libEvent is PurchaseEvent)
                    {
                        PurchaseHappened?.Invoke(this, new EventArgs());
                    }
                    else if (libEvent is DestructionEvent)
                    {
                        DestructionHappened?.Invoke(this, new EventArgs());
                    }
                }
            }

        }

        public void AddBook(int id, string title, string author, IEnumerable<LiteraryGenre> genres)
        {
            _data.Books.Add(id, value: new Book(id,title,author,genres));
        }

        public Book GetBook(int bookID)
        {
            return  _data.Books[bookID];
        }

        public bool ContainsBook(int bookID)
        {
            return _data.Books.ContainsKey(bookID);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _data.Books.Values.ToList();
        }

        public void UpdateBook(int orginalId, string title, string author, List<LiteraryGenre> genres)
        {
                _data.Books[orginalId].Title = title;
                _data.Books[orginalId].Author = author;
                _data.Books[orginalId].Genres = genres;
            
        }

        public bool DeleteBook(int bookID)
        {
            bool canRemove = true;
            foreach (KeyValuePair<int, Copy> copy in _data.Copies)
            {
                if (copy.Value.Borrowed == true && copy.Key == bookID ) canRemove = false;
            }
            if (canRemove) return _data.Books.Remove(bookID);
            return false;
        }

        public void AddReader(int id, string firstName, string lastName)
        {
            if(_data.Readers.Any(i => i.Id==id))
                throw new ArgumentException("Czytelnik o danym ID już znajduje się w wykazie");
            else
            _data.Readers.Add(new Reader(id,firstName,lastName));
        }

        public Reader GetReader(int id)
        {
            return _data.Readers.Find((x => x.Id == id));
        }

        public bool ContainsReader(int id)
        {
            return _data.Readers.Any(x => x.Id == id);
        }

        public IEnumerable<Reader> GetAllReaders()
        {
           return _data.Readers.ToList();
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

        public Copy GetCopy(int copyID)
        {
            return _data.Copies[copyID];
        }

        public bool ContainsCopy(int copyId)
        {
            return _data.Copies.ContainsKey(copyId);
        }

        public IEnumerable<Copy> GetAllCopies()
        {
            return _data.Copies.Values.ToList();
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
            if (_data.Copies[copyID].Borrowed) canRemove = false;
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

        public void CompleteBorrowingEvent (BorrowingEvent borrowing)
        {
            borrowing.Completed = true;
        }

        public void AddReturnEvent(int copyId, DateTimeOffset eventDate, int readerId, BorrowingEvent borrowing)
        {
            _data.Events.Add(new ReturnEvent(_data.Copies[copyId], eventDate, _data.Readers.Find(x => x.Id == readerId),borrowing));
        }

        public IEnumerable<LibEvent> GetAllEvents()
        {
            return _data.Events.ToList();
        }

 



    }
}
