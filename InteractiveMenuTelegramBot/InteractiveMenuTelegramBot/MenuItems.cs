using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMenuTelegramBot
{
    internal class MenuItems
    {
        public const string Start = "/start";
        public const string Exit = "/exit";
        public const string Help = "/help";
        public const string Info = "/info";
        public const string Echo = "/echo";
        public const string HelpConstant = "Помощь:\n /sart - запуск работы бота;\n /help - справочная информация;\n /info - описание программы, версия, дата создания;\n /echo - вывод введённого сообщения пользователем;\n /exit - завершение работы бота";
    }
}
