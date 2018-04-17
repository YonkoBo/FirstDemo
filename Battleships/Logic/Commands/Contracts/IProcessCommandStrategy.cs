﻿using Battleships.Models;
using System.Collections.Generic;

namespace Battleships.Logic.Commands.Contracts
{
    public interface IProcessCommandStrategy
    {
        void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData);
    }
}
