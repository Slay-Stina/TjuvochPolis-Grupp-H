﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvochPolis_Grupp_H;

internal class Tjuv : Person
{ 
    public bool TjuvStad { get; set; }




    private static List<string> tjuvNamn = File.ReadAllLines(@"tjuvnamn.txt").ToList();
    public static List<Tjuv> tjuvLista = SkapaTjuv();


    public Tjuv()
    {
        symbol = 'T';

        TjuvStad = true;

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


