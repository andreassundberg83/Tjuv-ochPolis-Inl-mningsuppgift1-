using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tjuv_ochPolis_Inlämningsuppgift1_
{
    class Citizen : Person
    {        
        /// <summary>
        /// Constructor for creating a person of the type Citizen.
        /// </summary>
        public Citizen(int number) : base("Medborgare "+number)
        {                           
            Inventory = new Inventory("Tillhörigheter", 1, 1, 1, 400);            
            Appearence = 'M';               
        }
        /// <summary>
        /// Adds the number of citizens to the inhabitants-list.
        /// </summary>
        /// <param name="numberOfCitizens"></param>
        public static void AddCitizen(int numberOfCitizens)
        {
            for (int i = 0; i < numberOfCitizens; i++)
            {
                inhabitants.Add(new Citizen(i+1));
            }
        }
        
       



    }
}
