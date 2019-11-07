using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data
{
   public class BorrowingEvent:LibEvent
    {
        
        public DateTimeOffset ReturnDate { get; set; }
        
        public Reader Reader { get; set; }
        
        public bool Completed { get; set; }

        
        public BorrowingEvent() { }
        public BorrowingEvent(Reader reader, Copy copy, DateTimeOffset eventDate, DateTimeOffset returnDate) : base(copy, eventDate)
        {
            this.ReturnDate = returnDate;
            this.Reader = reader;
            this.Completed = false;
        }

    }
}
