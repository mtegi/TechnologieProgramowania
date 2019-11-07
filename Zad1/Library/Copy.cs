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
        public CopyCondition Condition { get; set; }

        public Copy() { }

        public Copy(int copyID, Book book, CopyCondition condition)
        {
            CopyId = copyID;
            this.Book = book;
            Borrowed = false;
            Condition = condition;
        }
    }
}
