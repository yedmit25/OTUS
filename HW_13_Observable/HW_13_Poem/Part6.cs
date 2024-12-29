using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13_Poem
{
    internal class Part6
    {
        public List<string> Poem { get; private set; }

        public List<string> AddPart(List<string> collection)
        {
            Poem = new List<string>(collection)
        {
            "А это корова безрогая,\nЛягнувшая старого пса без хвоста,\nКоторый за шиворот треплет кота,\nКоторый пугает и ловит синицу,\nКоторая часто ворует пшеницу,\nКоторая в темном чулане хранится\nВ доме,\nКоторый построил Джек."
        };
            return Poem;
        }
    }
}
