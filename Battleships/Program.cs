using Battleships.Logic.Contracts;
using Battleships.Logic;
using Battleships.Logic.Factory;
using Unity;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer dependencyInection = UnityDependencyInjection();

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

            return dependencyInjection;
        }
    }
}
