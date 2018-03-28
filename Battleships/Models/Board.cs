using System;
using System.Collections.Generic;

namespace Battleships.Models
{
    class Board
    {
        public Grid[,] Grid { get; set; }
        int x = 0;
        int y = 0;
        public Board(Grid[,] grid)
        {
            Grid = grid;
            InitializeBoard();
            PopulateBoardShips();
        }

        private void InitializeBoard()
        {
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    var grid = new Grid();
                    Grid[y, x] = grid;
                }
            }
        }
        /// <summary>
        /// Populates the game board with random ships
        /// </summary>
        private void PopulateBoardShips()
        {
            //1 of length 2
            SetGrid(1, 2);
            SetGrid(2, 2);
            //2 of length 3
            SetGrid(4, 3);
            SetGrid(4, 4);
            SetGrid(4, 5);

            SetGrid(5, 0);
            SetGrid(6, 0);
            SetGrid(7, 0);
            //1 of length 4
            SetGrid(0, 8);
            SetGrid(1, 8);
            SetGrid(2, 8);
            SetGrid(3, 8);
            //1 of length 5
            SetGrid(7, 4);
            SetGrid(7, 5);
            SetGrid(7, 6);
            SetGrid(7, 7);
            SetGrid(7, 8);
        }

        /// <summary>
        /// Sets a ship to givven x and y coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetGrid(int x, int y)
        {
            Grid[x, y].IsShip = true;
        }

        /// <summary>
        /// Draws the boundries of the table.
        /// </summary>
        public void Draw()
        {
            Console.WriteLine("  ¦ 0 1 2 3 4 5 6 7 8");
            Console.WriteLine("--+-------------------");
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                Console.Write((i).ToString() + " ¦ ");
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Console.Write(Grid[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n");
        }
        public KeyValuePair<int, int> AskForCoordinates()
        {
            Console.WriteLine("Enter X");
            string xInput = Console.ReadLine();
            GetXCoordinate(xInput);
            Console.WriteLine("Enter Y");
            string yInput = Console.ReadLine();
            GetYCoordiante(yInput);
            return new KeyValuePair<int, int>(x, y);
        }
        public void GetXCoordinate(string input)
        {
            int value;
            if (int.TryParse(input, out value))
            {
                x = value;
            }
            else
            {
                Console.WriteLine("Not a valid number!");
            }
        }
        public void GetYCoordiante(string input)
        {
            int value;
            if (int.TryParse(input, out value))
            {
                y = value;
            }
            else
            {
                Console.WriteLine("Not a valid number!");
            }
        }

        public bool Move(int x, int y)
        {
            GetCurrentLocation(x, y).IsHit = true;
            if (GetCurrentLocation(x, y).IsShip)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Grid GetCurrentLocation(int x, int y)
        {
            return Grid[x, y];
        }
    }
}
