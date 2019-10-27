using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DestructionEvent:LibEvent
    {
        public string Reason { get; set; }

       public DestructionEvent(Copy copy, DateTimeOffset eventDate, string reason) : base(2, copy, eventDate)
        {
            this.Reason = reason;
        }
    }
}

