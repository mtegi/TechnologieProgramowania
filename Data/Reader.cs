using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /** Wykaz (czytelnik) */
    public class Reader : IComparable<Reader>
    {
        public Reader(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
     
        public int CompareTo(Reader other)
        {
            if (String.Compare(this.LastName, other.LastName) < 0)
                return -1;
            if (String.Compare(this.LastName, other.LastName) > 0)
                return 1;
            else
            {
                if (String.Compare(this.FirstName, other.FirstName) < 0)
                    return -1;
                if (String.Compare(this.FirstName, other.FirstName) > 0)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
