using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace TjuvochPolis_Grupp_H;

internal class Person
{
    public string Name { get; set; }

    public int KordX { get; set; }

    public int KordY { get; set; }

    public int[,] Kord { get; set; }

    public List<string> Saker {  get; set; } 

}

