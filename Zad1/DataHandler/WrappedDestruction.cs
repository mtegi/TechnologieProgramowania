using System;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public class WrappedDestruction : WrappedEvent
    {
     
        public string Reason { get;  }

        public WrappedDestruction (DestructionEvent destruction) :base (destruction)
        { 
            Reason = destruction.Reason;
        }
    }
}
