using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class ReferenceData
    {
#warning Todo:Remove Hardcoded values.
        public static readonly Soda Cola = new Soda() { Name="cola", Nr=5 };
        public static readonly Soda Sprite = new Soda { Name = "sprite", Nr = 3 };
        public static readonly Soda Fanta = new Soda { Name = "fanta", Nr = 3 };
    }
}
