using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    class DataRepository
    {
        private DataContext _data;

        public DataRepository(IDataProvider dataProvider) // Dependency Injection w kontruktorze
        {
            _data = new DataContext();
            dataProvider.Fill(_data);
        }


        public void AddBook(Book book)
        {
            if (_data.Books.ContainsKey(book.Id))
                throw new ArgumentException("Książka o danym ID już znajduje się w katalogu");
            else
            _data.Books.Add(book.Id, book);
        }

        public Book GetBook(int bookID)
        {
            return _data.Books[bookID];
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _data.Books.Values;
        }

        public void UpdateBook(int orginalId, Book newBook)
        {


            if (!_data.Books.ContainsKey(orginalId))
                throw new ArgumentException("Próba aktualizacji książki, której nie ma w katalogu");
            else
            {
                _data.Books.Remove(orginalId);
                _data.Books.Add(newBook.Id, newBook);
            }
        }

        public void UpdateBook(Book orginalBook, Book newBook)
        { 
            if (!_data.Books.ContainsValue(orginalBook))
                throw new ArgumentException("Próba aktualizacji książki, której nie ma w katalogu");
            else
            {
                _data.Books.Remove(orginalBook.Id);
                _data.Books.Add(newBook.Id, newBook);
            }

        }

        public List<Book> FindBooks(string title)
        {
            List<Book> result = new List<Book>();
            foreach (Book entry in _data.Books.Values)
            {
                if (String.Equals(entry.Title, title, StringComparison.CurrentCultureIgnoreCase))
                    result.Add(entry);
            }
            return result;
        }

        public void DeleteBook(Book book)
        {

            //TODO: sprawdzanie czy mozna, wyjatki
            _data.Books.Remove(book.Id);
        }

        public void DeleteBook(int bookID)
        {
            _data.Books.Remove(bookID);
        }

        public void AddReader(Reader reader)
        {
            if(_data.Readers.Any(i => i.Id==reader.Id))
                throw new ArgumentException("Czytelnik o danym ID już znajduje się w wykazie");
            else
            _data.Readers.Add(reader);
        }

        public Reader GetReader(int id)
        {
            return _data.Readers.Find(x => x.Id == id);
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            return _data.Readers;
        }

        public void DeleteReader(Reader reader)
        {
            //TODO: sprawdzic czy klient ma wypozyczenie bo inaczej nie mozna go usunac
            _data.Readers.Remove(reader);
        }

        public void DeleteReader(int readerId)
        {
            _data.Readers.RemoveAll(i => i.Id == readerId);
        }

        public void UpdateReader(int originalId, Reader reader)
        {
            if (!_data.Readers.Any(i => i.Id == reader.Id))
                throw new ArgumentException("Próba aktualizacji czytelnika, którego nie ma w bazie");
            else
            {
                _data.Readers.RemoveAll(i => i.Id == originalId);
                _data.Readers.Add(reader);
            }
        }

        public void AddCopy(Copy copy)
        {
            if (_data.Copies.ContainsKey(copy.CopyId))
                throw new ArgumentException("Książka o danym ID już znajduje się w katalogu");
            else
                _data.Copies.Add(copy.CopyId, copy);
        }

        public Copy GetCopy(int copyID)
        {
            return _data.Copies[copyID];
        }

        public IEnumerable<Copy> GetAllCopies()
        {
            return _data.Copies.Values;
        }

        public void UpdateCopy(int orginalId, Copy newCopy)
        {


            if (!_data.Copies.ContainsKey(orginalId))
                throw new ArgumentException("Próba aktualizacji książki, której nie ma w katalogu");
            else
            {
                _data.Copies.Remove(orginalId);
                _data.Copies.Add(newCopy.CopyId, newCopy);
            }
        }

        public void UpdateCopy(Copy orginalCopy, Copy newCopy)
        {
            if (!_data.Copies.ContainsValue(orginalCopy))
                throw new ArgumentException("Próba aktualizacji książki, której nie ma w katalogu");
            else
            {
                _data.Copies.Remove(orginalCopy.CopyId);
                _data.Copies.Add(newCopy.CopyId, newCopy);
            }

        }


        public void DeleteCopy(Copy copy)
        {
            _data.Copies.Remove(copy.CopyId);
        }

        public void DeleteCopy(int copyID)
        {
            _data.Copies.Remove(copyID);
        }

        public void AddBorrowing(Borrowing borrowing)
        {
           Copy tempCopy = GetCopy(borrowing.CopyId);
            if (tempCopy.Borrowed)
                throw new ArgumentException("Dana książka jest już wypożyczona");
            else
            {
               tempCopy.Borrowed = true;
              _data.Borrowings.Add(borrowing);
            }
         
        }

        // Wypożyczenia danego czytelnika
        public IEnumerable<Borrowing> GetReaderBorrowings(int readerId)
        {
            return _data.Borrowings.Where(x => x.ReaderId == readerId);
        }

        // Wypożyczenia danego czytelnika
        public IEnumerable<Borrowing> GetCopyBorrowings(int copyId)
        {
            return _data.Borrowings.Where(x => x.CopyId == copyId);
        }

        public IEnumerable<Borrowing> GetCopyReaderBorrowings(int copyId, int readerId)
        {
            return _data.Borrowings.Where(x => x.CopyId == copyId && x.ReaderId ==readerId);
        }

        public IEnumerable<Borrowing> GetAllBorrowings()
        {
            return _data.Borrowings;
        }

        //Metoda bezpieczna, zapewniająca spójność danych wypożyczanych książek
        public void DeleteBorrowing(Borrowing borrowing)
        {
            if (!borrowing.Completed)
                throw new InvalidOperationException("Nie mozna usunąć wypożyczenia w trakcie jego trwania!");
            else
            _data.Borrowings.Remove(borrowing);
        }
        
        // Metoda bezpośrednia, pozwalająca na usuwanie błędnych wpisów
        public void ForceDeleteBorrwing (Borrowing borrowing)
        {
            _data.Borrowings.Remove(borrowing);
        }

        public void UpdateBorrowing(Borrowing orginalBorrowing, Borrowing newBorrowing)
        {
            if (!_data.Borrowings.Contains(orginalBorrowing))
                throw new ArgumentException("Próba aktualizacji wypożyczenia które nie istnieje");
            else
            {
                _data.Borrowings.Remove(orginalBorrowing);
                _data.Borrowings.Add(newBorrowing);
            }
                
        }

        public void CompleteBorrowing(Borrowing borrowing)
        {
            if (!_data.Borrowings.Contains(borrowing))
                throw new ArgumentException("Próba zakończenia wypożyczenia które nie istnieje w bazie danych");
            if (borrowing.Completed)
                throw new ArgumentException("Próba zakończenia wypożyczenia które jest już zakończone");
            if (!_data.Copies.ContainsKey(borrowing.CopyId))
                throw new ArgumentException("Próba zakończenia wypożyczenia egzemplarza, którego nie ma w bazie");
            if (!_data.Copies[borrowing.CopyId].Borrowed)
                throw new ArgumentException("Próba zakończenia wypożyczenia egzemplarza, który nie jest aktualnie wypożyczony");
            else
            {
                borrowing.Completed = true;
                _data.Copies[borrowing.CopyId].Borrowed = false; 
            }
        }






    }
}
