using Battleships.DataLayer.Entities.AutoPlay.Contracts;
using System.Collections.Generic;

namespace Battleships.DataLayer.Entities.AutoPlay
{
    public class BotGameBoard : IBotGameBoard
    {
        public List<BotPanels> Panels { get; set; }
        public BotGameBoard()
        {
            Panels = new List<BotPanels>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Panels.Add(new BotPanels(i, j));
                }
            }
        }
    }
}
