using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Program3
{
    internal class CataloguePlanets
    {
        public delegate (Planet?, string?) PlanetValidatorDelegate(string name);
        private List<Planet> _planetslist = new();
        private int _countRequest = 0;

        public CataloguePlanets()
        {
            _planetslist.Add(new("Венера", 2, 1500, null));
            _planetslist.Add(new("Земля", 3, 2500, _planetslist.Last()));
            _planetslist.Add(new("Марс", 4, 3500, _planetslist.Last()));
        }
        public (int? id, int? length, string? error) GetPlanet(string name, PlanetValidatorDelegate validatorDelegate)
        {

            string ErrorMessage = validatorDelegate(name).Item2;

            if (ErrorMessage != "")
            {
                return (id: null, length: null, error: ErrorMessage);
            }

            foreach (var planet in _planetslist)
            {
                if (name.ToLower() == planet.Name.ToLower())
                {
                    Console.WriteLine($"Название планеты: {planet.Name}\n" +
                                      $"Порядковый номер: {planet.Id}, \n" +
                                      $"Длина экватора: {planet.LengthEquator}");
                    return (id: planet.Id, length: planet.LengthEquator, error: null);
                }
            }
            return (id: null, length: null, error: "Планету найти не удалось");
        }


    }
}
