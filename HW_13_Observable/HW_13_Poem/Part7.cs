using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13_Poem
{
    internal class Part7
    {
        public ImmutableList<string> AddPart(ImmutableList<string> poem)
        {
            return poem.Add("А это старушка, седая и строгая,\nКоторая доит корову безрогую,\nЛягнувшую старого пса без хвоста,\nКоторый за шиворот треплет кота,\nКоторый пугает и ловит синицу,\nКоторая часто ворует пшеницу,\nКоторая в темном чулане хранится\nВ доме,\nКоторый построил Джек.");
        }
    }
}
