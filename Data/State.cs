using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    // TODO: uzupelnic klase
    /** OpisStanu  */
    public class State
    {
        public int CopyNumber { get; set; }
        public int BookId { get; }
        public DateTimeOffset AquisitionDate { get; set; }
        public bool Borrowed { get; set; } //czy wypozyczona
    }
}
