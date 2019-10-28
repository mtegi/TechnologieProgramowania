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

        DataService(IDataRepository repository)
        {
            this.repository = repository;
        }

        IEnumerable<WrappedEvent> FindEvents ()
        {
            return repository.GetAllEvents();
        }

        IEnumerable<WrappedEvent> FindEvents(EventType type)
        {
            return repository.GetAllEvents().Where(x => x.Type == type);
        }


        IEnumerable<WrappedEvent> FindEvents (int copyId)
        {
            return repository.GetAllEvents().Where(x => x.Copy.CopyId == copyId);
        }

        IEnumerable<WrappedEvent> FindEvents(int copyId, EventType type)
        {
            return repository.GetAllEvents().Where(x => x.Copy.CopyId == copyId && x.Type == type);
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



        IEnumerable<WrappedBook>  FindBooksByTitle (string title)
        {
            return repository.GetAllBooks().Where(x => x.Title == title);
        }

        IEnumerable<WrappedBook> FindBooksByAuthor(string author)
        {
            return repository.GetAllBooks().Where(x => x.Author ==author);
        }


        public void AddCataloguePosition(int id, string title, string author, string genres)
        {
            repository.AddBook(id, title, author, genres);
        }

        public void RemoveCataloguePosition(int id)
        {
            repository.DeleteBook(id);
        }

        public void PurchaseCopy (int copyId, int bookId, CopyCondition condition, DateTimeOffset eventDate, string distributor)
        {   
            //ToDo: Exceptoions
            repository.AddPurchaseEvent(copyId, eventDate, bookId, distributor);
            repository.AddCopy(copyId, bookId, condition);
        }

        public void DestroyCopy ( int copyId, DateTimeOffset eventDate, string reason)
        {  //ToDo: Exceptions
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

        public void ReturnCopy (int copyId, int readerId, DateTimeOffset eventDate)
        {
            WrappedCopy returnedCopy = repository.GetCopy(copyId);
            WrappedBorrowing borrowing = FindUserBorrowings(readerId,copyId).First(x => x.Completed == false);

            repository.AddReturnEvent(copyId, eventDate, readerId, borrowing);
            repository.CompleteBorrowingEvent(borrowing);
            repository.UpdateCopy(returnedCopy.CopyId, returnedCopy.Book.Id, false, returnedCopy.Condition);

        }



    }
}
