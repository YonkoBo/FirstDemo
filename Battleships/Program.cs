using Battleships.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Battleships
{
    class Program
    {
        const string path = @"E:\SoftServe\BattleshipFiles\";
        static void Main(string[] args)
        {
            Console.Title = "Battleships";
            Console.WriteLine("Please enter your name:");
            string playerName = Console.ReadLine();
            Game game = new Game();
            game.Run();
            double timePlayed = game.TimePlayed;
            int playerScore = game.Score;

            CreateNewFile(path, playerName, timePlayed, playerScore);

            IEnumerable<string> allFiles = GetAllFiles(path);
            IEnumerable<BattleshipJsonObject> mappedData = MapFilesToBattleshipObject(allFiles);
            var sortedData = BubbleSortByScore(mappedData);
            foreach (var item in sortedData)
            {
                Console.WriteLine(string.Format("{0}, {1}, {2}, {3}", item.Id, item.PlayedTime, item.UserName, item.Score));
                Console.WriteLine();
            }
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
        /// <summary>
        /// Swapping values from given array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private static void Swap<T>(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        /// <summary>
        /// Uses Buble Sort algorithm to sort the data.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<BattleshipJsonObject> BubbleSortByScore(IEnumerable<BattleshipJsonObject> list)
        {
            var arr = list.ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = arr.Length - 1; j > i; j--)
                {
                    if (arr[j].Score > arr[j - 1].Score)
                    {
                        Swap(arr, j, j - 1);
                    }
                }
            }
            return arr;
        }
        #endregion
    }
}
