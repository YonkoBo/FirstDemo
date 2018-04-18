using Battleships.Logic.Contracts;
using Battleships.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Battleships.Logic
{
    public class Helpers : IHelpers
    {
        public void AskPlayerName()
        {
            Console.WriteLine("Please Enter Player Name:");
        }
        public bool AreAllShipsSunk(IList<IShip> Ships)
        {
            for (int i = 0; i < Ships.Count; i++)
            {
                if (!Ships[i].IsSunk)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
