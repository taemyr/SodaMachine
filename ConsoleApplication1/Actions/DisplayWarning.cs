using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Actions
{
    /// <summary>
    /// The machine should provide a warning to indicate that the requested action has not been performed.
    /// </summary>
    public class DisplayWarning : ActionBase
    {
        public DisplayWarning(string msg) : base(msg)
        {
        }

        public override bool StateChanged => false;
    }
}
