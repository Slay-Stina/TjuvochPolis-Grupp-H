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
        symbol = "M";
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
            nyMedborgare.Inventory.Add($"{name}1", new MobilTelefon());
            nyMedborgare.Inventory.Add($"{name}2", new plånbok());
            nyMedborgare.Inventory.Add($"{name}3", new smycken());
            nyMedborgare.Inventory.Add($"{name}4", new klocka());
            nyMedborgare.Inventory.Add($"{name}5", new nycklar());

            medborgarList.Add(nyMedborgare);
        }
        return medborgarList;
    }
}
