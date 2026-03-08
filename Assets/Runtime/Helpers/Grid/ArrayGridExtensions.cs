using System.Collections.Generic;
using System.Linq;
using UniCore.Extensions.Language;

namespace UniCore.Helpers.Grid
{
    public static class ArrayGridExtensions
    {
        public static Dictionary<Coordinates, T> ToDictionary<T>(this ArrayGrid<T> grid)
        {
            if (grid == null)
            {
                return null;
            }

            Dictionary<Coordinates, T> dictionary = null;

            grid.ForWithValue((c, t) =>
            {
                dictionary ??= new();
                dictionary[c] = t;
            });

            return dictionary;
        }

        public static ArrayGrid<T> FromDictionary<T>(this Dictionary<Coordinates, T> dictionary)
        {
            if (dictionary.IsNullOrEmpty())
            {
                return null;
            }

            Dictionary<Coordinates, T>.KeyCollection keys = dictionary.Keys;
            int width = keys.Max(k => k.X) + 1;
            int height = keys.Max(k => k.Y) + 1;

            ArrayGrid<T> grid = new(width, height);

            foreach (KeyValuePair<Coordinates, T> pair in dictionary)
            {
                grid[pair.Key] = pair.Value;
            }

            return grid;
        }
    }
}
