using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;
using Battleships.Models;
using Battleships.Models.Contracts;
using System.Collections.Generic;
using Unity;

namespace Battleships.Logic.Commands
{
    public class Context : IContext
    {
        public Dictionary<UserCommands, IProcessCommandStrategy> commandsDictionary = new Dictionary<UserCommands, IProcessCommandStrategy>();

        private IUnityContainer _dependencyInjection;

        public Context(IUnityContainer dependencyInjection)
        {
            this._dependencyInjection = dependencyInjection;
            commandsDictionary.Add(UserCommands.BubbleSort, _dependencyInjection.Resolve<IProcessCommandStrategy>("BubbleSort"));
            commandsDictionary.Add(UserCommands.SelectionSort, _dependencyInjection.Resolve<IProcessCommandStrategy>("SelectionSort"));
            commandsDictionary.Add(UserCommands.Exit, _dependencyInjection.Resolve<IProcessCommandStrategy>("Exit"));
            commandsDictionary.Add(UserCommands.Invalid, _dependencyInjection.Resolve<IProcessCommandStrategy>("Invalid"));
            commandsDictionary.Add(UserCommands.New, _dependencyInjection.Resolve<IProcessCommandStrategy>("New"));
            commandsDictionary.Add(UserCommands.Shoot, _dependencyInjection.Resolve<IProcessCommandStrategy>("Shoot"));
            commandsDictionary.Add(UserCommands.Show, _dependencyInjection.Resolve<IProcessCommandStrategy>("Show"));
            commandsDictionary.Add(UserCommands.AutoPlay, dependencyInjection.Resolve<IProcessCommandStrategy>("AutoPlay"));
        }
        public void ExecuteCommands(UserCommands command, Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> ship)
        {
            commandsDictionary[command].ProcessCommand(hiddenGrid, visibleGrid, shotPosition, totalAttempts, playerData, ship);
        }
    }
}
