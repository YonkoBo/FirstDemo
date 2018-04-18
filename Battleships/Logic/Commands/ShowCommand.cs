using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;
using Battleships.Models;
using System.Collections.Generic;
using Battleships.Models.Contracts;

namespace Battleships.Logic.Commands
{
    public class ShowCommand : IProcessCommandStrategy
    {
        private GameStatus gameStatus;
        private IRender renderer;
        private IInterface userInterface;
        public ShowCommand(IRender renderer, IInterface userInterface)
        {
            this.renderer = renderer; ;
            this.userInterface = userInterface;
        }
        public void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> ship)
        {
            this.gameStatus = GameStatus.Show;
            this.renderer.RenderStatusMessage(this.gameStatus.ToString());
            this.renderer.RenderGrid(hiddenGrid);
            this.userInterface.GetCommandFromInput();
            this.renderer.RenderGrid(visibleGrid);
            this.renderer.UpdateGrid(visibleGrid, shotPosition);
        }
    }
}
