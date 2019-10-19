using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public abstract class IdManager
    {
        public abstract int GenerateBookId();
        public abstract int GenerateCopyId();
        public abstract int GenerateReaderId();

        public abstract int getLastBookId();
        public abstract int getLastCopyId();
        public abstract int getLastReaderId();

    }
}
