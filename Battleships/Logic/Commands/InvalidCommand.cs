using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Common;
using Battleships.Models;
using System.Collections.Generic;
using Battleships.Models.Contracts;

namespace Battleships.Logic.Commands
{
    public class InvalidCommand : IProcessCommandStrategy
    {
        public void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> ship)
        {
            throw new InvalidOperationException(GlobalConstants.InvalidCommandMsg);
        }
    }
}
