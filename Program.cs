using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;

namespace TjuvochPolis_Grupp_H;

internal class Program
{
    public static int ChangeDir = 20;
    public static int CountToDir = 0;
    public static Random Rnd = new Random();
    static void Main(string[] args)
    {
        List<Person> personlista = new List<Person>();
        personlista.AddRange(Medborgare.medborgarLista);
        personlista.AddRange(Tjuv.tjuvLista);
        personlista.AddRange(Polis.polisLista);
        //bool wrotesymbol = false;

        char[,] arr = new char[25,100];
        //personlista.Sort((x,y) => x.KordY.CompareTo(y.KordY));

        //foreach (Person p in personlista)
        //{
        //    Console.WriteLine(p.KordX + " , " + p.KordY + "\t" + p.Name + "\t" + p.GetType().Name);
        //}
        while (true)
        {
            CheckMeetings();
            Console.Clear();
            MovePerson(personlista);
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
                        if (CheckPos(personlista, i, j))
                        {
                            ShowSymbol(personlista, i, j);
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
            foreach (Tjuv tjuv in Tjuv.tjuvLista)
            { 
                Console.WriteLine(tjuv.TjuvIStad + " " + tjuv.JailTime); 
            }
            Thread.Sleep(1);
            CountToDir++;
            if (CountToDir == ChangeDir)
            {
                foreach(Person person in personlista)
                {
                    person.DirX = Rnd.Next(3);
                    person.DirY = Rnd.Next(3);
                }
                CountToDir = 0;
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
        

        foreach (Tjuv tjuv in Tjuv.tjuvLista)
        {
            foreach (Medborgare medborgare in Medborgare.medborgarLista)
            {
                if (tjuv.KordX == medborgare.KordX && tjuv.KordY == medborgare.KordY && tjuv.TjuvIStad)
                {
                    if (medborgare.Inventory.Count > 0)
                    { 
                    Saker stolenItem = medborgare.Inventory[rnd.Next(medborgare.Inventory.Count)];

                    medborgare.Inventory.Remove(stolenItem);
                    tjuv.Inventory.Add(stolenItem);
                    }
                }
                        
            }
        }

        foreach (Tjuv tjuv in Tjuv.tjuvLista)
        {
            foreach (Polis polis in Polis.polisLista)
            {
                if (tjuv.KordX == polis.KordX && tjuv.KordY == polis.KordY && tjuv.TjuvIStad)
                {
                    if(tjuv.Inventory.Count > 0)
                    { 
                        polis.Inventory.AddRange(tjuv.Inventory);
                        tjuv.Inventory.ForEach(item => { tjuv.JailTime += item.value * tjuv.NumberOfTheft ;});

                        tjuv.Inventory.Clear();
                        tjuv.TjuvIStad = false;

                    }
                }

            }
        }
        
    }

}