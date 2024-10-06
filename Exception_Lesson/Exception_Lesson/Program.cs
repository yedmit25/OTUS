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
        enum ExceptionType
        {
            Overflow,
            Format
        }

        static void Main(string[] args)
        {
            //// Определяем справочник сообщений об ошибках
            //Dictionary<ExceptionType, string> ExceptionDictionary = new();
            //ExceptionDictionary.Add(ExceptionType.Overflow, "Введённое значение должно находиться в диапозоне от -2147483648 до 2147483647");
            //ExceptionDictionary.Add(ExceptionType.Format, "Неверный формат параметра");
            //
            Dictionary<string, string> inputParams = new();
            Dictionary<string, bool> validateParams = new();
            Dictionary<string, ExceptionType> errorParams = new();
            List<int> outputParams = new ();

            Console.WriteLine("Добро пожаловать.");
            Console.WriteLine("Решаем квадратное уравнение\n\ra * x^2 + b * x + c = 0");
            QuadraticEquation QuadraticEquation = new QuadraticEquation();
            while (true)
            {
                InputValues(inputParams);
                try
                {
                    ValidateParams(inputParams, validateParams, outputParams, errorParams);
                    QuadraticEquation.OutPut(outputParams);
                    QuadraticEquation.Сomputation(outputParams[0], outputParams[1], outputParams[2]);
                    break;
                }
                catch (InputException ex)
                {
                    FormatData(ex.Message, Severity.Error, inputParams, validateParams, errorParams);
                }
                catch (OtusException ex)
                {
                    FormatData(ex.Message, Severity.Warning, inputParams, validateParams, errorParams);
                }
                finally
                {

                    inputParams.Clear();
                    validateParams.Clear();
                    outputParams.Clear();
                    errorParams.Clear();
                }

            }



        }

        static void InputValues(Dictionary<string, string> inputParams)
        {
            string a = String.Empty;
            string b = String.Empty;
            string c = String.Empty;
            Console.WriteLine("Введите значение A:");
            a = Console.ReadLine();
            inputParams.Add("a", a);
            Console.WriteLine("Введите значение B:");
            b = Console.ReadLine();
            inputParams.Add("b", b);
            Console.WriteLine("Введите значение C:");
            c = Console.ReadLine();
            inputParams.Add("c", c);
        }

        static void ValidateParams(Dictionary<string, string> inputParams, Dictionary<string, bool> validateParams, List<int> outputParams, Dictionary<string, ExceptionType> errorParams)
        {
            bool isValidParam = false;;
            int validParam;

            foreach (KeyValuePair<string, string> entry in inputParams)
            {

                try
                {
                    outputParams.Add(int.Parse(entry.Value));
                }
                catch (FormatException ex)
                {
                    errorParams.Add(entry.Key, ExceptionType.Format);
                }
                catch (OverflowException ex)
                {
                    errorParams.Add(entry.Key, ExceptionType.Overflow);
                }
                    
            }
            if (errorParams.Count()!=0)
            {
                ExceptionType index = ExceptionType.Overflow;

                string message = "Неверный формат параметра";
                throw new InputException(message);
            }

        }
        static void FormatData(string message, Severity severity, Dictionary<string, string> inputParams, Dictionary<string, bool> validateParams, Dictionary<string, ExceptionType> errorParams)
        {

            if (severity == Severity.Error)
            {
                string listParams = "";
                string invalidParams = "";

                // Собираем сообщение об ошибке формата
                if (errorParams.ContainsValue(ExceptionType.Format))
                {
                    string line = "--------------------------------------------------";

                    string exceptionMessageFormat = "";
                    foreach (KeyValuePair<string, ExceptionType> entry in errorParams)
                    {
                        if (entry.Value == ExceptionType.Format)
                        {
                            invalidParams = invalidParams + $"{entry.Key}; ";

                            listParams = listParams + $"{entry.Key} = {inputParams[entry.Key]}\n";
                        }

                    }

                    exceptionMessageFormat = $"{line}\n{message} {invalidParams}\n{line}\n" +
                        $"{listParams}";

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(exceptionMessageFormat);

                    Console.ResetColor();

                }

                // собираем ошибку, если превышает допустимый диапозон для нашего типа
                if (errorParams.ContainsValue(ExceptionType.Overflow))
                {
                    listParams = "";
                    invalidParams = "";
                    string exceptionMessageOverflow = "";

                    foreach (KeyValuePair<string, ExceptionType> entry in errorParams)
                    {
                        if (entry.Value == ExceptionType.Overflow)
                        {
                            invalidParams = invalidParams + $"{entry.Key}; ";

                            listParams = listParams + $"{entry.Key} = {inputParams[entry.Key]}\n";
                        }

                    }
                    message = "Введённое значение должно находится в диапозоне от -2147483648 до 2147483647";
                    exceptionMessageOverflow = $"{message} {invalidParams}\n" +
                        $"{listParams}";
                    Console.ResetColor();
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(exceptionMessageOverflow);

                    Console.ResetColor();
                }
                

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
