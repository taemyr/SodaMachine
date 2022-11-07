using ConsoleApplication1.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class SodaMachine
    {
        public int Balance { get; private set; }

        InventoryCollection inventories = new InventoryCollection(new Inventory(ReferenceData.Cola,5),new Inventory(ReferenceData.Fanta,3),new Inventory(ReferenceData.Sprite,3));

        public SodaMachine()
        {

        }

        public SodaMachine(InventoryCollection inventories)
        {

        }

        public IEnumerable<ISodaMachineAction> Insert(int amount)
        {
            Balance += amount;
            return new ISodaMachineAction[] { new NoOp("Adding " + amount + " to credit") };
        }

        private IEnumerable<ISodaMachineAction> order(string sodaName, bool debit)
        {
            List<ISodaMachineAction> result = new List<ISodaMachineAction>();
            var inventory = inventories[sodaName];
            if (inventory == null)
            {
                result.Add( new DisplayWarning("No such soda"));
                return result;
            }
            if (inventory.Amount < 1)
            {
                result.Add(new DisplayWarning($"No {sodaName} left"));
                return result;
            }
            if (debit && inventory.Soda.Price> Balance)
            {
                result.Add(new DisplayWarning($"Need {(inventory.Soda.Price - Balance)} more"));
                return result;
            }
            result.Add(new EmitSoda(sodaName));
            inventory.Amount--;
            if(debit)
            {
                Balance -= inventory.Soda.Price;
                foreach (var action in Recall())
                {
                    result.Add( action);
                }
            }
            return result;
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
            var toReturn = Balance;
            Balance = 0;
            return new ISodaMachineAction[] { new ReturnMoney(toReturn) };
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
                Console.WriteLine("Inserted money: " + Balance);
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
