using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Actions
{
    public class EmitSoda : ActionBase
    {
        public EmitSoda(string sodaName) : base($"Giving {sodaName} out")
        {
        }

        public override bool StateChanged => true;
    }
}
