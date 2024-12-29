using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using System;

namespace HW17_ORM_Dapper
{
    internal class Repository
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Метод для получения всех клиентов
        public IEnumerable<customers> GetAllCustomers()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM customers;";
                return connection.Query<customers>(sql);
            }
        }

        // Параметризованный метод для поиска клиента по имени
        public IEnumerable<customers> GetCustomersByName(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM customers WHERE firstname = @Name;";
                return connection.Query<customers>(sql, new { Name = name });
            }
        }

        // Метод для получения всех заказов
        public IEnumerable<orders> GetAllOrders()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM orders;";
                return connection.Query<orders>(sql);
            }
        }

        // Параметризованный метод для получения заказов по CustomerId
        public IEnumerable<orders> GetOrdersByCustomerId(int customerId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM orders WHERE customerid = @CustomerId;";
                return connection.Query<orders>(sql, new { CustomerId = customerId });
            }
        }

        // Метод для получения всех продуктов
        public IEnumerable<products> GetAllProducts()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM products;";
                return connection.Query<products>(sql);
            }
        }

        // Параметризованный метод для поиска продукта по имени
        public IEnumerable<products> GetProductsByName(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM products WHERE name = @Name;";
                return connection.Query<products>(sql, new { Name = name });
            }
        }
    }
}
