using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    public enum CopyCondition
    {
        Mint, NearMint, Good, Poor, Damaged, HeavlyDamaged
    }

    /** OpisStanu  */
    public class Copy
    {
        public int CopyId { get; set; }
        public Book Book { get; }
        public bool Borrowed { get; set; } //czy wypozyczona
        public CopyCondition Condition { get; set; }

        public Copy(int copyID, Book book, bool borrowed, CopyCondition condition)
        {
            CopyId = copyID;
            this.Book = book;
            Borrowed = borrowed;
            Condition = condition;
        }
    }
}
