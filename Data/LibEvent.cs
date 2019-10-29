using DefinitionLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data
{
    public abstract class LibEvent
    {
        [XmlIgnore]
        public  EventType Type { get;  }
        [XmlIgnore]
        public Copy Copy { get; set; }
        [XmlIgnore]
        public DateTimeOffset EventDate { get; set; }

        public LibEvent ( EventType eventType, Copy copy, DateTimeOffset eventDate)
        {
            this.Type = eventType;
            this.Copy = copy;
            this.EventDate = eventDate; 
        }

        public LibEvent() { }
    }
}
