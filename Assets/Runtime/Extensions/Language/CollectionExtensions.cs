﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace UniCore.Extensions.Language
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Count() == 0;
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }


        public static int SafeCount<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return 0;
            }

            return enumerable.Count();
        }

        public static int SafeCount<T>(this ICollection<T> collection)
        {
            if (collection == null)
            {
                return 0;
            }

            return collection.Count;
        }

        public static int SafeLength<T>(this T[] array)
        {
            if (array == null)
            {
                return 0;
            }

            return array.Length;
        }


        public static bool IsAccessibleAt<T>(this IEnumerable<T> enumerable, int index)
        {
            if (enumerable.IsNullOrEmpty())
            {
                return false;
            }

            if (index < 0 || index >= enumerable.Count())
            {
                return false;
            }

            return true;
        }

        public static bool IsAccessibleAt<T>(this ICollection<T> collection, int index)
        {
            if (collection.IsNullOrEmpty())
            {
                return false;
            }

            if (index < 0 || index >= collection.Count)
            {
                return false;
            }

            return true;
        }

        public static bool IsAccessibleAt<T>(this T[] array, int index)
        {
            if (array.IsNullOrEmpty())
            {
                return false;
            }

            if (index < 0 || index >= array.Length)
            {
                return false;
            }

            return true;
        }


        public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return !enumerable.Any(predicate);
        }

        public static bool None<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            return !collection.Any(predicate);
        }

        public static bool None<T>(this T[] array, Func<T, bool> predicate)
        {
            return !array.Any(predicate);
        }


        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            if (entries != null)
            {
                Dictionary<TKey, TValue> dictionary = new();
                foreach (KeyValuePair<TKey, TValue> entry in entries)
                {
                    dictionary.Add(entry.Key, entry.Value);
                }
                return dictionary;
            }
            return null;
        }

        // Random.Shuffle<T>(T[]) exists but only applies to .net 8+
        public static T[] Shuffle<T>(this T[] items)
        {
            Random rand = new();
            T[] result = (T[])items.Clone();

            // For each spot in the array, pick
            // a random item to swap into that spot.

            for (int i = 0; i < result.Length - 1; i++)
            {
                int j = rand.Next(i, result.Length);
                T temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return result;
        }

        // Nasty duplication but better than abusing ToArray() & ToList()
        public static List<T> Shuffle<T>(this List<T> items)
        {
            Random rand = new();
            List<T> result = new(items);

            for (int i = 0; i < result.Count - 1; i++)
            {
                int j = rand.Next(i, result.Count);
                T temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return result;
        }
    }
}
