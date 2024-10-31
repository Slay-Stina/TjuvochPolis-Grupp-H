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

    internal class plånbok : Saker
    {
        public plånbok() : base()
        {
            Name = "plånbok";
            Value = 5;
            ReturnTime = Value;
        }
    }

    internal class nycklar : Saker 
    {
        public nycklar() : base()
        {
            Name = "nycklar";
            Value = 4;
            ReturnTime = Value;
        }
    }

    internal class smycken : Saker 
    {
        public smycken() : base()
        {
            Name = "smycken";
            Value = 8;
            ReturnTime = Value;
        }
    }

    internal class klocka : Saker 
    {
        public klocka() : base()
        {
            Name = "klocka";
            Value = 6;
            ReturnTime = Value;
        }
    }
}
