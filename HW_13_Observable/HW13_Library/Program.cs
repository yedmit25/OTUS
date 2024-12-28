using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;


namespace HW13_Library
{
    internal class Program
    {
        // Коллекция для хранения книг с их процентами прочтения
        static ConcurrentDictionary<string, int> books = new ConcurrentDictionary<string, int>();
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        static void Main(string[] args)
        {
            // Запускаем фоновой поток для обновления процентов
            Task.Run(() => UpdateReadingProgress(cancellationTokenSource.Token));

            // Основное меню
            bool running = true;
            while (running)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1 - добавить книгу;");
                Console.WriteLine("2 - вывести список непрочитанного;");
                Console.WriteLine("3 - выйти");
                Console.Write("Выберите опцию: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DisplayBooks();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод. Пожалуйста, попробуйте снова.");
                        break;
                }
            }

            // Останавливаем фоновый поток перед выходом
            cancellationTokenSource.Cancel();
            Console.WriteLine("Программа завершена.");
        }

        static void AddBook()
        {
            Console.Write("Введите название книги: ");
            var bookTitle = Console.ReadLine();

            // Добавление новой книги, если её ещё нет в коллекции
            if (books.TryAdd(bookTitle, 0))
            {
                Console.WriteLine($"Книга '{bookTitle}' добавлена.");
            }
            else
            {
                Console.WriteLine($"Книга '{bookTitle}' уже существует.");
            }
        }

        static void DisplayBooks()
        {
            Console.WriteLine("Список книг:");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Key} - {book.Value}%");
            }

            Console.WriteLine(); // Для лучшей читаемости
        }

        static void UpdateReadingProgress(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Список текущих книг
                foreach (var book in books.Keys)
                {
                    // Получаем текущий процент и увеличиваем его на 1
                    books.AddOrUpdate(book, 0, (key, oldValue) =>
                    {
                        int newValue = oldValue + 1;
                        // Если книга достигла 100%, удаляем её
                        if (newValue >= 100)
                        {
                            Console.WriteLine($"Книга '{book}' завершена (100%). Удалена из списка.");
                            return -1; // Возвращаем -1 для удаления
                        }
                        return newValue;
                    });
                }

                // Удаляем книги с процентом -1
                foreach (var key in books.Keys)
                {
                    if (books[key] == -1)
                    {
                        books.TryRemove(key, out _);
                    }
                }

                // Задержка на 1 секунду перед следующей итерацией
                Thread.Sleep(1000);
            }
        }
    }
}
