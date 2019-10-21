using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler;
using Data;

namespace DataCreator
{
    class ContextFiller : IDataProvider
    {
        FillIdManager idManager;
        public void Fill(DataContext data)
        {
            //Dodawanie ksiązek i egzemplarzy
            int id;
            Author author;
            idManager = new FillIdManager();
            
            id = idManager.GenerateBookId();
            author = new Author("Jan", "Lasica");
            data.Books.Add(id, new Book(id, "Wydra", author,
                new List<LiteraryGenre> {LiteraryGenre.Comedy, LiteraryGenre.Fantasy}));

            data.Copies.Add(new Copy(idManager.GenerateCopyId(), id, false, CopyCondition.Mint));
            data.Copies.Add(new Copy(idManager.GenerateCopyId(), id, false, CopyCondition.Good));



            id = idManager.GenerateBookId();
            author = new Author("Miłosz", "Liana");
            data.Books.Add(id, new Book(id, "Lolita", author,
                new List<LiteraryGenre> { LiteraryGenre.Biography, LiteraryGenre.SciFi }));

            data.Copies.Add(new Copy(idManager.GenerateCopyId(), id, false, CopyCondition.Mint));
            data.Copies.Add(new Copy(idManager.GenerateCopyId(), id, false, CopyCondition.Poor));


            id = idManager.GenerateBookId();
            author = new Author("Maciej", "Granat");
            data.Books.Add(id, new Book(id, "Kosmiczna Wojna", author,
               new List<LiteraryGenre> { LiteraryGenre.Thriller, LiteraryGenre.SciFi } ));

            data.Copies.Add(new Copy(idManager.GenerateCopyId(), id, false, CopyCondition.Mint));
            data.Copies.Add(new Copy(idManager.GenerateCopyId(), id, true, CopyCondition.NearMint));

            // Dodawanie Czytelnikow

            id = idManager.GenerateReaderId();
            data.Readers.Add(new Reader(id, "Jakub", "Nowek"));

            id = idManager.GenerateReaderId();
            data.Readers.Add(new Reader(id, "Aniela", "Rybicka"));

            id = idManager.GenerateReaderId();
            data.Readers.Add(new Reader(id, "Robert", "Złotek"));

            // Dodawanie Eventów

            data.Borrowings.Add(new Borrowing(idManager.getLastReaderId(), idManager.getLastCopyId(), new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0))));
            data.Borrowings.Add(new Borrowing(idManager.getLastReaderId()-1, idManager.getLastCopyId()-3, new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0))));


        }
    }
}
