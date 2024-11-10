namespace HW12_Dictionary_hashset
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OtusDictionary dictionary = new OtusDictionary();

            // Добавление элементов
            dictionary.Add(1, "Велосипед");
            dictionary.Add(2, "Самокат");
            dictionary.Add(3, "Мячи");

            // Получение элементов
            Console.WriteLine($"Ожидаем Первый элемент: {dictionary.Get(1)}"); // Ожидается "Первый элемент"
            Console.WriteLine($"Ожидаем Второй элемент: {dictionary[2]}"); // Ожидается "Второй элемент"
            Console.WriteLine($"Ожидаем Третий элемент: {dictionary[3]}"); // Ожидается "Второй элемент"


            // Проверка на коллизии
            Console.WriteLine("Проверка на коллизии");
            Console.WriteLine($"Проверка на не существующий элемент 33 {dictionary.Get(33)}");

            dictionary.Add(33, "Горные лыжи"); // 33 % 32 = 1 (коллизия с ключом 1)
            Console.WriteLine($"Ожидаем Первый элемент: {dictionary.Get(1)}"); // Ожидается "Первый элемент"
            Console.WriteLine($"Ожидаем Тридцать Третий элемент: {dictionary.Get(33)}"); // Ожидается "Третий элемент"

            // Использование индексатора
            Console.WriteLine("Использование индексатора");
            dictionary[4] = "Беговые дорожки";
            Console.WriteLine($"Ожидаем Четвертый элемент: {dictionary[4]}"); // Ожидается "Четвертый элемент"

            // Проверка на получение несуществующего элемента
            Console.WriteLine($"Проверка на не существующий элемент 99 {dictionary.Get(99)}"); // Ожидается null
        }
    }
}
