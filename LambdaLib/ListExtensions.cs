using System;
using System.Collections.Generic;

namespace LambdaLib
{
    public static class ListExtensions
    {
        private static readonly Random Rng = new Random();

        /// <summary>
        /// Performs the specified action on each element of the <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action">the specified action</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        /// <summary>
        /// Randomly pick one element from the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T PickOne<T>(this IList<T> collection)
        {
            return collection[Rng.Next(collection.Count)];
        }

        /// <summary>
        /// Randomly pick one element from the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T PickOne<T>(this T[] collection)
        {
            return collection[Rng.Next(collection.Length)];
        }

        /// <summary>
        /// shuffle the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// shuffle the list using the specified rng
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="rng"></param>
        /// <returns></returns>
        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> CloneAndSort<T>(this IEnumerable<T> list)
        {
            var clist = new List<T>(list);
            clist.Sort();
            return clist;
        }

        public static string Join<T>(this IEnumerable<T> enumerable, string separator = ", ", string prefix = "{", string postfix = "}")
        {
            return $"{prefix}{string.Join(separator, enumerable)}{postfix}";
        }
    }
}