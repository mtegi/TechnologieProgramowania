using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler;
using Data;

namespace DataCreator
{
   public class ContextFiller : IDataProvider
    {
        public void Fill(DataContext data)
        {
            Author author;
            
            author = new Author("Jan", "Lasica");
            int bookId = 1001;

            data.Books.Add(bookId, new Book(bookId, "Wydra", author,
                new List<LiteraryGenre> {LiteraryGenre.Comedy, LiteraryGenre.Fantasy}));

            int copyId = 2001;
            data.Copies.Add(copyId, new Copy(copyId, bookId, false, CopyCondition.Mint));

            copyId = 2002;
            data.Copies.Add(copyId, new Copy(copyId, bookId, false, CopyCondition.Good));


            author = new Author("Miłosz", "Liana");
            bookId = 1002;
            data.Books.Add(bookId, new Book(bookId, "Lolita", author,
                new List<LiteraryGenre> { LiteraryGenre.Biography, LiteraryGenre.SciFi }));

            copyId = 2003;
            data.Copies.Add(copyId, new Copy(copyId, bookId, true, CopyCondition.Mint));

            copyId = 2004;
            data.Copies.Add(copyId, new Copy(copyId, bookId, false, CopyCondition.Mint));

            bookId = 1003;
            author = new Author("Maciej", "Granat");
            data.Books.Add(bookId, new Book(bookId, "Kosmiczna Wojna", author,
               new List<LiteraryGenre> { LiteraryGenre.Thriller, LiteraryGenre.SciFi } ));

            copyId = 2005;
            data.Copies.Add(copyId, new Copy(copyId, bookId, false, CopyCondition.Mint));

            copyId = 2006;
            data.Copies.Add(copyId, new Copy(copyId, bookId, true, CopyCondition.NearMint));

            // Dodawanie Czytelnikow

           
            data.Readers.Add(new Reader(3001, "Jakub", "Nowek"));

 
            data.Readers.Add(new Reader(3002, "Aniela", "Rybicka"));


            data.Readers.Add(new Reader(3003, "Robert", "Złotek"));

            // Dodawanie Eventów

            data.Borrowings.Add(new Borrowing(3001, 2003, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0)),false));
            data.Borrowings.Add(new Borrowing(3002, 2006, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0)),false));


        }
    }
}
