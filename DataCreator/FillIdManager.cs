using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    class FillIdManager
    {
        private int bookId;
        private int copyId;
        private int readerId;

       public FillIdManager()
        {
            bookId = 0;
            copyId = 0;
            readerId = 0; 
        }


        public int GenerateBookId()
        {
            bookId++;
            return bookId;
        }

        public int GenerateCopyId()
        {
            copyId++;
            return copyId;
        }

        public int GenerateReaderId()
        {
            readerId++;
            return readerId;
        }

        public int getLastBookId()
        {
            return bookId;
        }

        public int getLastCopyId()
        {
            return copyId;
        }

        public int getLastReaderId()
        {
            return copyId;
        }
    }
}
