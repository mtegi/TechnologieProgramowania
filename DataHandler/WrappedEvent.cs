using System;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public enum EventType
    {
        Purchase=1, Destruction=2, Borrowing=3, Return=4
    } 

    public abstract class WrappedEvent
    {
        protected LibEvent libEvent;

        public EventType Type { get; }
        public WrappedCopy Copy { get; }
        public DateTimeOffset EventDate { get; }

       public WrappedEvent(LibEvent libEvent)
        {
            this.libEvent = libEvent;

            this.Type = (EventType)libEvent.EventType;
            this.Copy = new WrappedCopy(libEvent.Copy);
            this.EventDate = libEvent.EventDate;
        }

        internal LibEvent GetEvent()
        {
            return libEvent;
        }
    }
}
