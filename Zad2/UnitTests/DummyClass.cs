using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [Serializable]
    public class DummyClass
    {
        public DummyClass Other { get; set; }
        public int Id { get; set; }

    }
}
