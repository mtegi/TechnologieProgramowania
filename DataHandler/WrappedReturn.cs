using System;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public class WrappedReturn:WrappedEvent
    {
        public WrappedBorrowing BorrowingEvent { get; }
        public WrappedReader Reader { get; }

       public WrappedReturn(ReturnEvent returnEvent) : base (returnEvent)
        {
            this.BorrowingEvent = new WrappedBorrowing(returnEvent.Borrowing);
            this.Reader = new WrappedReader(returnEvent.Reader);
        }

    }
}
