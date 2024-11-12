using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System;
using System.Drawing;
using System.Text;

namespace TjuvochPolis_Grupp_H;

internal class Program
{
    public static int ChangeDir = 20;
    public static int CountToDir = 0;
    public static Random Rnd = new Random();
    public static List<Person> Prison = new List<Person>();
    public static List<Person> PersonLista = new List<Person>();
    public static Queue<string> Events = new Queue<string>();
    public static SortedList<string,int> HighScore = new SortedList<string,int>();
    public static int Speed = 500;
    public static bool RaveMode = false;

    protected static int origRow;
    protected static int origCol;

    protected static void WriteAt(string s, int x, int y)
    {
        try
        {
            Console.SetCursorPosition(origCol + x, origRow + y);
            Console.Write(s);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.SetWindowSize(150,40);
        Console.SetBufferSize(150,40);

        PersonLista.AddRange(Medborgare.medborgarLista);
        PersonLista.AddRange(Tjuv.tjuvLista);
        PersonLista.AddRange(Polis.polisLista);

        char[,] city = new char[25,100];
        char[,] prison = new char[10,25];

        while (true)
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            PrisonTime();
            ReturnTime();
            
            DrawCity(city);
            CheckMeetings();
            MovePerson();
            DrawPrison(prison);

            PrintHighscore();

            if (Events.Count > 0)
            {
                PrintEvent();
            }

            if(Console.KeyAvailable)
            {
                PlaySpeed();
            }
            WriteAt("↑↓ Använd piltangenterna för att ändra hastighet", 101, 11);
            WriteAt($"Speed - {Speed}", 101, 12);
            if (Speed == 0)
            {
                Speed = 1;
            }
            Thread.Sleep(Speed);

            CountToDir++;
            if (CountToDir == ChangeDir)
            {
                foreach (Person person in PersonLista)
                {
                    person.DirX = Rnd.Next(3);
                    person.DirY = Rnd.Next(3);
                }
                CountToDir = 0;
            }
        }
    }

