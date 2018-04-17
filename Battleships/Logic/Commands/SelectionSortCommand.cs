using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;
using System.Collections.Generic;
using Battleships.Models;

namespace Battleships.Logic.Commands
{
    public class SelectionSortCommand : IProcessCommandStrategy
    {
        private GameStatus gameStatus;
        private IRender renderer;
        public List<PlayerData> PlayerData { get; set; }
        public SelectionSortCommand(IRender renderer, List<PlayerData> playerData)
        {
            this.renderer = renderer;
            PlayerData = playerData;
        }
        public void ProcessCommand()
        {
            this.gameStatus = GameStatus.Sorting;
            this.renderer.Clear();
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sorting Data by Time Played Using SelectionSort");
            IEnumerable<PlayerData> sortedData = SortingAlgorithms.SelectionSortByTimePlayed(PlayerData);
            foreach (var data in sortedData)
            {
                Console.WriteLine(string.Format("ID:{0},Time Played:{1} s.,Player Name:{2},Score:{3}", data.ID, data.TimePlayed, data.PlayerName, data.Score));
            }
        }
    }
}
