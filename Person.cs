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

    public int KordX = rnd.Next(1, 100);

    public int KordY = rnd.Next(1, 25);

    public char symbol { get; set; }
    public int[,] Kord { get; set; }

    public List<Saker> Inventory = new List<Saker>();

}

