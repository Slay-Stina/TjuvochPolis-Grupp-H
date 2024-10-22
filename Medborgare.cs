using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvochPolis_Grupp_H;

internal class Medborgare : Person
{
    public static List<string> medborgarNamn = File.ReadAllLines("medborgarNamn.txt").ToList();
    private static int medborgarNum = 0;

    public static Medborgare SkapaMedborgare()
    {
        Medborgare medborgare = new Medborgare();
        medborgare.Name = medborgarNamn[medborgarNum];
        medborgarNum++;

        return medborgare;
    }

}
