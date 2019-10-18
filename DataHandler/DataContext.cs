using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    class DataContext
    {

        public DataContext()
        {
            Readers = new List<Reader>();
            Books = new Dictionary<int, Book>(); ;
            Borrowings = new ObservableCollection<Borrowing>();
            States = new List<State>();
        }

        public List<Reader> Readers { get; }
        public Dictionary<int, Book> Books { get; }
        public ObservableCollection<Borrowing> Borrowings { get; }
        public List<State> States { get; }
    }
}
