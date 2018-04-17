using Battleships.Models;
using System;

namespace Battleships.Logic.Contracts
{
    public interface IPlayerFactory
    {
        PlayerData CreatePlayerData(string playerName, int score, double timePlayed, Guid id);
    }
}
