using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvochPolis_Grupp_H;

internal class Medborgare : Person
{
 

    

    public Medborgare(int kordX, int kordY, int[,] kord)
    {
        KordX = kordX;
        KordY = kordY;
        Kord = kord;
        Saker = new List<string>();
    }




    private static List<string> medborgarNamn = File.ReadAllLines($"medborgarNamn.txt").ToList();
    public static List<Medborgare> medborgarLista = SkapaMedborgare();

    private static List<Medborgare> SkapaMedborgare()
    {
        List<Medborgare> medborgarList = new List<Medborgare>();

        foreach (string name in medborgarNamn)
        {
            Medborgare nyMedborgare = new Medborgare();
            nyMedborgare.Name = name;
            medborgarList.Add(nyMedborgare);
        }
        return medborgarList;
    }
}
