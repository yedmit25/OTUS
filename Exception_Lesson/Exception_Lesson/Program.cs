using System.Drawing;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace Exception_Lesson
{
    internal class Program
    {
        enum Severity
        {
            Warning,
            Error,
            Information,
        }
        static void Main(string[] args)
        {

            string a = String.Empty;
            string b = String.Empty;
            string c = String.Empty;
            Console.WriteLine("Добро пожаловать.");
            Console.WriteLine("Решаем квадратное уравнение\n\ra * x^2 + b * x + c = 0");
            bool Abool = false;
            bool Bbool = false;
            bool Cbool = false;
            QuadraticEquation quadraticEquation = new QuadraticEquation();

            while (true)
            {
                Console.WriteLine("Введите значение A:");
                a = Console.ReadLine();
                Console.WriteLine("Введите значение B:");
                b = Console.ReadLine();
                Console.WriteLine("Введите значение C:");
                c = Console.ReadLine();

                try
                {
                    Validate(a, b, c);
                    break;
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }

            }

            quadraticEquation.Сomputation(a, b, c);

        }

        static void Validate(string a, string b, string c)
        {
            double A;
            bool Abool = double.TryParse(a, out A);
            double B;
            bool Bbool = double.TryParse(b, out B);
            double C;
            bool Cbool = double.TryParse(c, out C);

            if (!Abool || !Bbool || !Cbool)
            {
                string line = "--------------------------------------------------";
                String Message = "Неверный формат параметра:";
                string Params;
                Params = $"{(Abool != true ?  "a;" : "")}" +
                    $"{(Bbool != true ? " b;" : "")}" +
                    $"{(Cbool != true ? " c;" : "")}\n" +
                    $"{line}\n" +
                    $"a = {a}\n" +
                    $"b = {b}\n" +
                    $"c = {c}\n";
                Message = line + "\n" + Message + Params;
                throw new Exception(Message);
            }
        }

    }
}
