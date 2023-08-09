using System;
using System.Collections.Generic;
using System.Linq;

namespace UniCore.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return !source.Any(predicate);
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
    }
}
