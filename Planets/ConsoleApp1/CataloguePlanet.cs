using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets2
{
    internal class CataloguePlanet
    {
        private Planet _planet2;
        private Planet _planet3;
        private Planet _planet4;
        private List<Planet> _planetslist;
        private int _countRequest = 0;

        public CataloguePlanet()
        {
            _planet2 = new("Венера", 2, 1500, null);
            _planet3 = new("Земля", 3, 2500, _planet2);
            _planet4 = new("Марс", 4, 3500, _planet3);
            _planetslist = new List<Planet>(new[] { _planet2, _planet3, _planet4 });
        }
        public (int? id, int? length, string? error) GetPlanet(string name)
        {
            _countRequest++;
            if (_countRequest % 3 == 0)
            {
                return (id: null, length: null, error: "Вы спрашиваете слишком часто");
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
