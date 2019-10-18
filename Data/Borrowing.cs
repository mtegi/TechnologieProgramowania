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
        public Borrowing(int readerId, int stateId, DateTimeOffset borrowDate, DateTimeOffset returnDate)
        {
            ReaderId = readerId;
            StateId = stateId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
        }

        public int ReaderId { get; }
        public int StateId { get; }
        public DateTimeOffset BorrowDate { get; set; }
        public DateTimeOffset ReturnDate { get; set; }

    }
}
