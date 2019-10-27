using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public abstract class LibEvent
    {
        private static int eventTypesNumber = 4;

        public int EventType { get;  }
        public Copy Copy { get; set; }
        public DateTimeOffset EventDate { get; set; }

        public LibEvent ( int eventType, Copy copy, DateTimeOffset eventDate)
        {
            if (eventType > eventTypesNumber || eventType < 1)
                throw new ArgumentException("eventType poza dozwolonymi wartościami");
            this.EventType = eventType;
            this.Copy = copy;
            this.EventDate = eventDate; 
        }
    }
}
