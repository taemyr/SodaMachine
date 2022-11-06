using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Actions
{
    public abstract class ActionBase : ISodaMachineAction
    {
        public ActionBase(string msg)
        {
            Msg = msg;
        }

        public string Msg { get; private set; }
        public abstract bool StateChanged { get; }
    }
}
