using Battleships.DataLayer.Common;

namespace Battleships.DataLayer.Entities.AutoPlay.Ships
{
    public class BotBattleship : BotsShip
    {
        public BotBattleship()
        {
            Name = "Battleship";
            Width = 4;
            OccupationType = OccupationType.Battleship;
        }
    }
}
