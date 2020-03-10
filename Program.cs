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
            game.computer.Name = "Computer";
            Console.SetBufferSize(200, 200);
            Console.SetWindowSize(100, 50);
            Console.WriteLine("Welcome to battleship");
            Console.WriteLine("Please enter your name: ");
            game.player.Name = Console.ReadLine();
            Console.Clear();
            Board board = new Board();
            Board enemyBoard = new Board();
            board.CreateBoard();
            enemyBoard.CreateBoard();
            game.PlaceEnemyShips();
            SetShips(board);
            while (!game.CheckForWinner(game.player) && !game.CheckForWinner(game.computer))
            {
                Console.Clear();
                Console.WriteLine("Your board");
                PrintBoard(board, game.player);
                Console.WriteLine();
                Console.WriteLine("Enemy board");
                PrintBoard(enemyBoard, game.computer);
                Console.WriteLine("Enter the position you wish to hit");
                if (game.CheckForWinner(game.player))
                {
                    Console.Clear();
                    Console.WriteLine("{0} has won the game", game.computer.Name);
                }
                else if (game.CheckForWinner(game.computer))
                {
                    Console.Clear();
                    Console.WriteLine("{0} has won the game", game.player.Name);
                }
                else
                {
                    bool check = false;
                    while (!check)
                    {
                        string startPos = Console.ReadLine().ToUpper();
                        if (digits.Contains(startPos.ToCharArray()[0].ToString()))
                        {
                            if (startPos.ToCharArray().Length == 2)
                            {
                                if (Convert.ToInt32(startPos.ToCharArray()[1].ToString()) >= 1 && Convert.ToInt32(startPos.ToCharArray()[1].ToString()) <= 9)
                                {
                                    check = true;
                                }
                            }
                            else if (startPos.ToCharArray().Length == 3)
                            {
                                if (Convert.ToInt32(startPos.ToCharArray()[1].ToString() + startPos.ToCharArray()[2].ToString()) == 10)
                                {
                                    check = true;
                                }
                            }
                        }
                        if (check)
                        {
                            game.computer.playerHits.Add(startPos);
                            game.EnemyHit();
                        }
                    }
                }
            }

            Console.ReadKey();
        }
        /// <summary>
        /// Prints the given board, with the right types
        /// </summary>
        /// <param name="board">the board to display</param>
        /// <param name="player">the player, whos board shall be printed</param>
        static void PrintBoard(Board board, Player player)
        {
            game.SetHits(player.playerHits, board);
            foreach (Ship ship in player.playerShips)
            {
                string sunkenShip = game.SetShip(ship, board);
                if (sunkenShip != null)
                {
                    Console.WriteLine(sunkenShip);
                }
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
                    else if (board.fields[i][j].Ship && player.Name != "Computer")
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
        /// <summary>
        /// Method used for placing the users ships on the board
        /// </summary>
        /// <param name="board"></param>
        static void SetShips(Board board)
        {
            foreach (Ship ship in game.player.playerShips)
            {
                bool check = false;
                while (!check)
                {
                    Console.Clear();
                    PrintBoard(board, game.player);
                    Console.WriteLine("Choose start position for {0}, which has the size of {1}", ship.Name, ship.Size);
                    string startPos = Console.ReadLine().ToUpper();
                    if (digits.Contains(startPos.ToCharArray()[0].ToString()))
                    {
                        if (startPos.ToCharArray().Length == 2)
                        {
                            if (Convert.ToInt32(startPos.ToCharArray()[1].ToString()) >= 1 && Convert.ToInt32(startPos.ToCharArray()[1].ToString()) <= 9)
                            {
                                check = true;
                            }
                        }
                        else if (startPos.ToCharArray().Length == 3)
                        {
                            if (Convert.ToInt32(startPos.ToCharArray()[1].ToString() + startPos.ToCharArray()[2].ToString()) == 10)
                            {
                                check = true;
                            }
                        }
                    }
                    if (check)
                    {
                        Console.WriteLine("Good choise, now choose the end position for it");
                        List<string> options = game.GetPositionOptions(ship, startPos);
                        if (options.Count == 1)
                        {
                            string endPos = options[0];
                            game.InsertPosition(startPos, endPos, ship);
                            check = true;
                        }
                        else if (options.Count > 0)
                        {
                            for (int i = 0; i < options.Count; i++)
                            {
                                Console.WriteLine("{0}: {1}", i + 1, options[i]);
                            }
                            string endPos = options[int.Parse(Console.ReadLine()) - 1];
                            game.InsertPosition(startPos, endPos, ship);
                            check = true;
                        }
                        else
                        {
                            check = false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Prints out the given character, and the right color
        /// </summary>
        /// <param name="character">The character to print out</param>
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
