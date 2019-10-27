using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public enum CopyCondition
    {
        Mint=6, NearMint= 5 , Good =4 , Poor =3, Damaged =2 , HeavlyDamaged=1
    }

    public class WrappedCopy
    {
        private Copy Copy;

        public int Id { get; }
        public WrappedBook Book { get; }
        public bool Borrowed { get; }
        public CopyCondition Condition { get; }



       public WrappedCopy(Copy copy)
        {
            this.Copy = copy;
            this.Id = copy.CopyId;
            this.Book = new WrappedBook(Copy.Book);
            this.Borrowed = copy.Borrowed;
            this.Condition = (CopyCondition)copy.Condition;

        }

        internal Copy GetCopy()
        {
            return this.Copy;
        }
    }
}
