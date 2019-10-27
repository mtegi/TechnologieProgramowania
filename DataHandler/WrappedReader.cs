using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{

    public class WrappedReader
    {
        private Reader reader;

        public int Id { get; }
        public string FristName { get; }
        public string LastName { get; }

      public  WrappedReader(Reader reader)
        {
            this.reader = reader;

            this.Id = Id; 
            this.FristName = reader.FirstName;
            this.LastName = reader.LastName;
        }

        internal Reader GetReader()
        {
            return this.reader;
        }
    }
}
