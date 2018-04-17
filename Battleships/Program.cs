using Battleships.Logic.Contracts;
using Battleships.Logic;
using Battleships.Logic.Factory;
using Unity;
using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Commands;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer dependencyInection = UnityDependencyInjection();

            IContext context = new Context(dependencyInection);

            IEngine engine = dependencyInection.Resolve<Engine>();
            engine.Run();
        }
        private static IUnityContainer UnityDependencyInjection()
        {
            IUnityContainer dependencyInjection = new UnityContainer();

            //App
            dependencyInjection.RegisterType<IEngine, Engine>();

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

            //Commands
            dependencyInjection.RegisterType<IProcessCommandStrategy, ShowCommand>("Show");
            dependencyInjection.RegisterType<IProcessCommandStrategy, ShootCommand>("Shoot");
            dependencyInjection.RegisterType<IProcessCommandStrategy, BubbleSortCommand>("Bubblesort");
            dependencyInjection.RegisterType<IProcessCommandStrategy, SelectionSortCommand>("Selectionsort");
            dependencyInjection.RegisterType<IProcessCommandStrategy, ExitCommand>("Exit");
            dependencyInjection.RegisterType<IProcessCommandStrategy, NewGameCommand>("New");
            dependencyInjection.RegisterType<IProcessCommandStrategy, InvalidCommand>("Invalid");

            IProcessCommandStrategy showCommand = dependencyInjection.Resolve<IProcessCommandStrategy>("Show");


            return dependencyInjection;
        }
    }
}
