using ConsoleApplication1.Actions;
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

        public IEnumerable<ISodaMachineAction> Insert(int amount)
        {
            money += amount;
            yield return new NoOp("Adding " + amount + " to credit");
        }

        private IEnumerable<ISodaMachineAction> order(string sodaName, bool debit)
        {
            var inventory = inventories[sodaName];
            if (inventory == null)
            {
                yield return new DisplayWarning("No such soda");
                yield break;
            }
            if (inventory.Amount < 1)
            {
                yield return new DisplayWarning($"No {sodaName} left");
                yield break;
            }
            if (debit && inventory.Soda.Price>money)
            {
                yield return new DisplayWarning($"Need {(inventory.Soda.Price - money)} more");
                yield break;
            }
            yield return new EmitSoda(sodaName);
            inventory.Amount--;
            if(debit)
            {
                money -= inventory.Soda.Price;
                foreach (var action in Recall())
                {
                    yield return action;
                }
            }
        }

        public IEnumerable<ISodaMachineAction> Order(string sodaName)
        {
            return order(sodaName, true);
        }

        public IEnumerable<ISodaMachineAction> SmsOrder(string sodaName)
        {
            return order(sodaName, false);
        }


        public IEnumerable<ISodaMachineAction> Recall()
        {
            yield return new ReturnMoney(money);
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
                IEnumerable<ISodaMachineAction> result;
                if (input.StartsWith("insert"))
                {
                    //Add to credit
                    var arg = int.Parse(input.Split(' ')[1]);
                    result=Insert(arg);
                }
                else if (input.StartsWith("order"))
                {
                    // split string on space
                    var arg = input.Split(' ')[1];
                    result = Order(arg);
                }
                else if (input.StartsWith("sms order"))
                {
                    var arg = input.Split(' ')[2];
                    result = SmsOrder(arg);
                }

                else if (input.Equals("recall"))
                {
                    result = Recall();
                }
                else
                {
                    result = new ISodaMachineAction[] { new DisplayWarning("Unknown command") };
                }

                foreach(var action in result)
                {
                    Console.WriteLine(action.Msg);
                }
            }
        }


    }
}
