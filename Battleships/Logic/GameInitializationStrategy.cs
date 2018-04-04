using Battleships.Common;
using Battleships.Logic.Contracts;
using Battleships.Models;
using Battleships.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Battleships.Logic
{
    public class GameInitializationStrategy : IGameInitializationStrategy
    {
        private static Random random;

        private readonly Dictionary<Type, int> shipsType;
        private ShipFactory shipFactory;

        static GameInitializationStrategy()
        {
            random = new Random();
        }

        public GameInitializationStrategy()
        {
            this.shipsType = new Dictionary<Type, int>
            {
                { typeof(Battleship), GlobalConstants.InitialBattleshipsCount },
                { typeof(Destroyer), GlobalConstants.InitialDestroyerCount }
            };

            this.shipFactory = new ShipFactory();
        }

        public void Initialize(Grid hiddenGrid, Grid visibleGrid, IList<IShip> ships)
        {
            this.FillInitialGrid(visibleGrid, GlobalConstants.NoShotSymbol); //Filling blank visible grid with '.'
            this.FillInitialGrid(hiddenGrid, GlobalConstants.BlankSymbol); //Filing blank hidden grid with empty chars.
            this.AddShips(hiddenGrid, ships); //Adding ships for hidden grid.
        }

        private void FillInitialGrid(Grid grid, char symbol)
        {
            for (int row = 0; row < grid.TotalRows; row++)
            {
                for (int col = 0; col < grid.TotalCols; col++)
                {
                    grid.SetCell(row, col, symbol);
                }
            }
        }

        private void AddShips(Grid grid, IList<IShip> ships)
        {
            foreach (var shipType in this.shipsType)
            {
                for (int i = 0; i < shipType.Value; i++)
                {
                    ShipDirection direction = this.GetRandomShipDirection(); //Gets a ranodm ship direction
                    IShip ship = this.shipFactory.Get(shipType.Key.Name, direction); //Creates the ship using factory.
                    Position randomShipPosition = this.GetRandomShipPosition(ship.Size, direction); //Returns random Ship position.

                    while (this.ShipsOverlap(ship, grid)) //Checks if ships overlap.
                    {
                        ship.ShipPosition = this.GetRandomShipPosition(ship.Size, direction);
                    }

                    this.AddShip(ship, ships);
                    grid.PlaceShip(ship);
                }
            }
        }

        private void AddShip(IShip ship, IList<IShip> ships)
        {
            ships.Add(ship);
        }

        private bool ShipsOverlap(IShip ship, Grid grid)
        {
            int shipRow = ship.ShipPosition.Row;
            int shipCol = ship.ShipPosition.Col;

            for (int i = 0; i < ship.Size; i++)
            {
                if (grid.GetCell(shipRow, shipCol) == ship.Image)
                {
                    return true;
                }

                if (ship.Direction == ShipDirection.Vertical)
                {
                    shipRow++;
                }
                else
                {
                    shipCol++;
                }
            }

            return false;
        }

        private ShipDirection GetRandomShipDirection()
        {
            return random.Next(0, 2) == 0 ? ShipDirection.Vertical : ShipDirection.Horizontal; //if 0 returns vertical otherwise return horizontal.
        }

        private Position GetRandomShipPosition(int shipSize, ShipDirection direction)
        {
            int row;
            int col;

            if (direction == ShipDirection.Horizontal)
            {
                row = random.Next(0, GlobalConstants.GridRowsCount);
                col = random.Next(0, GlobalConstants.GridColsCount - shipSize);
            }
            else
            {
                row = random.Next(0, GlobalConstants.GridRowsCount - shipSize);
                col = random.Next(0, GlobalConstants.GridColsCount);
            }

            return new Position(row, col);
        }
    }
}
