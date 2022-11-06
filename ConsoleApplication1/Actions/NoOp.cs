using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Actions
{
    /// <summary>
    /// Action has changed internal state in engine.  No furter action required.
    /// </summary>
    public class NoOp : ActionBase
    {
        public NoOp() : base(null)
        {
        }

        public override bool StateChanged => true;
    }
}
