using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tjuv_ochPolis_Inlämningsuppgift1_
{
    class Thief : Person
    {
        /// <summary>
        /// Constructor for creating a person of the type Thief.
        /// </summary>
        public Thief(int number) : base("Tjuv "+number)
        {            
            Inventory = new Inventory("Stöldgods");           
            Appearence = 'T';            
        }
        /// <summary>
        /// Adds the specified number of thiefs to the inhabitants-list.
        /// </summary>
        /// <param name="numberOfCitizens"></param>
        public static void AddThief(int numberOfThieves)
        {
            for (int i = 0; i < numberOfThieves; i++)
            {
                inhabitants.Add(new Thief(i+1));
            }
        }
        /// <summary>
        /// Thief steals random item from citizen and shows what happened in a "message box".
        /// </summary>
        /// <param name="thiefIndex"></param>
        /// <param name="citizenIndex"></param>
        public static void Steal(int thiefIndex, int citizenIndex)
        {
            Random stealSomething = new Random();
            bool someThingWasStolen = false;
            int numberOfBelongings = inhabitants[citizenIndex].Inventory.Key + inhabitants[citizenIndex].Inventory.Watch +
                inhabitants[citizenIndex].Inventory.Phone + inhabitants[citizenIndex].Inventory.Money;

            ClearMessageBox(4);
            Console.SetCursorPosition(MESSAGE_BOX_X, MESSAGE_BOX_Y);
            
            if (numberOfBelongings > 0)
            {
                do
                {
                    int whatToSteel = stealSomething.Next(4);
                    switch (whatToSteel)
                    {
                        case 0:
                            if (inhabitants[citizenIndex].Inventory.Key > 0)
                            {
                                inhabitants[thiefIndex].Inventory.Key += inhabitants[citizenIndex].Inventory.Key;
                                inhabitants[citizenIndex].Inventory.Key = 0;                               
                                Console.WriteLine($"{inhabitants[thiefIndex].Name} stal {inhabitants[citizenIndex].Name}s nyckel.");                                
                                someThingWasStolen = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        case 1:
                            if (inhabitants[citizenIndex].Inventory.Watch > 0)
                            {
                                inhabitants[thiefIndex].Inventory.Watch += inhabitants[citizenIndex].Inventory.Watch;
                                inhabitants[citizenIndex].Inventory.Watch = 0;                                
                                Console.WriteLine($"{inhabitants[thiefIndex].Name} stal {inhabitants[citizenIndex].Name}s klocka.");                                
                                someThingWasStolen = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        case 2:
                            if (inhabitants[citizenIndex].Inventory.Phone > 0)
                            {
                                inhabitants[thiefIndex].Inventory.Phone += inhabitants[citizenIndex].Inventory.Phone;
                                inhabitants[citizenIndex].Inventory.Phone = 0;
                                Console.WriteLine($"{inhabitants[thiefIndex].Name} stal {inhabitants[citizenIndex].Name}s mobil.");
                                someThingWasStolen = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        case 3:
                            if (inhabitants[citizenIndex].Inventory.Money > 0)
                            {
                                inhabitants[thiefIndex].Inventory.Money += inhabitants[citizenIndex].Inventory.Money;                                
                                Console.WriteLine($"{inhabitants[thiefIndex].Name} stal {inhabitants[citizenIndex].Inventory.Money} kronor av" +
                                    $" {inhabitants[citizenIndex].Name}.");
                                inhabitants[citizenIndex].Inventory.Money = 0;
                                someThingWasStolen = true;
                                break;
                            }
                            else
                            {
                                break;
                            }

                    }
                } while (someThingWasStolen == false);
            }
            else
            {
                Console.WriteLine($"{inhabitants[thiefIndex].Name} försökte stjäla av {inhabitants[citizenIndex].Name}.");
                Console.SetCursorPosition(MESSAGE_BOX_X, MESSAGE_BOX_Y + 1);
                Console.WriteLine($"{inhabitants[citizenIndex].Name} hade dock inget av värde.");
            }
            Thread.Sleep(PAUS);

        }
    }
}
