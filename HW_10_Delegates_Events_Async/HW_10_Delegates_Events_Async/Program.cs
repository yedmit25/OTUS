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
            "https://unsplash.com/photos/qkiCFFQp-Bg/download?force=true&w=2400",
            // Добавьте другие 9 URL-адресов изображений здесь
            "https://unsplash.com/photos/3JdubjN5xTI/download?force=true",
            "https://unsplash.com/photos/H-1j_s0dhCw/download?force=true",
            "https://unsplash.com/photos/nEvzSXBIhiU/download?force=true&w=2400",
            "https://unsplash.com/photos/PSF2RhUBORs/download?ixid=M3wxMjA3fDB8MXxzZWFyY2h8MTR8fG5ldyUyMHplYWxhbmR8ZW58MHx8fHwxNzMxMTk2Mzg5fDA&force=true",
            "https://unsplash.com/photos/75_s8iWHKLs/download?ixid=M3wxMjA3fDB8MXxzZWFyY2h8MTl8fG5ldyUyMHplYWxhbmR8ZW58MHx8fHwxNzMxMTk2Mzg5fDA&force=true",
            "https://unsplash.com/photos/eaRZniHTCD4/download?ixid=M3wxMjA3fDB8MXxzZWFyY2h8Nzh8fG5ldyUyMHplYWxhbmR8ZW58MHx8fHwxNzMxMTg5MjM4fDA&force=true",
            "https://unsplash.com/photos/Ton3nTyeAYI/download?ixid=M3wxMjA3fDB8MXxzZWFyY2h8MjkyfHxuZXclMjB6ZWFsYW5kfGVufDB8fHx8MTczMTIxNjUyMnww&force=true",
            "https://unsplash.com/photos/B5aGZ6kYCX4/download?ixid=M3wxMjA3fDB8MXxzZWFyY2h8MzM5fHxuZXclMjB6ZWFsYW5kfGVufDB8fHx8MTczMTIxNjU3Nnww&force=true",
            "https://unsplash.com/photos/Gb2LYrknHGE/download?force=true",
            "https://unsplash.com/photos/8ZpLCdnxSBk/download?ixid=M3wxMjA3fDB8MXxzZWFyY2h8NDA2fHxuZXclMjB6ZWFsYW5kfGVufDB8fHx8MTczMTIxNjY5Nnww&force=true"
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
