using System.Text;

namespace Files
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DirectInfo directInfo = new DirectInfo();

            string dir1Path = @"C:\Otus\TestDir1";
            string dir2Path = @"C:\Otus\TestDir2";

            // Создание директорий
            directInfo.CreateDirectory(dir1Path);
            directInfo.CreateDirectory(dir2Path);

            // Создание файлов в каждой директории
            for (int i = 1; i <= 10; i++)
            {
                File myFile1 = new File(dir1Path, $"File{i}.txt");
                await myFile1.CreateFileAsync();

                File myFile2 = new File(dir2Path, $"File{i}.txt");
                await myFile2.CreateFileAsync();
            }

            // Чтение и вывод содержимого файлов
            Console.WriteLine("\nСодержимое файлов:");
            await ReadAndDisplayFilesContentsAsync(dir1Path);
            await ReadAndDisplayFilesContentsAsync(dir2Path);
        }

        static async Task ReadAndDisplayFilesContentsAsync(string directoryPath)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            try
            {
                foreach (var file in directory.GetFiles("*.txt"))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(file.FullName, Encoding.UTF8))
                        {
                            string content = await reader.ReadToEndAsync();
                            Console.WriteLine($"{file.Directory}\\{file.Name}: {content}");
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Нет разрешения на чтение файла {file.Name}.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка чтения файла {file.Name}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении файлов из директории {directoryPath}: {ex.Message}");
            }
        }
    }
}
