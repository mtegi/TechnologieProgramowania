using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    class DataService
    {
        private IDataRepository repository;

        public DataService (IDataRepository repository)
        {
            this.repository = repository;
        }

        public void CreateReader(int id, string firstName, string lastName)
        {
            repository.AddReader(new Reader(id, firstName, lastName));
        }

        public void CreateBook()


       IEnumerable<Book> FindBooksByTitle(string title)
        {
            List<Book> result = new List<Book>();
            foreach (Book entry in repository.GetAllBooks())
            {
                if (String.Equals(entry.Title, title, StringComparison.CurrentCultureIgnoreCase))
                    result.Add(entry);
            }
            return result;
        }

        IEnumerable<Book> FindBooksByAuthor(string LastName)
        {
            List<Book> result = new List<Book>();
            foreach (Book entry in repository.GetAllBooks())
            {
                if (String.Equals(entry.Author.LastName, LastName, StringComparison.CurrentCultureIgnoreCase))
                    result.Add(entry);
            }
            return result;
        }

        IEnumerable<Book> FindBooksByAuthor(string LastName, string FirstName)
        {
            List<Book> result = new List<Book>();
            foreach (Book entry in repository.GetAllBooks())
            {
                if (String.Equals(entry.Author.LastName, LastName, StringComparison.CurrentCultureIgnoreCase) && String.Equals(entry.Author.FirstName, FirstName, StringComparison.CurrentCultureIgnoreCase))
                    result.Add(entry);
            }
            return result;
        }



        List<Reader> GetAlphabeticalReadersList()
        {
            List<Reader> result = repository.GetAllReaders().ToList();
            result.Sort();
            return result;
        }



        // Wypożyczenia danego czytelnika
        public IEnumerable<Borrowing> GetReaderBorrowings(int readerId)
        {
            return repository.GetAllBorrowings().Where(x => x.ReaderId == readerId);
        }

        // Wypożyczenia danego czytelnika
        public IEnumerable<Borrowing> GetCopyBorrowings(int copyId)
        {
            return repository.GetAllBorrowings().Where(x => x.CopyId == copyId);
        }

        public IEnumerable<Borrowing> GetCopyReaderBorrowings(int copyId, int readerId)
        {
            return repository.GetAllBorrowings().Where(x => x.CopyId == copyId && x.ReaderId == readerId);
        }

        public void CreateBorrowing(int copyId, int readerId,  DateTimeOffset BorrowDate, DateTimeOffset ReturnDate)
        {
            if (!repository.ContainsCopy(copyId))
                throw new ArgumentException("Podano bledne id egzemplarza");
            if (repository.GetCopy(copyId).Borrowed)
                throw new ArgumentException("Dany egzemplarz jest aktualnie wypożyczony");
            if (!repository.ContainsReader(readerId))
                throw new ArgumentException("Podano bledne id czytelnika");
            else
            repository.AddBorrowing(new Borrowing(copyId, readerId, BorrowDate, ReturnDate, false));
         
        }

        public void CompleteBorrowing (int copyId, int readerId)
        {
            if (!repository.ContainsCopy(copyId))
                throw new ArgumentException("Podano bledne id egzemplarza");
            if (!repository.ContainsReader(readerId))
                throw new ArgumentException("Podano bledne id czytelnika");
            Borrowing borrowing = GetCopyReaderBorrowings(copyId, readerId).FirstOrDefault(x => x.Completed == false);

            if (borrowing == null)
                throw new ArgumentException("Dla podanego zestawu danych nie istnieje aktywne wypozyczenie");
            else
            {
                borrowing.Completed = true;
                repository.GetCopy(borrowing.CopyId).Borrowed = false;
            }
        }


    }
}
