using Battleships.DataLayer.Common;

namespace Battleships.DataLayer.Entities.AutoPlay.Ships
{
    public class BotSubmarine : BotsShip
    {
        public BotSubmarine()
        {
            Name = "Submarine";
            Width = 3;
            OccupationType = OccupationType.Submarine;
        }
    }
}
