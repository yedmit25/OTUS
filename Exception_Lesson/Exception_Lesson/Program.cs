using System.Security.Cryptography.X509Certificates;

namespace Exception_Lesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string a = String.Empty;
            string b = String.Empty;
            string c = String.Empty;
            Console.WriteLine("Добро пожаловать.");
            Console.WriteLine("Решаем квадратное уравнение\n\ra * x^2 + b * x + c = 0");

            //int a;
            //int b;
            //int c;

            QuadraticEquation QuadraticEquation = new QuadraticEquation();

            Console.WriteLine($"a Из класса QuadraticEquation {QuadraticEquation.a}");
            //try
            //{

                Console.WriteLine("Введите значение a");
                a = Console.ReadLine();
                Console.WriteLine("Введите значение b");
                b = Console.ReadLine();
                Console.WriteLine("Введите значение c");
                c = Console.ReadLine();

            if (int.TryParse(a, out int numValue))
            {
                QuadraticEquation.a = numValue;
            }
            else
            {
                Console.WriteLine("Ошибка формата");
            }
                //QuadraticEquation.a = Int32.Parse(input);

            //}

            //Console.WriteLine("Введите значение b");
            //QuadraticEquation.b = int.Parse(Console.ReadLine());

            //Console.WriteLine("Введите значение b");
            //QuadraticEquation.cc = Console.Read();

            //catch (FormatException)
            //{
            //    Console.WriteLine($"Unable to parse '{a}'");
            //    Console.WriteLine("косяк");
            //}
            //finally
            //{
            //    Console.WriteLine("Всё гуд");
            //}


        }

        //class QuadraticEquation
        //{ 
        //    public int aa;
        //    public int bb;
        //    public int cc;

        //    public void Print()
        //    {
        //        Console.WriteLine($"{aa}*x^2 + {bb}*x^+ {cc}  = 0");
        //    }
        //}

    }
}
