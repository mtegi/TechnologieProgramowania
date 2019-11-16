using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{

    /** OpisStanu  */
    [Serializable]
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

        public Copy(int copyID, Book book, CopyCondition condition, bool borrowed)
        {
            CopyId = copyID;
            this.Book = book;
            Borrowed = borrowed;
            Condition = condition;
        }
    }
}
