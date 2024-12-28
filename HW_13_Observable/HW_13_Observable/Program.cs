namespace HW_13_Observable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            Customer customer = new Customer();

            // Подписываем покупателя на изменения в магазине
            shop.ItemChanged += customer.OnItemChanged;

            while (true)
            {
                Console.WriteLine("Введите A для добавления товара, D для удаления товара или X для выхода:");
                string command = Console.ReadLine();

                if (command.ToUpper() == "A")
                {
                    Console.Write("Введите название товара: ");
                    string itemName = Console.ReadLine();
                    shop.AddItem(itemName);
                }
                else if (command.ToUpper() == "D")
                {
                    Console.Write("Введите идентификатор товара для удаления: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        shop.RemoveItem(id);
                    }
                    else
                    {
                        Console.WriteLine("Неверный идентификатор.");
                    }
                }
                else if (command.ToUpper() == "X")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверная команда. Пожалуйста, попробуйте снова.");
                }
            }
        }
    }
}
