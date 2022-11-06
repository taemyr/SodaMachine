using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Actions
{
    public class ReturnMoney : ActionBase
    {
        public ReturnMoney(int amount) : base($"Returning {amount} to customer")
        {
        }

        public override bool StateChanged => true;
    }
}
