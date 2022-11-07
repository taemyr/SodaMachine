using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    /// <summary>
    /// Handles collections of different sodas.
    /// Multiple inventories of different sodas with same name is not supported and not checked.
    /// </summary>
    public class InventoryCollection //: IReadOnlyDictionary<string,Inventory> Needs .net framework 4.5
    {
        private List<Inventory> inventories;

        public Inventory this[string name]
        {
            get
            {
                return inventories.Where(x => x.Soda.Name == name).OrderByDescending(x => x.Amount).FirstOrDefault();
            }
        }

        public InventoryCollection(IEnumerable<Inventory> inventories)
        {
            inventories = inventories ?? throw new ArgumentNullException(nameof(inventories));
            this.inventories = inventories.ToList();
        }

    } 
}
