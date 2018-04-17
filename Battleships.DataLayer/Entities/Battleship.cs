using Battleships.Common;
using Battleships.Models.Contracts;

namespace Battleships.Models
{
    public class Battleship : Ship, IShip
    {
        public Battleship(ShipDirection direction)
            : base(GlobalConstants.BattleshipSize, direction, GlobalConstants.BattleshipSymbol)
        {
        }

        public Battleship(ShipDirection direction, Position topLeft)
            : base(GlobalConstants.BattleshipSize, direction, GlobalConstants.BattleshipSymbol, topLeft)
        {
        }
    }
}
