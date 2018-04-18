using Battleships.DataLayer.Common;
using System.ComponentModel;

namespace Battleships.DataLayer.Entities.AutoPlay
{
    public class BotPanels
    {
        public OccupationType OccupationType { get; set; }
        public BotCoordinates Coordinates { get; set; }
        public BotPanels(int row, int column)
        {
            Coordinates = new BotCoordinates(row, column);
            OccupationType = OccupationType.Empty;
        }
        public string Status
        {
            get
            {
                return OccupationType.GetAttributeOfType<DescriptionAttribute>().Description;
            }
        }

        public bool IsOccupied
        {
            get
            {
                return OccupationType == OccupationType.Battleship
                    || OccupationType == OccupationType.Destroyer
                    || OccupationType == OccupationType.Cruiser
                    || OccupationType == OccupationType.Submarine
                    || OccupationType == OccupationType.Carrier;
            }
        }

        public bool IsRandomAvailable
        {
            get
            {
                return (Coordinates.Row % 2 == 0 && Coordinates.Column % 2 == 0)
                    || (Coordinates.Row % 2 == 1 && Coordinates.Column % 2 == 1);
            }
        }
    }
}
