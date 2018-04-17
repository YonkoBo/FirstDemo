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
        public int TotalAttempts { get; set; }
        public IList<IShip> ShipsAdded { get; set; }
        public ShootCommand()
        {
        }
        public void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData)
        {
            if (hiddenGrid.GetCell(shotPosition) != GlobalConstants.BlankSymbol)
            {
                this.ProcessShipHit(visibleGrid, shotPosition);
                visibleGrid.SetCell(shotPosition, GlobalConstants.HitSymbol);
            }
            else
            {
                visibleGrid.SetCell(shotPosition, GlobalConstants.MissSymbol);
            }
            totalAttempts++;
        }
        private void ProcessShipHit(Grid visibleGrid, Position shotPosition)
        {
            for (int i = 0; i < this.Ships.Count; i++)
            {
                var currentShip = this.Ships[i];
                if (IsShipHit(currentShip, shotPosition) && visibleGrid.GetCell(shotPosition) != GlobalConstants.HitSymbol)
                {
                    HitShip(currentShip);

                    if (IsShipSinking(currentShip))
                    {
                        SinkShip(currentShip);
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
