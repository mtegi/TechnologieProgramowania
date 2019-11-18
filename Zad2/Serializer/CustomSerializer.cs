using Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Serializer
{
    public class CustomSerializer
    {

        #region Serialization
        public void Serialize(DataContext data, Stream stream)
        {
            ObjectIDGenerator idGenerator = new ObjectIDGenerator();

            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (Book book in data.Books.Values)
                {
                    long bookId = idGenerator.GetId(book, out bool firstTime);
                    StringBuilder builder = new StringBuilder();
                    builder.Append(bookId);
                    builder.Append(';');
                    builder.Append(book.Id);
                    builder.Append(';');
                    builder.Append(book.Author);
                    builder.Append(';');
                    builder.Append(book.Title);
                    builder.Append(';');
                    foreach (LiteraryGenre genre in book.Genres)
                    {
                        builder.Append('|');
                        builder.Append(genre);
                        builder.Append('|');
                    }
                    writer.WriteLine(builder.ToString());
                }
                writer.WriteLine('#');
                foreach (Reader reader in data.Readers)
                {
                    long readerId = idGenerator.GetId(reader, out bool firstTime);
                    StringBuilder builder = new StringBuilder();
                    builder.Append(readerId);
                    builder.Append(';');
                    builder.Append(reader.Id);
                    builder.Append(';');
                    builder.Append(reader.FirstName);
                    builder.Append(';');
                    builder.Append(reader.LastName);
                    writer.WriteLine(builder.ToString());
                }
                writer.WriteLine('#');
                foreach (Copy copy in data.Copies.Values)
                {
                    long copyId = idGenerator.GetId(copy, out bool firstTime);
                    long bookId = idGenerator.GetId(copy.Book, out firstTime);
                    StringBuilder builder = new StringBuilder();
                    builder.Append(copyId);
                    builder.Append(';');
                    builder.Append(copy.CopyId);
                    builder.Append(';');
                    builder.Append(bookId);
                    builder.Append(';');
                    builder.Append(copy.Borrowed);
                    builder.Append(';');
                    builder.Append(copy.Condition);
                    writer.WriteLine(builder.ToString());
                }
                writer.WriteLine('#');
                foreach (LibEvent libEvent in data.Events)
                {
                    long libEventId = idGenerator.GetId(libEvent, out bool firstTime);
                    long copyId = idGenerator.GetId(libEvent.Copy, out firstTime);

                    StringBuilder builder = new StringBuilder();
                    builder.Append(libEventId);
                    builder.Append(';');
                    builder.Append(libEvent.GetType());
                    builder.Append(';');
                    builder.Append(copyId);
                    builder.Append(';');
                    builder.Append(libEvent.EventDate.ToString());
                    builder.Append(';');
                    if (libEvent is BorrowingEvent)
                    {

                        BorrowingEvent borrowing = libEvent as BorrowingEvent;
                        Int64 readerId = idGenerator.GetId(borrowing.Reader, out firstTime);
                        builder.Append(readerId);
                        builder.Append(';');
                        builder.Append(borrowing.ReturnDate);
                        builder.Append(';');
                        builder.Append(borrowing.Completed);
                    }
                    else if (libEvent is ReturnEvent)
                    {
                        ReturnEvent returnEvent = libEvent as ReturnEvent;
                        Int64 readerId = idGenerator.GetId(returnEvent.Reader, out firstTime);
                        Int64 borrowingId = idGenerator.GetId(returnEvent.Borrowing, out firstTime);
                        builder.Append(borrowingId);
                        builder.Append(';');
                        builder.Append(readerId);
                    }
                    else if (libEvent is PurchaseEvent)
                    {
                        PurchaseEvent purchaseEvent = libEvent as PurchaseEvent;
                        builder.Append(purchaseEvent.Distributor);
                        builder.Append(';');
                        builder.Append(purchaseEvent.Price);
                    }
                    else if (libEvent is DestructionEvent)
                    {
                        DestructionEvent destructionEvent = libEvent as DestructionEvent;
                        builder.Append(destructionEvent.Reason);
                    }
                    writer.WriteLine(builder.ToString());
                }
                writer.WriteLine('#');
            }
        }
        #endregion
        #region Deserialization
        public DataContext Deserialize(Stream stream)
        {
            Dictionary<string, Book> tmpBooks = new Dictionary<string, Book>();
            Dictionary<string, Copy> tmpCopies = new Dictionary<string, Copy>();
            Dictionary<string, Reader> tmpReaders = new Dictionary<string, Reader>();
            Dictionary<string, LibEvent> tmpEvents = new Dictionary<string, LibEvent>();

            Dictionary<int, Book> booksToContext = new Dictionary<int, Book>();
            List<Reader> readersToContext = new List<Reader>();
            Dictionary<int, Copy> copiesToContext = new Dictionary<int, Copy>();
            ObservableCollection<LibEvent> eventsToContext = new ObservableCollection<LibEvent>();

            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                while (!(line = reader.ReadLine()).StartsWith("#"))
                {
                    string[] bookProperties = line.Split(';');
                    string objectId = bookProperties[0];
                    string bookId = bookProperties[1];
                    string author = bookProperties[2];
                    string title = bookProperties[3];
                    string[] genres = bookProperties[4].Split('|');

                    if (tmpBooks.ContainsKey(objectId))
                    {
                        Book deserializedBook = tmpBooks[objectId];
                        booksToContext.Add(deserializedBook.Id, deserializedBook);
                    }
                    else
                    {
                        List<LiteraryGenre> literaryGenres = new List<LiteraryGenre>();
                        foreach (string g in genres)
                        {
                            if (Enum.TryParse<LiteraryGenre>(g, out LiteraryGenre genre))
                                literaryGenres.Add(genre);
                        }
                        Book deserializedBook = new Book(Int32.Parse(bookId), title, author, literaryGenres);
                        tmpBooks.Add(objectId, deserializedBook);
                        booksToContext.Add(deserializedBook.Id, deserializedBook);
                    }
                }
                while (!(line = reader.ReadLine()).StartsWith("#"))
                {
                    string[] readerProps = line.Split(';');
                    string objectId = readerProps[0];
                    string readerId = readerProps[1];
                    string firstName = readerProps[2];
                    string lastName = readerProps[3];
                    if (tmpReaders.ContainsKey(objectId))
                    {
                        Reader deserializedReader = tmpReaders[objectId];
                        readersToContext.Add(deserializedReader);
                    }
                    else
                    {
                        Reader deserializedReader = new Reader(Int32.Parse(readerId), firstName, lastName);
                        tmpReaders.Add(objectId, deserializedReader);
                        readersToContext.Add(deserializedReader);
                    }
                }
                while (!(line = reader.ReadLine()).StartsWith("#"))
                {
                    string[] props = line.Split(';');
                    string objectId = props[0];
                    string copyId = props[1];
                    string bookId = props[2];
                    string borrowed = props[3];
                    string condition = props[4];
                    Enum.TryParse<CopyCondition>(condition, true, out CopyCondition conditionEnum);
                    Copy deserializedCopy = new Copy(int.Parse(copyId), tmpBooks[bookId], conditionEnum, Convert.ToBoolean(borrowed));
                    tmpCopies.Add(objectId, deserializedCopy);
                    copiesToContext.Add(deserializedCopy.CopyId, deserializedCopy);
                }
                while (!(line = reader.ReadLine()).StartsWith("#"))
                {
                    string[] eventProps = line.Split(';');
                    string objectId = eventProps[0];                    
                    Type eventType = Type.GetType(eventProps[1] + ",Library");
                    string copyId = eventProps[2];
                    string eventDate = eventProps[3];
                    LibEvent deserializedEvent = null;
                    if(eventType == typeof(BorrowingEvent))
                    {
                        string readerId = eventProps[4];
                        string returnDate = eventProps[5];
                        string completed = eventProps[6];
                        deserializedEvent = new BorrowingEvent(tmpReaders[readerId], tmpCopies[copyId], Convert.ToDateTime(eventDate), Convert.ToDateTime(returnDate));
                    }
                    else if(eventType == typeof(ReturnEvent))
                    {
                        string borrowingId = eventProps[4];
                        string readerId = eventProps[5];
                        deserializedEvent = new ReturnEvent(tmpCopies[copyId], Convert.ToDateTime(eventDate), tmpReaders[readerId], tmpEvents[borrowingId] as BorrowingEvent);
                    }
                    else if (eventType == typeof(PurchaseEvent))
                    {
                        string distributor = eventProps[4];
                        string price = eventProps[5];
                        deserializedEvent = new PurchaseEvent(tmpCopies[copyId], Convert.ToDateTime(eventDate), double.Parse(price), distributor);
                    }
                    else if (eventType == typeof(DestructionEvent))
                    {
                        string reason = eventProps[4];
                        deserializedEvent = new DestructionEvent(tmpCopies[copyId], Convert.ToDateTime(eventDate), reason);                 
                    }
                    tmpEvents.Add(objectId, deserializedEvent);
                    eventsToContext.Add(deserializedEvent);

                }
                    return new DataContext(readersToContext, booksToContext, eventsToContext, copiesToContext);
            }
        }
            #endregion
    }
}