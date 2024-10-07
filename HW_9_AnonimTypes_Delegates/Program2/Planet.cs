using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program2
{
    internal class Planet
    {
        private string name;
        private int id;
        private int lengthEquator;
        private object previusPlanet;

        public Planet(string _name, int _id, int _lengthEquator, object _previusPlanet)
        {
            Name = _name;
            Id = _id;
            LengthEquator = _lengthEquator;
            PreviusPlanet = _previusPlanet;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int LengthEquator
        {
            get { return lengthEquator; }
            set { lengthEquator = value; }
        }
        public object PreviusPlanet
        {
            get { return previusPlanet; }
            set { previusPlanet = value; }
        }
    }
}
