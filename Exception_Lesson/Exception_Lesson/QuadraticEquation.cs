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

        public void Discriminant(int a, int b, int c, out double result)
        {
            //Вычисляем дискриминант
            result = (long)Math.Pow(b, 2) - (long)(4 * a * c);
            if (result < 0)
            {
                string message = "Вещественных значений не найдено:";
                throw new OtusException(message);
            }
        }
        public void Сomputation(int a, int b, int c)
        {
            //Дискриминант
            double d;
            //Корни уровнения
            double x1 = 0;
            double x2 = 0;
            Discriminant(a, b, c, out d);
            if (d>0)
            {

                x1 = (-b + Math.Sqrt(d)) / 2 * a;
                x2 = (-b - Math.Sqrt(d)) / 2 * a;
            } else
            {
                x1 = (-b + Math.Sqrt(d)) / 2 * a;
            }
            // Выводим решение уравнения
            Console.WriteLine($"x1 = {x1}\n" +
                $"{(d > 0 ? "x2 = " + x2 : "")}");
        
         }
        public void OutPut(List<int> outputParams)
        {

            int a = +1 * outputParams[0];
            int b = +1 * outputParams[1];
            int c = +1 * outputParams[2];

            Console.WriteLine($"Решаем квадратное уравнение\n\r{a} * x^2 {(b < 0 ? "":"+")} {b} * x {(c < 0 ? "" : "+")} {c} = 0");
        }

    }
}
