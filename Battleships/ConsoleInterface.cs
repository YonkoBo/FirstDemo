﻿using Battleships.Common;
using Battleships.Logic;
using Battleships.Logic.Contracts;
using Battleships.Models;
using System;

namespace Battleships
{
    public class ConsoleInterface : IInterface
    {
        private string coordinates;

        public string GetUserInput() //Gets user input
        {
            var input = Console.ReadLine();
            return input;
        }

        public UserCommands GetCommandFromInput() //Gets user input from console.
        {
            string input = this.GetUserInput().ToUpper();

            switch (input)
            {
                case GlobalConstants.ShowCommand:
                    return UserCommands.Show;
                case GlobalConstants.ExitCommand:
                    return UserCommands.Exit;
                case GlobalConstants.NewGameCommand:
                    return UserCommands.New;
                case GlobalConstants.AgreeCommand:
                    return UserCommands.Agree;
                case GlobalConstants.BubbleSortCommand:
                    return UserCommands.BubbleSort;
                case GlobalConstants.SelectionSortCommand:
                    return UserCommands.SelectionSort;
                case GlobalConstants.AutoPlayCommand:
                    return UserCommands.AutoPlay;
                default:
                    if (this.AreValidCoordinates(input)) //Validation for correct cordinates.
                    {
                        this.coordinates = input;
                        return UserCommands.Shoot;
                    }

                    return UserCommands.Invalid;
            }
        }

        public Position GetShotPositionFromInput()
        {
            return Position.GetFromBattleshipBoard(this.coordinates[0], this.coordinates.Substring(1));
        }

        public void ExitGame()
        {
            Environment.Exit(0);
        }

        private bool AreValidCoordinates(string coordinates)
        {
            int col;
            if (coordinates.Length < 2 || coordinates.Length > 3 || !char.IsLetter(coordinates[0])
                || coordinates[0] < GlobalConstants.MinRowValueOnGrid || coordinates[0] > GlobalConstants.MaxRowValueOnGrid || !int.TryParse(coordinates.Substring(1), out col))
            {
                return false;
            }

            return true;
        }
    }
}
