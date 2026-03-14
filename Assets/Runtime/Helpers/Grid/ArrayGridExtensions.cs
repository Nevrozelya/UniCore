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

        public static bool IsLineComplete<T>(this IReadOnlyArrayGrid<T> grid, int y)
        {
            if (grid.IsNullOrEmpty())
            {
                return false;
            }

            if (y < 0 || y >= grid.Height)
            {
                return false;
            }

            for (int x = 0; x < grid.Width; x++)
            {
                Coordinates c = new(x, y);

                if (grid[c] == null)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsColumnComplete<T>(this IReadOnlyArrayGrid<T> grid, int x)
        {
            if (grid.IsNullOrEmpty())
            {
                return false;
            }

            if (x < 0 || x >= grid.Width)
            {
                return false;
            }

            for (int y = 0; y < grid.Height; y++)
            {
                Coordinates c = new(x, y);

                if (grid[c] == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
