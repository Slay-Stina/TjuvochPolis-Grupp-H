using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace TjuvochPolis_Grupp_H;

internal class Polis : Person
{
    List<string> polisNamn = new List<string>
    {
        "Alexandro Moulton",
        "Skye Dukes",
        "Jaela Laws",
        "Alliyah Hutto",
        "April Shanahan",
        "Eden Dobbs",
        "Kahlil Craig",
        "Carter Adame",
        "Marguerite Corrigan",
        "Treyvon Knight"
    };
}
