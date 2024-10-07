namespace Program2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 2
            var catalog = new CataloguePlanets();
            Console.WriteLine(catalog.GetPlanet("Земля"));
            Console.WriteLine(catalog.GetPlanet("Лимония"));
            Console.WriteLine(catalog.GetPlanet("Марс"));

            Console.WriteLine(catalog.GetPlanet("Лимпопо"));
            Console.WriteLine(catalog.GetPlanet("Венера"));
            Console.WriteLine(catalog.GetPlanet("Земля"));
        }
    }
}
