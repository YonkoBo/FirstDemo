using Battleships.DataLayer.Common;
using Battleships.DataLayer.Entities.AutoPlay.Extensions;
using Battleships.DataLayer.Entities.AutoPlay.Ships;
using Battleships.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.DataLayer.Entities.AutoPlay
{
    public class BotPlayer
    {
        public string Name { get; set; }
        public BotGameBoard GameBoard { get; set; }
        public BotFiringBoard FiringBoard { get; set; }
        public List<BotsShip> Ships { get; set; }
        public bool HasLost
        {
            get
            {
                return Ships.All(x => x.IsSunk);
            }
        }
        public BotPlayer(string name)
        {
            Name = name;
            Ships = new List<BotsShip>()
            {
                new BotDestroyer(),
                new BotSubmarine(),
                new BotCruiser(),
                new BotBattleship(),
                new BotCarrier()
            };
            GameBoard = new BotGameBoard();
            FiringBoard = new BotFiringBoard();
        }
        public void OutputBoards()
        {
            Console.WriteLine(Name);
            Console.WriteLine("Own Board:                          Firing Board:");
            for (int row = 1; row <= 10; row++)
            {
                for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    Console.Write(GameBoard.Panels.At(row, ownColumn).Status + " ");
                }
                Console.Write("                ");
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    Console.Write(FiringBoard.Panels.At(row, firingColumn).Status + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

        public void PlaceShips()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in Ships)
            {
                //Select a random row/column combination, then select a random orientation.
                //If none of the proposed panels are occupied, place the ship
                //Do this for all ships

                bool isOpen = true;
                while (isOpen)
                {
                    var startcolumn = rand.Next(1, 11);
                    var startrow = rand.Next(1, 11);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rand.Next(1, 101) % 2; //0 for Horizontal

                    List<int> panelNumbers = new List<int>();
                    if (orientation == 0)
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endcolumn++;
                        }
                    }

                    //We cannot place ships beyond the boundaries of the board
                    if (endrow > 10 || endcolumn > 10)
                    {
                        isOpen = true;
                        continue;
                    }

                    //Check if specified panels are occupied
                    var affectedPanels = GameBoard.Panels.Range(startrow, startcolumn, endrow, endcolumn);
                    if (affectedPanels.Any(x => x.IsOccupied))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach (var panel in affectedPanels)
                    {
                        panel.OccupationType = ship.OccupationType;
                    }
                    isOpen = false;
                }
            }
        }

        public BotCoordinates FireShot()
        {
            //If there are hits on the board with neighbors which don't have shots, we should fire at those first.
            var hitNeighbors = FiringBoard.GetHitNeighbors();
            BotCoordinates coords;
            if (hitNeighbors.Any())
            {
                coords = SearchingShot();
            }
            else
            {
                coords = RandomShot();
            }
            Console.WriteLine(Name + " says: \"Firing shot at " + coords.Row.ToString() + ", " + coords.Column.ToString() + "\"");
            return coords;
        }


        private BotCoordinates RandomShot()
        {
            var availablePanels = FiringBoard.GetOpenRandomPanels();
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var panelID = rand.Next(availablePanels.Count);
            return availablePanels[panelID];
        }

        private BotCoordinates SearchingShot()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var hitNeighbors = FiringBoard.GetHitNeighbors();
            var neighborID = rand.Next(hitNeighbors.Count);
            return hitNeighbors[neighborID];
        }

        public GameStatus ProcessShot(BotCoordinates coords)
        {
            var panel = GameBoard.Panels.At(coords.Row, coords.Column);
            if (!panel.IsOccupied)
            {
                Console.WriteLine(Name + " says: \"Miss!\"");
                return GameStatus.Miss;
            }
            var ship = Ships.First(x => x.OccupationType == panel.OccupationType);
            ship.Hits++;
            Console.WriteLine(Name + " says: \"Hit!\"");
            if (ship.IsSunk)
            {
                Console.WriteLine(Name + " says: \"You sunk my " + ship.Name + "!\"");
            }
            return GameStatus.Hit;
        }

        public void ProcessShotResult(BotCoordinates coords, GameStatus result)
        {
            var panel = FiringBoard.Panels.At(coords.Row, coords.Column);
            switch (result)
            {
                case GameStatus.Hit:
                    panel.OccupationType = OccupationType.Hit;
                    break;

                default:
                    panel.OccupationType = OccupationType.Miss;
                    break;
            }
        }
    }
}
