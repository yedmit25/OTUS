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
            int nextRandom = 1000000;

            //создаем объект
            Stopwatch stopwatch = new Stopwatch();

            int i = 0;
            // размерность коллекции
            int maxSize = 1000000;

            // индекс искомого элемента
            int seekElement = 496753;

            // Деление без остатка на dividedBy
            int dividedBy = 777;

            // объявляем переменные для засечек времени исполнения

            long executionTimeList;

            long executionTimeArrayList;

            long executionTimeLinkedList;

            Console.WriteLine($"Замеряем производительсть при заполнении коллекций в:\t{maxSize} записей.");

            var lnkList = new LinkedList<int>();
            //засекаем время начала операции
            stopwatch.Start();
            for (i = 0; i < maxSize; i++)
            {
                lnkList.AddLast(rng.Next(nextRandom));
            }
            //останавливаем счётчик
            stopwatch.Stop();
            //смотрим сколько миллисекунд было затрачено на выполнение
            Console.WriteLine($"-- заполнение коллекции LinkedList заняло:\t{stopwatch.ElapsedMilliseconds,8}");


            var intList = new List<int>(maxSize);
            stopwatch.Reset();
            stopwatch.Start();
            for (i = 0; i < maxSize; i++)
            {
                intList.Add(rng.Next(nextRandom));
            }
            stopwatch.Stop();
            Console.WriteLine($"-- заполнение коллекции List заняло:\t{stopwatch.ElapsedMilliseconds,16}");

            var arrayList = new ArrayList(maxSize);
            stopwatch.Reset();
            //засекаем время начала операции
            stopwatch.Start();
            for (i = 0; i < maxSize; i++)
            {
                arrayList.Add(rng.Next(nextRandom));
            };
            stopwatch.Stop();
            Console.WriteLine($"-- заполнение коллекции ArrayList заняло:\t{stopwatch.ElapsedMilliseconds,8}");

            Console.WriteLine($"\n\nПоиск элементов в коллекциях. Индекс искомого элемента {seekElement}");

            stopwatch.Restart();
            Console.WriteLine($"--- элемент LinkedList:\t{lnkList.ElementAt(seekElement),30}");
            stopwatch.Stop();
            Console.WriteLine($"-- поиск элемента в LinkedList заняло:\t{stopwatch.ElapsedMilliseconds,14}");


            stopwatch.Restart();
            Console.WriteLine($"\n--- элемент в List:\t{intList[seekElement],30}");
            stopwatch.Stop();
            Console.WriteLine($"-- поиск элемента в List заняло:\t{stopwatch.ElapsedMilliseconds,14}");


            stopwatch.Restart();
            Console.WriteLine($"\n--- элемент в ArrayList:\t{arrayList[seekElement],22}");
            stopwatch.Stop();
            //смотрим сколько миллисекунд было затрачено на выполнение
            Console.WriteLine($"-- поиск элемента в ArrayList заняло:\t {stopwatch.ElapsedMilliseconds,13}");

            // Поиск деления на dividedBy без остатка

            Console.WriteLine($"Вывод элементов коллекций делённых на {dividedBy} без остатка:");
            stopwatch.Restart();
            foreach (int elementlinkedList in lnkList)
            {
                if (elementlinkedList != 0 && elementlinkedList % dividedBy == 0)
                {
                    Console.WriteLine($"... LinkedList: = {elementlinkedList}");
                };
            }
            //останавливаем счётчик
            stopwatch.Stop();
            //смотрим сколько миллисекунд было затрачено на выполнение
            executionTimeLinkedList = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            foreach (int elementList in intList)
            {
                if (elementList != 0 && elementList % dividedBy == 0)
                {
                    Console.WriteLine($"... ArrayList: = {elementList}");
                }

            }
            //останавливаем счётчик
            stopwatch.Stop();
            executionTimeList = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            foreach (int elementarrayList in arrayList)
            {
                if (elementarrayList != 0 && elementarrayList % dividedBy == 0)
                {
                    Console.WriteLine($"... ArrayList: = {elementarrayList}");
                }

            }
            //останавливаем счётчик
            stopwatch.Stop();
            //смотрим сколько миллисекунд было затрачено на выполнение
            executionTimeArrayList = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"\n-- деление без остатка на {dividedBy} LinkedList заняло:\t{executionTimeLinkedList,8}");
            Console.WriteLine($"\n-- деление без остатка на {dividedBy} List заняло:\t{executionTimeList,16}");
            Console.WriteLine($"\n-- деление без остатка на {dividedBy} ArrayList заняло:\t{executionTimeArrayList,16}");
        }
    }

}
