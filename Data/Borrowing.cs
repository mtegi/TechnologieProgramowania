using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /** Zdarzenie (wypozyczenie) */
    public class Borrowing
    {
        public Borrowing(Reader reader, Copy copy, DateTimeOffset borrowDate, DateTimeOffset returnDate, bool completed)
        {
            this.Reader = reader;
            this.Copy = copy;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            Completed = completed;
        }

        public Reader Reader{ get; }
        public Copy Copy { get; }
        public DateTimeOffset BorrowDate { get; set; }
        public DateTimeOffset ReturnDate { get; set; }
        public bool Completed { get; set; }

    }
}
