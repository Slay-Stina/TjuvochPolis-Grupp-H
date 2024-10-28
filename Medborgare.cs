using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvochPolis_Grupp_H;

internal class Medborgare : Person
{
 
    public Medborgare() 
    {

        Inventory.Add(new MobilTelefon());
        Inventory.Add(new plånbok());
        Inventory.Add(new smycken());
        Inventory.Add(new klocka());
        Inventory.Add(new nycklar());
        symbol = 'M';


    }


    private static List<string> medborgarNamn = File.ReadAllLines(@"..\..\..\medborgarNamn.txt").ToList();
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
