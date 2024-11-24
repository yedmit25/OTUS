using Dapper;
using Npgsql;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JewelryBot
{
    class Program
    {
        private static IConfigurationRoot configuration;
        private static string ConnectionString;
        private static ITelegramBotClient botClient;

        private static async Task Main(string[] args)
        {
            // Настройка конфигурации
            ConfigureSettings();

            var cts = new CancellationTokenSource();
            botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, cancellationToken: cts.Token);

            Console.WriteLine("Bot is running...");
            await Task.Run(() => Console.ReadLine());
            cts.Cancel();
        }

        private static void ConfigureSettings()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Получение строки подключения и токена бота
            ConnectionString = configuration.GetConnectionString("JewelryBotDB");
            var token = configuration["TelegramBot:Token"];
            botClient = new TelegramBotClient(token);
        }

        public static async Task SendMainMenu(long chatId)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[] { new KeyboardButton("Заказы"), new KeyboardButton("Товары") },
                new[] { new KeyboardButton("Корзина") }
            })
            {
                ResizeKeyboard = true // Автоматическая подстройка размера клавиатуры
            };

            await botClient.SendTextMessageAsync(chatId, "Выберите опцию:", replyMarkup: replyKeyboard);
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message.Text != null)
            {
                var messageText = update.Message.Text;

                if (messageText.StartsWith("/start"))
                {
                    await HandleStartCommand(update);
                }
                else
                {
                    // Устанавливаем имя пользователя
                    await SetUserName(update.Message.Chat.Id, messageText);
                    // После установки имени показываем продукты
                    await ShowProducts(update.Message.Chat.Id);
                }
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
            }
        }

        private static async Task HandleStartCommand(Update update)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
            var existingUser = await dbConnection.QuerySingleOrDefaultAsync<string>(
                "SELECT name FROM users WHERE user_id = @UserId",
                new { UserId = update.Message.Chat.Id });

            if (existingUser != null)
            {
                // Пользователь существует, приветствуем его
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Рады вас видеть снова, {existingUser}!");
                // Выводим каталог товаров после приветствия
                await ShowProducts(update.Message.Chat.Id);
            }
            else
            {
                // Пользователь не существует, прося представиться
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Пожалуйста, представьтесь (введите ваше имя):");
            }
        }

        private static async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            string[] data = callbackQuery.Data.Split('_');
            var action = data[0];
            var userId = callbackQuery.From.Id;

            if (action.StartsWith("add"))
            {
                int productId = int.Parse(data[1]);
                await AddToCart(userId, productId);
                await botClient.AnswerCallbackQuery(callbackQuery.Id, $"Товар добавлен в корзину.");
            }
            else if (action == "remove")
            {
                int productId = int.Parse(data[1]);
                await RemoveFromCart(userId, productId);
                await botClient.AnswerCallbackQuery(callbackQuery.Id, "Количество товара уменьшено.");
            }
            else if (action == "view")
            {
                await ViewCart(botClient, userId);
            }
        }

        private static async Task SetUserName(long chatId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                await botClient.SendTextMessageAsync(chatId, "Имя не может быть пустым. Пожалуйста, введите ваше имя.");
                return;
            }

            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
            var existingUser = await dbConnection.QuerySingleOrDefaultAsync<string>(
                "SELECT name FROM users WHERE user_id = @UserId",
                new { UserId = chatId }
            );

            if (existingUser != null)
            {
                await botClient.SendTextMessageAsync(chatId, $"Добро пожаловать обратно, {existingUser}!");
            }
            else
            {
                await dbConnection.ExecuteAsync(
                    "INSERT INTO users (user_id, name) VALUES (@UserId, @Name)",
                    new { UserId = chatId, Name = name }
                );

                await botClient.SendTextMessageAsync(chatId, $"Приятно познакомиться, {name}!");
            }
        }

        private static async Task ShowProducts(long chatId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
            var products = await dbConnection.QueryAsync<(int Id, string Name, decimal Price, string ImageUrl)>(
                "SELECT product_id, name, price, image_url FROM products");

            foreach (var product in products)
            {
                var message = $"Цена: {product.Price}₽";
                var replyMarkup = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithCallbackData("В Корзину", $"add_{product.Id}"),
                });

                await botClient.SendPhotoAsync(chatId, product.ImageUrl, caption: message, replyMarkup: replyMarkup);
            }

            // Кнопка "Посмотреть корзину"
            var cartButton = InlineKeyboardButton.WithCallbackData("Корзина", "view");
            var cartKeyboard = new InlineKeyboardMarkup(new[] { new[] { cartButton } });

            await botClient.SendTextMessageAsync(chatId, "Выберите товар:", replyMarkup: cartKeyboard);
        }

        private static async Task AddToCart(long userId, int productId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
            await dbConnection.ExecuteAsync(
                "INSERT INTO cart (user_id, product_id, quantity) VALUES (@UserId, @ProductId, 1) " +
                "ON CONFLICT (user_id, product_id) DO UPDATE SET quantity = cart.quantity + 1;",
                new { UserId = userId, ProductId = productId });
        }

        private static async Task RemoveFromCart(long userId, int productId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
            var currentQuantity = await dbConnection.QuerySingleOrDefaultAsync<int>(
                "SELECT quantity FROM cart WHERE user_id = @UserId AND product_id = @ProductId",
                new { UserId = userId, ProductId = productId });

            if (currentQuantity > 0)
            {
                var newQuantity = currentQuantity - 1;
                if (newQuantity > 0)
                {
                    await dbConnection.ExecuteAsync(
                        "UPDATE cart SET quantity = @Quantity WHERE user_id = @UserId AND product_id = @ProductId;",
                        new { UserId = userId, ProductId = productId, Quantity = newQuantity });
                }
                else
                {
                    await dbConnection.ExecuteAsync(
                        "DELETE FROM cart WHERE user_id = @UserId AND product_id = @ProductId",
                        new { UserId = userId, ProductId = productId });
                }
            }
        }

        private static async Task ViewCart(ITelegramBotClient botClient, long userId)
        {
            try
            {
                using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
                var cartItems = await dbConnection.QueryAsync<(int ProductId, int Quantity)>(
                    "SELECT product_id, quantity FROM cart WHERE user_id = @UserId",
                    new { UserId = userId });

                if (!cartItems.Any())
                {
                    await botClient.SendTextMessageAsync(userId, "Ваша корзина пуста.");
                    return;
                }

                var productIds = cartItems.Select(item => item.ProductId).ToArray();
                var products = await dbConnection.QueryAsync<(int ProductId, string Name, decimal Price, string ImageUrl)>(
                    "SELECT product_id, name, price, image_url FROM products WHERE product_id = ANY(@ProductIds)",
                    new { ProductIds = productIds });

                var productsDict = products.ToDictionary(p => p.ProductId);

                foreach (var item in cartItems)
                {
                    if (productsDict.TryGetValue(item.ProductId, out var product))
                    {
                        if (string.IsNullOrEmpty(product.ImageUrl))
                        {
                            await botClient.SendTextMessageAsync(userId, $"{product.Name} - Цена: {product.Price}₽, Количество: {item.Quantity}. Изображение отсутствует.");
                            continue;
                        }

                        var message = $"{product.Name} - Цена: {product.Price}₽, Количество: {item.Quantity}";
                        var replyMarkup = new InlineKeyboardMarkup(new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Добавить", $"add_{product.ProductId}"),
                                InlineKeyboardButton.WithCallbackData("Удалить", $"remove_{product.ProductId}")
                            }
                        });

                        await botClient.SendPhotoAsync(userId, product.ImageUrl, caption: message, replyMarkup: replyMarkup);
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(botClient, ex, CancellationToken.None);
            }
        }

        private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}