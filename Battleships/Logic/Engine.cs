using Battleships.Common;
using Battleships.Logic.Commands.Contracts;
using Battleships.Logic.Contracts;
using Battleships.Models;
using Battleships.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Battleships.Logic
{
    public class Engine : IEngine
    {
        #region PrivateFields
        private IRender renderer;
        private IInterface userInterface;
        private IGameInitializationStrategy gameInitializationStrategy;
        private IList<IShip> ships;
        private GameStatus gameStatus;
        private Position shotPosition;
        private int totalAttempts;
        private List<PlayerData> playerData;
        private IGridViewFactory gridFactory;
        private Grid visibleGrid;
        private Grid hiddenGrid;
        private IHelpers helper;
        private IPlayerFactory playerFactory;
        private IDataCreator dataCreator;
        private IDataLoader dataLoader;
        private IContext context;
        #endregion

        public Engine(IRender renderer, IInterface userInterface, IGameInitializationStrategy gameInitializationStrategy, IGridViewFactory gridFactory, IHelpers helper,
                      IPlayerFactory playerFactory, IDataCreator dataCreator, IDataLoader dataLoader, IContext context)
        {
            this.renderer = renderer; //Grid builder
            this.userInterface = userInterface; // user interface.
            this.gameInitializationStrategy = gameInitializationStrategy; //Types of ships
            this.ships = new List<IShip>();
            this.gridFactory = gridFactory;
            this.visibleGrid = gridFactory.CreateNewGrid();
            this.hiddenGrid = gridFactory.CreateNewGrid();
            this.totalAttempts = 0; //
            this.helper = helper;
            this.playerFactory = playerFactory;
            this.dataCreator = dataCreator;
            this.dataLoader = dataLoader;
            this.playerData = dataLoader.LoadData(playerFactory);
            this.context = context;
            this.gameStatus = GameStatus.Play;
        }

        public IList<IShip> Ships
        {
            get
            {
                return new List<IShip>(this.ships);
            }
        }


        public void Run()
        {
            Stopwatch timer = new Stopwatch(); //Adding timer to track the time.
            timer.Start();
            helper.AskPlayerName(); // Asks user for Name.
            string playerName = Console.ReadLine(); //Gets user name.

            this.gameInitializationStrategy.Initialize(this.hiddenGrid, this.visibleGrid, this.ships); //Initialize hidden and visible grid.
            this.renderer.RenderGrid(this.visibleGrid);
            this.renderer.RenderMessage(GlobalConstants.EnterCoordinatesMsg);

            while (true)
            {
                this.renderer.RenderStatusMessage(this.gameStatus.ToString());

                UserCommands command = this.userInterface.GetCommandFromInput();
                this.renderer.ClearError(); //Clears the console from the error message.

                try
                {
                    if (command == UserCommands.Shoot)
                    {
                        context.ExecuteCommands(command, this.hiddenGrid, this.visibleGrid, this.shotPosition = this.userInterface.GetShotPositionFromInput(),
                                           this.totalAttempts, this.playerData, this.Ships);
                    }
                    else
                    {
                        context.ExecuteCommands(command, this.hiddenGrid, this.visibleGrid, this.shotPosition,
                                          this.totalAttempts, this.playerData, this.Ships);
                    }

                }
                catch (Exception e)
                {
                    this.gameStatus = GameStatus.Error;
                    this.renderer.RenderStatusMessage(this.gameStatus.ToString());
                    this.renderer.RenderErrorMessage(e.Message);
                }
                this.renderer.UpdateGrid(this.visibleGrid, this.shotPosition);

                if (helper.AreAllShipsSunk(Ships))
                {
                    timer.Stop();
                    double timePlayed = timer.Elapsed.Seconds;
                    int score = GlobalConstants.MaxScore - totalAttempts;
                    this.dataCreator.CreateNewPlayerFile(playerName, timePlayed, score, playerFactory);
                    dataCreator.CreateNewPlayerFile(playerName, timePlayed, score, playerFactory);
                    this.gameStatus = GameStatus.End;
                }
            }
        }
    }
}
