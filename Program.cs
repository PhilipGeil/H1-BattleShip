using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    class Program
    {
        static Game game = new Game();
        static List<string> digits = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        static void Main(string[] args)
        {
            Console.SetBufferSize(200, 200);
            Console.SetWindowSize(100, 50);
            Board board = new Board();
            Board enemyBoard = new Board();
            board.CreateBoard();
            enemyBoard.CreateBoard();
            game.PlaceEnemyShips();
            SetShips(board);
            while (!game.CheckForPlayerOneWinner() && !game.CheckForPlayerTwoWinner())
            {
                Console.Clear();
                Console.WriteLine("Your board");
                PrintBoard(board);
                Console.WriteLine();
                Console.WriteLine("Enemy board");
                PrintEnemyBoard(enemyBoard);
                Console.WriteLine("Enter the position you wish to hit");
                if (game.CheckForPlayerOneWinner())
                {
                    Console.Clear();
                    Console.WriteLine("Player 1 has won the game");
                }
                else if (game.CheckForPlayerTwoWinner())
                {
                    Console.Clear();
                    Console.WriteLine("The computer won");
                }
                else
                {
                    game.playerOneHits.Add(Console.ReadLine().ToUpper());
                    game.EnemyHit();
                }
            }

            Console.ReadKey();
        }

        static void PrintBoard(Board board)
        {
            SetHits(game.playerTwoHits, board);
            foreach (Ship ship in game.playerOneShips)
            {
                SetShip(ship, board);
            }
            Console.Write(" ");
            for (int i = 1; i < 11; i++)
            {
                Console.Write("  " + i.ToString() + "  ");
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < board.fields.Count; i++)
            {
                Console.Write(digits[i]);
                for (int j = 0; j < board.fields[i].Count; j++)
                {
                    if (board.fields[i][j].Ship && board.fields[i][j].Hit)
                    {
                        PrintOut("H");
                    }
                    else if (board.fields[i][j].Hit)
                    {
                        PrintOut("X");
                    }
                    else if (board.fields[i][j].Ship)
                    {
                        PrintOut("S");
                    }
                    else
                    {
                        PrintOut("O");

                    }


                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        static void SetShips(Board board)
        {
            foreach (Ship ship in game.playerOneShips)
            {
                Console.Clear();
                PrintBoard(board);
                Console.WriteLine("Choose start position for {0}, which has the size of {1}", ship.Name, ship.Size);
                string startPos = Console.ReadLine().ToUpper();
                Console.WriteLine("Good choise, now choose the end position for it");
                int chosenLetter = digits.IndexOf(startPos.ToCharArray()[0].ToString()) + 1;
                double chosenNumb = char.GetNumericValue(startPos.ToCharArray()[1]);
                List<string> options = new List<string>();
                if (startPos.ToCharArray().Length == 3)
                {
                    chosenNumb = int.Parse((char.GetNumericValue(startPos.ToCharArray()[1]).ToString() + (char.GetNumericValue(startPos.ToCharArray()[2]).ToString())));
                }
                if (chosenLetter - ship.Size >= 0)
                {
                    options.Add(digits[chosenLetter - ship.Size].ToString() + chosenNumb.ToString());
                }
                if (chosenLetter + ship.Size - 2 <= 9)
                {
                    options.Add(digits[ship.Size + chosenLetter - 2].ToString() + chosenNumb.ToString());
                }
                if (chosenNumb - ship.Size >= 0)
                {
                    options.Add(digits[chosenLetter - 1] + (chosenNumb - ship.Size + 1).ToString());
                }
                if (chosenNumb + ship.Size - 1 <= 9)
                {
                    options.Add(digits[chosenLetter - 1] + (chosenNumb + ship.Size - 1).ToString());
                }
                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine("{0}: {1}", i + 1, options[i]);
                }
                string endPos = options[int.Parse(Console.ReadLine()) - 1];
                InsertPosition(startPos, endPos, ship);
            }
        }

        static void InsertPosition(string start, string end, Ship ship)
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
        static void PrintEnemyBoard(Board board)
        {
            SetHits(game.playerOneHits, board);
            foreach (Ship ship in game.playerTwoShips)
            {
                SetShip(ship, board);
            }
            Console.Write(" ");
            for (int i = 1; i < 11; i++)
            {
                Console.Write("  " + i.ToString() + "  ");
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < board.fields.Count; i++)
            {
                Console.Write(digits[i]);
                for (int j = 0; j < board.fields[i].Count; j++)
                {
                    if (board.fields[i][j].Ship && board.fields[i][j].Hit)
                    {
                        PrintOut("H");
                    }
                    else if (board.fields[i][j].Hit)
                    {
                        PrintOut("X");
                    }
                    else
                    {
                        PrintOut("O");

                    }


                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        static void SetShip(Ship ship, Board board)
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
                    game.HitShip(ship);
                }
            }
            if (ship.Sunk && !game.sunkenShips.Contains(ship))
            {
                Console.WriteLine("{0} has just been shot to the bottom of the ocean!", ship.Name);
                game.sunkenShips.Add(ship);
            }
            for (int i = 0; i < boatDigitIndex.Count; i++)
            {
                board.fields[boatDigitIndex[i]][Convert.ToInt32(boatNumbIndex[i] - 1)].Ship = true;
            }
        }

        static void SetHits(List<string> hits, Board board)
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

        static void PrintOut(string character)
        {
            switch (character)
            {
                case "X":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("  X  ");
                    break;
                case "S":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("  S  ");
                    break;
                case "H":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("  H  ");
                    break;
                default:
                    Console.Write("  O  ");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
