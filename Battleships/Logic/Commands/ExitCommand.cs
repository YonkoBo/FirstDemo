using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;
using Battleships.Common;
using Battleships.Models;
using System.Collections.Generic;
using Battleships.Models.Contracts;

namespace Battleships.Logic.Commands
{
    public class ExitCommand : IProcessCommandStrategy
    {
        private IRender renderer;
        private IInterface userInterface;
        public ExitCommand(IRender renderer, IInterface userInterface)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
        }
        public void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> ship)
        {
            this.renderer.RenderMessage(string.Format(GlobalConstants.ExitMsg, GlobalConstants.AgreeCommand));
            UserCommands command = this.userInterface.GetCommandFromInput();

            if (command == UserCommands.Agree)
            {
                this.userInterface.ExitGame();
            }
            else
            {
                this.renderer.RenderMessage(GlobalConstants.EnterCoordinatesMsg);
            }
        }
    }
}
