using Battleships.DataLayer.Common;
namespace Battleships.DataLayer.Entities.AutoPlay
{
    public abstract class BotsShip
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Hits { get; set; }
        public OccupationType OccupationType { get; set; }
        public bool IsSunk
        {
            get
            {
                return Hits >= Width;
            }
        }
    }
}
