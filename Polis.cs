using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace TjuvochPolis_Grupp_H;

internal class Polis : Person
{





    public Polis()
    {
        symbol = 'P';

    }

    private static List<string> polisNamn = File.ReadAllLines(@"polisnamn.txt").ToList();
    public static List<Polis> polisLista = SkapaPolis();

    private static List<Polis> SkapaPolis()
    {
        List<Polis> PolisList = new List<Polis>();

        foreach (string name in polisNamn)
        {
            Polis nyPolis = new Polis();
            nyPolis.Name = name;
            PolisList.Add(nyPolis);
        }
        return PolisList;
    }

}
