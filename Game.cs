using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    class Game
    {

        List<string> digits = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        //List created in order to see which ships are sunken
        public List<Ship> sunkenShips = new List<Ship>();

        public Player player = new Player();

        public Player computer = new Player();

        public Game() { }
        /// <summary>
        /// Place a ship
        /// </summary>
        /// <param name="ship">The ship to place</param>
        /// <param name="pos">The position of the placed ship</param>
        public void SetShips(Ship ship, List<string> pos)
        {
            ship.position = pos;
        }
        /// <summary>
        /// Hits a ship
        /// </summary>
        /// <param name="ship"></param>
        public void HitShip(Ship ship)
        {
            ship.Health--;
            if (ship.Health == 0)
            {
                ship.Sunk = true;
            }
        }
        /// <summary>
        /// Checks if player two has won the game
        /// </summary>
        /// <returns></returns>
        public bool CheckForWinner(Player player)
        {
            foreach (Ship ship in player.playerShips)
            {
                if (!ship.Sunk)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Randomly places the enemy ships
        /// </summary>
        public void PlaceEnemyShips()
        {
            List<string> allPos = new List<string>();
            Random rand = new Random();
            foreach (Ship ship in computer.playerShips)
            {
                bool check = false;
                while (!check)
                {
                    List<string> pos = new List<string>();
                    string startDigit = digits[rand.Next(0, 10)];
                    int startNumber = numbers[rand.Next(0, 10)];
                    string startPos = startDigit + startNumber.ToString();
                    pos.Add(startPos);
                    //Randomly decides whether the ship should be placed vertical or horizontal
                    if (rand.Next(0, 2) == 0)
                    {
                        //Place a ship horizontal
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
                        //Place a ship vertical
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
                    //Checks if positions is available, to avoid double positions
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
        /// <summary>
        /// Method to make the enemy attack, a new random position
        /// </summary>
        public void EnemyHit()
        {
            Random rand = new Random();
            bool check = true;
            while (check)
            {
                string pos = digits[rand.Next(0, 10)] + numbers[rand.Next(0, 10)].ToString();
                if (!player.playerHits.Contains(pos))
                {
                    player.playerHits.Add(pos);
                    check = false;
                }
            }
        }

        public List<string> GetPositionOptions(Ship ship, string startPos)
        {
            int chosenLetter = digits.IndexOf(startPos.ToCharArray()[0].ToString()) + 1;
            double chosenNumb = char.GetNumericValue(startPos.ToCharArray()[1]);
            List<string> options = new List<string>();
            if (startPos.ToCharArray().Length == 3)
            {
                chosenNumb = int.Parse((char.GetNumericValue(startPos.ToCharArray()[1]).ToString() + (char.GetNumericValue(startPos.ToCharArray()[2]).ToString())));
            }
            if (chosenLetter - ship.Size >= 0)
            {
                string pos = digits[chosenLetter - ship.Size].ToString() + chosenNumb.ToString();
                if (CheckValidPosition(startPos, pos))
                {
                    options.Add(pos);
                }
            }
            if (chosenLetter + ship.Size - 2 <= 9)
            {
                string pos = digits[ship.Size + chosenLetter - 2].ToString() + chosenNumb.ToString();
                if (CheckValidPosition(startPos, pos))
                {
                    options.Add(pos);

                }
            }
            if (chosenNumb - ship.Size >= 0)
            {
                string pos = digits[chosenLetter - 1] + (chosenNumb - ship.Size + 1).ToString();
                if (CheckValidPosition(startPos, pos))
                {
                    options.Add(pos);
                }
            }
            if (chosenNumb + ship.Size - 1 <= 9)
            {
                string pos = digits[chosenLetter - 1] + (chosenNumb + ship.Size - 1).ToString();
                if (CheckValidPosition(startPos, pos))
                {
                    options.Add(pos);
                }
            }
            return options;
        }

        bool CheckValidPosition(string startPos, string endPos)
        {
            List<string> occPos = new List<string>();
            foreach (Ship ship in player.playerShips)
            {
                occPos.AddRange(ship.position);
            }
            string startLetter = startPos.ToCharArray()[0].ToString();
            string endLetter = endPos.ToCharArray()[0].ToString();
            int startNumber = int.Parse((startPos.ToCharArray()[1]).ToString());
            int endNumber = int.Parse((endPos.ToCharArray()[1]).ToString());
            if (startPos.ToCharArray().Length == 3)
            {
                startNumber = int.Parse((startPos.ToCharArray()[1]).ToString() + (startPos.ToCharArray()[2]).ToString());
            }
            if (endPos.ToCharArray().Length == 3)
            {
                endNumber = int.Parse((endPos.ToCharArray()[1]).ToString() + (endPos.ToCharArray()[2]).ToString());
            }
            if (startLetter == endLetter)
            {
                if (startNumber > endNumber)
                {
                    while (startNumber >= endNumber)
                    {
                        if (occPos.Contains(startLetter + startNumber.ToString()))
                        {
                            return false;
                        }
                        startNumber--;
                    }
                }
                else
                {
                    while (startNumber <= endNumber)
                    {
                        if (occPos.Contains(startLetter + startNumber.ToString()))
                        {
                            return false;
                        }
                        startNumber++;
                    }
                }
            }
            else
            {
                int startIndex = digits.IndexOf(startLetter);
                int endIndex = digits.IndexOf(endLetter);
                if (startIndex > endIndex)
                {
                    while (startIndex >= endIndex)
                    {
                        if (occPos.Contains(digits[startIndex] + startNumber.ToString()))
                        {
                            return false;
                        }
                        startIndex--;
                    }
                }
                else
                {
                    while (startIndex <= endIndex)
                    {
                        if (occPos.Contains(digits[startIndex] + startNumber.ToString()))
                        {
                            return false;
                        }
                        startIndex++;
                    }
                }
            }
            return true;
        }
        public void InsertPosition(string start, string end, Ship ship)
        {
            //Store letters and numbers in variables for easy use
            string startLetter = start.ToCharArray()[0].ToString();
            string endLetter = end.ToCharArray()[0].ToString();
            int startNumber = int.Parse((start.ToCharArray()[1]).ToString());
            int endNumber = int.Parse((end.ToCharArray()[1]).ToString());
            //Check if the number is 10
            if (start.ToCharArray().Length == 3)
            {
                startNumber = int.Parse((start.ToCharArray()[1]).ToString() + (start.ToCharArray()[2]).ToString());
            }
            if (end.ToCharArray().Length == 3)
            {
                endNumber = int.Parse((end.ToCharArray()[1]).ToString() + (end.ToCharArray()[2]).ToString());
            }
            //Check if the letters is the same
            if (startLetter == endLetter)
            {
                if (startNumber > endNumber)
                {
                    while (startNumber >= endNumber)
                    {
                        ship.position.Add((start.ToCharArray()[0]).ToString() + startNumber.ToString());
                        startNumber--;
                    }
                }
                else
                {
                    while (startNumber <= endNumber)
                    {
                        ship.position.Add((start.ToCharArray()[0]).ToString() + startNumber.ToString());
                        startNumber++;
                    }
                }
            }
            else
            {
                int startIndex = digits.IndexOf(startLetter);
                int endIndex = digits.IndexOf(endLetter);
                if (startIndex > endIndex)
                {
                    while (startIndex >= endIndex)
                    {
                        ship.position.Add(digits[startIndex] + startNumber.ToString());
                        startIndex--;
                    }
                }
                else
                {
                    while (startIndex <= endIndex)
                    {
                        ship.position.Add(digits[startIndex] + startNumber.ToString());
                        startIndex++;
                    }
                }
            }
        }
        public void SetHits(List<string> hits, Board board)
        {
            List<int> hitDigitIndex = new List<int>();
            List<double> hitNumbIndex = new List<double>();
            foreach (string pos in hits)
            {
                hitDigitIndex.Add(digits.IndexOf(pos.ToCharArray()[0].ToString()));
                if (pos.ToCharArray().Length == 3)
                {
                    hitNumbIndex.Add(double.Parse((pos.ToCharArray()[1]).ToString() + (pos.ToCharArray()[2]).ToString()));
                }
                else
                {
                    hitNumbIndex.Add(char.GetNumericValue(pos.ToCharArray()[1]));
                }
            }
            for (int i = 0; i < hitDigitIndex.Count; i++)
            {
                board.fields[hitDigitIndex[i]][Convert.ToInt32(hitNumbIndex[i] - 1)].Hit = true;
            }
        }

        public string SetShip(Ship ship, Board board)
        {
            List<int> boatDigitIndex = new List<int>();
            List<double> boatNumbIndex = new List<double>();
            ship.Health = ship.Size;
            foreach (string item in ship.position)
            {
                int letterIndex = digits.IndexOf(item.ToCharArray()[0].ToString());
                double numbIndex = char.GetNumericValue(item.ToCharArray()[1]);
                boatDigitIndex.Add(letterIndex);
                if (item.ToCharArray().Length == 3)
                {
                    numbIndex = double.Parse((item.ToCharArray()[1]).ToString() + (item.ToCharArray()[2]).ToString());

                }
                boatNumbIndex.Add(numbIndex);
                if (board.fields[letterIndex][Convert.ToInt32(numbIndex) - 1].Hit)
                {
                    HitShip(ship);
                }
            }
            if (ship.Sunk && !sunkenShips.Contains(ship))
            {
                sunkenShips.Add(ship);
                return ship.Name + " has just been shot to the bottom of the ocean!";
            }
            for (int i = 0; i < boatDigitIndex.Count; i++)
            {
                board.fields[boatDigitIndex[i]][Convert.ToInt32(boatNumbIndex[i] - 1)].Ship = true;
            }
            return null;
        }
    }
}
