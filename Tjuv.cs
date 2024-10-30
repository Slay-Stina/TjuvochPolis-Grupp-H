using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvochPolis_Grupp_H;

internal class Tjuv : Person
{ 
public bool TjuvIStad { get; set; }

public int JailTime = 0;

public int NumberOfConvicted = 0;
  


    private static List<string> tjuvNamn = File.ReadAllLines(@"..\..\..\TjuvNamn.txt").ToList();
    public static List<Tjuv> tjuvLista = SkapaTjuv();


    public Tjuv()
    {
        symbol = 'T';

        TjuvIStad = true;
        
    }

    private static List<Tjuv> SkapaTjuv() 
    {
        List<Tjuv> TjuvList = new List<Tjuv>();

        foreach (string name in tjuvNamn)
        {
            Tjuv nyTjuv = new Tjuv();
            nyTjuv.Name = name;
            TjuvList.Add(nyTjuv);
        }
        return TjuvList;
    }
}


