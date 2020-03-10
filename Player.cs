using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    class Player
    {
        /// <summary>
        /// The name of the player
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The hits on the players board
        /// </summary>
        public List<string> playerHits = new List<string>();
        /// <summary>
        /// The ships of the player
        /// </summary>
        public List<Ship> playerShips = new List<Ship>() { new Ship("Patruljebåd", 2), new Ship("Ubåd", 3), new Ship("Destroyer", 3), new Ship("Slagskib", 4), new Ship("Hangarskib", 5) };

        public Player() { }
    }
}
