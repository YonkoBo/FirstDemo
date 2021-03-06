﻿using Battleships.Logic.Contracts;
using Battleships.Logic;
using Battleships.Logic.Factory;
using Unity;
using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Commands;
using Battleships.DataLayer.Entities.AutoPlay.Contracts;
using Battleships.DataLayer.Entities.AutoPlay;
using Battleships.AutoPlay;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer dependencyInection = UnityDependencyInjection();

            //IBotGamePlay botPlay = dependencyInection.Resolve<BotGamePlay>();
            //botPlay.Run();
            IEngine engine = dependencyInection.Resolve<Engine>();
            engine.Run();
        }
        private static IUnityContainer UnityDependencyInjection()
        {
            IUnityContainer dependencyInjection = new UnityContainer();

            //App
            dependencyInjection.RegisterType<IEngine, Engine>();
            dependencyInjection.RegisterType<IContext, Context>();

            //Factories
            dependencyInjection.RegisterType<IPlayerFactory, PlayerFactory>();
            dependencyInjection.RegisterType<IGridViewFactory, GridFactory>();

            //Data
            dependencyInjection.RegisterType<IDataCreator, DataCreator>();
            dependencyInjection.RegisterType<IDataLoader, DataLoader>();

            //Game
            dependencyInjection.RegisterType<IInterface, ConsoleInterface>();
            dependencyInjection.RegisterType<IRender, ConsoleRender>();
            dependencyInjection.RegisterType<IGameInitializationStrategy, GameInitializationStrategy>();
            dependencyInjection.RegisterType<IHelpers, Helpers>();

            //AutoPlay
            //dependencyInjection.RegisterType<IBotGame, BotGame>();
            dependencyInjection.RegisterType<IBotGamePlay, BotGamePlay>();

            //Commands
            dependencyInjection.RegisterType<IProcessCommandStrategy, ShowCommand>("Show");
            dependencyInjection.RegisterType<IProcessCommandStrategy, ShootCommand>("Shoot");
            dependencyInjection.RegisterType<IProcessCommandStrategy, BubbleSortCommand>("BubbleSort");
            dependencyInjection.RegisterType<IProcessCommandStrategy, SelectionSortCommand>("SelectionSort");
            dependencyInjection.RegisterType<IProcessCommandStrategy, ExitCommand>("Exit");
            dependencyInjection.RegisterType<IProcessCommandStrategy, NewGameCommand>("New");
            dependencyInjection.RegisterType<IProcessCommandStrategy, InvalidCommand>("Invalid");
            dependencyInjection.RegisterType<IProcessCommandStrategy, AutoPlayCommand>("AutoPlay");


            return dependencyInjection;
        }
    }
}
