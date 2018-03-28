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
            Game game = new Game();
            game.Run();
            //IEnumerable<string> allFiles = GetAllFiles(path);
            //IEnumerable<BattleshipJsonObject> score = GetScores(allFiles);
            //var sortedData = BubbleSortByScore(score);
            //foreach (var item in sortedData)
            //{
            //    Console.WriteLine(string.Format("{0}, {1}, {2}, {3}", item.Id, item.PlayedTime, item.UserName, item.Score));
            //    Console.WriteLine();
            //}
        }

        public static IEnumerable<string> GetAllFiles(string path)
        {
            return Directory.EnumerateFiles(path, "*.json");
        }

        public static IEnumerable<BattleshipJsonObject> GetScores(IEnumerable<string> data)
        {
            foreach (string item in data)
            {
                string[] split = item.Split('_');
                yield return new BattleshipJsonObject
                {
                    Id = new Guid(split[1]),
                    UserName = split[2],
                    PlayedTime = Int32.Parse(split[3]),
                    Score = Int32.Parse(split[4])
                };
            }
        }

        /// <summary>
        /// Creates a new score file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userName"></param>
        /// <param name="playedTime"></param>
        /// <param name="score"></param>
        public static void CreateScoreFile(string path, string userName, int playedTime, int score)
        {
            BattleshipJsonObject obj = new BattleshipJsonObject
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                PlayedTime = playedTime,
                Score = score
            };
            string fileName = string.Format("{0}_{1}_{2}_{3}_{4}_.json", path, obj.Id, obj.UserName, obj.PlayedTime, obj.Score);
            File.Create(fileName);
        }
        private static void Swap<T>(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        public static IEnumerable<BattleshipJsonObject> BubbleSortByScore(IEnumerable<BattleshipJsonObject> list)
        {
            var arr = list.ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = arr.Length - 1; j > i; j--)
                {
                    if (arr[j].Score < arr[j - 1].Score)
                    {
                        Swap(arr, j, j - 1);
                    }
                }
            }
            return arr;
        }
    }
}
