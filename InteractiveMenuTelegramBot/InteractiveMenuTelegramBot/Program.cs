namespace InteractiveMenuTelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? userName;
            string? commandValue;
            Console.WriteLine("Добро пожаловать! Это Telegram - бот. Для вас доступны команды /start, /help, /info, /exit.");
            do
            {
                commandValue = Console.ReadLine();
                parseCommand(commandValue);
            }
            while (commandValue != MenuItems.Exit);

            void parseCommand(string? command)
            {
                switch (command)
                {
                    case MenuItems.Start:
                        Console.WriteLine("Введите имя пользователя");
                        userName = Console.ReadLine();
                        break;
                    case MenuItems.Help:
                        Console.WriteLine("Помогите");
                        break;
                    default:
                        Console.WriteLine("Пока");
                        break;
                }

            }
        }
    }
}
