﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public class BorrowingEvent:LibEvent
    {
        public DateTimeOffset ReturnDate { get; set; }
        public Reader Reader { get; set; }
        public bool Completed { get; set; }

        // Null until completed
        public ReturnEvent Return { get; set; }
        


        public BorrowingEvent(Reader reader, Copy copy, DateTimeOffset eventDate, DateTimeOffset returnDate) : base(3, copy, eventDate)
        {
            this.ReturnDate = returnDate;
            this.Reader = reader;
            this.Completed = false;
            this.Return = null; 
        }

    }
}