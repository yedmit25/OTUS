using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    internal class File
    {
        private readonly string _filePath;

        public File(string directoryPath, string fileName)
        {
            _filePath = Path.Combine(directoryPath, fileName);
        }

        public async Task CreateFileAsync()
        {
            string content = $"{Path.GetFileName(_filePath)}\n{DateTime.Now}";

            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath, false, Encoding.UTF8))
                {
                    await writer.WriteLineAsync(content);
                    Console.WriteLine($"Файл {_filePath} записан.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Нет разрешения на запись в файл {_filePath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в файл {_filePath}: {ex.Message}");
            }
        }
    }
}
