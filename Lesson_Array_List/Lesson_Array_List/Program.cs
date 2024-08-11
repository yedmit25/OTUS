using System.Collections;

namespace ComparisonLiastArraylistLinkedlist
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Массив");

            int[] arr = new int[10];

            arr[0] = 1;

            arr[1] = 2;

            arr[2] = 3;

            Console.WriteLine(arr[0]);
            Console.WriteLine(arr[1]);

            Random rng = new Random();

            int i = 0;

            Console.WriteLine("Список");

            var intl1 = new List<int>();

            var intl2 = new List<int>(100000);

            var intl3 = new List<int>(new[] { 1, 2, 3, 4, 5 });

            intl1.Add(1000);

            intl1.Insert(0, 500);

            Console.WriteLine("Элемент списка intl1 " + intl1[1]);

            intl2.Insert(0, 500);
            intl2.Insert(1, 1500);
            intl2.Insert(2, 2500);
            intl2.Insert(3, 3500);


            Console.WriteLine("Элемент списка intl2 " + intl2[2]);

            intl2.Clear();


            for (i = 0; i < 1000000; i++)
            {
                intl2.Add(rng.Next(100));

                if (i > 999990)
                {
                    Console.WriteLine("Элементы в intl2 " + intl2[i]);
                }
            }

            Console.WriteLine("Коллекции ArrayList");

            var arl = new ArrayList();

            arl.Add(0);
            arl.Add("Привет");
            arl.Add(true);


            Console.WriteLine("Коллекция arl " + arl);


            Console.WriteLine("Элемент коллекции arl: " + arl[1]);

            Console.WriteLine("Contains коллекции arl: " + arl.Contains(2));


            Console.WriteLine("Добавление через цкил for в arl");

            arl.Clear();



            Console.WriteLine("Количество элементов в коллекции arl: " + arl.Count);

            //for (int i = 0; i < arl.Count; i++)


            for (i = 0; i < 1000000; i++)
            {
                arl.Add(rng.Next(100));

                if (i > 999990)
                {
                    Console.WriteLine("Элементы в arl " + arl[i]);
                }
            };

            Console.WriteLine("Количество элементов в arl " + arl.Count);



            Console.WriteLine("LinkedList");

            var lnkList1 = new LinkedList<int>();
            for (i = 0; i < 1000000; i++)
            {
                lnkList1.AddLast(rng.Next(100));

                if (i > 999990)
                {
                    Console.WriteLine("Элементы LinkedList в lnkList1 " + lnkList1.ElementAt(i));
                }
            }
        }
    }
}
