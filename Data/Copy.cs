using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    /** OpisStanu  */
    public class Copy
    {
        public int CopyId { get; set; }
        public Book Book { get; set; }
        public bool Borrowed { get; set; } //czy wypozyczona
        public int Condition { get; set; }

        public Copy(int copyID, Book book, int condition)
        {

            if (condition < 1 || condition > 6)
                throw new ArgumentException("condition nie znajduje się w dowzolonym przedziale 1-6");

            CopyId = copyID;
            this.Book = book;
            Borrowed = false;
            Condition = condition;
        }
    }
}
