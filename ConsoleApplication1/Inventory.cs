using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Inventory
    {
        public Inventory(Soda soda, int amount)
        {
            this.Soda = soda ?? throw new ArgumentNullException(nameof(soda));
            this.Amount = amount;
        }

        public Soda Soda { get; private set; }
        public int Amount { get; set; }
    }
}
