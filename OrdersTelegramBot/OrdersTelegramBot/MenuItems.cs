using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrdersTelegramBot
{
    internal class MenuItems
    {
        public const string Start = "/start";
        public const string Exit = "/exit";
        public const string Help = "/Help";
        public const string Info = "/info";
        public const string Echo = "/echo";
        public const string HelpConstant = "Помощь:\n /sart - запуск работы бота;\n /help - справочная информация;\n /info - описание программы, версия, дата создания;\n" +
            " /NewOrder - Создать новый заказ;\n" +
            " /Order - Посмотреть заказ;\n" +
            " /MyOrders - Посмотреть мои заказы;\n" +
            " /exit - завершение работы бота";

        public const string NewOrder = "/NewOrder";
        public const string Order = "/Order";
        public const string MyOrders = "/MyOrders";


    }
}
