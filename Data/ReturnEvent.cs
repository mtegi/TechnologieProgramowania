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

        public  ReturnEvent(Copy copy, DateTimeOffset eventDate, Reader reader) : base(4, copy, eventDate)
        {
            this.Reader = reader;
        }
    }
}
