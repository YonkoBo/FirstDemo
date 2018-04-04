using Battleships.Common;
using Battleships.Logic.Contracts;
using Battleships.Logic.Factory;
using Battleships.Models;
using Battleships.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Battleships.Logic
{
    public class Engine : IEngine
    {
        private IRender renderer;
        private IInterface userInterface;
        private IGameInitializationStrategy gameInitializationStrategy;
        private IList<IShip> ships;
        private Grid visibleGrid;
        private Grid hiddenGrid;
        private GameStatus gameStatus;
        private Position shotPosition;
        private int totalAttempts;
        private List<PlayerData> playerData;
        private PlayerFactory playerFactory;

        public Engine(IRender renderer, IInterface userInterface, IGameInitializationStrategy gameInitializationStrategy)
        {
            this.renderer = renderer; //Grid builder
            this.userInterface = userInterface; // user interface.
            this.gameInitializationStrategy = gameInitializationStrategy; //Types of ships
            this.ships = new List<IShip>();
            this.hiddenGrid = new Grid();
            this.visibleGrid = new Grid();
            this.totalAttempts = 0; //
            this.gameStatus = GameStatus.Play; //Sets current gamestatus to Play.
            this.playerFactory = new PlayerFactory();
            this.playerData = LoadData();
        }

        public IList<IShip> Ships
        {
            get
            {
                return new List<IShip>(this.ships);
            }
        }

        public List<PlayerData> LoadData() //Loading all existing files from given path
        {
            List<PlayerData> playerData = new List<PlayerData>();
            IEnumerable<string> files = Directory.EnumerateFiles(GlobalConstants.Path, "*.json");
            foreach (var file in files)
            {
                PlayerData dataToAdd = ParsePlayerData(file);
                playerData.Add(dataToAdd);
            }
            return playerData;
        }
        private PlayerData ParsePlayerData(string data) //Creating new object of type PlayerData.
        {
            string[] dataSplited = data.Split('_');
            Guid id = new Guid(dataSplited[1]);
            string playerName = dataSplited[2];
            double timePlayed = double.Parse(dataSplited[3]);
            int score = Int32.Parse(dataSplited[4]);

            PlayerData playerData = playerFactory.CreatePlayerData(playerName, score, timePlayed, id);
            return playerData;
        }
        public void CreateNewPlayerFile(string playerName, double timePlayed, int score) //Creats a new .json file in selected path.
        {
            Guid id = Guid.NewGuid();
            PlayerData newPlayerData = playerFactory.CreatePlayerData(playerName, score, timePlayed, id);
            string fileName = string.Format("_{0}_{1}_{2}_{3}_.json", newPlayerData.ID, newPlayerData.PlayerName, newPlayerData.TimePlayed, newPlayerData.Score);
            File.Create(fileName);
        }
        public void Run()
        {
            Stopwatch timer = new Stopwatch(); //Adding timer to track the time.
            timer.Start();
            AskPlayerName(); // Asks user for Name.
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
                    this.ProcessCommand(command);
                }
                catch (Exception e)
                {
                    this.gameStatus = GameStatus.Error;
                    this.renderer.RenderStatusMessage(this.gameStatus.ToString());
                    this.renderer.RenderErrorMessage(e.Message);
                }

                this.renderer.UpdateGrid(this.visibleGrid, this.shotPosition);

                if (this.AreAllShipsSunk())
                {
                    timer.Stop();
                    double timePlayed = timer.Elapsed.Seconds;
                    int score = GlobalConstants.MaxScore - totalAttempts;
                    CreateNewPlayerFile(playerName, timePlayed, score);
                    this.gameStatus = GameStatus.End;
                }

                if (this.gameStatus == GameStatus.End)
                {
                    this.renderer.RenderStatusMessage(this.gameStatus.ToString());
                    this.ProcessGameEnd();
                }
            }
        }
        private void AskPlayerName()
        {
            Console.WriteLine("Please Enter Player Name:");
        }

        private void ProcessCommand(UserCommands command) //Processes the commands input from the user.
        {
            switch (command)
            {
                case UserCommands.Show:
                    this.ProcessShowCommand();
                    break;
                case UserCommands.Exit:
                    this.ProcessExitCommand();
                    break;
                case UserCommands.New:
                    this.ProcessNewGame();
                    break;
                case UserCommands.Shoot:
                    this.shotPosition = this.userInterface.GetShotPositionFromInput();
                    this.ProcessShootCommand();
                    break;
                case UserCommands.BubbleSort:
                    this.ProcessBubbleSortCommand();
                    break;
                case UserCommands.SelectionSort:
                    this.ProcessSelectionSortCommand();
                    break;
                case UserCommands.Invalid:
                default:
                    throw new InvalidOperationException(GlobalConstants.InvalidCommandMsg);
            }
        }

        private void ProcessSelectionSortCommand()
        {
            this.gameStatus = GameStatus.Sorting;
            this.renderer.Clear();
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sorting Data by Time Played Using SelectionSort");
            IEnumerable<PlayerData> sortedData = SortingAlgorithms.SelectionSortByTimePlayed(this.playerData);
            foreach (var data in sortedData)
            {
                Console.WriteLine(string.Format("ID:{0},Time Played:{1} s.,Player Name:{2},Score:{3}", data.ID, data.TimePlayed, data.PlayerName, data.Score));
            }
        }
        private void ProcessBubbleSortCommand()
        {
            this.gameStatus = GameStatus.Sorting;
            this.renderer.Clear();
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sorting Data by Score Ascending Using BubbbleSort");
            IEnumerable<PlayerData> sortedData = SortingAlgorithms.BubbleSortByScore(this.playerData);
            foreach (var data in sortedData)
            {
                Console.WriteLine(string.Format("ID:{0},Time Played:{1} s.,Player Name:{2},Score:{3}", data.ID, data.TimePlayed, data.PlayerName, data.Score));
            }
        }
        private void ProcessShowCommand()
        {
            this.gameStatus = GameStatus.Show;
            this.renderer.RenderStatusMessage(this.gameStatus.ToString());
            this.renderer.RenderGrid(this.hiddenGrid);
            this.ProcessCommand(this.userInterface.GetCommandFromInput());
            this.renderer.RenderGrid(this.visibleGrid);
        }

        private void ProcessShootCommand()
        {
            if (this.hiddenGrid.GetCell(this.shotPosition) != GlobalConstants.BlankSymbol)
            {
                this.ProcessShipHit();
                this.visibleGrid.SetCell(this.shotPosition, GlobalConstants.HitSymbol);
            }
            else
            {
                this.visibleGrid.SetCell(this.shotPosition, GlobalConstants.MissSymbol);
                this.gameStatus = GameStatus.Miss;
            }

            this.totalAttempts++;
        }

        private void ProcessShipHit()
        {
            for (int i = 0; i < this.Ships.Count; i++)
            {
                var currentShip = this.Ships[i];
                if (this.IsShipHit(currentShip, this.shotPosition) && this.visibleGrid.GetCell(this.shotPosition) != GlobalConstants.HitSymbol)
                {
                    this.HitShip(currentShip);
                    this.gameStatus = GameStatus.Hit;

                    if (this.IsShipSinking(currentShip))
                    {
                        this.SinkShip(currentShip);
                        this.gameStatus = GameStatus.Sunk;
                    }
                }
            }
        }

        private bool IsShipHit(IShip ship, Position position)
        {
            var row = ship.ShipPosition.Row;
            var col = ship.ShipPosition.Col;

            for (int j = 0; j < ship.Size; j++)
            {
                if (position.Row == row && position.Col == col)
                {
                    return true;
                }

                if (ship.Direction == ShipDirection.Horizontal)
                {
                    col++;
                }
                else
                {
                    row++;
                }
            }

            return false;
        }

        private void HitShip(IShip ship)
        {
            ship.HitsCount++;

            if (ship.HitsCount > ship.Size)
            {
                throw new InvalidOperationException(GlobalConstants.InvalidShipHit);
            }
        }

        private bool IsShipSinking(IShip ship)
        {
            if (ship.HitsCount == ship.Size)
            {
                return true;
            }

            return false;
        }

        private void SinkShip(IShip ship)
        {
            ship.IsSunk = true;
        }

        private void ProcessExitCommand()
        {
            this.renderer.RenderMessage(string.Format(GlobalConstants.ExitMsg, GlobalConstants.AgreeCommand));
            UserCommands command = this.userInterface.GetCommandFromInput();

            if (command == UserCommands.Agree)
            {
                this.userInterface.ExitGame();
            }
            else
            {
                this.renderer.RenderMessage(GlobalConstants.EnterCoordinatesMsg);
            }
        }

        private bool AreAllShipsSunk()
        {
            for (int i = 0; i < this.Ships.Count; i++)
            {
                if (!this.Ships[i].IsSunk)
                {
                    return false;
                }
            }

            return true;
        }

        private void ProcessGameEnd()
        {
            this.renderer.RenderMessage(string.Format(GlobalConstants.GameEndMsg, this.totalAttempts, GlobalConstants.AgreeCommand), false);
            UserCommands command = this.userInterface.GetCommandFromInput();
            if (command == UserCommands.Agree)
            {
                this.ProcessNewGame();
            }
            else if (command == UserCommands.BubbleSort)
            {
                this.ProcessBubbleSortCommand();
            }
            else if (command == UserCommands.SelectionSort)
            {
                this.ProcessSelectionSortCommand();
            }
            else
            {
                this.userInterface.ExitGame();
            }
        }

        private void ProcessNewGame()
        {
            this.gameStatus = GameStatus.Play;
            this.renderer.Clear();
            this.totalAttempts = 0;
            this.Run();
        }
    }
}
