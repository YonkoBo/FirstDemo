using System;
using System.Collections.Generic;
using Battleships.Logic.Commands.Contracts;
using Battleships.Models;
using Battleships.Models.Contracts;
using Battleships.AutoPlay;

namespace Battleships.Logic.Commands
{
    public class AutoPlayCommand : IProcessCommandStrategy
    {
        IBotGamePlay _botGame;
        public AutoPlayCommand(IBotGamePlay botGame)
        {
            this._botGame = botGame;
        }
        public void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> ship)
        {
            this._botGame.Run();
        }
    }
}
