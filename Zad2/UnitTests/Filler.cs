using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UnitTests
{
   public class Filler : IDataProvider
    {
        
        private Random random = new Random();
        
        public void Fill(DataContext data)
        {
            StreamReader bookFile = null;
            StreamReader readerFile = null;
            StreamReader copiesFile = null;
            System.Xml.Serialization.XmlSerializer deserializer;
            try
            {
                bookFile = new StreamReader("Books.xml");
                readerFile = new StreamReader("Readers.xml");
                copiesFile = new StreamReader("Copies.xml");
            }
            catch (FileNotFoundException)
            {
                WriteFile();

                bookFile = new StreamReader("Books.xml");
                readerFile = new StreamReader("Readers.xml");
                copiesFile = new StreamReader("Copies.xml");
            }
 
                deserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Book>));
                List<Book> books = (List<Book>)deserializer.Deserialize(bookFile);
                bookFile.Close();

                deserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Reader>));
                List<Reader> readers = (List<Reader>)deserializer.Deserialize(readerFile);
                readerFile.Close();

                deserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Copy>));
                List<Copy> copies = (List<Copy>)deserializer.Deserialize(copiesFile);
                copiesFile.Close();




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

                SortedSet<int> rndCopyIndex = new SortedSet<int>();
                while (rndCopyIndex.Count < 20)
                {
                    rndCopyIndex.Add(random.Next(copies.Count));
                }
                for (int i = 0; i <= random.Next(1, 21); i++)
                {
                    Reader randomReader = readers[random.Next(readers.Count)];
                    Copy randomCopy = copies[rndCopyIndex.ElementAt(i)];
                    DateTime randDate = GetRandomDate(new DateTime(2019, 10, 1), new DateTime(2019, 12, 31));
                    DateTime randDate2 = GetRandomDate(randDate, randDate.AddDays(100));
                    data.Events.Add(new BorrowingEvent(randomReader, randomCopy, new DateTimeOffset(randDate), new DateTimeOffset(randDate2)));
                    data.Copies[randomCopy.CopyId].Borrowed = true;
                }
            
        }


        private void WriteFile()
        {
            List<Book> books = new List<Book> {
                 new Book(1, "Wydra", "Jan Lasica", new LiteraryGenre[] { LiteraryGenre.Fatansy }),
                 new Book(2, "Lolita", "Milosz Liana", new LiteraryGenre[] { LiteraryGenre.Romance }),
                 new Book(3, "Kosmiczna Wojna", "Maciej Granat", new LiteraryGenre[] { LiteraryGenre.SciFi }),
                 new Book(4, "Chrzest Ognia", "Andrzej Sapkowski", new LiteraryGenre[] { LiteraryGenre.Fatansy }),
                 new Book(5, "Pani Jeziora", "Andrzej Sapkowski", new LiteraryGenre[] { LiteraryGenre.Fatansy }),
                 new Book(6, "Miecz Przeznaczenia", "Andrzej Sapkowski", new LiteraryGenre[] { LiteraryGenre.Fatansy }),
                 new Book(7, "Sezon Burz", "Andrzej Sapkowski", new LiteraryGenre[] { LiteraryGenre.Fatansy }),
                 new Book(8, "Mindhunter", "John Douglas", new LiteraryGenre[] { LiteraryGenre.Biography }),
                 new Book(9, "Ostatnie Krolestwo", "Bernard Cornwell", new LiteraryGenre[] { LiteraryGenre.Novel}),
                 new Book(10, "To", "Stephen King", new LiteraryGenre[] { LiteraryGenre.Horror})
            };

            List<Reader> readers = new List<Reader> {
                new Reader(1, "Jakub", "Nowek"),
                new Reader(2, "Aniela", "Rybicka"),
                new Reader(3, "Robert", "Złotek"),
                new Reader(4, "Mirosław", "Złotek"),
                new Reader(5, "Mirosław", "Kowalski"),
                new Reader(6, "Jan", "Kowalski"),
                new Reader(7, "Anna", "Kowalska"),
                new Reader(8, "Hanna", "Kowalska"),
                new Reader(9, "Iwona", "Nowak"),
                new Reader(10, "Iwona", "Buk"),
                new Reader(11, "Jakub", "Tkacz"),
                new Reader(12, "Aniela", "Jakubowska"),
                new Reader(13, "Robert", "Karol"),
                new Reader(14, "Karol", "Karol"),
                new Reader(15, "Mirosław", "Karol"),
                new Reader(16, "Jan", "Kowalski"),
                new Reader(17, "Anna", "Jakubowska"),
                new Reader(18, "Hanna", "Jakubowska"),
                new Reader(19, "Iwona", "Lubicz"),
                new Reader(20, "Gniewoj", "Buk"),
            };

            List<CopyCondition> copyConditions = new List<CopyCondition>
            {
                CopyCondition.Damaged,
                CopyCondition.Good,
                CopyCondition.HeavlyDamaged,
                CopyCondition.Mint,
                CopyCondition.NearMint,
                CopyCondition.Poor
            };

            List<Copy> copies = new List<Copy>();
            for (int i = 1; i<= 20; i++)
                copies.Add(new Copy(i, books[random.Next(books.Count)], copyConditions[random.Next(copyConditions.Count)]));

            Serialize(new StreamWriter("Books.xml"), books);
            Serialize(new StreamWriter("Readers.xml"), readers);
            Serialize(new StreamWriter("Copies.xml"), copies);
        }
        private void Serialize(StreamWriter file, object o)
        {
           XmlSerializer writer = new XmlSerializer(o.GetType());
            writer.Serialize(file, o);
            file.Close();
        }

        private DateTime GetRandomDate(DateTime from, DateTime to)
        {
            TimeSpan range = to - from;
            TimeSpan randTimeSpan = new TimeSpan((long)(random.NextDouble() * range.Ticks));
            return from + randTimeSpan;
        }
    }

}
