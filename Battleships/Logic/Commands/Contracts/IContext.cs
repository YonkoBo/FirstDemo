using Battleships.Models;
using Battleships.Models.Contracts;
using System.Collections.Generic;

namespace Battleships.Logic.Commands.Contracts
{
    public interface IContext
    {
        void ExecuteCommands(UserCommands command, Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> Ship);
    }
}
