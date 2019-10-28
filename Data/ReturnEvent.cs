using DefinitionLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReturnEvent: LibEvent
    {
        public Reader Reader { get; set; }
        public BorrowingEvent Borrowing { get; set; }

        public  ReturnEvent(Copy copy, DateTimeOffset eventDate, Reader reader, BorrowingEvent borrowing) : base(EventType.Return, copy, eventDate)
        {
            this.Reader = reader;
            this.Borrowing = borrowing;
        }
    }
}
