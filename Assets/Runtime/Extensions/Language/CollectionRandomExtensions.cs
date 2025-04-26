using System;
using System.Collections.Generic;
using System.Linq;

namespace UniCore.Extensions.Language
{
    public static class CollectionRandomExtensions
    {
        public static T PickRandom<T>(this IEnumerable<T> enumerable, Random rng = null)
        {
            if (enumerable.IsNullOrEmpty())
            {
                return default;
            }

            rng ??= new();

            int randomIndex = rng.Next(enumerable.Count());
            return enumerable.ElementAt(randomIndex);
        }

        public static T PickRandom<T>(this ICollection<T> collection, Random rng = null)
        {
            if (collection.IsNullOrEmpty())
            {
                return default;
            }

            rng ??= new();

            int randomIndex = rng.Next(collection.Count);
            return collection.ElementAt(randomIndex);
        }

        public static T PickRandom<T>(this T[] array, Random rng = null)
        {
            if (array.IsNullOrEmpty())
            {
                return default;
            }

            rng ??= new();

            int randomIndex = rng.Next(array.Length);
            return array[randomIndex];
        }

        // Random.Shuffle<T>(T[]) exists but only applies to .net 8+
        public static T[] Shuffle<T>(this T[] items, Random rng = null)
        {
            if (items.IsNullOrEmpty())
            {
                return items;
            }

            rng ??= new();

            T[] result = (T[])items.Clone();

            // For each spot in the array, pick
            // a random item to swap into that spot.

            for (int i = 0; i < result.Length - 1; i++)
            {
                int j = rng.Next(i, result.Length);

                T temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return result;
        }

        // Nasty duplication but better than abusing ToArray() & ToList()
        public static IList<T> Shuffle<T>(this IList<T> items, Random rng = null)
        {
            if (items.IsNullOrEmpty())
            {
                return items;
            }

            rng ??= new();

            List<T> result = new(items);

            for (int i = 0; i < result.Count - 1; i++)
            {
                int j = rng.Next(i, result.Count);

                T temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return result;
        }
    }
}
