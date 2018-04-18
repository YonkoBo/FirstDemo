using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;
using System.Collections.Generic;
using Battleships.Models;
using Battleships.Models.Contracts;

namespace Battleships.Logic.Commands
{
    public class SelectionSortCommand : IProcessCommandStrategy
    {
        private IRender renderer;
        public SelectionSortCommand(IRender renderer)
        {
            this.renderer = renderer;
        }
        public void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> ship)
        {
            this.renderer.Clear();
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sorting Data by Time Played Using SelectionSort");
            IEnumerable<PlayerData> sortedData = SortingAlgorithms.SelectionSortByTimePlayed(playerData);
            foreach (var data in sortedData)
            {
                Console.WriteLine(string.Format("ID:{0},Time Played:{1} s.,Player Name:{2},Score:{3}", data.ID, data.TimePlayed, data.PlayerName, data.Score));
            }
        }
    }
}
