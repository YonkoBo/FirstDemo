using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;
using Battleships.Common;

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
        public void ProcessCommand()
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
