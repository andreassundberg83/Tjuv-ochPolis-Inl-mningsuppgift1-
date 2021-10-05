using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tjuv_ochPolis_Inlämningsuppgift1_
{
    class Police :Person
    {
        /// <summary>
        /// Constructor for creating a person of the type Police.
        /// </summary>
        public Police(int number) : base("Polisman " +number)
        {            
            Inventory = new Inventory("Beslagtaget gods");            
            Appearence = 'P';          
        }
        /// <summary>
        /// Adds the specified number of police to the inhabitants-list.
        /// </summary>
        /// <param name="numberOfCitizens"></param>
        public static void AddPolice(int numberOfPolicemen)
        {
            for (int i = 0; i < numberOfPolicemen; i++)
            {
                inhabitants.Add(new Police(i+1));
            }
        }

        /// <summary>
        /// Police confiscates all the thiefs stolen goods and shows what happened in a "message box". 
        /// </summary>
        /// <param name="policeIndex"></param>
        /// <param name="thiefIndex"></param>
        public static void Confiscate(int policeIndex, int thiefIndex)
        {
            int numberOfConfiscated = 0;
            inhabitants[policeIndex].Inventory.Key += inhabitants[thiefIndex].Inventory.Key;
            if (inhabitants[thiefIndex].Inventory.Key > 0)
            {
                numberOfConfiscated++;
            }
            inhabitants[policeIndex].Inventory.Watch += inhabitants[thiefIndex].Inventory.Watch;
            if (inhabitants[thiefIndex].Inventory.Watch > 0)
            {
                numberOfConfiscated++;
            }
            inhabitants[policeIndex].Inventory.Phone += inhabitants[thiefIndex].Inventory.Phone;
            if (inhabitants[thiefIndex].Inventory.Phone > 0)
            {
                numberOfConfiscated++;
            }
            inhabitants[policeIndex].Inventory.Money += inhabitants[thiefIndex].Inventory.Money;
            if (inhabitants[thiefIndex].Inventory.Money > 0)
            {
                numberOfConfiscated++;
            }

            
            if (numberOfConfiscated > 0)
            {
                ClearMessageBox(8);
                Console.SetCursorPosition(MESSAGE_BOX_X, MESSAGE_BOX_Y);
                Console.WriteLine($"{inhabitants[policeIndex].Name} arresterade {inhabitants[thiefIndex].Name}.");
                Console.SetCursorPosition(MESSAGE_BOX_X, MESSAGE_BOX_Y +1);
                Console.WriteLine("Detta är vad han konfiskerade:");
                Console.SetCursorPosition(MESSAGE_BOX_X, MESSAGE_BOX_Y + 2);
                Console.WriteLine($"Nycklar: {inhabitants[thiefIndex].Inventory.Key}");
                Console.SetCursorPosition(MESSAGE_BOX_X, MESSAGE_BOX_Y + 3);
                Console.WriteLine($"Klockor: {inhabitants[thiefIndex].Inventory.Watch}");
                Console.SetCursorPosition(MESSAGE_BOX_X, MESSAGE_BOX_Y + 4);
                Console.WriteLine($"Mobil: {inhabitants[thiefIndex].Inventory.Phone}");
                Console.SetCursorPosition(MESSAGE_BOX_X, MESSAGE_BOX_Y + 5);
                Console.WriteLine($"Pengar: {inhabitants[thiefIndex].Inventory.Money} kr");
                Thread.Sleep(PAUS);
                ClearMessageBox();
            }
            else
            {
                ClearMessageBox(4);
                Console.SetCursorPosition(MESSAGE_BOX_X, MESSAGE_BOX_Y);
                Console.WriteLine($"{inhabitants[policeIndex].Name} arresterade {inhabitants[thiefIndex].Name}.");
                Console.SetCursorPosition(MESSAGE_BOX_X,MESSAGE_BOX_Y+1);
                Console.WriteLine("Det fanns dock inget stöldgods att beslagta.");
                Thread.Sleep(PAUS);
            }
            
            inhabitants[thiefIndex].Inventory.Key = 0;
            inhabitants[thiefIndex].Inventory.Watch = 0;
            inhabitants[thiefIndex].Inventory.Phone = 0;
            inhabitants[thiefIndex].Inventory.Money = 0;
        }

        
    }
}
