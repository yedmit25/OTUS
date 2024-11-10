namespace HW_10_Delegates_Events_Async
{
    internal class Program
    {

        private static List<ImageDownloader> downloaders = new List<ImageDownloader>();
        private static List<Task> downloadTasks = new List<Task>();
        private static CancellationTokenSource cancellation = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            string[] imageUrls = {
            "https://img3.akspic.ru/attachments/originals/4/1/1/3/0/103114-lug-priroda-holm-teatralnye_dekoracii-nebo-2844x1600.jpg",
            "https://images.unsplash.com/photo-1560185007-c5ca9d2c014d?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1560185007-5f0bb1866cab?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://plus.unsplash.com/premium_photo-1676823553207-758c7a66e9bb?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1556593825-c11de986cb0b?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1560448205-17d3a46c84de?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1600494448850-6013c64ba722?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1560449752-8b6023e2ab5a?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1643949914872-317d6047f107?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1560185009-dddeb820c7b7?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
            "https://images.unsplash.com/photo-1533779283484-8ad4940aa3a8?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        };

            Console.WriteLine("Начинаем загрузку 10 изображений...");

            foreach (var url in imageUrls)
            {
                string fileName = $"Image_{downloaders.Count + 1}.jpg";
                var downloader = new ImageDownloader(url);
                downloader.ImageStarted += () => Console.WriteLine("Скачивание файла началось");
                downloader.ImageCompleted += () => Console.WriteLine("Скачивание файла закончилось");
                downloaders.Add(downloader);
                downloadTasks.Add(downloader.Download(fileName));
            }

            // Ожидание нажатия клавиши
            Console.WriteLine("Нажмите клавишу 'A' для завершения всех загрузок, либо любую другую клавишу для проверки статуса загрузок");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).KeyChar;
                    if (key == 'A')
                    {
                        cancellation.Cancel();
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < downloaders.Count; i++)
                        {
                            Console.WriteLine($"Загрузка {i + 1}: {(downloadTasks[i].IsCompleted ? "Загружена" : "Не загружена")}");
                        }
                    }
                }
                await Task.Delay(500); // Ожидание небольшого времени перед следующей проверкой
            }

            Console.WriteLine("Загрузка завершена. Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }
    }
}
