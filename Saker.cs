using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvochPolis_Grupp_H
{
    internal class Saker
    {
        public string Owner { get; set; }
        public int value {  get; set; }

    }

    internal class MobilTelefon : Saker
    {
        public MobilTelefon()
        {
            value = 7;
        }
    }

    internal class plånbok : Saker
    {
        public plånbok()
        {
            value = 5; 
        }
    }

    internal class nycklar : Saker 
    {
        public nycklar()
        {
            value = 4;
        }
    }

    internal class smycken : Saker 
    {
        public smycken()
        {
            value = 8;
        }
    }

    internal class klocka : Saker 
    {
        public klocka()
        {
            value = 6;
        }
    }
}
