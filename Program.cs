using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;

namespace TjuvochPolis_Grupp_H;

internal class Program
{
    public static int ChangeDir = 20;
    public static int CountToDir = 0;
    public static Random Rnd = new Random();
    public static List<Person> Prison = new List<Person>();
    public static List<Person> PersonLista = new List<Person>();
    public static Queue<string> Events = new Queue<string>();
    public static int Speed = 200;

    static void Main(string[] args)
    {
        PersonLista.AddRange(Medborgare.medborgarLista);
        PersonLista.AddRange(Tjuv.tjuvLista);
        PersonLista.AddRange(Polis.polisLista);

        char[,] city = new char[25,100];
        char[,] prison = new char[10,25];

        while (true)
        {
            
            PrisonTime();
            CheckMeetings();
            Console.Clear();
            MovePerson(PersonLista);

            DrawSquare(city, PersonLista);
            DrawSquare(prison, Prison);

            

            if (Events.Count > 0)
            {
                PrintEvent();
            }
            
            foreach (Tjuv tjuv in Prison)
            { 
                Console.WriteLine(tjuv.Name + " " + tjuv.JailTime); 
            }

            //PlaySpeed();
            Thread.Sleep(Speed);

            CountToDir++;
            if (CountToDir == ChangeDir)
            {
                foreach(Person person in PersonLista)
                {
                    person.DirX = Rnd.Next(3);
                    person.DirY = Rnd.Next(3);
                }
                CountToDir = 0;
            }
        }
    }

    private static void DrawSquare(char[,] arr, List<Person> peopleList)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                if (i == 0 || j == 0 || i == arr.GetLength(0) - 1 || j == arr.GetLength(1) - 1)
                {
                    Console.Write('X');
                }

                else
                {
                    if (CheckPos( peopleList, i, j))
                    {
                        ShowSymbol(peopleList, i, j);
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }
            Console.Write(" ");
            Console.WriteLine();
        }
    }

    //private static void PlaySpeed()
    //{  
    //    Thread.Sleep(Speed);

    //    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    //    if(keyInfo.Key == ConsoleKey.UpArrow)
    //    {
    //        Speed += 100;
    //    }
    //    if(keyInfo.Key == ConsoleKey.DownArrow && Speed > 0)
    //    {
    //        Speed -= 100;
    //    }
    //}

    private static void PrintEvent()
    {
        foreach (object happens in Events)
        {
            Console.WriteLine(happens);
        }
        if (Events.Count > 5)
        {
            Events.Dequeue();
        }
    }

    private static void PrisonTime()
    {
        foreach (Tjuv tjuv in Prison)
        {
            tjuv.JailTime--;
            if(tjuv.JailTime == 0 )
            {
                PersonLista.Add(tjuv);
                Prison.Remove(tjuv);
                return;
            }
        }
    }

    private static void MovePerson(List<Person> personlista)
    {
        foreach (Person person in personlista)
        {
            switch (person.DirX)
            {
                case 0:
                    break;
                case 1:
                    person.KordX++;
                    break;
                case 2:
                    person.KordX--;
                    break;
            }
            switch (person.DirY)
            {
                case 0:
                    break;
                case 1:
                    person.KordY++;
                    break;
                case 2:
                    person.KordY--;
                    break;
            }
            if (person.KordX == 0)
            {
                person.KordX = 98;
            }
            if (person.KordY <= 1) 
            { 
                person.KordY = 23; 
            }
            if (person.KordX >= 99)
            {
                person.KordX = 1;
            }
            if(person.KordY >= 24)
            {
                person.KordY = 1;
            }
        }
    }

    private static void ShowSymbol(List<Person> personlista, int i, int j)
    {
        foreach (Person person in personlista)
        {
            if (person.KordY == i && person.KordX == j)
            {
                if (person.symbol == 'M')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(person.symbol);
                    Console.ForegroundColor= ConsoleColor.White;
                }
                if (person.symbol == 'P')
                {  Console.ForegroundColor = ConsoleColor.Blue; 
                    Console.Write(person.symbol);
                    Console.ForegroundColor= ConsoleColor.White;
                }
                if (person.symbol == 'T')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(person.symbol);
                    Console.ForegroundColor= ConsoleColor.White;
                }
                
            }
        }
    }

    private static bool CheckPos(List<Person> personlista, int i, int j)
    {
        bool isPos = false;

        foreach (Person person in personlista)
        {
            if (person.KordY == i && person.KordX == j)
            {
                isPos = true;
            }
        }
        return isPos;
    }

    public static void CheckMeetings()
    {
        Random rnd = new Random();
        
        foreach (Person person1 in PersonLista)
        {
            foreach (Person person2 in PersonLista)
            {
                if (person1 is Tjuv && person2 is Medborgare && person1.KordX == person2.KordX && person1.KordY == person2.KordY)
                {
                    Tjuv tjuv = (Tjuv)person1;
                    Medborgare medborgare = (Medborgare)person2;

                    if (medborgare.Inventory.Count > 0)
                    {
                        Saker stolenItem = medborgare.Inventory[rnd.Next(medborgare.Inventory.Count)];

                        Events.Enqueue($"{tjuv.Name} stal {stolenItem.GetType().Name} från {medborgare.Name}");

                        medborgare.Inventory.Remove(stolenItem);
                        tjuv.Inventory.Add(stolenItem);
                    }
                }

                if (person1 is Tjuv && person2 is Polis && person1.KordX == person2.KordX && person1.KordY == person2.KordY)
                {
                    Tjuv tjuv = (Tjuv)person1;
                    Polis polis = (Polis)person2;

                    if (tjuv.Inventory.Count > 0)
                    {
                        tjuv.NumberOfConvicted++;
                        polis.Inventory.AddRange(tjuv.Inventory);
                        tjuv.Inventory.ForEach(item => { tjuv.JailTime += item.value * tjuv.NumberOfConvicted; });

                        Events.Enqueue($"{tjuv.Name} arresterades av {polis.Name}");

                        tjuv.Inventory.Clear();
                        Prison.Add(tjuv);
                        PersonLista.Remove(tjuv);
                        tjuv.KordX = Random.Shared.Next(1, 24);
                        tjuv.KordY = Random.Shared.Next(1, 9);
                        return;
                    }
                }
            }
        }
    }

}