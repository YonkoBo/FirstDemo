using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Models
{
    public class Game
    {
        private Board GameBoard { get; set; }
        public int ShipsHitCount { get; set; }
        public bool GameFinished => ShipsHitCount >= 9;

        public Game()
        {
            GameBoard = new Board(new Grid[GameSettings.BoardWidht, GameSettings.BoardHeight]);
        }
        public void Run()
        {
            while (!GameFinished)
            {
                Draw();
                KeyValuePair<int, int> input = GameBoard.AskForCoordinates(); //Asks user for X and Y coordinates.
                try
                {
                    var shipHit = GameBoard.Move(input.Key, input.Value);
                    if (shipHit)
                    {
                        ShipsHitCount++;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Error: Please enter numbers between 0 and 9. (Inclusive)");
                    Console.ReadLine();
                }
            }
            if (GameFinished)
            {
                Draw();
                Console.WriteLine("Game Complete");
                Console.ReadLine();
            }
        }
        private void Draw()
        {
            Console.Clear();
            GameBoard.Draw();
        }
    }
}
