using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartASSWeb.Bll.Core
{
    public static class HelperMethods
    {
        private static readonly Random rng = new Random();

        private static IEnumerable<T> Shuffle<T>(this IEnumerable<T> col)
        {
            var list = (List<T>)col;
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public static T RandomItem<T>(this IEnumerable<T> list)
        {
            return Shuffle<T>(list).First();
        }
    }
}