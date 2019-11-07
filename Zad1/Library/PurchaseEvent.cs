using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PurchaseEvent:LibEvent
    {
        public double Price { get; set; }
        public string Distributor { get; set; }

        public PurchaseEvent (Copy copy, DateTimeOffset eventDate, double price, string distributor) : base (copy, eventDate )
        {
            this.Price = price;
            this.Distributor = distributor; 
        }
    }
}
