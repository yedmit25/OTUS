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

            for (var i = 0; i < _size; i++)
            {

                mainStack.Add(slaveStack.Pop());
            }

        }
    }
}
