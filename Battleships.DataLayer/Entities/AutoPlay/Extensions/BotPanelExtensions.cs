using System.Collections.Generic;
using System.Linq;

namespace Battleships.DataLayer.Entities.AutoPlay.Extensions
{
    public static class BotPanelExtensions
    {
        public static BotPanels At(this List<BotPanels> panels, int row, int column)
        {
            return panels.Where(x => x.Coordinates.Row == row && x.Coordinates.Column == column).First();
        }

        public static List<BotPanels> Range(this List<BotPanels> panels, int startRow, int startColumn, int endRow, int endColumn)
        {
            return panels.Where(x => x.Coordinates.Row >= startRow
                                     && x.Coordinates.Column >= startColumn
                                     && x.Coordinates.Row <= endRow
                                     && x.Coordinates.Column <= endColumn).ToList();
        }
    }
}
