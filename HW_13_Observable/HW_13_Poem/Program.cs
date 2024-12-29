namespace HW_13_Poem
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

            // Инициализация коллекции и добавление частей стихотворения
            List<string> initialCollection = new List<string>();
            List<string> poemPart1 = part1.AddPart(initialCollection);
            List<string> poemPart2 = part2.AddPart(poemPart1);
            List<string> poemPart3 = part3.AddPart(poemPart2);
            List<string> poemPart4 = part4.AddPart(poemPart3);
            List<string> poemPart5 = part5.AddPart(poemPart4);
            List<string> poemPart6 = part6.AddPart(poemPart5);
            List<string> poemPart7 = part7.AddPart(poemPart6);
            List<string> poemPart8 = part8.AddPart(poemPart7);
            List<string> poemPart9 = part9.AddPart(poemPart8);

            // Вывод результата
            Console.WriteLine($"Часть 1:\n{string.Join("\n", part1.Poem)}");
            Console.WriteLine();
            Console.WriteLine($"Часть 2:\n{string.Join("\n", part2.Poem)}");
            Console.WriteLine();
            Console.WriteLine($"Часть 3:\n{string.Join("\n", part3.Poem)}");
            Console.WriteLine();
            Console.WriteLine($"Часть 4:\n{string.Join("\n", part4.Poem)}");
            Console.WriteLine();
            Console.WriteLine($"Часть 5:\n{string.Join("\n", part5.Poem)}");
            Console.WriteLine();
            Console.WriteLine($"Часть 6:\n{string.Join("\n", part6.Poem)}");
            Console.WriteLine();
            Console.WriteLine($"Часть 7:\n{string.Join("\n", part7.Poem)}");
            Console.WriteLine();
            Console.WriteLine($"Часть 8:\n{string.Join("\n", part8.Poem)}");
            Console.WriteLine();
            Console.WriteLine($"Часть 9:\n{string.Join("\n", part9.Poem)}");
        }
    }
}
