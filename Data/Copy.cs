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

    // TODO: uzupelnic klase
    /** OpisStanu  */
    public class Copy
    {
        public int CopyID { get; set; }
        public int BookId { get; }
        public bool Borrowed { get; set; } //czy wypozyczona
        public CopyCondition Condition { get; set; }

        public Copy(int copyID, int bookId, bool borrowed, CopyCondition condition)
        {
            CopyID = copyID;
            BookId = bookId;
            Borrowed = borrowed;
            Condition = condition;
        }
    }
}
