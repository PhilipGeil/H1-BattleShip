using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    class Player
    {
        public string Name { get; set; }

        public List<string> playerHits = new List<string>();
        public List<Ship> playerShips = new List<Ship>() { new Ship("Patruljebåd", 2), new Ship("Ubåd", 3), new Ship("Destroyer", 3), new Ship("Slagskib", 4), new Ship("Hangarskib", 5) };

        public Player() { }
    }
}
