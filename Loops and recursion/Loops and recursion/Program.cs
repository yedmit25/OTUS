
using System.Diagnostics;


namespace Loops_and_recursion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //создаем объект
            Stopwatch stopwatch = new Stopwatch();

            // засекем время выполнения каждой отдельной рекурсии 
            stopwatch.Start();
            int fib4 = Fibanaci(5);
            Console.WriteLine($" N = 5 -- выполнение рекурсии  заняло:\t{stopwatch.ElapsedMilliseconds,8}");

            stopwatch.Start();
            int fib5 = Fibanaci(10);
            Console.WriteLine($" N = 10 -- выполнение рекурсии  заняло:\t{stopwatch.ElapsedMilliseconds,8}");

            stopwatch.Start();
            int fib6 = Fibanaci(20);
            Console.WriteLine($" N = 20 -- выполнение рекурсии  заняло:\t{stopwatch.ElapsedMilliseconds,8}");

            // Выводим расчет вычисление функции Фабиначи по рекурсии

            Console.WriteLine("Вычисление по рекурсии:");
            Console.WriteLine($" F(5) = {fib4}");
            Console.WriteLine($" F(10) = {fib5}");
            Console.WriteLine($" F(20) = {fib6}");

            //Вычисляем время вполнения функции Фабиначи по циклу
            stopwatch.Start();
            int fib7 = Fibonaci_loops(5);
            Console.WriteLine($" N = 5 -- выполнение по циклу заняло:\t{stopwatch.ElapsedMilliseconds,8}");
            stopwatch.Start();
            int fib8 = Fibonaci_loops(10);
            Console.WriteLine($" N = 10 -- выполнение по циклу заняло:\t{stopwatch.ElapsedMilliseconds,8}");

            stopwatch.Start();
            int fib9 = Fibonaci_loops(20);
            Console.WriteLine($" N = 20 -- выполнение по циклу заняло:\t{stopwatch.ElapsedMilliseconds,8}");

            Console.WriteLine("Вычисление по циклу:");
            Console.WriteLine($" F(5) = {fib7}");
            Console.WriteLine($" F(10) = {fib8}");
            Console.WriteLine($" F(20) = {fib9}");

        }
        static public int Fibanaci(int n)
        {

            if (n == 0 || n == 1) return n;

            return Fibanaci(n - 1) + Fibanaci(n - 2);
        }

        static public int Fibonaci_loops(int n)
        {
            int result = 0;
            int b = 1;
            int tmp;

            for (int i = 0; i < n; i++)
            {
                tmp = result;
                result = b;
                b += tmp;
            }

            return result;
        }
    }
}
