using Battleships.Models;

namespace Battleships.Logic.Contracts
{
    public interface IRender
    {
        void RenderGrid(Grid grid);

        void UpdateGrid(Grid grid, Position position);

        void RenderMessage(string message, bool setCursor = true);

        void RenderStatusMessage(string message);

        void RenderErrorMessage(string message);

        void Clear();

        void ClearError();
    }
}
