using Battleships.DataLayer.Common;

namespace Battleships.DataLayer.Entities.AutoPlay.Ships
{
    class BotDestroyer : BotsShip
    {
        public BotDestroyer()
        {
            Name = "Destroyer";
            Width = 2;
            OccupationType = OccupationType.Destroyer;
        }
    }
}
