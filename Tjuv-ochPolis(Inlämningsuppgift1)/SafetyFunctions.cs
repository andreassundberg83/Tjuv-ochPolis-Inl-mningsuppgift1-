using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuv_ochPolis_Inlämningsuppgift1_
{
    /// <summary>
    /// Contains methods for verifying user input.
    /// </summary>
    public static class SafetyFunctions
    {
        /// <summary>
        /// Checks if supplied string is convertable to int. Asks for new entry if illegal.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int IsNumber(string input)
        {
            bool test = false;
            int returnValue = 0;
            do
            {
                test = int.TryParse(input, out returnValue);
                if (test == false)
                {
                    Console.WriteLine("Felaktig inmatning. Endast siffror.");
                    input = Console.ReadLine();
                }
            } while (test == false);
            return returnValue;
        }
        /// <summary>
        /// Determines if user input for gametype is valid. Asks for new input if not. Returns a Char.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static char IsValidLetter(string input)
        {
            bool test = false;
            char returnValue = ' ';
            do
            {
                input = input.ToLower();
                if (input.StartsWith('s'))
                {
                    test = true;
                    returnValue = 's';
                }
                else if (input.StartsWith('g'))
                {
                    test = true;
                    returnValue = 'g';
                }
                else if (input.StartsWith('r'))
                {
                    test = true;
                    returnValue = 'r';
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning.\nS för stölder.\nG för gripanden.\nR för rundor.");
                    input = Console.ReadLine();
                }
            } while (test == false);
            return returnValue;
        }

        /// <summary>
        /// Checks if number supplied is a legal number. Returns a legal number in any case.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int IsValidNumber(string input)
        {
            int numberOfPersons = IsNumber(input);
            if (numberOfPersons < Person.MIN_NUMBER_OF_PLAYERS)
            {
                Console.WriteLine($"Du skrev ett för lågt värde. Sätter värdet till minimum: {Person.MIN_NUMBER_OF_PLAYERS}");
                return Person.MIN_NUMBER_OF_PLAYERS;
            }
            else if (numberOfPersons > Person.MAX_NUMBER_OF_PLAYERS)
            {
                Console.WriteLine($"Du skrev ett för högt värde. Sätter värdet till minimum: {Person.MAX_NUMBER_OF_PLAYERS}");
                return Person.MAX_NUMBER_OF_PLAYERS;
            }
            else
            {
                return numberOfPersons;
            }
        }
    }
}
