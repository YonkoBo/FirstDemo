using Battleships.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Battleships.Logic.Contracts;
using Battleships.Logic;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            IInterface userInterface = new ConsoleInterface();
            IRender renderer = new ConsoleRender();
            IGameInitializationStrategy gameInitializationStrategy = new GameInitializationStrategy();
            Engine gameEngine = new Engine(renderer, userInterface, gameInitializationStrategy);

            gameEngine.Run();
        }
    }
}
