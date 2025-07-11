using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGeekComposer.Model.Extensions
{
    public static class CulomoExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IList<T> GetShuffled<T>(this IList<T> list, int startingIndex = 0)
        {
            var tempList = list;
            var preservedOrderList = new List<T>();

            if (startingIndex != 0)
            {
                preservedOrderList.AddRange(tempList.Take(startingIndex).ToList());
                preservedOrderList.ForEach(x =>
                {
                    tempList.Remove(x);
                });
            }

            int n = tempList.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = tempList[k];
                tempList[k] = tempList[n];
                tempList[n] = value;
            }
            preservedOrderList.AddRange(tempList);
            return preservedOrderList;
        }

        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static String ShuffleSegments(this String s, Int32 partLength)
        {
            var list = s.SplitInParts(partLength).ToList();
            list.Shuffle();
            return string.Join("", list);
        }

    }
}
