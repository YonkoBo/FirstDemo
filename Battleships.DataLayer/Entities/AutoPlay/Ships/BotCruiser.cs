using Battleships.DataLayer.Common;

namespace Battleships.DataLayer.Entities.AutoPlay.Ships
{
    public class BotCruiser : BotsShip
    {
        public BotCruiser()
        {
            Name = "Cruiser";
            Width = 3;
            OccupationType = OccupationType.Cruiser;
        }
    }
}
