﻿using Battleships.Models;

namespace Battleships.Logic.Commands.Contracts
{
    public interface IProcessCommandStrategy
    {
        void ProcessCommand(Grid hiddenGrid, Grid visibleGrid);
    }
}
