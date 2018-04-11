using Battleships.Common;
using Battleships.Logic.Contracts;
using Battleships.Logic.Factory;
using Battleships.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Battleships.Logic
{
    public class DataLoader : IDataLoader
    {
        private List<PlayerData> playerData;
        private PlayerFactory playerFactory;
        public DataLoader()
        {
            this.playerFactory = new PlayerFactory();
            this.playerData = new List<PlayerData>();

        }
        public List<PlayerData> LoadData() //Loading all existing files from given path
        {
            IEnumerable<string> files = Directory.EnumerateFiles(GlobalConstants.Path, "*.json");
            foreach (var file in files)
            {
                PlayerData dataToAdd = ParsePlayerData(file);
                playerData.Add(dataToAdd);
            }
            return playerData;
        }
        public PlayerData ParsePlayerData(string data) //Creating new object of type PlayerData.
        {
            string[] dataSplited = data.Split('_');
            Guid id = new Guid(dataSplited[1]);
            string playerName = dataSplited[2];
            double timePlayed = double.Parse(dataSplited[3]);
            int score = Int32.Parse(dataSplited[4]);

            PlayerData playerData = playerFactory.CreatePlayerData(playerName, score, timePlayed, id);
            return playerData;
        }
    }
}
