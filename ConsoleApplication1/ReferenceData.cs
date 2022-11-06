using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class ReferenceData
    {
#warning Todo:Remove Hardcoded values.
        public static readonly Soda Cola = new Soda() { Name="cola", Price = 20 };
        public static readonly Soda Sprite = new Soda() { Name = "sprite", Price = 15 };
        public static readonly Soda Fanta = new Soda() { Name = "fanta", Price = 15 };
    }
}
