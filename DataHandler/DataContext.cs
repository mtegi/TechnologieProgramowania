using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
   public class DataContext
    {

        public DataContext()
        {
            Readers = new List<Reader>();
            Books = new Dictionary<int, Book>(); ;
            Borrowings = new ObservableCollection<Borrowing>();
            Copies = new List<Copy>();
        }

        public List<Reader> Readers { get; }
        public Dictionary<int, Book> Books { get; }
        public ObservableCollection<Borrowing> Borrowings { get; }
        public List<Copy> Copies { get; }

    }
}
