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
        //const string path = @"E:\SoftServe\BattleshipFiles\";
        static void Main(string[] args)
        {
            IInterface userInterface = new ConsoleInterface();
            IRender renderer = new ConsoleRender();
            IGameInitializationStrategy gameInitializationStrategy = new GameInitializationStrategy();
            Engine gameEngine = new Engine(renderer, userInterface, gameInitializationStrategy);

            gameEngine.Run();
        }
        #region Methods
        /// <summary>
        /// Reads all files from the given directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAllFiles(string path)
        {
            return Directory.EnumerateFiles(path, "*.json");
        }
        /// <summary>
        /// Maps the files to object of type BattleshipJsonObject.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerable<BattleshipJsonObject> MapFilesToBattleshipObject(IEnumerable<string> data)
        {
            foreach (string item in data)
            {
                string[] split = item.Split('_');
                yield return new BattleshipJsonObject
                {
                    Id = new Guid(split[1]),
                    UserName = split[2],
                    PlayedTime = double.Parse(split[3]),
                    Score = Int32.Parse(split[4])
                };
            }
        }

        /// <summary>
        /// Creats a new file and saves it to the givven path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="playerName"></param>
        /// <param name="timePlayed"></param>
        /// <param name="score"></param>
        public static void CreateNewFile(string path, string playerName, double timePlayed, int score)
        {
            BattleshipJsonObject obj = new BattleshipJsonObject
            {
                Id = Guid.NewGuid(),
                UserName = playerName,
                PlayedTime = timePlayed,
                Score = score
            };
            string fileName = string.Format("{0}_{1}_{2}_{3}_{4}_.json", path, obj.Id, obj.UserName, obj.PlayedTime, obj.Score);
            File.Create(fileName);
        }
        #endregion
    }
}
