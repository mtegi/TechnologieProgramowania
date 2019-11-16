using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library
{
    [Serializable]
    public abstract class LibEvent
    {
          
        public Copy Copy { get; set; }
        
        public DateTimeOffset EventDate { get; set; }

        public LibEvent (Copy copy, DateTimeOffset eventDate)
        {
            this.Copy = copy;
            this.EventDate = eventDate; 
        }

        public LibEvent() { }
    }
}
