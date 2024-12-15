using System.Linq.Expressions;

namespace HW14_LINQ_operator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int> { 10, 2, 13, 40, 501, 61, 37, 18, 29, 303, 42 };
            var top30Percent = list.Top(30);

            Console.WriteLine("Top 30% of list: " + string.Join(", ", top30Percent));

            var people = new List<Person>
        {
            new Person { Name = "Алиса", Age = 25 },
            new Person { Name = "Брис", Age = 30 },
            new Person { Name = "Михаил", Age = 35 },
            new Person { Name = "Петрония", Age = 28 },
            new Person { Name = "Артём", Age = 18 },
            new Person { Name = "Володя", Age = 45 },
            new Person { Name = "Алина", Age = 42 },
            new Person { Name = "Илья", Age = 60 }
        };
            var top30PercentByAge = people.Top(30, person => person.Age);

            Console.WriteLine("Top 30% by age: " + string.Join(", ", top30PercentByAge.Select(p => p.Name + " (" + p.Age + ")")));
            
            // задаём аргумент за пределами диапазона от 1 до 100
            var top30PercentByAge_exc = people.Top(101, person => person.Age);

            Console.WriteLine("Top 30% by age: " + string.Join(", ", top30PercentByAge_exc.Select(p => p.Name + " (" + p.Age + ")")));
        }
    }
}
