using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Data
{
   public class DataService
    {
    
        private IDataRepository repository;

        public event EventHandler PurchaseHappened
        {
            add => repository.PurchaseHappened += value;
            remove => repository.PurchaseHappened -= value;
        }

        public event EventHandler DestructionHappened
        {
            add => repository.DestructionHappened += value;
            remove => repository.DestructionHappened -= value;
        }

        public event EventHandler BorrowingHappened
        {
            add => repository.BorrowingHappened += value;
            remove => repository.BorrowingHappened -= value;
        }

        public event EventHandler ReturnHappened
        {
            add => repository.ReturnHappened += value;
            remove => repository.ReturnHappened -= value;
        }

       public DataService(IDataRepository repository)
        {
            this.repository = repository;
        }


        public void PurchaseCopy (int copyId, int bookId, CopyCondition condition, DateTimeOffset eventDate, string distributor)
        {
            repository.AddCopy(copyId, bookId, condition);
            repository.AddPurchaseEvent(copyId, eventDate, bookId, distributor);
            
        }

        public void DestroyCopy ( int copyId, DateTimeOffset eventDate, string reason)
        {  
            if (repository.GetCopy(copyId).Borrowed)
                throw new ArgumentException("Nie można usunąć kopii, która jest aktualnie wypozyczona");
            else
            {
                repository.AddDestructionEvent(copyId, eventDate, reason);
                repository.DeleteCopy(copyId);
             }
        }

        public void BorrowCopy (int copyId, int readerId, DateTimeOffset eventDate, DateTimeOffset returnDate)
        {
            Copy borrowedCopy = repository.GetCopy(copyId);

            if (borrowedCopy.Borrowed == true)
                throw new ArgumentException("Próba wypożyczenia kopii, która jest juz wypożyczona!");
            else
            {
                repository.AddBorrowingEvent(copyId, eventDate, returnDate, readerId);
                repository.UpdateCopy(borrowedCopy.CopyId, borrowedCopy.Book.Id, true, borrowedCopy.Condition);
            }
        }

        public void ReturnCopy (int copyId, int readerId, DateTimeOffset eventDate, CopyCondition condition)
        {
            Copy returnedCopy = repository.GetCopy(copyId);
            BorrowingEvent borrowing = FindUserBorrowings(readerId,copyId).First(x => x.Completed == false);

            if (borrowing == null)
                throw new ArgumentException("Nie udało się odnaleźć wypożyczenia, które ma zostać zakończone");
            if (returnedCopy.Borrowed == false)
                throw new ArgumentException(" Próba zwrócenia kopii, która nie jest aktualnie wypożyczona");
            else
            {
                repository.AddReturnEvent(copyId, eventDate, readerId, borrowing);
                repository.CompleteBorrowingEvent(borrowing);
                repository.UpdateCopy(returnedCopy.CopyId, returnedCopy.Book.Id, false, condition);
            }

        }


        public void AddBook(int id, string title, string author, IEnumerable<LiteraryGenre> genres)=>repository.AddBook(id,title,author,genres);
        public Book GetBook(int bookID) => repository.GetBook(bookID);
        public bool ContainsBook(int bookID) => repository.ContainsBook(bookID);
        public IEnumerable<Book> GetAllBooks() => repository.GetAllBooks();
        public void UpdateBook(int orginalId, string title, string author, List<LiteraryGenre> genres) => repository.UpdateBook(orginalId, title, author, genres);
        public bool DeleteBook(int bookID) => repository.DeleteBook(bookID);

        public void AddReader(int id, string firstName, string lastName) => repository.AddReader(id, firstName, lastName);
        public Reader GetReader(int id) => repository.GetReader(id);
        public bool ContainsReader(int id) => repository.ContainsReader(id);
        public IEnumerable<Reader> GetAllReaders() => repository.GetAllReaders();
        public bool DeleteReader(int readerId) => repository.DeleteReader(readerId);
        public void UpdateReader(int originalId, string firstName, string lastName) => repository.UpdateReader(originalId, firstName, lastName);

        public Copy GetCopy(int copyID) => repository.GetCopy(copyID);
        public bool ContainsCopy(int copyId) => repository.ContainsCopy(copyId);
        public IEnumerable<Copy> GetAllCopies() => repository.GetAllCopies();
        public void UpdateCopy(int id, int bookId, bool borrowed, CopyCondition condition) => repository.UpdateCopy(id, bookId, borrowed, condition);



        public IEnumerable<LibEvent> FindEvents(Type type)
        {
            return repository.GetAllEvents().Where(x => x.GetType() == type);
        }
        public IEnumerable<LibEvent> FindEvents(int copyId)
        {
            return repository.GetAllEvents().Where(x => x.Copy.CopyId == copyId);
        }
        public IEnumerable<LibEvent> FindEvents(int copyId, Type type)
        {
            return repository.GetAllEvents().Where(x => x.Copy.CopyId == copyId && x.GetType() == type);
        }
        public IEnumerable<LibEvent> FindEventsInPeriod(DateTimeOffset beginning, DateTimeOffset end)
        {
            return repository.GetAllEvents().Where(x => x.EventDate >= beginning && x.EventDate<=end);
        }


        public IEnumerable<BorrowingEvent> FindUserBorrowings(int readerId)
        {
            return repository.GetAllEvents().Where(x => x.GetType() == typeof(BorrowingEvent)).Cast<BorrowingEvent>().Where(x => x.Reader.Id == readerId);
        }
        public IEnumerable<BorrowingEvent> FindUserBorrowings(int readerId, int copyId)
        {
            return repository.GetAllEvents().Where(x => x.GetType() == typeof(BorrowingEvent)).Cast<BorrowingEvent>().Where(x => x.Reader.Id == readerId && x.Copy.CopyId == copyId);

        }

        public IEnumerable<ReturnEvent> FindUserReturns(int readerId)
        {
            return repository.GetAllEvents().Where(x => x.GetType() == typeof(ReturnEvent)).Cast<ReturnEvent>().Where(x => x.Reader.Id == readerId);
        }
        public IEnumerable<ReturnEvent> FindUserReturns(int readerId, int copyId)
        {
            return repository.GetAllEvents().Where(x => x.GetType() == typeof(ReturnEvent)).Cast<ReturnEvent>().Where(x => x.Reader.Id == readerId && x.Copy.CopyId == copyId);
        }

        public BorrowingEvent FindLastBorrowing(int copyId)
        {
            return repository.GetAllEvents().Where(x => x.GetType() == typeof(BorrowingEvent)).Cast<BorrowingEvent>().Last(x => x.Copy.CopyId == copyId);
        }

        public ReturnEvent FindLastReturn(int copyId)
        {
            return repository.GetAllEvents().Where(x => x.GetType() == typeof(ReturnEvent)).Cast<ReturnEvent>().Last(x => x.Copy.CopyId == copyId);
        }

        public IEnumerable<Book> FindBooksByTitle(string title)
        {
            return repository.GetAllBooks().Where(x => x.Title == title);
        }

        public IEnumerable<Book> FindBooksByAuthor(string author)
        {
            return repository.GetAllBooks().Where(x => x.Author == author);
        }

        public IEnumerable<Reader> FindReaders (string lastName )
        {
            return repository.GetAllReaders().Where(x => x.LastName == lastName);
        }

    }
}
