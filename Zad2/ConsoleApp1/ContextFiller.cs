using Library;
using System;
using System.Collections.Generic;

namespace Filler
{
    public class ContextFiller : IDataProvider
    {
        public void Fill(DataContext data)
        {

            List<Book> books = new List<Book> {
                 new Book(1, "Wydra", "Jan Lasica", new LiteraryGenre[] { LiteraryGenre.Fatansy }),
                 new Book(2, "Lolita", "Milosz Liana", new LiteraryGenre[] { LiteraryGenre.Romance }),
                 new Book(3, "Kosmiczna Wojna", "Maciej Granat", new LiteraryGenre[] { LiteraryGenre.SciFi })
            };

            List<Reader> readers = new List<Reader> {
                new Reader(1, "Jakub", "Nowek"),
                new Reader(2, "Aniela", "Rybicka"),
                new Reader(3, "Robert", "Złotek"),
            };

            List<Copy> copies = new List<Copy>
            {
                new Copy(1, books[0], CopyCondition.HeavlyDamaged),
                new Copy(2, books[0], CopyCondition.Damaged),
                new Copy(3,books[1], CopyCondition.Poor),
                new Copy(4,books[1],CopyCondition.Good),
                new Copy(5,books[2],CopyCondition.NearMint),
                new Copy(6,books[2],CopyCondition.Mint),
            };

            foreach (Book b in books)
            {
                data.Books.Add(b.Id, b);
            }

            foreach (Reader r in readers)
            {
                data.Readers.Add(r);
            }
            foreach (Copy c in copies)
            {
                data.Copies.Add(c.CopyId, c);
            }

            // Dodawanie Eventów
            BorrowingEvent borrowing = new BorrowingEvent(data.Readers[2],data.Copies[4], new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0)));
            data.Events.Add(new BorrowingEvent(data.Readers[0], data.Copies[3], new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0))));
            data.Copies[3].Borrowed = true;
            data.Events.Add(new BorrowingEvent(data.Readers[1], data.Copies[6], new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0))));
            data.Events.Add(borrowing);
            data.Events.Add(new ReturnEvent(data.Copies[4], new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0)), data.Readers[2], borrowing));           
            data.Copies[6].Borrowed = true;

        }
    }
}
