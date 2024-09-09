using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Lesson
{
    public class Quadra
    {
        public string a;
        public string b;
        public string c;


        public void Discriminant(int a, int b, int c, out long result)
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
            long d;
            //Корни уровнения
            double x1 = 0;
            double x2 = 0;
            Discriminant(a, b, c, out d);
            if (d > 0)
            {

                x1 = (-b + Math.Sqrt(d)) / 2 * a;
                x2 = (-b - Math.Sqrt(d)) / 2 * a;
            }
            else
            {
                x1 = (-b + Math.Sqrt(d)) / 2 * a;
            }
            // Выводим решение уравнения
            Console.WriteLine($"x1 = {x1}\n" +
                $"{(d > 0 ? "x2 = " + x2 : "")}");

        }



    }
}
