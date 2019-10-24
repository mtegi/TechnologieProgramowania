using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    interface IDataRepository
    {
         void AddBook(int id, string authorFirstName, string authorLastName, string title);
         Book GetBook(int id);
         bool ContainsBook(int id);
         IEnumerable<Book> GetAllBooks();
         void DeleteBook(Book book);
         void DeleteBook(int id);
         void UpdateBook(int orginalId, Book newBook);
         void UpdateBook(Book orginalBook, Book newBook);

         void AddCopy(Copy copy);
         Copy GetCopy(int id);
         bool ContainsCopy(int id);
         IEnumerable<Copy> GetAllCopies();
         void DeleteCopy(Copy copy);
         void DeleteCopy(int id);
         void UpdateCopy(int orginalId, Copy newCopy);
         void UpdateCopy(Copy orginalCopy, Copy newCopy);

         void AddReader(Reader reader);
         Reader GetReader(int id);
         bool ContainsReader(int id);
         IEnumerable<Reader> GetAllReaders();
         void DeleteReader(Reader reader);
         void DeleteReader(int id);
         void UpdateReader(int orginalId, Reader newReader);
         void UpdateReader(Reader orginalReader, Reader newReader);

         void AddBorrowing(Borrowing borrowing);
         IEnumerable<Borrowing> GetAllBorrowings();
         void DeleteBorrowing(Borrowing borrowing);
         void UpdateBorrowing(Borrowing orginalBorrowing, Borrowing newBorrowing);
    }
}
