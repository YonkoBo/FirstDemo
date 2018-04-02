using Battleships.Models;

namespace Battleships.Logic.Contracts
{
    public interface IInterface
    {
        string GetUserInput();

        UserCommands GetCommandFromInput();

        Position GetShotPositionFromInput();

        void ExitGame();
    }
}
