using Battleships.Logic.Contracts;
using Battleships.Models;
using System;
using System.IO;

namespace Battleships.Logic
{
    public class DataCreator : IDataCreator
    {
        public void CreateNewPlayerFile(string playerName, double timePlayed, int score, IPlayerFactory playerFactory) //Creats a new .json file in selected path.
        {
            Guid id = Guid.NewGuid();
            PlayerData newPlayerData = playerFactory.CreatePlayerData(playerName, score, timePlayed, id);
            string fileName = string.Format("_{0}_{1}_{2}_{3}_.json", newPlayerData.ID, newPlayerData.PlayerName, newPlayerData.TimePlayed, newPlayerData.Score);
            File.Create(fileName);
        }
    }
}
