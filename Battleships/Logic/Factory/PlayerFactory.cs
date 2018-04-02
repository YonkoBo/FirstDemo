using Battleships.Models;
using System;

namespace Battleships.Logic.Factory
{
    public class PlayerFactory
    {
        public PlayerData CreatePlayerData(string playerName, int score, double timePlayed, Guid id)
        {
            return new PlayerData
            {
                ID = id,
                PlayerName = playerName,
                Score = score,
                TimePlayed = timePlayed
            };
        }
    }
}
