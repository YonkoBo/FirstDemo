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
        public DataLoader()
        {
        }
        public List<PlayerData> LoadData(IPlayerFactory playerFactory) //Loading all existing files from given path
        {
            List<PlayerData> playerData = new List<PlayerData>();
            IEnumerable<string> files = Directory.EnumerateFiles(GlobalConstants.Path, "*.json");
            foreach (var file in files)
            {
                PlayerData dataToAdd = ParsePlayerData(file, playerFactory);
                playerData.Add(dataToAdd);
            }
            return playerData;
        }
        private PlayerData ParsePlayerData(string data, IPlayerFactory playerFactory) //Creating new object of type PlayerData.
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
