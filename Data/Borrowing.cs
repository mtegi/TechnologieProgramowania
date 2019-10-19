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
        public Borrowing(int readerId, int copyId, DateTimeOffset borrowDate, DateTimeOffset returnDate)
        {
            ReaderId = readerId;
            CopyId = copyId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
        }

        public int ReaderId { get; }
        public int CopyId { get; }
        public DateTimeOffset BorrowDate { get; set; }
        public DateTimeOffset ReturnDate { get; set; }

    }
}
