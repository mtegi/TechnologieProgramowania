using Data;
using DefinitionLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
   public interface IDataRepository
    {
        event EventHandler PurchaseHappened;
        event EventHandler DestructionHappened;
        event EventHandler BorrowingHappened;
        event EventHandler ReturnHappened;


        void AddBook(int id, string title, string author, IEnumerable<LiteraryGenre> genres);
         WrappedBook GetBook(int id);
         bool ContainsBook(int id);
         IEnumerable<WrappedBook> GetAllBooks();
         bool DeleteBook(int id);
         void UpdateBook(int orginalId, string title, string author, List<LiteraryGenre> genres);

         void AddCopy(int copyId, int bookId, CopyCondition condition);
         WrappedCopy GetCopy(int id);
         bool ContainsCopy(int id);
         IEnumerable<WrappedCopy> GetAllCopies();
         bool DeleteCopy(int id);
         void UpdateCopy(int orginalId, int bookId, bool borrowed, CopyCondition condition);

         void AddReader(int id, string FirstName, string Lastname);
         WrappedReader GetReader(int id);
         bool ContainsReader(int id);
         IEnumerable<WrappedReader> GetAllReaders();
         bool DeleteReader(int id);
         void UpdateReader(int orginalId, string FirstName, string Lastname);

         void AddPurchaseEvent(int copyId, DateTimeOffset eventDate, int price, string distributor);
         void AddDestructionEvent(int copyId, DateTimeOffset eventDate, string reason);
         void AddBorrowingEvent(int copyId, DateTimeOffset eventDate, DateTimeOffset returnDate, int readerId);
         void CompleteBorrowingEvent(WrappedBorrowing borrowing);
         void AddReturnEvent(int copyId, DateTimeOffset eventDate, int readerId, WrappedBorrowing borrowing);
         IEnumerable<WrappedEvent> GetAllEvents();


    }
}
