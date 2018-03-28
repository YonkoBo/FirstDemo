using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Battleships.Models
{
    public class Game
    {
        private Board GameBoard { get; set; }
        public int ShipsHitCount { get; set; }
        public double TimePlayed { get; set; }
        public int Score = 81;

        public bool GameFinished => ShipsHitCount >= 2;

        public Game()
        {
            GameBoard = new Board(new Grid[GameSettings.BoardWidht, GameSettings.BoardHeight]);
        }

        #region GameMethods
        public void Run()
        {
            Stopwatch stopWatch = new Stopwatch();//Used fro tracking time played.
            stopWatch.Start(); //Starting the timer.
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
                    else
                    {
                        Score--;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Error: Please enter numbers between 0 and 8!");
                    Console.ReadLine();
                }
            }
            if (GameFinished)
            {
                Draw();
                Console.WriteLine("Game Complete");
                stopWatch.Stop();//Stopping the timer.
                TimePlayed = Math.Round(stopWatch.Elapsed.TotalMinutes); //Indicating time played.
                Console.ReadLine();
            }
        }
        private void Draw()
        {
            Console.Clear();
            GameBoard.Draw();
        }
        #endregion
    }
}
