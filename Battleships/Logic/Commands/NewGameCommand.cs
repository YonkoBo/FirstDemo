using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;

namespace Battleships.Logic.Commands
{
    public class NewGameCommand : IProcessCommandStrategy
    {
        private IRender renderer;
        private GameStatus gameStatus;
        private IEngine engine;
        private IPlayerFactory playerFactory;
        public int TotalAtempts { get; set; }
        public NewGameCommand(IRender renderer, IEngine engine, IPlayerFactory playerFactory, int totalAtempts, GameStatus gameStatus)
        {
            this.renderer = renderer;
            this.engine = engine;
            this.playerFactory = playerFactory;
            this.gameStatus = gameStatus;
            TotalAtempts = totalAtempts;
        }
        public void ProcessCommand()
        {
            this.gameStatus = GameStatus.Play;
            this.renderer.Clear();
            TotalAtempts = 0;
            engine.Run();
        }
    }
}
