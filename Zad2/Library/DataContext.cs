using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
   [Serializable]
   public class DataContext
    {
        public DataContext(DataContext context)
        {
            Readers = new List<Reader>(context.Readers);
            Books = new Dictionary<int, Book>(context.Books); ;
            Events = new ObservableCollection<LibEvent>(context.Events);
            Copies = new Dictionary<int, Copy>(context.Copies);
        }

        public DataContext()
        {
            Readers = new List<Reader>();
            Books = new Dictionary<int, Book>(); ;
            Events = new ObservableCollection<LibEvent>();
            Copies = new Dictionary<int,Copy>();
        }

        public DataContext(List<Reader> readers, Dictionary<int, Book> books, ObservableCollection<LibEvent> events, Dictionary<int, Copy> copies)
        {
            Readers = readers ?? throw new ArgumentNullException(nameof(readers));
            Books = books ?? throw new ArgumentNullException(nameof(books));
            Events = events ?? throw new ArgumentNullException(nameof(events));
            Copies = copies ?? throw new ArgumentNullException(nameof(copies));
        }

        public List<Reader> Readers { get; }
        public Dictionary<int, Book> Books { get; }
        public ObservableCollection<LibEvent> Events { get; }
        public Dictionary<int,Copy> Copies { get; }

    }
}
