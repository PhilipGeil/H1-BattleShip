using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    class Ship
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public bool Sunk { get; set; }
        public int Health { get; set; }

        public List<string> position = new List<string>();
        public Ship(string name, int size)
        {
            Name = name;
            Size = size;
            Health = size;
            Sunk = false;
        }
    }
}
