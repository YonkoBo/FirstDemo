using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;
using Battleships.Models;
using Battleships.Models.Contracts;
using System.Collections.Generic;

namespace Battleships.Logic.Commands
{
    public class NewGameCommand : IProcessCommandStrategy
    {
        private IRender renderer;
        private GameStatus gameStatus;
        private IEngine engine;
        private IPlayerFactory playerFactory;
        public NewGameCommand(IRender renderer, IPlayerFactory playerFactory)
        {
            this.renderer = renderer;
            this.playerFactory = playerFactory;
        }
        public void ProcessCommand(Grid hiddenGrid, Grid visibleGrid, Position shotPosition, int totalAttempts, List<PlayerData> playerData, IList<IShip> ship)
        {
            this.gameStatus = GameStatus.Play;
            this.renderer.Clear();
            totalAttempts = 0;
            engine.Run();
        }
    }
}
