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
        public Grid HiddenGrid { get; set; }
        public Grid VisibleGrid { get; set; }
        public ShowCommand(Grid hiddenGrid, Grid visibleGrid, IRender renderer, IInterface userInterface)
        {
            this.renderer = renderer; ;
            this.userInterface = userInterface;
            HiddenGrid = hiddenGrid;
            VisibleGrid = visibleGrid;
        }
        public void ProcessCommand()
        {
            this.gameStatus = GameStatus.Show;
            this.renderer.RenderStatusMessage(this.gameStatus.ToString());
            this.renderer.RenderGrid(HiddenGrid);
            this.userInterface.GetCommandFromInput();
            this.renderer.RenderGrid(VisibleGrid);
        }
    }
}
