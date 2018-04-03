using Battleships.Common;
using Battleships.Models.Contracts;

namespace Battleships.Models
{
    public class Destroyer : Ship, IShip
    {
        public Destroyer(ShipDirection direction)
            : base(GlobalConstants.DestroyerSize, direction, GlobalConstants.DestroyerSymbol)
        {
        }

        public Destroyer(ShipDirection direction, Position topLeft)
            : base(GlobalConstants.DestroyerSize, direction, GlobalConstants.DestroyerSymbol, topLeft)
        {
        }
    }
}
