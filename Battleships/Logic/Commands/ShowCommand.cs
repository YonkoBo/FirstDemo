using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;
using Battleships.Models;

namespace Battleships.Logic.Commands
{
    public class ShowCommand : IProcessCommandStrategy
    {
        private GameStatus gameStatus;
        private IRender renderer;
        private IInterface userInterface;
        public void ProcessCommand( Grid hiddenGrid, Grid visibleGrid)
        {
            this.gameStatus = GameStatus.Show;
            this.renderer.RenderStatusMessage(this.gameStatus.ToString());
            this.renderer.RenderGrid(hiddenGrid);
            this.renderer.RenderGrid(visibleGrid);
        }
    }
}