    private static void Rave()
    {
        while (RaveMode)
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            foreach (ConsoleColor color in colors)
            {
                if (!(color == ConsoleColor.White) && !(color == ConsoleColor.Black))
                {
                    Console.BackgroundColor = color;
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
    }

    private static void PrintHighscore()
    {
        var sortedHighscore = HighScore.OrderBy(KeyValuePair =>  KeyValuePair.Value).ToList();
        sortedHighscore.Reverse();
        int i = 27;
        int color = 1;
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        WriteAt("#HIGHSCORE#", 3, 26);
        Console.ForegroundColor = ConsoleColor.White;
        
        foreach(var score in sortedHighscore)
        {
            Console.ForegroundColor = (ConsoleColor)color;
            WriteAt($"{score.Key} - {score.Value}",0,i);
            i++;
            color++;
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static void ReturnTime()
    {
        foreach(Person polis in PersonLista)
        {
            if(polis is Polis && polis.Inventory.Count > 0)
            {
                foreach(KeyValuePair<string,Saker> sak in polis.Inventory)
                {
                    sak.Value.ReturnTime--;
                    if(sak.Value.ReturnTime == 0)
                    {
                        foreach(Person medborgare in PersonLista)
                        {
                            if (sak.Key.Contains(medborgare.Name))
                            {
                                medborgare.Inventory.Add(sak.Key,sak.Value);
                                sak.Value.ReturnTime = sak.Value.Value;
                                Events.Enqueue($"{medborgare.Name} fick tillbaka {sak.Value.Name} från {polis.Name}");
                                polis.Inventory.Remove(sak.Key);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    private static void DrawPrison(char[,] prison)
    {
        WriteAt("FÄNGELSE", 109, 14);
        for (int i = 15; i < prison.GetLength(0) + 15; i++)
        {
            for (int j = 101; j < prison.GetLength(1) + 101; j++)
            {
                if (i == 15 || j == 101 || i == prison.GetLength(0) + 14 || j == prison.GetLength(1) + 100)
                {
                    WriteAt("X", j, i);
                }
            }
        }
        PrisonList();
    }

    private static void PrisonList()
    {
        int i = 16;
        foreach (Tjuv tjuv in Prison)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteAt($"{tjuv.Name} - {tjuv.JailTime}", 103, i);
            Console.ForegroundColor = ConsoleColor.White;
            i++;
        }
    }

    private static void DrawCity(char[,] city)
    {
        for (int i = 0; i < city.GetLength(0); i++)
        {
            for (int j = 0; j < city.GetLength(1); j++)
            {
                if (i == 0 || j == 0 || i == city.GetLength(0) - 1 || j == city.GetLength(1) - 1)
                {
                    WriteAt("X", j, i);
                }
                else
                {
                    ShowSymbol(i, j);
                }
            }
        }
    }

    private static void PlaySpeed()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            if(Speed == 1)
            { 
                Speed = 100; 
            }
            else
            { 
                Speed += 100; 
            }
        }
        if (keyInfo.Key == ConsoleKey.DownArrow && Speed > 0 && !(Speed == 1))
        {
            Speed -= 100;
        }
        /* ############# RAVE ############## */
        Thread raveThread = null;  // Declare raveThread outside the if statement

        if (keyInfo.Key == ConsoleKey.Spacebar)
        {
            if (RaveMode == false)
            {
                RaveMode = true;
                if (raveThread == null || !raveThread.IsAlive)  // Check if raveThread is already running
                {
                    raveThread = new Thread(new ThreadStart(Rave));
                    raveThread.Start();
                }
            }
            else
            {
                RaveMode = false;
            }
        }
    }

    private static void PrintEvent()
    {
        int i = 0;
        foreach (string happens in Events)
        {
            if(happens.Contains("stal"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteAt(happens, 101, i);
                Console.ForegroundColor = ConsoleColor.White;
            }
            if(happens.Contains("arresterades"))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                WriteAt(happens, 101, i);
                Console.ForegroundColor = ConsoleColor.White;
            }
            if(happens.Contains("tillbaka"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                WriteAt(happens, 101, i);
                Console.ForegroundColor = ConsoleColor.White;
            }
            i++;
        }
        if (Events.Count > 10)
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

    private static void MovePerson()
    {
        foreach (Person person in PersonLista)
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
            if (person.KordX <= 0)
            {
                person.KordX = 98;
            }
            if (person.KordY <= 0) 
            { 
                person.KordY = 23; 
            }
            if (person.KordX >= 99)
            {
                person.KordX = 1;
            }
            if (person.KordY >= 24)
            {
                person.KordY = 1;
            }
        }
    }

    private static void ShowSymbol(int i, int j)
    {
        foreach (Person person in PersonLista)
        {
            if (person.KordY == i && person.KordX == j)
            {
                if (person.symbol == "M")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    WriteAt(person.symbol, j, i);
                    Console.ForegroundColor= ConsoleColor.White;
                }
                if (person.symbol == "P")
                {  
                    Console.ForegroundColor = ConsoleColor.Blue;
                    WriteAt(person.symbol, j, i);
                    Console.ForegroundColor= ConsoleColor.White;
                }
                if (person.symbol == "T")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteAt(person.symbol, j, i);
                    Console.ForegroundColor= ConsoleColor.White;
                }
                
            }
        }
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
                    EventMarker(person1);
                    Tjuv tjuv = (Tjuv)person1;
                    Medborgare medborgare = (Medborgare)person2;

                    if (medborgare.Inventory.Count > 0)
                    {
                        int randomIndex = rnd.Next(medborgare.Inventory.Count);
                        KeyValuePair<string,Saker> randomItem = medborgare.Inventory.ElementAt(randomIndex);

                        Events.Enqueue($"{tjuv.Name} stal {randomItem.Value.Name} från {medborgare.Name}");

                        medborgare.Inventory.Remove(randomItem.Key);
                        tjuv.Inventory.Add(randomItem.Key,randomItem.Value);
                    }
                }

                if (person1 is Tjuv && person2 is Polis && person1.KordX == person2.KordX && person1.KordY == person2.KordY)
                {
                    Tjuv tjuv = (Tjuv)person1;
                    Polis polis = (Polis)person2;

                    if (tjuv.Inventory.Count > 0)
                    {
                        EventMarker(person1);
                        tjuv.NumberOfConvicted++;
                        foreach (KeyValuePair<string, Saker> item in tjuv.Inventory)
                        {
                            polis.Inventory.Add(item.Key, item.Value);
                            tjuv.JailTime += item.Value.Value * tjuv.NumberOfConvicted;
                            tjuv.Score += item.Value.Value;
                        }
                        AddHighscore(tjuv);
                        
                        tjuv.Score = 0;

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

    private static void EventMarker(Person person1)
    {
        Console.BackgroundColor = ConsoleColor.White;
        WriteAt(" ", person1.KordX,person1.KordY);
        Console.BackgroundColor = ConsoleColor.Black;
    }

    private static void AddHighscore(Tjuv tjuv)
    {
        if (!HighScore.ContainsKey(tjuv.Name))
        {
            HighScore.Add(tjuv.Name, tjuv.Score);
        }
        if (HighScore[tjuv.Name] < tjuv.Score)
        {
            HighScore[tjuv.Name] = tjuv.Score;
        }
        else
        {
            return;
        }
    }
}