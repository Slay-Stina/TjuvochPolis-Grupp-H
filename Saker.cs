using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvochPolis_Grupp_H
{
    internal class Saker
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public int Value {  get; set; }
        public int ReturnTime {  get; set; }
    }

    internal class MobilTelefon : Saker
    {
        public MobilTelefon() : base()
        {
            Name = "mobiltelefon";
            Value = 7;
            ReturnTime = Value;
        }
    }

    internal class Plånbok : Saker
    {
        public Plånbok() : base()
        {
            Name = "plånbok";
            Value = 5;
            ReturnTime = Value;
        }
    }

    internal class Babyoil : Saker 
    {
        public Babyoil() : base()
        {
            Name = "babyolja";
            Value = 4;
            ReturnTime = Value;
        }
    }

    internal class Smycken : Saker 
    {
        public Smycken() : base()
        {
            Name = "smycken";
            Value = 8;
            ReturnTime = Value;
        }
    }

    internal class Klocka : Saker 
    {
        public Klocka() : base()
        {
            Name = "klocka";
            Value = 6;
            ReturnTime = Value;
        }
    }
}
