using System.Collections.Generic;
using System.Linq;

namespace Battleships.Models
{
    public class SortingAlgorithms
    {
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
        public static IEnumerable<PlayerData> BubbleSortByScore(IEnumerable<PlayerData> list)
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
        public static int IntArrayMin(PlayerData[] data, int start)
        {
            int minPos = start;
            for (int pos = start + 1; pos < data.Length; pos++)
                if (data[pos].TimePlayed < data[minPos].TimePlayed)
                    minPos = pos;
            return minPos;
        }

        public static IEnumerable<PlayerData> SelectionSortByTimePlayed(IEnumerable<PlayerData> list)
        {
            PlayerData[] data = list.ToArray();

            for (int i = 0; i < data.Length - 1; i++)
            {
                int k = IntArrayMin(data, i);
                if (i != k)
                    Swap(data, i, k);
            }
            return data;
        }
    }
}
