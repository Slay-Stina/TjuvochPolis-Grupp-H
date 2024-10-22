namespace TjuvochPolis_Grupp_H
{
    internal class Program
    {
        //Här står det text
        static void Main(string[] args)
        {
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
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }


            foreach (Medborgare unikMedborgare in Medborgare.medborgarLista)
            {

                Console.WriteLine(unikMedborgare.Name);

            }

        }
    }
}
