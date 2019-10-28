using DefinitionLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public abstract class LibEvent
    {

        public  EventType Type { get;  }
        public Copy Copy { get; set; }
        public DateTimeOffset EventDate { get; set; }

        public LibEvent ( EventType eventType, Copy copy, DateTimeOffset eventDate)
        {
            this.Type = eventType;
            this.Copy = copy;
            this.EventDate = eventDate; 
        }
    }
}
