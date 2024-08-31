using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Lesson
{
    internal class QuadraticEquation
    {

        public int ?a;
        public int ?b;
        public int ?c;

        public void Print()
        {
            Console.WriteLine($"{a}*x^2 + {b}*x^+ {c}  = 0");
        }
    }
}
