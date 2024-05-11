using System.Collections;
using System.Collections.Generic;
using System;

namespace MyExtensions.ListExtension
{
    public static class ListExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // Filter elements of a list based on a predicate
        public static List<T> Filter<T>(this IList<T> list, Func<T, bool> predicate)
        {
            List<T> filteredList = new List<T>();
            foreach (T item in list)
            {
                if (predicate(item))
                {
                    filteredList.Add(item);
                }
            }
            return filteredList;
        }
    }
}
