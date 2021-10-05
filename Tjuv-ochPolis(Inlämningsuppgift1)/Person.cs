using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tjuv_ochPolis_Inlämningsuppgift1_
{
    /// <summary>
    /// Base class for all the characters of the game.
    /// </summary>
    abstract class Person
    {   
        private Inventory inventory;
        Random randomGenerator;
        private char appearence;                                
        private string name;
        private int positionX;
        private int positionY;
        private int directionX;
        private int directionY;
        
        //Constants for developer to set game properties.        
        public const int FIELD_SIZE_X = 125;                    
        public const int FIELD_SIZE_Y = 30;                    
        public const int MESSAGE_BOX_X = FIELD_SIZE_X/2 - 20;   
        public const int MESSAGE_BOX_Y = FIELD_SIZE_Y/2 - 5;
        public const int PAUS = 2000;                           
        public const int ROUND_TIME = 500;                      
        public const int MAX_NUMBER_OF_PLAYERS = 30;            
        public const int MIN_NUMBER_OF_PLAYERS = 10;
        
        /// <summary>
        /// A list of all the inhabitants in the "town".
        /// </summary>
        public static List<Person> inhabitants = new List<Person>();
        public Inventory Inventory { get => inventory; set => inventory = value; }
        public char Appearence { get => appearence; set => appearence = value; }
        public string Name { get => name; set => name = value; }

        public int PositionX { get => positionX; set { positionX = value; } }
        public int PositionY { get => positionY; set { positionY = value; } }
        /// <summary>
        /// Sets the x-direction according to the random number generated in the constructor.
        /// </summary>
        public int DirectionX 
        { 
            get => directionX;
            set 
            {
                switch (value)
                {
                    case 0:
                        directionX = -1;
                        break;
                    case 1:
                        directionX = 0;
                        break;
                    case 2:
                        directionX = 1;
                        break;
                                            
                } 
            }
        }
        /// <summary>
        /// Sets the y-direction according to the random number generated in the constructor.
        /// </summary>
        public int DirectionY
        {
            get => directionY;
            set
            {
                switch (value)
                {
                    case 0:
                        directionY = -1;
                        break;
                    case 1:
                        directionY = 0;
                        break;                        
                    case 2:
                        directionY = 1;
                        break;

                };
            }
        }
       
        /// <summary>
        /// Base creator for person. Randomizes the starting position and direction. Also makes sure they have a direction.
        /// </summary>
        public Person(string _name)
        {
            randomGenerator = new Random();
            int _directionX = 0;
            int _directionY = 0;
            int _positionX;
            int _positionY;
            
            do
            {
                _directionX = randomGenerator.Next(3);
                _directionY = randomGenerator.Next(3);
            } while (_directionX == 1 && _directionY == 1);
            DirectionX = _directionX;
            DirectionY = _directionY;
            do
            {
                _positionX = randomGenerator.Next(FIELD_SIZE_X);
                _positionY = randomGenerator.Next(FIELD_SIZE_Y - 5);
            } while (CheckIfUsedPosition(_positionX, _positionY));
            PositionX = _positionX;
            positionY = _positionY;
            Name= _name;
        }
        /// <summary>
        /// Draws the position of the person with the character provided by the Appearence property.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="type"></param>        
        public static void DrawPerson(int posX, int posY, char type)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write(type);

        }
        /// <summary>
        /// Moves the persons in the direction set when initialized. Makes sure they dont go out of bounds.
        /// </summary>
        public static void MovePersons()
        {
            for (int i = 0; i < inhabitants.Count; i++)
            {
                inhabitants[i].PositionX += inhabitants[i].DirectionX;
                inhabitants[i].PositionY += inhabitants[i].DirectionY;
                if (inhabitants[i].PositionX > FIELD_SIZE_X-1)
                {
                    inhabitants[i].PositionX = 0;
                }
                if (inhabitants[i].PositionX < 0)
                {
                    inhabitants[i].PositionX = FIELD_SIZE_X-1;
                }
                if (inhabitants[i].PositionY > FIELD_SIZE_Y-5)
                {
                    inhabitants[i].PositionY = 0;
                }
                if (inhabitants[i].PositionY < 0)
                {
                    inhabitants[i].PositionY = FIELD_SIZE_Y-5;
                }

            }
            Thread.Sleep(ROUND_TIME);
        }
        /// <summary>
        /// Clear the space where the messagebox will appear and draws lines above and beneath.
        /// </summary>
        /// <param name="numberOfLines"></param>
        public static void ClearMessageBox(int numberOfLines)
        {
            for (int i = -1; i < numberOfLines-1; i++)
            {
                Console.SetCursorPosition(0, MESSAGE_BOX_Y + i);
                for (int x = 0; x < FIELD_SIZE_X; x++)
                {
                    if (i == -1 || i == numberOfLines - 2)
                    {
                        Console.Write("-");
                    }
                    else 
                    {
                        Console.Write(" ");
                    }
                    
                }
            }
        }
        /// <summary>
        /// Clears a space where the message box appeared so that a bigger one won't display if overwritten by a smaller one.
        /// </summary>
        public static void ClearMessageBox()
        {
            for (int i = -1; i < 8 - 1; i++)
            {
                Console.SetCursorPosition(0, MESSAGE_BOX_Y + i);
                for (int x = 0; x < FIELD_SIZE_X; x++)
                {          
                        Console.Write(" ");    
                }
            }
        }
        /// <summary>
        /// Controls if there is either a police and a thief or a thief and a citizen in the same spot. Executes Confiscate() / Steal() if there is.
        /// </summary>
        /// <param name="numberOfArrests"></param>
        /// <param name="numberOfThefts"></param>
        public static void CheckForAction(ref int numberOfArrests, ref int numberOfThefts)
        {
            for (int i = 0; i < inhabitants.Count; i++)
            {
                for (int x = 0; x < inhabitants.Count; x++)
                {
                    if (inhabitants[i].PositionX == inhabitants[x].PositionX && inhabitants[i].PositionY == inhabitants[x].PositionY)
                    {
                        if (inhabitants[i] is Police && inhabitants[x] is Thief)
                        {
                            Police.Confiscate(i, x);
                            numberOfArrests++;
                            
                        }
                        if (inhabitants[i] is Thief && inhabitants[x]is Citizen)
                        {
                            Thief.Steal(i, x);
                            numberOfThefts++;
                        }
                    }
                }
            }            
        }
        /// <summary>
        /// Writes out stats from the game, including all persons names and inventory.
        /// </summary>
        /// <param name="counter"></param>
        /// <param name="numberOfArrests"></param>
        /// <param name="numberOfThefts"></param>
        public static void ShowStats(int counter, int numberOfArrests, int numberOfThefts)
        {
            int x = FIELD_SIZE_X;
            int y = FIELD_SIZE_Y;
            Console.Clear();
            foreach (Person mrX in inhabitants)
            {
                Console.SetCursorPosition((FIELD_SIZE_X - x), (FIELD_SIZE_Y - y));
                Console.Write(mrX.Name);
                y--;
                Console.SetCursorPosition((FIELD_SIZE_X - x), (FIELD_SIZE_Y - y));
                Console.Write($"{mrX.Inventory.Name}:");
                y--;
                Console.SetCursorPosition((FIELD_SIZE_X - x), (FIELD_SIZE_Y - y));
                Console.Write($"Nycklar: { mrX.Inventory.Key}");
                y--;
                Console.SetCursorPosition((FIELD_SIZE_X - x), (FIELD_SIZE_Y - y));
                Console.Write($"Klockor: {mrX.Inventory.Watch}");
                y--;
                Console.SetCursorPosition((FIELD_SIZE_X - x), (FIELD_SIZE_Y - y));
                Console.Write($"Mobil: {mrX.Inventory.Phone}");
                y--;
                Console.SetCursorPosition((FIELD_SIZE_X - x), (FIELD_SIZE_Y - y));
                Console.Write($"Pengar: {mrX.Inventory.Money} kr");
                  
                x -= 20;                                //Moves the "column".
                y += 5;
                if (x < FIELD_SIZE_X-105)               //Resets the "column" and draws a line between rows.            
                {
                    x = FIELD_SIZE_X;
                    y -= 7;
                    Console.WriteLine();
                    for (int i = 0; i < FIELD_SIZE_X; i++)
                    {
                        Console.Write("_");
                    }
                }

            }
            Console.WriteLine();
            for (int i = 0; i < FIELD_SIZE_X; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine($"Antal spelade rundor: {counter}");
            Console.WriteLine($"Antal gripna tjuvar: {numberOfArrests}");
            Console.Write($"Antal rånade medborgare: {numberOfThefts}");


        }
        /// <summary>
        /// Returns true if position is taken, false if free.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        public static bool CheckIfUsedPosition(int posX, int posY)
        {
            bool isUsed = false;
            foreach (Person mrX in inhabitants)
            {
                if (mrX.positionX == posX && mrX.positionY == posY)
                {
                    isUsed = true;
                }
            }
            return isUsed;
        }

    }
}
