using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Models;
using System.Collections.Generic;
using Battleships.Logic.Contracts;

namespace Battleships.Logic.Commands
{
    public class BubbleSortCommand : IProcessCommandStrategy
    {
        private IRender renderer;
        private GameStatus gamestatus;
        public List<PlayerData> PlayerData { get; set; }
        public BubbleSortCommand(List<PlayerData> playerData, IRender renderer)
        {
            PlayerData = playerData;
            this.renderer = renderer;
        }
        public void ProcessCommand()
        {
            this.gamestatus = GameStatus.Sorting;
            this.renderer.Clear();
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sorting Data by Score Ascending Using BubbbleSort");
            IEnumerable<PlayerData> sortedData = SortingAlgorithms.BubbleSortByScore(PlayerData);
            foreach (var data in sortedData)
            {
                Console.WriteLine(string.Format("ID:{0},Time Played:{1} s.,Player Name:{2},Score:{3}", data.ID, data.TimePlayed, data.PlayerName, data.Score));
            }
        }
    }
}
