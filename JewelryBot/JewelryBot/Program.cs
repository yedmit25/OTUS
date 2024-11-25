using Dapper;
using Npgsql;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Text;

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
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Получение строки подключения и токена бота
            ConnectionString = configuration.GetConnectionString("JewelryBotDB");
            var token = configuration["TelegramBot:Token"];
            botClient = new TelegramBotClient(token);

            var cts = new CancellationTokenSource();
            botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, cancellationToken: cts.Token);

            Console.WriteLine("Bot is running...");
            await Task.Run(() => Console.ReadLine());
            cts.Cancel();
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message.Text != null)
            {
                var messageText = update.Message.Text;
                var userId = update.Message.Chat.Id;

                if (messageText.StartsWith("/start"))
                {
                    using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
                    var existingUser = await dbConnection.QuerySingleOrDefaultAsync<string>(
                        "SELECT username FROM customers WHERE customer_id = @UserId",
                        new { UserId = userId });

                    if (existingUser != null)
                    {
                        await botClient.SendTextMessageAsync(userId, $"Рады вас видеть снова, {existingUser}!");
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(userId, "Пожалуйста, представьтесь (введите ваше имя):");
                        UserStates[userId] = new UserState { AwaitingName = true };
                    }

                    // Отображаем главное меню
                    await ShowMainMenu(botClient, userId);
                }
                else
                {
                    // Проверяем состояние ожидания для имени
                    if (UserStates.TryGetValue(userId, out var currentUserState) && currentUserState.AwaitingName)
                    {
                        string name = messageText;
                        await botClient.SendTextMessageAsync(userId, "Введите ваш email:");
                        currentUserState.Name = name; // сохраняем имя
                        currentUserState.AwaitingName = false;
                        currentUserState.AwaitingEmail = true; // теперь ждем email
                    }
                    else if (UserStates.TryGetValue(userId, out currentUserState) && currentUserState.AwaitingEmail)
                    {
                        string email = messageText;
                        await SetUser(botClient, userId, currentUserState.Name, email);
                        await ShowMainMenu(botClient, userId);
                        UserStates.Remove(userId);
                    }
                    else if (messageText == "Товары")
                    {
                        await ShowProducts(botClient, userId);
                    }
                    else if (messageText == "Корзина")
                    {
                        // Получаем текущий черновик заказа
                        using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
                        var currentOrder = await dbConnection.QuerySingleOrDefaultAsync<int?>(
                            "SELECT order_id FROM orders WHERE customer_id = @UserId AND status = 'draft'",
                            new { UserId = userId });

                        if (currentOrder != null)
                        {
                            // Вызов ViewCart с правильным orderId
                            await ViewCart(botClient, userId, currentOrder.Value);
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(userId, "Ваша корзина пуста или не существует.");
                        }
                    }
                    else if (messageText == "Заказы")
                    {
                        await ShowOrderDetails(botClient, userId);
                    }
                    else if (messageText == "Отменить заказ")
                    {
                        await CancelOrder(userId);
                        await botClient.SendTextMessageAsync(userId, "Ваш заказ был отменен.");
                    }
                    else if (messageText == "Оформить заказ")
                    {
                        await botClient.SendTextMessageAsync(userId, "Введите адрес доставки:");
                        UserStates[userId] = new UserState { AwaitingAddress = true };
                    }
                    else if (UserStates.TryGetValue(userId, out currentUserState) && currentUserState.AwaitingAddress)
                    {
                        string address = messageText;

                        // Получаем текущий черновик заказа
                        using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
                        var currentOrderId = await dbConnection.QuerySingleOrDefaultAsync<int?>(
                            "SELECT order_id FROM orders WHERE customer_id = @UserId AND status = 'draft'",
                            new { UserId = userId });

                        if (currentOrderId != null)
                        {
                            // Обновляем заказ с адресом доставки
                            await dbConnection.ExecuteAsync(
                                "UPDATE orders SET shipping_address = @Address, status = 'Paid' WHERE order_id = @OrderId",
                                new { Address = address, OrderId = currentOrderId.Value });

                            await botClient.SendTextMessageAsync(userId, $"Заказ оформлен! Адрес доставки: {address}");
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(userId, "Не удалось оформить заказ.");
                        }

                        // Убираем состояние ожидания
                        UserStates.Remove(userId);
                    }
                    else if (messageText == "Выход из заказа")
                    {
                        await ShowMainMenu(botClient, userId); // возвращаем в главное меню
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(userId, "Пожалуйста, используйте /start для начала.");
                    }
                }
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
            }
        }

        private static async Task ShowMainMenu(ITelegramBotClient botClient, long chatId)
        {
            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new[] { new KeyboardButton("Товары"), new KeyboardButton("Корзина"), new KeyboardButton("Заказы") }
            })
            {
                ResizeKeyboard = true // для более компактного отображения кнопок
            };

            await botClient.SendTextMessageAsync(chatId, "Выберите опцию:", replyMarkup: keyboard);
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
                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Товар добавлен в корзину.");
            }
            else if (action == "remove")
            {
                int productId = int.Parse(data[1]);
                await RemoveFromCart(userId, productId);
                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Количество товара уменьшено.");
            }

            else if (action == "Корзина")
            {
                // Получаем текущий черновик заказа
                using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
                var currentOrder = await dbConnection.QuerySingleOrDefaultAsync<int?>(
                    "SELECT order_id FROM orders WHERE customer_id = @UserId AND status = 'draft'",
                    new { UserId = userId });

                if (currentOrder != null)
                {
                    // Вызов ViewCart с правильным orderId
                    await ViewCart(botClient, userId, currentOrder.Value);
                }
                else
                {
                    await botClient.SendTextMessageAsync(userId, "Ваша корзина пуста или не существует.");
                }
            }


        }

        private static async Task SetUser(ITelegramBotClient botClient, long chatId, string name, string email)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);

            // Проверка на существование пользователя
            var existingUser = await dbConnection.QuerySingleOrDefaultAsync<string>(
                "SELECT username FROM customers WHERE customer_id = @UserId",
                new { UserId = chatId }
            );

            if (existingUser != null)
            {
                // Пользователь существует
                await botClient.SendTextMessageAsync(chatId, $"Добро пожаловать обратно, {existingUser}!");
            }
            else
            {
                // Проверка, пуст ли email
                if (string.IsNullOrEmpty(email))
                {
                    email = "email@domain.com"; // Значение по умолчанию
                    await botClient.SendTextMessageAsync(chatId, "Email не был введен, будет использовано значение по умолчанию: " + email);
                }

                // Добавляем нового пользователя
                await dbConnection.ExecuteAsync(
                    "INSERT INTO customers (customer_id, username, email) VALUES (@UserId, @Name, @Email)",
                    new { UserId = chatId, Name = name, Email = email }
                );

                await botClient.SendTextMessageAsync(chatId, $"Приятно познакомиться, {name}! Ваш email: {email}");
            }
        }


        public static Dictionary<long, UserState> UserStates = new Dictionary<long, UserState>();
        private static async Task ShowProducts(ITelegramBotClient botClient, long chatId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
            var products = await dbConnection.QueryAsync<(int Id, string Name, decimal Price, string ImageUrl)>(
                "SELECT product_id, name, price, image_url FROM products");

            foreach (var product in products)
            {
                var message = $"Цена: {product.Price}₽";
                var replyMarkup = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithCallbackData("В корзину", $"add_{product.Id}")
                });

                await botClient.SendPhotoAsync(chatId, product.ImageUrl, caption: message, replyMarkup: replyMarkup);
            }
        }

        private static async Task AddToCart(long userId, int productId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);

            // Пытаемся найти текущий черновик заказа
            var currentOrder = await dbConnection.QuerySingleOrDefaultAsync<int?>(
                "SELECT order_id FROM orders WHERE customer_id = @UserId AND status = 'draft'",
                new { UserId = userId });

            if (currentOrder == null)
            {
                // Если заказа нет, создаем новый
                currentOrder = await dbConnection.ExecuteScalarAsync<int>(
                    "INSERT INTO orders (customer_id, status) VALUES (@UserId, 'draft') RETURNING order_id;",
                    new { UserId = userId });

                // Создаем cart с новым order_id
                await dbConnection.ExecuteAsync(
                    "INSERT INTO cart (customer_id, product_id, quantity, order_id) VALUES (@UserId, @ProductId, 1, @OrderId) ON CONFLICT (customer_id, product_id, order_id) DO UPDATE SET quantity = cart.quantity + 1;",
                    new { UserId = userId, ProductId = productId, OrderId = currentOrder });
            }
            else
            {
                // Если черновик заказа есть, просто добавляем товар в cart
                await dbConnection.ExecuteAsync(
                    "INSERT INTO cart (customer_id, product_id, quantity, order_id) VALUES (@UserId, @ProductId, 1, @OrderId) ON CONFLICT (customer_id, product_id, order_id) DO UPDATE SET quantity = cart.quantity + 1;",
                    new { UserId = userId, ProductId = productId, OrderId = currentOrder });
            }
        }

        private static async Task RemoveFromCart(long userId, int productId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);

            // Пытаемся найти текущий черновик заказа
            var currentOrder = await dbConnection.QuerySingleOrDefaultAsync<int?>(
                "SELECT order_id FROM orders WHERE customer_id = @UserId AND status = 'draft'",
                new { UserId = userId });

            // Получаем текущую корзину и ее order_id
            var currentCartItem = await dbConnection.QuerySingleOrDefaultAsync<(int Quantity, int OrderId)>(
                "SELECT quantity, order_id FROM cart WHERE customer_id = @UserId AND product_id = @ProductId and order_id = @currentOrder",
                new { UserId = userId, ProductId = productId, currentOrder = currentOrder});


            if (currentCartItem.Quantity > 0)
            {
                var newQuantity = currentCartItem.Quantity - 1;

                if (newQuantity > 0)
                {
                    await dbConnection.ExecuteAsync(
                        "UPDATE cart SET quantity = @Quantity WHERE customer_id = @UserId AND product_id = @ProductId and order_id = @currentOrder;",
                        new { UserId = userId, ProductId = productId, Quantity = newQuantity, currentOrder = currentCartItem.OrderId });
                }
                else
                {
                    // Удаляем товар из корзины, если количество 0
                    await dbConnection.ExecuteAsync(
                        "DELETE FROM cart WHERE customer_id = @UserId AND product_id = @ProductId and order_id = @currentOrder",
                        new { UserId = userId, ProductId = productId, currentOrder = currentCartItem.OrderId });
                }

                // Проверяем корзину на пустоту
                var areCartItemsEmpty = await dbConnection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM cart WHERE order_id = @OrderId",
                    new { OrderId = currentCartItem.OrderId });

                if (areCartItemsEmpty == 0)
                {
                    // Удаляем заказ, если корзина пустая
                    await dbConnection.ExecuteAsync(
                        "DELETE FROM orders WHERE order_id = @OrderId",
                        new { OrderId = currentCartItem.OrderId });
                }
            }
        }


        private static async Task ViewCart(ITelegramBotClient botClient, long userId, int orderId)
        {
            try
            {
                using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
                var cartItems = await dbConnection.QueryAsync<(int ProductId, int Quantity)>(
                    "SELECT product_id, quantity FROM cart WHERE order_id = @OrderId",
                    new { OrderId = orderId });

                if (!cartItems.Any())
                {
                    await botClient.SendTextMessageAsync(userId, "Ваша корзина пуста.");
                    return;
                }

                foreach (var item in cartItems)
                {
                    // Получаем информацию о продукте
                    var product = await dbConnection.QuerySingleOrDefaultAsync<(int ProductId, string Name, decimal Price, string ImageUrl)>(
                        "SELECT product_id, name, price, image_url FROM products WHERE product_id = @ProductId",
                        new { ProductId = item.ProductId });

                    if (product == default) // если продукт не найден
                    {
                        Console.WriteLine($"Продукт с ID {item.ProductId} не найден.");
                        continue; // пропускаем итерацию
                    }

                    // Проверка на наличие изображения
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        // Формируем сообщение с текущей стоимостью и кнопками
                        var message = $"{product.Name} - Цена: {product.Price}₽, Количество: {item.Quantity}";
                        var replyMarkup = new InlineKeyboardMarkup(new[]
                        {
                    new[] {
                        InlineKeyboardButton.WithCallbackData("Добавить", $"add_{product.ProductId}"),
                        InlineKeyboardButton.WithCallbackData("Удалить", $"remove_{product.ProductId}")
                    }
                });

                        await botClient.SendPhotoAsync(
                            userId,
                            product.ImageUrl,
                            caption: message,
                            replyMarkup: replyMarkup
                        );
                    }
                    else
                    {
                        Console.WriteLine($"У продукта {product.Name} отсутствует изображение.");
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(botClient, ex, CancellationToken.None);
            }
        }

        private static async Task ShowOrderDetails(ITelegramBotClient botClient, long userId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);
            var currentOrder = await dbConnection.QuerySingleOrDefaultAsync<int?>(
                "SELECT order_id FROM orders WHERE customer_id = @UserId AND status = 'draft'",
                new { UserId = userId });

            if (currentOrder == null)
            {
                await botClient.SendTextMessageAsync(userId, "Ваша корзина пуста или заказа нет.");
                return;
            }

            // Получаем состав корзины
            var cartItems = await dbConnection.QueryAsync<(int ProductId, int Quantity)>(
                "SELECT product_id, quantity FROM cart WHERE order_id = @OrderId",
                new { OrderId = currentOrder });

            if (!cartItems.Any())
            {
                await botClient.SendTextMessageAsync(userId, "Ваша корзина пуста.");
                return;
            }

            decimal totalCost = 0;
            var orderDetailsMessage = new StringBuilder("Ваш заказ:\n");

            foreach (var item in cartItems)
            {
                // Получаем информацию о продукте
                var product = await dbConnection.QuerySingleOrDefaultAsync<(int ProductId, string Name, decimal Price)>(
                    "SELECT product_id, name, price FROM products WHERE product_id = @ProductId",
                    new { ProductId = item.ProductId });

                if (product == default) // если продукт не найден
                {
                    continue; // пропускаем итерацию
                }

                orderDetailsMessage.AppendLine($"{product.Name} - Цена: {product.Price}₽, Количество: {item.Quantity}");
                totalCost += product.Price * item.Quantity;
            }

            orderDetailsMessage.AppendLine($"Общая стоимость: {totalCost}₽");

            // Пользовательский интерфейс с кнопками для управления
            var replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                new[] { new KeyboardButton("Выход из заказа"), new KeyboardButton("Назад к меню"), new KeyboardButton("Оформить заказ"), new KeyboardButton("Отменить заказ") }
            })
            {
                ResizeKeyboard = true // для более компактного отображения кнопок
            };

            await botClient.SendTextMessageAsync(userId, orderDetailsMessage.ToString(), replyMarkup: replyMarkup);
        }

        private static async Task CancelOrder(long userId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(ConnectionString);

            // Получаем текущий черновик заказа
            var currentOrder = await dbConnection.QuerySingleOrDefaultAsync<int?>(
                "SELECT order_id FROM orders WHERE customer_id = @UserId AND status = 'draft'",
                new { UserId = userId });

            if (currentOrder != null)
            {
                // Обновляем статус заказа на 'Canceled'
                await dbConnection.ExecuteAsync(
                    "UPDATE orders SET status = 'Canceled' WHERE order_id = @OrderId",
                    new { OrderId = currentOrder });
            }
            else
            {
                await botClient.SendTextMessageAsync(userId, "У вас нет активных заказов для отмены.");
            }
        }

        private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}