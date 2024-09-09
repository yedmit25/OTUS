using System.Drawing;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Linq.Expressions;
using System.Linq;

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

            Dictionary<string, string> paramsDictionary = new();
            Dictionary<string, bool> paramsValidate = new();

            Dictionary<string, string> ErrorValidate = new();
            List<Int64> paramsListValid = new ();

            string a = String.Empty;
            string b = String.Empty;
            string c = String.Empty;
            Console.WriteLine("Добро пожаловать.");
            Console.WriteLine("Решаем квадратное уравнение\n\ra * x^2 + b * x + c = 0");
            QuadraticEquation QuadraticEquation = new QuadraticEquation();

            //Quadra quadra = new Quadra();
            //quadra.b = b;
            //quadra.c = c;
            //quadra.a = a;


            while (true)
            {
                Console.WriteLine("Введите значение A:");
                a = Console.ReadLine();
                paramsDictionary.Add("a", a);
                Console.WriteLine("Введите значение B:");
                b = Console.ReadLine();
                paramsDictionary.Add("b", b);
                Console.WriteLine("Введите значение C:");
                c = Console.ReadLine();
                paramsDictionary.Add("c", c);
                try
                {
                    ValidateParams(paramsDictionary, paramsValidate, ErrorValidate, paramsListValid);
                    QuadraticEquation.Сomputation(paramsListValid[0], paramsListValid[1], paramsListValid[2]);
                    break;
                }
                catch (OtusException mes)
                {
                    FormatData(mes.Message, Severity.Warning, paramsDictionary, paramsValidate);
                }
                catch (FormatException ex)
                {
                    FormatData(ex.Message, Severity.Error, paramsDictionary, paramsValidate);
                }
                finally
                {

                    paramsDictionary.Clear();
                    paramsValidate.Clear();
                    paramsListValid.Clear();
                    ErrorValidate.Clear();
                }

            }



        }

        static void ValidateParams(Dictionary<string, string> paramsDictionary, Dictionary<string, bool> paramsValidate, Dictionary<string, string> ErrorValidate, List<Int64> paramsListValid)
        {
            bool isValidParam = false;;
            Int64 validParam = 0;

            foreach (KeyValuePair<string, string> entry in paramsDictionary)
            {

                try
                {
                    validParam = Int64.Parse(entry.Value);
                    paramsValidate.Add(entry.Key, true);
                    paramsListValid.Add(validParam);
                }
                catch (FormatException ex)
                {
                    ErrorValidate.Add(entry.Key, ex.Message);
                }
                catch (OverflowException ex)
                {
                    ErrorValidate.Add(entry.Key, ex.Message);
                }

                //isValidParam = Int64.TryParse(entry.Value, out validParam);
                //paramsValidate.Add(entry.Key, isValidParam);
                //paramsListValid.Add(validParam);
            }
            if (ErrorValidate.Count() != 0)
            {
                throw new FormatException(ErrorValidate.Values.First());
            } 

        }
        static void FormatData(string message, Severity severity, Dictionary<string, string> paramsDictionary, Dictionary<string, bool> paramsValidate)
        {
            if (severity == Severity.Error)
            {
                string line = "--------------------------------------------------";
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                string invalidParams = "";

                foreach (KeyValuePair<string, bool> entry in paramsValidate)
                {
                    if (entry.Value == false)
                    {
                        invalidParams = invalidParams + $"{entry.Key}; ";
                    }
                }
                string exceptinMessage = "";
                string listParams="";
                foreach (KeyValuePair<string, string> entry in paramsDictionary)
                {
                    listParams = listParams + $"{entry.Key} = {entry.Value}\n";
                }

                exceptinMessage = $"{line}\n{message} {invalidParams}\n{line}\n" +
                    $"{listParams}";

                Console.WriteLine(exceptinMessage);
                Console.ResetColor();
            } else if (severity == Severity.Warning)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine($"{message}\n");
                Console.ResetColor();
            }
            else
            {
                Console.ResetColor();
            }
        }

    }
}
