namespace ObjectClasses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var s = new Stack("asdf54", "b", "c", "d", "sdfsdf");

            try
            {
                Console.WriteLine("Исходный массив S:");
                foreach (var x in s)
                {
                    Console.Write($"{x}  ");
                }

                // size = 5, Top = 'sdfsdf'
                Console.WriteLine($"\nsize = {s.Size}, Top = '{s.Top}'");

                var deleted = s.Pop();
                // Извлек верхний элемент 'sdfsdf' Size = 4
                Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
                s.Add("d");
                // size = 4, Top = 'd'
                Console.WriteLine($"Добавили новый элемент\n\rsize = {s.Size}, Top = '{s.Top}'");
                Console.WriteLine("Удалеяем все элементы посредством метода Pop()");
                s.Pop();
                s.Pop();
                s.Pop();
                s.Pop();
                s.Pop();
                // size = 0, Top = null
                Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");

                foreach (var x in s)
                {
                    Console.Write(x);
                }
                //Доп задание 1
                var smerge = new Stack("A", "B", "C", "D");

                smerge.Merge(new Stack("1", "2", "3", "4"));
                Console.WriteLine("Объединенный массив А и Б");
                foreach (var m in smerge)
                {
                    Console.Write($"{m};");
                }

                //Доп задание 2

                Console.WriteLine("\nИсходные массивы для конкетенации");

                Console.WriteLine("\nМассив 1");
                Stack s1 = new Stack("z", "a", "q");
                foreach (var x in s1)
                {
                    Console.Write($"{x} ;");
                }
                Console.WriteLine("\nМассив 2");
                Stack s2 = new Stack("s", "d", "f", "g");
                foreach (var x in s2)
                {
                    Console.Write($"{x} ;");
                }

                Console.WriteLine("\nМассив 3");
                Stack s3 = new Stack("1", "2", "3", "4");
                foreach (var x in s3)
                {
                    Console.Write($"{x} ;");
                }

                Console.WriteLine("\nМассив 4");
                Stack s4 = new Stack("А", "Б", "В", "Г");
                foreach (var x in s4)
                {
                    Console.Write($"{x} ;");
                }


                Console.WriteLine("\nМассив после конкетенации");
                var sconcat = Stack.Concat(s1, s2, s3, s4);

                foreach (string item in sconcat)
                {
                    Console.Write($"{item}; ");
                }


                Console.WriteLine("\nПытаемся удалить элемент из пустого массива S");
                s.Pop();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
