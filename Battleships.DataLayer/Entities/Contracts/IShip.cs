namespace Battleships.Models.Contracts
{
    public interface IShip
    {
        int Size { get; }

        ShipDirection Direction { get; }

        char Image { get; }

        Position ShipPosition { get; set; }

        bool IsSunk { get; set; }

        int HitsCount { get; set; }
    }
}
