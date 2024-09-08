using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectClasses
{
    public static class StackExtensions
    {
        public static void Merge(this Stack mainStack, Stack slaveStack)
        {
            int _size = slaveStack.Size;

            List<string> _sttmList = new();

            for (var i = 0; i < _size; i++)
            {

                _sttmList.Add(slaveStack.Pop());
            }

            foreach (var sttm in _sttmList.Reverse<string>())
            {
                mainStack.Add(sttm);
            }

        }
    }
}
