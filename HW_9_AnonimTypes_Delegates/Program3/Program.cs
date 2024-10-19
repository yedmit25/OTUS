using System.Security.Cryptography.X509Certificates;

namespace Program3
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int _countRequest = 0;

            (Planet?, string?) PlanetValidator(string Name)
            {
                _countRequest++;
                if (_countRequest % 3 == 0)
                {
                    return new(null, "Вы спрашиваете слишком часто");
                }
                return new(null, "");
            }


            // Задание 3
            var SolarSystem = new CataloguePlanets();

            Console.WriteLine(SolarSystem.GetPlanet("Кебаб", PlanetValidator));

            Console.WriteLine(SolarSystem.GetPlanet("Марс", PlanetValidator));

            Console.WriteLine(SolarSystem.GetPlanet("Венера", PlanetValidator));

            Console.WriteLine(SolarSystem.GetPlanet("Земля", PlanetValidator));

            Console.WriteLine(SolarSystem.GetPlanet("Венера", PlanetValidator));

            // Через лямбду выражение

            _countRequest = 0;

            Console.WriteLine("Через лямбда-выражение");
            Console.WriteLine(SolarSystem.GetPlanet("Венера", name =>
            {

                _countRequest++;

                if (_countRequest % 3 == 0)
                {
                    return new(null, "Вы спрашиваете слишком часто");
                }
                return new(null, "");

            }));

            Console.WriteLine(SolarSystem.GetPlanet("Марс", name =>
            {

                _countRequest++;

                if (_countRequest % 3 == 0)
                {
                    return new(null, "Вы спрашиваете слишком часто");
                }
                return new(null, "");

            }));

            Console.WriteLine(SolarSystem.GetPlanet("Лимпопо", name =>
            {

                _countRequest++;

                if (_countRequest % 3 == 0)
                {
                    return new(null, "Вы спрашиваете слишком часто");
                }
                return new(null, "");

            }));

            //4 запрос
            Console.WriteLine(SolarSystem.GetPlanet("Лимпопо", name =>
            {

                _countRequest++;

                if (_countRequest % 3 == 0)
                {
                    return new(null, "Вы спрашиваете слишком часто");
                }
                return new(null, "");

            }));
        }
    }
}
