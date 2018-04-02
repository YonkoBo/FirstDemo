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
    }
}
