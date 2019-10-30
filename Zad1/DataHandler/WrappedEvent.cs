using System;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefinitionLib;

namespace DataHandler
{

    public abstract class WrappedEvent
    {
        protected LibEvent libEvent;

        public EventType Type { get; }
        public WrappedCopy Copy { get; }
        public DateTimeOffset EventDate { get; }

       public WrappedEvent(LibEvent libEvent)
        {
            this.libEvent = libEvent;

            this.Type = libEvent.Type;
            this.Copy = new WrappedCopy(libEvent.Copy);
            this.EventDate = libEvent.EventDate;
        }

        internal LibEvent GetEvent()
        {
            return libEvent;
        }
    }
}
