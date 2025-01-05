using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13_Poem
{
    internal class Part4
    {
        public ImmutableList<string> AddPart(ImmutableList<string> poem)
        {
            return poem.Add("А это веселая птица-синица,\nКоторая часто ворует пшеницу,\nКоторая в темном чулане хранится\nВ доме,\nКоторый построил Джек.");
        }
    }
}
