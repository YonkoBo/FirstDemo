using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Models;
using System.Collections.Generic;
using Battleships.Logic.Contracts;
using Battleships.Models.Contracts;

namespace Battleships.Logic.Commands
{
    public class BubbleSortCommand : IProcessCommandStrategy
    {
        private IRender renderer;
        public BubbleSortCommand(IRender renderer)
        {
            this.renderer = renderer;
        }
        public void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> ship)
        {
            this.renderer.Clear();
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sorting Data by Score Ascending Using BubbbleSort");
            IEnumerable<PlayerData> sortedData = SortingAlgorithms.BubbleSortByScore(playerData);
            foreach (var data in sortedData)
            {
                Console.WriteLine(string.Format("ID:{0},Time Played:{1} s.,Player Name:{2},Score:{3}", data.ID, data.TimePlayed, data.PlayerName, data.Score));
            }
        }
    }
}
