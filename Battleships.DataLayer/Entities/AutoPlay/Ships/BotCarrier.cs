using Battleships.DataLayer.Common;
namespace Battleships.DataLayer.Entities.AutoPlay.Ships
{
    class BotCarrier : BotsShip
    {
        public BotCarrier()
        {
            Name = "Aircraft Carrier";
            Width = 5;
            OccupationType = OccupationType.Carrier;
        }
    }
}
