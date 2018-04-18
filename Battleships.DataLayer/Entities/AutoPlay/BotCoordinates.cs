namespace Battleships.DataLayer.Entities.AutoPlay
{
    public class BotCoordinates
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public BotCoordinates(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
