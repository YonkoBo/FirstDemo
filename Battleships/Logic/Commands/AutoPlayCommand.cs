using System;
using System.Collections.Generic;
using Battleships.Logic.Commands.Contracts;
using Battleships.Models;
using Battleships.Models.Contracts;

namespace Battleships.Logic.Commands
{
    public class AutoPlayCommand : IProcessCommandStrategy
    {
        public AutoPlayCommand()
        {

        }
        public void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> ship)
        {
            throw new NotImplementedException();
        }
    }
}
