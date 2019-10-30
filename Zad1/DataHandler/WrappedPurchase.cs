using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public class WrappedPurchase:WrappedEvent
    {
   
        public double Price { get; }
        public string Distributor { get;  }

        public WrappedPurchase(PurchaseEvent purchase) : base(purchase)
        {
            this.Price = purchase.Price;
            this.Distributor = purchase.Distributor;
        }


    }
}
