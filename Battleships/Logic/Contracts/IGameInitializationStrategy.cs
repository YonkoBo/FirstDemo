using Battleships.Models;
using Battleships.Models.Contracts;
using System.Collections.Generic;

namespace Battleships.Logic.Contracts
{
    public interface IGameInitializationStrategy
    {
        void Initialize(Grid hiddenGrid, Grid visibleGrid, IList<IShip> ships);
    }
}
