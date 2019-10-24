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
            data.Copies.Add(copyId, new Copy(copyId, data.Books[0], false, CopyCondition.Mint));

            copyId = 2002;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[0], false, CopyCondition.Good));


            author = new Author("Miłosz", "Liana");
            bookId = 1002;
            data.Books.Add(bookId, new Book(bookId, "Lolita", author,
                new List<LiteraryGenre> { LiteraryGenre.Biography, LiteraryGenre.SciFi }));

            copyId = 2003;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[1], true, CopyCondition.Mint));

            copyId = 2004;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[1], false, CopyCondition.Mint));

            bookId = 1003;
            author = new Author("Maciej", "Granat");
            data.Books.Add(bookId, new Book(bookId, "Kosmiczna Wojna", author,
               new List<LiteraryGenre> { LiteraryGenre.Thriller, LiteraryGenre.SciFi } ));

            copyId = 2005;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[2], false, CopyCondition.Mint));

            copyId = 2006;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[2], true, CopyCondition.NearMint));

            // Dodawanie Czytelnikow

           
            data.Readers.Add(new Reader(3001, "Jakub", "Nowek"));

 
            data.Readers.Add(new Reader(3002, "Aniela", "Rybicka"));


            data.Readers.Add(new Reader(3003, "Robert", "Złotek"));

            // Dodawanie Eventów

            data.Borrowings.Add(new Borrowing(data.Readers[0], data.Copies[3], new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0)),false));
            data.Borrowings.Add(new Borrowing(data.Readers[1], data.Copies[6], new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0)),false));


        }
    }
}
