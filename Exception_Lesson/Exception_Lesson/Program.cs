using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
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
            bool Abool = false;
            bool Bbool = false;
            bool Cbool = false;
            bool Dbool = true;

            while (Dbool)
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
                    Dbool = false;

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

        static void Validate(string a, string b, string c)
        {
            int A;
            bool Abool = Int32.TryParse(a, out A);
            int B;
            bool Bbool = Int32.TryParse(b, out B);
            int C;
            bool Cbool = Int32.TryParse(c, out C);

            if (!Abool || !Bbool || !Cbool)
            {
                string Message = "Неверный формат параметра";
                Message = $" {(Abool != true ? Message + " a: " + a : "")}\n" +
                    $" {(Bbool != true ? Message + " b: " + b : "")}\n" +
                    $"{(Cbool != true ? Message + " c: " + c : "")}";

                throw new Exception(Message);
            }


        }
    }
}
