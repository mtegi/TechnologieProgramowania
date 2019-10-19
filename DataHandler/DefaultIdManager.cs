using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    class DefaultIdManager:IdManager
    {
        private int bookId;
        private int copyId;
        private int readerId;

       public DefaultIdManager()
        {
            bookId = 0;
            copyId = 0;
            readerId = 0; 
        }


        public override int GenerateBookId()
        {
            bookId++;
            return bookId;
        }

        public override int GenerateCopyId()
        {
            copyId++;
            return copyId;
        }

        public override int GenerateReaderId()
        {
            readerId++;
            return readerId;
        }

        public override int getLastBookId()
        {
            return bookId;
        }

        public override int getLastCopyId()
        {
            return copyId;
        }

        public override int getLastReaderId()
        {
            return copyId;
        }
    }
}
