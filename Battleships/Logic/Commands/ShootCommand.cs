using System;
using Battleships.Logic.Commands.Contracts;
using Battleships.Models;
using Battleships.Common;
using System.Collections.Generic;
using Battleships.Models.Contracts;

namespace Battleships.Logic.Commands
{
    public class ShootCommand : IProcessCommandStrategy
    {
        public IList<IShip> Ships
        {
            get
            {
                return new List<IShip>(ShipsAdded);
            }
        }
        public Grid HiddenGrid { get; set; }
        public Grid VisibleGrid { get; set; }
        public Position ShotPosition { get; set; }
        public int TotalAttempts { get; set; }
        public IList<IShip> ShipsAdded { get; set; }
        GameStatus gameStatus;
        public ShootCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, GameStatus gameStatus, IList<IShip> shipsAdded)
        {
            HiddenGrid = hiddenGrid;
            VisibleGrid = visibleGrid;
            ShotPosition = shotPosition;
            TotalAttempts = totalAttempts;
            this.gameStatus = gameStatus;
            ShipsAdded = shipsAdded;
        }
        public void ProcessCommand()
        {
            if (HiddenGrid.GetCell(ShotPosition) != GlobalConstants.BlankSymbol)
            {
                this.ProcessShipHit();
                VisibleGrid.SetCell(ShotPosition, GlobalConstants.HitSymbol);
            }
            else
            {
                VisibleGrid.SetCell(ShotPosition, GlobalConstants.MissSymbol);
                gameStatus = GameStatus.Miss;
            }

            TotalAttempts++;
        }
        private void ProcessShipHit()
        {
            for (int i = 0; i < this.Ships.Count; i++)
            {
                var currentShip = this.Ships[i];
                if (IsShipHit(currentShip, ShotPosition) && VisibleGrid.GetCell(ShotPosition) != GlobalConstants.HitSymbol)
                {
                    HitShip(currentShip);
                    gameStatus = GameStatus.Hit;

                    if (IsShipSinking(currentShip))
                    {
                        SinkShip(currentShip);
                        gameStatus = GameStatus.Sunk;
                    }
                }
            }
        }
        private bool IsShipHit(IShip ship, Position position)
        {
            var row = ship.ShipPosition.Row;
            var col = ship.ShipPosition.Col;

            for (int j = 0; j < ship.Size; j++)
            {
                if (position.Row == row && position.Col == col)
                {
                    return true;
                }

                if (ship.Direction == ShipDirection.Horizontal)
                {
                    col++;
                }
                else
                {
                    row++;
                }
            }

            return false;
        }
        private void HitShip(IShip ship)
        {
            ship.HitsCount++;

            if (ship.HitsCount > ship.Size)
            {
                throw new InvalidOperationException(GlobalConstants.InvalidShipHit);
            }
        }

        private bool IsShipSinking(IShip ship)
        {
            if (ship.HitsCount == ship.Size)
            {
                return true;
            }

            return false;
        }

        private void SinkShip(IShip ship)
        {
            ship.IsSunk = true;
        }
    }
}
