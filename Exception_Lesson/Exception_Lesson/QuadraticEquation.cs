using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Lesson
{
    internal class QuadraticEquation
    {

        //public int ?a;
        //public int ?b;
        //public int ?c;

        //public void Print()
        //{
        //    Console.WriteLine($"{a}*x^2 + {b}*x^+ {c}  = 0");
        //}

        public void Discriminant(double A, double B, double C, out double result)
        {
            result = (long)Math.Pow(B, 2) - (long)(4 * A * C);
            if (result < 0)
            {
                string Message = "Нет корней";
                throw new Exception(Message);
            }
        }
        public void Сomputation(string a, string b, string c)
        {
            double D;
            double A;
            double B;
            double C;
            A = double.Parse(a);
            B = double.Parse(b);
            C = double.Parse(c);

            try
            {

                double x1 = 0;
                double x2 = 0;
                Discriminant(A, B, C, out D);
                if (D>0)
                {

                    x1 = (-B + Math.Sqrt(D)) / 2 * A;
                    x2 = (-B - Math.Sqrt(D)) / 2 * A;
                } else
                {
                    x1 = (-B + Math.Sqrt(D)) / 2 * A;
                }


                Console.WriteLine($"x1 = {x1}\n" +
                    $"{(D > 0 ? "x2 = " + x2 : "")}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
