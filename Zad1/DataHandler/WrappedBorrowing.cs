using System;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public class WrappedBorrowing:WrappedEvent
    {
   
        public WrappedReader  Reader {get;}
        public DateTimeOffset ReturnDate { get; }
        public bool Completed { get; }


       public WrappedBorrowing (BorrowingEvent borrowing):base(borrowing)
        {
            this.ReturnDate = borrowing.ReturnDate;
            this.Reader = new WrappedReader(borrowing.Reader);
            this.Completed = borrowing.Completed;

        }





    }
}
