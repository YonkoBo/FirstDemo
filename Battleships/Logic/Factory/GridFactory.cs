using Battleships.Logic.Contracts;
using Battleships.Models;

namespace Battleships.Logic.Factory
{
    public class GridFactory : IGridViewFactory
    {
        public Grid CreateNewGrid()
        {
            return new Grid();
        }
    }
}
