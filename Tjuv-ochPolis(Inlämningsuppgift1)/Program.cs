using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;




namespace Tjuv_ochPolis_Inlämningsuppgift1_    
{
    using static SafetyFunctions;
    class Program
    {        
        static void Main(string[] args)
        {
            InitializeGame();            
            Console.ReadKey();           
        }

        /// <summary>
        /// Sets the options for the game. Then executes RunGame().
        /// </summary>
        /// <param name="numberOfRounds"></param>
        static void InitializeGame()
        {
            char typeOfGame;
            int numberOfRounds = 0;
            Console.SetWindowSize(Person.FIELD_SIZE_X, Person.FIELD_SIZE_Y);
            Console.WriteLine("INSTÄLLNINGAR:");
            Console.Write($"Hur många medborgare vill du lägga till i staden? ({Person.MIN_NUMBER_OF_PLAYERS}-{Person.MAX_NUMBER_OF_PLAYERS}): ");
            Citizen.AddCitizen(IsValidNumber(Console.ReadLine()));
            Console.Write($"Hur många tjuvar? ({Person.MIN_NUMBER_OF_PLAYERS}-{Person.MAX_NUMBER_OF_PLAYERS}): ");
            Thief.AddThief(IsValidNumber(Console.ReadLine()));
            Console.Write($"Och hur många poliser? ({Person.MIN_NUMBER_OF_PLAYERS}-{Person.MAX_NUMBER_OF_PLAYERS}): ");
            Police.AddPolice(IsValidNumber(Console.ReadLine()));
            Console.Write("Hur vill du spela?\nFyll i vad du vill räkna. Stölder(S), Gripanden(G) eller Rundor(R): ");
            typeOfGame = IsValidLetter(Console.ReadLine());
            Console.Write("Hur långt vill du räkna? ");
            numberOfRounds = IsNumber(Console.ReadLine());            
            Console.CursorVisible = false;
            Welcome();
            UpdateScreen(0,0,0);
            RunGame(numberOfRounds, typeOfGame);
        }
        /// <summary>
        /// Displays a welcome message.
        /// </summary>
        static void Welcome()
        {
            Console.Clear();
            Person.ClearMessageBox(4);
            Console.SetCursorPosition(Person.MESSAGE_BOX_X+6, Person.MESSAGE_BOX_Y);
            Console.WriteLine("NU SPELAR VI TJUV OCH POLIS");
            Console.SetCursorPosition(Person.MESSAGE_BOX_X-1, Person.MESSAGE_BOX_Y+1);
            Console.WriteLine("LUTA DIG TILLBAKA OCH INVÄNTA RESULTATET");
            Thread.Sleep(6000);
        }
        /// <summary>
        /// Contains the methods which run the game.
        /// </summary>
        /// <param name="numberOfRounds"></param>
        static void RunGame(int numberOfRounds, char typeOfGame)
        {
            int numberOfArrests = 0;
            int numberOfThefts = 0;
            int counter = 0;
            
            switch (typeOfGame)
            {
                case 'r': //Game mode "Counting Rounds"
                    for (counter = 0; counter < numberOfRounds; counter++)
                    {                        
                        Person.CheckForAction(ref numberOfArrests, ref numberOfThefts);
                        Person.MovePersons();
                        UpdateScreen(counter+1, numberOfArrests, numberOfThefts);
                    }
                    break;

                case 'g': //Game mode "Counting Arrests"                        

                    while (numberOfArrests < numberOfRounds) 
                    {
                        Person.CheckForAction(ref numberOfArrests, ref numberOfThefts);
                        Person.MovePersons();
                        UpdateScreen(counter + 1, numberOfArrests, numberOfThefts);
                        counter++;
                    } 
                        break;

                case 's': //Game mode "Counting Thefts"                        
                    while (numberOfThefts < numberOfRounds)
                    {                        
                        Person.CheckForAction(ref numberOfArrests, ref numberOfThefts);
                        Person.MovePersons();
                        UpdateScreen(counter+1, numberOfArrests, numberOfThefts);
                        counter++;

                    } 
                    break;
                          
            
            }
            Thread.Sleep(Person.PAUS);
            Person.ShowStats(counter, numberOfArrests, numberOfThefts);
        }
        /// <summary>
        /// Draws out the position of each character in game and stats for round, arrests and thefts.
        /// </summary>
        /// <param name="counter"></param>
        static void UpdateScreen(int counter, int _numberOfArrests, int _numberOfThefts)
        {
            Console.Clear();
            foreach (Person person in Person.inhabitants)
            {
                Person.DrawPerson(person.PositionX, person.PositionY, person.Appearence);
            }

            Console.SetCursorPosition(0, Person.FIELD_SIZE_Y - 4);
            for (int i = 0; i < Person.FIELD_SIZE_X; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine($"Antal spelade rundor: {counter}");
            Console.WriteLine($"Antal gripna tjuvar: {_numberOfArrests}");
            Console.Write($"Antal rånade medborgare: {_numberOfThefts}");
        }      
    }
}
