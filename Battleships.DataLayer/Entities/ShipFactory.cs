using Battleships.Common;
using Battleships.Models.Contracts;
using System;

namespace Battleships.Models
{
    public class ShipFactory
    {
        public IShip Get(string shipType, ShipDirection direction)
        {
            switch (shipType)
            {
                case "Battleship":
                    return new Battleship(direction);
                case "Destroyer":
                    return new Destroyer(direction);
                default:
                    throw new InvalidOperationException(GlobalConstants.InvalidShipMsg);
            }
        }
    }
}
