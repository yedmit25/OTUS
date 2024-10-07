using Planets2;

namespace Planet3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int _countRequest = 0;


            bool PlanetValidator(string Name)
            {
                _countRequest++;
                if (_countRequest % 3 == 0)
                {
                    return true;
                }
                return false;
            }

            // Задание 3
            var SolarSystem = new CataloguePlanet();

            Console.WriteLine(SolarSystem.GetPlanet("Кебаб", PlanetValidator));

            Console.WriteLine(SolarSystem.GetPlanet("Марс", PlanetValidator));

            Console.WriteLine(SolarSystem.GetPlanet("Венера", PlanetValidator));

            Console.WriteLine(SolarSystem.GetPlanet("Венера", PlanetValidator));

            Console.WriteLine(SolarSystem.GetPlanet("Венера", PlanetValidator));

            // Через лямбду выражение

            _countRequest = 0;

            Console.WriteLine("Через лямбда-выражение");
            Console.WriteLine(SolarSystem.GetPlanet("Венера", name =>
            {

                _countRequest++;

                if (_countRequest % 3 == 0)
                {
                    return true;
                }
                return false;

            }));

            Console.WriteLine(SolarSystem.GetPlanet("Марс", name =>
            {

                _countRequest++;

                if (_countRequest % 3 == 0)
                {
                    return true;
                }
                return false;

            }));

            Console.WriteLine(SolarSystem.GetPlanet("Лимпопо", name =>
            {

                _countRequest++;

                if (_countRequest % 3 == 0)
                {
                    return true;
                }
                return false;

            }));

            // 4 запрос
            Console.WriteLine(SolarSystem.GetPlanet("Лимпопо", name =>
            {

                _countRequest++;

                if (_countRequest % 3 == 0)
                {
                    return true;
                }
                return false;

            }));
        }


    }
}
