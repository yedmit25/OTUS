using Microsoft.VisualBasic;
using System.ComponentModel.Design;
using System.Reflection;

namespace InteractiveMenuTelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? userName = "";
            string? commandValue;
            Console.WriteLine($"Добро пожаловать! Это Telegram - бот. Для вас доступны команды {MenuItems.Start}, {MenuItems.Help}, {MenuItems.Info}, {MenuItems.Exit}.");
            do
            {
                Console.Write("-->");
                commandValue = Console.ReadLine();
                executeCommand(commandValue);
            }
            while (commandValue != MenuItems.Exit);

            void executeCommand(string? command)
            {
                if (command != null)
                {
                    if (userName != "" && command.TrimStart().StartsWith($"{MenuItems.Echo}"))
                    {
                        List<string> wordsOfCommand = new(command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                        wordsOfCommand.RemoveAt(0);
                        string result = string.Join(" ", wordsOfCommand);
                        Console.WriteLine(result);
                    }
                    switch (command)
                    {
                        case MenuItems.Start:
                            Console.WriteLine("Введите имя пользователя");
                            userName = Console.ReadLine();
                            break;
                        case MenuItems.Help:
                                Console.WriteLine($"{(userName != "" ? userName + ",\n" : "")}{MenuItems.HelpConstant}");
                            break;
                        case MenuItems.Info:
                            Console.WriteLine("info");
                            Console.WriteLine(string.Format($"{(userName != "" ? userName + ",\n" : "")}Текущая версия системы: {Assembly.GetExecutingAssembly().GetName().Version}\nДата создания: {Utils.GetBuildDate(Assembly.GetExecutingAssembly()):d}"));
                            break;
                        case MenuItems.Exit:
                            Console.WriteLine($"Пока{(userName != "" ? ", " + userName : "")}");
                            break;
                    }


                }
            }
        }
    }
}
