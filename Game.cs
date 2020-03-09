using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    class Game
    {
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }

        public List<Ship> playerOneShips = new List<Ship>() { new Ship("Patruljebåd", 2), new Ship("Ubåd", 3), new Ship("Destroyer", 3), new Ship("Slagskib", 4), new Ship("Hangarskib", 5) };
        public List<Ship> playerTwoShips = new List<Ship>() { new Ship("Patruljebåd", 2), new Ship("Ubåd", 3), new Ship("Destroyer", 3), new Ship("Slagskib", 4), new Ship("Hangarskib", 5) };
        public List<string> playerOneHits = new List<string>();
        public List<string> playerTwoHits = new List<string>();
        public List<Ship> sunkenShips = new List<Ship>();

        public Game() { }

        public void SetShips(Ship ship, List<string> pos)
        {
            ship.position = pos;
        }

        public void HitShip(Ship ship)
        {
            ship.Health--;
            if (ship.Health == 0)
            {
                ship.Sunk = true;
            }
        }

        public bool CheckForPlayerOneWinner()
        {
            foreach (Ship ship in playerTwoShips)
            {
                if (!ship.Sunk)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckForPlayerTwoWinner()
        {
            foreach (Ship ship in playerOneShips)
            {
                if (!ship.Sunk)
                {
                    return false;
                }
            }
            return true;
        }

        public void PlayerOneShoot(string pos)
        {
            playerOneHits.Add(pos);
        }
        public void PlaceEnemyShips()
        {
            List<string> allPos = new List<string>();
            Random rand = new Random();
            foreach (Ship ship in playerTwoShips)
            {
                bool check = false;
                while (!check)
                {
                    List<string> pos = new List<string>();
                    List<string> digits = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                    List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    string startDigit = digits[rand.Next(0, 10)];
                    int startNumber = numbers[rand.Next(0, 10)];
                    string startPos = startDigit + startNumber.ToString();
                    pos.Add(startPos);
                    //Randomly decides whether the ship should be placed vertical or horizontal
                    if (rand.Next(0, 2) == 0)
                    {
                        for (int i = 1; i < ship.Size; i++)
                        {
                            if (startNumber + ship.Size <= 10)
                            {
                                string nextPos = startDigit + (startNumber + i).ToString();
                                pos.Add(nextPos);
                            }
                            else
                            {
                                string nextPos = startDigit + (startNumber - i).ToString();
                                pos.Add(nextPos);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Size; i++)
                        {
                            int letterIndex = digits.IndexOf(startDigit);
                            if (letterIndex + ship.Size <= 10)
                            {
                                string nextPos = digits[letterIndex + i] + startNumber.ToString();
                                pos.Add(nextPos);
                            }
                            else
                            {
                                string nextPos = digits[letterIndex - i] + startNumber.ToString();
                                pos.Add(nextPos);
                            }
                        }
                    }
                    check = true;
                    foreach (string item in pos)
                    {
                        if (allPos.Contains(item))
                        {
                            check = false;
                        }
                    }
                    if (check)
                    {
                        allPos.AddRange(pos);
                        ship.position = pos;
                    }
                }
            }
        }

        public void EnemyHit()
        {
            List<string> digits = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Random rand = new Random();
            bool check = true;
            while (check)
            {
                string pos = digits[rand.Next(0, 10)] + numbers[rand.Next(0, 10)].ToString();
                if (!playerTwoHits.Contains(pos))
                {
                    playerTwoHits.Add(pos);
                    check = false;
                }
            }
        }
    }
}
