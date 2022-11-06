using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Actions
{
    public interface ISodaMachineAction
    {
        string Msg { get; }
        bool StateChanged { get; }
    }
}
