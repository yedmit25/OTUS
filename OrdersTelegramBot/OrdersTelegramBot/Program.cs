using InteractiveMenuTelegramBot;
using System.Reflection;

namespace OrdersTelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string? userName = "";
            User user = new User();
            string? commandValue;


            Console.WriteLine($"Добро пожаловать! Это Telegram - бот. Для вас доступны команды {MenuItems.Start}, {MenuItems.Help},  {MenuItems.Info}, {MenuItems.Exit}.");

            do
            {
                Console.Write("-->");
                commandValue = Console.ReadLine();
                ExecuteCommand(commandValue, user);
            }
            while (commandValue != MenuItems.Exit);


        }

        static void ExecuteCommand(string? command, User? user)
        {
            Utils utils = new Utils();

            if (command != null)
            {
                if (user.name != "")
                    //if (user.name != "")
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
                        user.name = Console.ReadLine();

                        Console.WriteLine($"Добро пожаловать, {user.name}");
                        break;
                    case MenuItems.Help:
                        Console.WriteLine($"{(user.name != "" ? user.name + ",\n" : "")}{MenuItems.HelpConstant}");
                        break;
                    case MenuItems.Info:
                        Console.WriteLine("info");
                        Console.WriteLine(string.Format($"{(user.name != "" ? user.name + ",\n" : "")}Текущая версия системы: {Assembly.GetExecutingAssembly().GetName().Version}\nДата создания: {Utils.GetBuildDate(Assembly.GetExecutingAssembly()):d}"));
                        break;
                    case MenuItems.Exit:
                        Console.WriteLine($"Пока {(user.name != "" ? ", " + user.name : "")}");
                        break;
                }
            }

        }
    }
}
