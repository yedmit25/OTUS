using System;
using System.Collections.Immutable;

namespace HW_13_Poem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Начальная пустая коллекция
            ImmutableList<string> initialCollection = ImmutableList<string>.Empty;

            // Создание экземпляров всех классов
            Part1 part1 = new Part1();
            Part2 part2 = new Part2();
            Part3 part3 = new Part3();
            Part4 part4 = new Part4();
            Part5 part5 = new Part5();
            Part6 part6 = new Part6();
            Part7 part7 = new Part7();
            Part8 part8 = new Part8();
            Part9 part9 = new Part9();

            // Добавление частей стихотворения в коллекцию, сохраняя неизменность исходной коллекции
            ImmutableList<string> poemPart1 = part1.AddPart(initialCollection);
            ImmutableList<string> poemPart2 = part2.AddPart(poemPart1);
            ImmutableList<string> poemPart3 = part3.AddPart(poemPart2);
            ImmutableList<string> poemPart4 = part4.AddPart(poemPart3);
            ImmutableList<string> poemPart5 = part5.AddPart(poemPart4);
            ImmutableList<string> poemPart6 = part6.AddPart(poemPart5);
            ImmutableList<string> poemPart7 = part7.AddPart(poemPart6);
            ImmutableList<string> poemPart8 = part8.AddPart(poemPart7);
            ImmutableList<string> poemPart9 = part9.AddPart(poemPart8);

            //Console.WriteLine(part6);

            //// Вывод результата
            //Console.WriteLine("Стихотворение \"Дом, который построил Джек\":\n");
            foreach (var line in poemPart6)
            {
                Console.WriteLine(line);
            }
        }
    }
}
