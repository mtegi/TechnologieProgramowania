using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public interface IDataRepository
    {
        event EventHandler PurchaseHappened;
        event EventHandler DestructionHappened;
        event EventHandler BorrowingHappened;
        event EventHandler ReturnHappened;


        void AddBook(int id, string title, string author, IEnumerable<LiteraryGenre> genres);
         Book GetBook(int id);
         bool ContainsBook(int id);
         IEnumerable<Book> GetAllBooks();
         bool DeleteBook(int id);
         void UpdateBook(int orginalId, string title, string author, List<LiteraryGenre> genres);

         void AddCopy(int copyId, int bookId, CopyCondition condition);
         Copy GetCopy(int id);
         bool ContainsCopy(int id);
         IEnumerable<Copy> GetAllCopies();
         bool DeleteCopy(int id);
         void UpdateCopy(int orginalId, int bookId, bool borrowed, CopyCondition condition);

         void AddReader(int id, string FirstName, string Lastname);
         Reader GetReader(int id);
         bool ContainsReader(int id);
         IEnumerable<Reader> GetAllReaders();
         bool DeleteReader(int id);
         void UpdateReader(int orginalId, string FirstName, string Lastname);

         void AddPurchaseEvent(int copyId, DateTimeOffset eventDate, int price, string distributor);
         void AddDestructionEvent(int copyId, DateTimeOffset eventDate, string reason);
         void AddBorrowingEvent(int copyId, DateTimeOffset eventDate, DateTimeOffset returnDate, int readerId);
         void CompleteBorrowingEvent(BorrowingEvent borrowing);
         void AddReturnEvent(int copyId, DateTimeOffset eventDate, int readerId, BorrowingEvent borrowing);
         IEnumerable<LibEvent> GetAllEvents();


    }
}
