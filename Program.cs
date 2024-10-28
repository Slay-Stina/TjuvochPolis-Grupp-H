using System.ComponentModel.Design;

namespace TjuvochPolis_Grupp_H;

internal class Program
{
    //Här står det text
    static void Main(string[] args)
    {
        List<Person> personlista = new List<Person>();
        personlista.AddRange(Medborgare.medborgarLista);
        personlista.AddRange(Tjuv.tjuvLista);
        personlista.AddRange(Polis.polisLista);
        //bool wrotesymbol = false;

        char[,] arr = new char[25,100];

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
            Console.WriteLine();
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

}