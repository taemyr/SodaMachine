using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Inventory
    {
        public Inventory(Soda soda, int count)
        {
            this.Soda = Soda ?? throw new ArgumentNullException(nameof(soda));
            this.Amount = Amount;
        }

        public Soda Soda { get; private set; }
        public int Amount { get; set; }
    }
}
