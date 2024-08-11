using System.Collections;
using System.Diagnostics;

namespace ComparisonLiastArraylistLinkedlist
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // создаём оъект класса Random
            Random rng = new Random();

            //создаем объект
            Stopwatch stopwatch = new Stopwatch();

            int i=0;
            int maxSize = 1000000;


            Console.WriteLine($"Замеряем производительсть при заполнении коллекций в:\t{maxSize} записей.");

            var lnkList = new LinkedList<int>();
            //засекаем время начала операции
            stopwatch.Start();
            for (i = 0; i < maxSize; i++)
            {
                lnkList.AddLast(rng.Next(100));
            }
            //останавливаем счётчик
            stopwatch.Stop();
            //смотрим сколько миллисекунд было затрачено на выполнение
            Console.WriteLine($"-- заполнение коллекции LinkedList заняло:\t{stopwatch.ElapsedMilliseconds, 8}");


            var intList = new List<int>(maxSize);
            stopwatch.Reset();
            stopwatch.Start();
            for (i = 0; i < maxSize; i++)
            {
                intList.Add(rng.Next(100));
            }
            stopwatch.Stop();
            Console.WriteLine($"-- заполнение коллекции List заняло:\t{stopwatch.ElapsedMilliseconds, 16}");

            var arrayList = new ArrayList(maxSize);
            stopwatch.Reset();
            //засекаем время начала операции
            stopwatch.Start();
            for (i = 0; i < maxSize; i++)
            {
                arrayList.Add(rng.Next(100));
            };
            stopwatch.Stop();
            Console.WriteLine($"-- заполнение коллекции ArrayList заняло:\t{stopwatch.ElapsedMilliseconds, 8}");

            // индекс искомого элемента
            int seekElement = 496753;
            Console.WriteLine($"\n\nПоиск элементов в коллекциях. Индекс искомого элемента {seekElement}");
           
            stopwatch.Restart();
            Console.WriteLine($"--- элемент LinkedList:\t{lnkList.ElementAt(seekElement), 30}");
            stopwatch.Stop();
            Console.WriteLine($"-- поиск элемента в LinkedList заняло:\t{stopwatch.ElapsedMilliseconds,14}");


            stopwatch.Restart();
            Console.WriteLine($"\n--- элемент в List:\t{intList[seekElement],30}");
            stopwatch.Stop();
            Console.WriteLine($"-- поиск элемента в List заняло:\t{stopwatch.ElapsedMilliseconds,14}");


            stopwatch.Restart();

            Console.WriteLine($"\n--- элемент в ArrayList:\t{arrayList[seekElement], 22}");
            stopwatch.Stop();
            //смотрим сколько миллисекунд было затрачено на выполнение
            Console.WriteLine($"-- поиск элемента в ArrayList заняло:\t {stopwatch.ElapsedMilliseconds, 13}");

        }
    }
}
