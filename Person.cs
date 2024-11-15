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
    public static Random rnd = new Random();
    public string Name { get; set; }

    public int KordX = rnd.Next(1, 99);

    public int KordY = rnd.Next(1, 24);



    public string symbol { get; set; }
    public int DirX = rnd.Next(3);
    public int DirY = rnd.Next(3);

    public Dictionary<string,Saker> Inventory = new Dictionary<string,Saker>();

}

