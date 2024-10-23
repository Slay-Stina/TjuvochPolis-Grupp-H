using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvochPolis_Grupp_H;

internal class Tjuv : Person
{ 
    public bool TjuvStad { get; set; }

    



    List<string> tjuvNamn = new List<string>
    {
        "P Diddy",
        ""
    };
    public Tjuv(int kordX,int kordY,int[,] kord)
    {
        KordX = kordX;
        KordY = kordY;
        Kord = kord;
        TjuvStad = true;
        Saker = new List<string>();
    }
}


