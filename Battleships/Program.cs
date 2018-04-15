using Battleships.Logic.Contracts;
using Battleships.Logic;
using Battleships.Logic.Factory;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            #region DependencyInjection
            IInterface userInterface = new ConsoleInterface();
            IRender renderer = new ConsoleRender();
            IGameInitializationStrategy gameInitializationStrategy = new GameInitializationStrategy();
            IGridViewFactory gridFactory = new GridFactory();
            IHelpers helper = new Helpers();
            IPlayerFactory playerFactory = new PlayerFactory();
            IDataCreator dataCreator = new DataCreator();
            IDataLoader dataLoader = new DataLoader();
            #endregion
            Engine gameEngine = new Engine(renderer, userInterface, gameInitializationStrategy, gridFactory, helper, playerFactory, dataCreator, dataLoader);

            gameEngine.Run();
        }
    }
}
