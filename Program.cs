using System.ComponentModel.Design;

namespace TjuvochPolis_Grupp_H
{
    internal class Program
    {
        //Här står det text
        static void Main(string[] args)
        {
            List<Person> personlista = new List<Person>();
            personlista.AddRange(Medborgare.medborgarLista);
            personlista.AddRange(Tjuv.tjuvLista);
            personlista.AddRange(Polis.polisLista);
            bool wrotesymbol = false;

            char[,] arr = new char[25,100];

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == arr.GetLength(0)-1 || j == arr.GetLength(1) - 1)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        wrotesymbol = false;
                        foreach (Person person in personlista) //metod som tar in i och j och skickar tillbaka bool och char om person e po kordinaterna
                        {
                            if (person.KordX == j && person.KordY == i)
                                Console.Write(person.symbol); 
                            wrotesymbol = true;
                            
                        }

                        {
                            Console.Write(' ');
                        }

                    }

                }
                Console.WriteLine();
            }

        }
    }
}
