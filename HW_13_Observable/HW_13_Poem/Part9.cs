using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13_Poem
{
    internal class Part9
    {
        public List<string> Poem { get; private set; }

        public List<string> AddPart(List<string> collection)
        {
            Poem = new List<string>(collection)
        {
            "Вот два петуха,\nКоторые будят того пастуха,\nКоторый бранится с коровницей строгою,\nКоторая доит корову безрогую,\nЛягнувшую старого пса без хвоста,\nКоторый за шиворот треплет кота,\nКоторый пугает и ловит синицу,\nКоторая часто ворует пшеницу,\nКоторая в темном чулане хранится\nВ доме,\nКоторый построил Джек."
        };
            return Poem;
        }
    }
}
