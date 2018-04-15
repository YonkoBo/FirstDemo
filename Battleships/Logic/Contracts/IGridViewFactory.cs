using Battleships.Models;

namespace Battleships.Logic.Contracts
{
    public interface IGridViewFactory
    {
        Grid CreateNewGrid();
    }
}
