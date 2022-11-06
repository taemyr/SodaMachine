using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class SodaMachine
    {
        private static int money;

        Soda[] inventory = new[] { new Soda { Name = "coke", Nr = 5 }, new Soda { Name = "sprite", Nr = 3 }, new Soda { Name = "fanta", Nr = 3 } };

        public void Insert(int amount)
        {
            money += amount;
            Console.WriteLine("Adding " + amount + " to credit");
        }

        public void Order(string sodaName)
        {
            //Find out witch kind
            switch (sodaName)
            {
                case "coke":
                    var coke = inventory[0];
                    if (coke.Name == sodaName && money > 19 && coke.Nr > 0)
                    {
                        Console.WriteLine("Giving coke out");
                        money -= 20;
                        Console.WriteLine("Giving " + money + " out in change");
                        money = 0;
                        coke.Nr--;
                    }
                    else if (coke.Name == sodaName && coke.Nr == 0)
                    {
                        Console.WriteLine("No coke left");
                    }
                    else if (coke.Name == sodaName)
                    {
                        Console.WriteLine("Need " + (20 - money) + " more");
                    }

                    break;
                case "fanta":
                    var fanta = inventory[2];
                    if (fanta.Name == sodaName && money > 14 && fanta.Nr >= 0)
                    {
                        Console.WriteLine("Giving fanta out");
                        money -= 15;
                        Console.WriteLine("Giving " + money + " out in change");
                        money = 0;
                        fanta.Nr--;
                    }
                    else if (fanta.Name == sodaName && fanta.Nr == 0)
                    {
                        Console.WriteLine("No fanta left");
                    }
                    else if (fanta.Name == sodaName)
                    {
                        Console.WriteLine("Need " + (15 - money) + " more");
                    }

                    break;
                case "sprite":
                    var sprite = inventory[1];
                    if (sprite.Name == sodaName && money > 14 && sprite.Nr > 0)
                    {
                        Console.WriteLine("Giving sprite out");
                        money -= 15;
                        Console.WriteLine("Giving " + money + " out in change");
                        money = 0;
                        sprite.Nr--;
                    }
                    else if (sprite.Name == sodaName && sprite.Nr == 0)
                    {
                        Console.WriteLine("No sprite left");
                    }
                    else if (sprite.Name == sodaName)
                    {
                        Console.WriteLine("Need " + (15 - money) + " more");
                    }
                    break;
                default:
                    Console.WriteLine("No such soda");
                    break;
            }
        }

        public void SmsOrder(string cName)
        {
            switch (cName)
            {
                case "coke":
                    if (inventory[0].Nr > 0)
                    {
                        Console.WriteLine("Giving coke out");
                        inventory[0].Nr--;
                    }
                    break;
                case "sprite":
                    if (inventory[1].Nr > 0)
                    {
                        Console.WriteLine("Giving sprite out");
                        inventory[1].Nr--;
                    }
                    break;
                case "fanta":
                    if (inventory[2].Nr > 0)
                    {
                        Console.WriteLine("Giving fanta out");
                        inventory[2].Nr--;
                    }
                    break;
            }
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
                    var arg = input.Split(' ')[1];
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
