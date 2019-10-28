using System;
using DefinitionLib;
using System.Collections.Generic;
using DataHandler;
using Data;

namespace DataCreator
{
    public class ContextFiller : IDataProvider
    {
        public void Fill(DataContext data)
        {
          
            int bookId = 1001;

            data.Books.Add(bookId, new Book(bookId, "Wydra", "Jan Lasica", "przygodowa"));

            int copyId = 2001;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[0], CopyCondition.HeavlyDamaged));

            copyId = 2002;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[0],  CopyCondition.Damaged));

            bookId = 1002;
            data.Books.Add(bookId, new Book(bookId, "Lolita", "Milosz Liana", "biografia"));

            copyId = 2003;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[1], CopyCondition.Poor));

            copyId = 2004;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[1], CopyCondition.Good));

            bookId = 1003;
            data.Books.Add(bookId, new Book(bookId, "Kosmiczna Wojna", "Maciej Granat", "S-F"));

            copyId = 2005;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[2], CopyCondition.NearMint));

            copyId = 2006;
            data.Copies.Add(copyId, new Copy(copyId, data.Books[2], CopyCondition.Mint));

            // Dodawanie Czytelnikow

           
            data.Readers.Add(new Reader(3001, "Jakub", "Nowek"));

 
            data.Readers.Add(new Reader(3002, "Aniela", "Rybicka"));


            data.Readers.Add(new Reader(3003, "Robert", "Złotek"));

            // Dodawanie Eventów

            data.Events.Add(new BorrowingEvent(data.Readers[0], data.Copies[3], new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0))));
            data.Events.Add(new BorrowingEvent(data.Readers[1], data.Copies[6], new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0))));


        }
    }
}
