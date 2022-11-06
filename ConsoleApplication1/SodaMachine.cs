using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class SodaMachine
    {
        private static int money;

        InventoryCollection inventories = new InventoryCollection(new Inventory[] { new Inventory(ReferenceData.Cola,5),new Inventory(ReferenceData.Fanta,3),new Inventory(ReferenceData.Sprite,3) });

        public void Insert(int amount)
        {
            money += amount;
            Console.WriteLine("Adding " + amount + " to credit");
        }

        private void order(string sodaName, bool debit)
        {
            var inventory = inventories[sodaName];
            if (inventory == null)
            {
                Console.WriteLine("No such soda");
                return;
            }
            if (inventory.Amount < 1)
            {
                Console.WriteLine($"No {sodaName} left");
                return;
            }
            if (debit && inventory.Soda.Price>money)
            {
                Console.WriteLine($"Need {(inventory.Soda.Price - money)} more");
                return;
            }
            Console.WriteLine($"Giving {sodaName} out");
            inventory.Amount--;
            if(debit)
            {
                money -= inventory.Soda.Price;
                Recall();
            }
        }

        public void Order(string sodaName)
        {
            order(sodaName, true);
        }

        public void SmsOrder(string sodaName)
        {
            order(sodaName, false);
        }


        private void Recall()
        {
            Console.WriteLine("Returning " + money + " to customer");
            money = 0;
        }

        /// <summary>
        /// This is the starter method for the machine
        /// </summary>
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n\nAvailable commands:");
                Console.WriteLine("insert (money) - Money put into money slot");
                Console.WriteLine("order (coke, sprite, fanta) - Order from machines buttons");
                Console.WriteLine("sms order (coke, sprite, fanta) - Order sent by sms");
                Console.WriteLine("recall - gives money back");
                Console.WriteLine("-------");
                Console.WriteLine("Inserted money: " + money);
                Console.WriteLine("-------\n\n");

                var input = Console.ReadLine();

                if (input.StartsWith("insert"))
                {
                    //Add to credit
                    var arg = int.Parse(input.Split(' ')[1]);
                    Insert(arg);
                }
                if (input.StartsWith("order"))
                {
                    // split string on space
                    var arg = input.Split(' ')[1];
                    Order(arg);
                }
                if (input.StartsWith("sms order"))
                {
                    var arg = input.Split(' ')[2];
                    SmsOrder(arg);
                }

                if (input.Equals("recall"))
                {
                    Recall();
                }

            }
        }


    }
}
