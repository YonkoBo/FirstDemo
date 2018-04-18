using Battleships.Models.Contracts;
using System.Collections.Generic;

namespace Battleships.Logic.Contracts
{
    public interface IHelpers
    {
        void AskPlayerName();
        bool AreAllShipsSunk(IList<IShip> Ships);
    }
}
