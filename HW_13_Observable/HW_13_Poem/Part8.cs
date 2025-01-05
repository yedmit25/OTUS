using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13_Poem
{
    internal class Part8
    {
        public ImmutableList<string> AddPart(ImmutableList<string> poem)
        {
            return poem.Add("А это ленивый и толстый пастух,\nКоторый бранится с коровницей строгою,\n");
        }
    }
}
