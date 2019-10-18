using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /** Wykaz (czytelnik) */
    public class Reader
    {
        public Reader(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
