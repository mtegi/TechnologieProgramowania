using System;
using DataHandler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic
{
    class DataService
    {
      private IDataRepository repository;

        DataService(IDataRepository repository)
        {
            this.repository = repository;
        }

        IEnumerable<WrappedBorrowing> FindBorrowingEvents ()
        {
            return repository.GetAllEvents().Where(x => x.Type == EventType.Borrowing).Cast<WrappedBorrowing>();
        }

        IEnumerable<WrappedBorrowing> FindBorrowingEventsByCopy(int copyId)
        {
            return FindBorrowingEvents().Where(x => x.Copy.copyId == copyId);
        }

        IEnumerable<WrappedEvent> FindEventsByCopy (int copyId)
        {
            return repository.GetAllEvents().Where(x => x.Copy.copyId == copyId);
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
            repository.UpdateCopy(borrowedCopy.copyId, borrowedCopy.Book.Id, true, borrowedCopy.Condition);
        }

        public void ReturnCopy (int copyId, int readerId, DateTimeOffset eventDate)
        {
            WrappedCopy returnedCopy = repository.GetCopy(copyId);
            WrappedBorrowing borrowing = FindBorrowingEventsByCopy(copyId).First(x => x.Completed == false);

            repository.AddReturnEvent(copyId, eventDate, readerId, borrowing);
            repository.CompleteBorrowingEvent(borrowing);
            repository.UpdateCopy(returnedCopy.copyId, returnedCopy.Book.Id, false, returnedCopy.Condition);

        }



    }
}
