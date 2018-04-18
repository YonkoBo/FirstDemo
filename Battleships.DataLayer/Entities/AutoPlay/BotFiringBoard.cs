using Battleships.DataLayer.Common;
using Battleships.DataLayer.Entities.AutoPlay.Contracts;
using Battleships.DataLayer.Entities.AutoPlay.Extensions;
using Battleships.Models;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.DataLayer.Entities.AutoPlay
{
    public class BotFiringBoard : BotGameBoard
    {
        public List<BotCoordinates> GetOpenRandomPanels()
        {
            return Panels.Where(x => x.OccupationType == OccupationType.Empty && x.IsRandomAvailable).Select(x => x.Coordinates).ToList();
        }
        public List<BotCoordinates> GetHitNeighbors()
        {
            List<BotPanels> panels = new List<BotPanels>();
            var hits = Panels.Where(x => x.OccupationType == OccupationType.Hit);
            foreach (var hit in hits)
            {
                panels.AddRange(GetNeighbors(hit.Coordinates).ToList());
            }
            return panels.Distinct().Where(x => x.OccupationType == OccupationType.Empty).Select(x => x.Coordinates).ToList();
        }

        public List<BotPanels> GetNeighbors(BotCoordinates coordinates)
        {
            int row = coordinates.Row;
            int column = coordinates.Column;
            List<BotPanels> panels = new List<BotPanels>();
            if (column > 1)
            {
                panels.Add(Panels.At(row, column - 1));
            }
            if (row > 1)
            {
                panels.Add(Panels.At(row - 1, column));
            }
            if (row < 10)
            {
                panels.Add(Panels.At(row + 1, column));
            }
            if (column < 10)
            {
                panels.Add(Panels.At(row, column + 1));
            }
            return panels;
        }
    }
}
