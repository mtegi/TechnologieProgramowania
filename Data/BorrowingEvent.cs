using DefinitionLib;
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
        [XmlIgnore]
        public DateTimeOffset ReturnDate { get; set; }
        [XmlIgnore]
        public Reader Reader { get; set; }
        [XmlIgnore]
        public bool Completed { get; set; }

        
        public BorrowingEvent() { }
        public BorrowingEvent(Reader reader, Copy copy, DateTimeOffset eventDate, DateTimeOffset returnDate) : base(EventType.Borrowing, copy, eventDate)
        {
            this.ReturnDate = returnDate;
            this.Reader = reader;
            this.Completed = false;
        }

    }
}
