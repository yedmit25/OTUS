using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    internal class DirectInfo
    {
        public void CreateDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                try
                {
                    dir.Create();
                    Console.WriteLine($"Директория {path} создана.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка создания директории {path}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Директория {path} уже существует.");
            }
        }
    }
}
