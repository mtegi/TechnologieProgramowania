using Data;
using DefinitionLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{

    public class WrappedCopy
    {
        private Copy Copy;

        public int CopyId { get; }
        public WrappedBook Book { get; }
        public bool Borrowed { get; }
        public CopyCondition Condition { get; }



       public WrappedCopy(Copy copy)
        {
            this.Copy = copy;

            this.CopyId = copy.CopyId;
            this.Book = new WrappedBook(Copy.Book);
            this.Borrowed = copy.Borrowed;
            this.Condition = copy.Condition;

        }

        internal Copy GetCopy()
        {
            return this.Copy;
        }
    }
}
