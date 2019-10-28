using System;
using DataHandler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefinitionLib;

namespace BuisnessLogic
{
    class DataService
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

        //KONTRUKTOR
        DataService(IDataRepository repository)
        {
            this.repository = repository;
        }


        public void PurchaseCopy (int copyId, int bookId, CopyCondition condition, DateTimeOffset eventDate, string distributor)
        {   
            repository.AddPurchaseEvent(copyId, eventDate, bookId, distributor);
            repository.AddCopy(copyId, bookId, condition);
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
            repository.AddBorrowingEvent(copyId, eventDate, returnDate, readerId);
            WrappedCopy borrowedCopy = repository.GetCopy(copyId);
            repository.UpdateCopy(borrowedCopy.CopyId, borrowedCopy.Book.Id, true, borrowedCopy.Condition);
        }

        public void ReturnCopy (int copyId, int readerId, DateTimeOffset eventDate, CopyCondition condition)
        {
            WrappedCopy returnedCopy = repository.GetCopy(copyId);
            WrappedBorrowing borrowing = FindUserBorrowings(readerId,copyId).First(x => x.Completed == false);

            repository.AddReturnEvent(copyId, eventDate, readerId, borrowing);
            repository.CompleteBorrowingEvent(borrowing);
            repository.UpdateCopy(returnedCopy.CopyId, returnedCopy.Book.Id, false, condition);

        }


        public void AddBook(int id, string title, string author, IEnumerable<LiteraryGenre> genres)=>repository.AddBook(id,title,author,genres);
        public WrappedBook GetBook(int bookID) => repository.GetBook(bookID);
        public bool ContainsBook(int bookID) => repository.ContainsBook(bookID);
        public IEnumerable<WrappedBook> GetAllBooks() => repository.GetAllBooks();
        public void UpdateBook(int orginalId, string title, string author, List<LiteraryGenre> genres) => repository.UpdateBook(orginalId, title, author, genres);
        public bool DeleteBook(int bookID) => repository.DeleteBook(bookID);

        public void AddReader(int id, string firstName, string lastName) => repository.AddReader(id, firstName, lastName);
        public WrappedReader GetReader(int id) => repository.GetReader(id);
        public bool ContainsReader(int id) => repository.ContainsReader(id);
        public IEnumerable<WrappedReader> GetAllReaders() => repository.GetAllReaders();
        public bool DeleteReader(int readerId) => repository.DeleteReader(readerId);
        public void UpdateReader(int originalId, string firstName, string lastName) => repository.UpdateReader(originalId, firstName, lastName);

        public WrappedCopy GetCopy(int copyID) => repository.GetCopy(copyID);
        public bool ContainsCopy(int copyId) => repository.ContainsCopy(copyId);
        public IEnumerable<WrappedCopy> GetAllCopies() => repository.GetAllCopies();
        public void UpdateCopy(int id, int bookId, bool borrowed, CopyCondition condition) => repository.UpdateCopy(id, bookId, borrowed, condition);



        IEnumerable<WrappedEvent> FindEvents(EventType type)
        {
            return repository.GetAllEvents().Where(x => x.Type == type);
        }
        IEnumerable<WrappedEvent> FindEvents(int copyId)
        {
            return repository.GetAllEvents().Where(x => x.Copy.CopyId == copyId);
        }
        IEnumerable<WrappedEvent> FindEvents(int copyId, EventType type)
        {
            return repository.GetAllEvents().Where(x => x.Copy.CopyId == copyId && x.Type == type);
        }
        IEnumerable<WrappedEvent> FindEventsInPeriod(DateTimeOffset beginning, DateTimeOffset end)
        {
            return repository.GetAllEvents().Where(x => x.EventDate >= beginning && x.EventDate<=end);
        }


        IEnumerable<WrappedBorrowing> FindUserBorrowings(int readerId)
        {
            return repository.GetAllEvents().Where(x => x.Type == EventType.Borrowing).Cast<WrappedBorrowing>().Where(x => x.Reader.Id == readerId);
        }
        IEnumerable<WrappedBorrowing> FindUserBorrowings(int readerId, int copyId)
        {
            return repository.GetAllEvents().Where(x => x.Type == EventType.Borrowing).Cast<WrappedBorrowing>().Where(x => x.Reader.Id == readerId && x.Copy.CopyId == copyId);

        }

        IEnumerable<WrappedReturn> FindUserReturns(int readerId)
        {
            return repository.GetAllEvents().Where(x => x.Type == EventType.Return).Cast<WrappedReturn>().Where(x => x.Reader.Id == readerId);
        }
        IEnumerable<WrappedReturn> FindUserReturns(int readerId, int copyId)
        {
            return repository.GetAllEvents().Where(x => x.Type == EventType.Return).Cast<WrappedReturn>().Where(x => x.Reader.Id == readerId && x.Copy.CopyId == copyId);
        }

        WrappedBorrowing FindLastBorrowing(int copyId)
        {
            return repository.GetAllEvents().Where(x => x.Type == EventType.Borrowing).Cast<WrappedBorrowing>().Last(x => x.Copy.CopyId == copyId);
        }

        WrappedReturn FindLastReturn(int copyId)
        {
            return repository.GetAllEvents().Where(x => x.Type == EventType.Return).Cast<WrappedReturn>().Last(x => x.Copy.CopyId == copyId);
        }

        IEnumerable<WrappedBook> FindBooksByTitle(string title)
        {
            return repository.GetAllBooks().Where(x => x.Title == title);
        }

        IEnumerable<WrappedBook> FindBooksByAuthor(string author)
        {
            return repository.GetAllBooks().Where(x => x.Author == author);
        }

        IEnumerable<WrappedReader> FindReaders (string lastName )
        {
            return repository.GetAllReaders().Where(x => x.LastName == lastName);
        }

    }
}
