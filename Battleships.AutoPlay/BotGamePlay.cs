using Battleships.DataLayer.Entities.AutoPlay;
using Battleships.DataLayer.Entities.AutoPlay.Contracts;
using System;

namespace Battleships.AutoPlay
{
    public class BotGamePlay : IBotGamePlay
    {
        public BotGamePlay()
        {
        }
        public void Run()
        {
            int player1Wins = 0, player2Wins = 0;

            Console.WriteLine("How many games do you want to play?");
            var numGames = int.Parse(Console.ReadLine());

            for (int i = 0; i < numGames; i++)
            {
                BotGame game1 = new BotGame();
                game1.PlayToTheEnd();
                if (game1.Player1.HasLost)
                {
                    player2Wins++;
                }
                else
                {
                    player1Wins++;
                }
            }

            Console.WriteLine("Player 1 Wins: " + player1Wins.ToString());
            Console.WriteLine("Player 2 Wins: " + player2Wins.ToString());
            Console.ReadLine();
        }
    }
}
