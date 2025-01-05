using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13_Poem
{
    internal class Part1
    {
        public ImmutableList<string> AddPart(ImmutableList<string> poem)
        {
            return poem.Add("Это дом, который построил Джек.");
        }
    }
}
