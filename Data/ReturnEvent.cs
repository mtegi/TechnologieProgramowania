using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReturnEvent: LibEvent
    {
        public BorrowingEvent Borrowing { get; set; }
        public Reader Reader { get; set; }

        public  ReturnEvent(Copy copy, DateTimeOffset eventDate, BorrowingEvent borrowing, Reader reader) : base(4, copy, eventDate)
        {
            this.Borrowing = borrowing;
            this.Reader = reader;
        }
    }
}
