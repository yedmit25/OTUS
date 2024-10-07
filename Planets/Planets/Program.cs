namespace Planets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Planet2 = new
            {
                Name = "Венера",
                Id = 2,
                LenghtEquator = 1000,
                PreviusPlanet = ""
            };
            var Planet3 = new
            {
                Name = "Земля",
                Id = 3,
                LenghtEquator = 3000,
                PreviusPlanet = Planet2.Name
            };
            var Planet4 = new
            {
                Name = "Марс",
                Count = 4,
                LenghtEquator = 1500,
                PreviusPlanet = Planet3.Name
            };


            Console.WriteLine("-------------1 задание");


            //Задание 1
            Console.WriteLine("Сравнивает Землю с Венерой " + Planet3.Equals(Planet2));

            Console.WriteLine("Сравнивает Марс с Венерой " + Planet4.Equals(Planet2));

            Console.WriteLine("Сравнивает Венеру с Венерой " + Planet2.Equals(Planet2));

        }
    }
}
