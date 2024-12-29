using Dapper;
using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Text;

namespace HW17_ORM_Dapper
{
    internal class Program
    {
        private static IConfigurationRoot configuration;
        private static string ConnectionString;
        static void Main(string[] args)
        {
            // Настройка конфигурации
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //string connectionString = "Host=localhost;Port=5434;Database=Shop;Username=postgres;Password=zlbvf25;";
            ConnectionString = configuration.GetConnectionString("ShopDB");
            var repository = new Repository(ConnectionString);

            // Получение всех клиентов
            var customers = repository.GetAllCustomers();
            Console.WriteLine("Клиенты: ");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id}: {customer.firstname} - {customer.age}");
            }

            // Получение клиентов по имени
            Console.WriteLine("Введите для поиска имя клиента: ");
            string name = Console.ReadLine();
            var filteredCustomers = repository.GetCustomersByName(name);
            Console.WriteLine("Отфильтровано по имени клиента: ");
            foreach (var customer in filteredCustomers)
            {
                Console.WriteLine($"{customer.Id}: {customer.firstname} - {customer.age}");
            }

            // Получение всех заказов
            var orders = repository.GetAllOrders();
            Console.WriteLine("Заказы: ");
            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Id}: Клиент ИД {order.customerid} - Всего: {order.quantity}");
            }

            // Получение заказов по CustomerId
            Console.WriteLine("Введите ИД клиента для поиска заказа: ");
            int customerId = int.Parse(Console.ReadLine());
            var customerOrders = repository.GetOrdersByCustomerId(customerId);
            Console.WriteLine("Заказы клиента: ");
            foreach (var order in customerOrders)
            {
                Console.WriteLine($"{order.Id}: Всего: {order.quantity}");
            }

            // Получение всех продуктов
            var products = repository.GetAllProducts();
            Console.WriteLine("Товары: ");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.id}: {product.name} - Цена: {product.price}");
            }

            // Получение продуктов по имени
            Console.WriteLine("Введите наименование товара для поиска: ");
            string productName = Console.ReadLine();
            var filteredProducts = repository.GetProductsByName(productName);
            Console.WriteLine("Отфильтровано по наименованию товара: ");
            foreach (var product in filteredProducts)
            {
                Console.WriteLine($"{product.id}: {product.name} - Цена: {product.price}");
            }
        }
    }
}
