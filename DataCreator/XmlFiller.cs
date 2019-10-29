using Data;
using DataHandler;
using DefinitionLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataCreator
{
   public class XmlFiller : IDataProvider
    {
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
            } catch (FileNotFoundException)
            {
                WriteFile();
            }
            deserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Book>));
            List<Book> books = (List<Book>) deserializer.Deserialize(bookFile);
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

           /* StreamReader eventsFile = new StreamReader("Events.xml");
            deserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<BorrowingEvent>));
            List<BorrowingEvent> events = (List<BorrowingEvent>)deserializer.Deserialize(eventsFile);
            eventsFile.Close();
            foreach (BorrowingEvent e in events)
            {
                data.Events.Add(e);
            }*/
        }

        private void WriteFile()
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
                new Copy(3, books[1], CopyCondition.Poor),
                new Copy(4, books[1], CopyCondition.Good),
                new Copy(5, books[2], CopyCondition.NearMint),
                new Copy(6, books[2], CopyCondition.Mint),
            };

            /*List<BorrowingEvent> events = new List<BorrowingEvent>
            {
                new BorrowingEvent(readers[1], copies[6], new DateTimeOffset(2019, 10, 19, 22, 0, 0, new TimeSpan(2, 0, 0)), new DateTimeOffset(2019, 10, 29, 22, 0, 0, new TimeSpan(2, 0, 0))),
            };*/

            Serialize(new StreamWriter("Books.xml"), books);
            Serialize(new StreamWriter("Readers.xml"), readers);
            Serialize(new StreamWriter("Copies.xml"), copies);
            //Serialize(new StreamWriter("Events.xml"), events);


        }
        private void Serialize(StreamWriter file, object o)
        {
           XmlSerializer writer = new XmlSerializer(o.GetType());
            writer.Serialize(file, o);
            file.Close();
        }
    }

}
