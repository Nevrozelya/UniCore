using System;
using System.Collections;
using System.Collections.Generic;
using UniCore.Extensions;

namespace UniCore.Helpers.Grid
{
    public class ArrayGrid<T> : IReadOnlyArrayGrid<T>
    {
        public readonly int Width;
        public readonly int Height;

        private readonly T[][] _grid; // x is first index, y is second index

        public ArrayGrid(int width, int height)
        {
            Width = width;
            Height = height;

            _grid = new T[Width][];
            for (int i = 0; i < Width; i++)
            {
                _grid[i] = new T[Height];
            }
        }

        public T this[int x, int y]
        {
            get => Get(x, y);
            set => Set(x, y, value);
        }

        public T this[Coordinates coordinates]
        {
            get => Get(coordinates.X, coordinates.Y);
            set => Set(coordinates.X, coordinates.Y, value);
        }

        public void Clear()
        {
            For((x, y) =>
            {
                _grid[x][y] = default;
            });
        }

        public Coordinates? GetFirstCoordinates(Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                return null;
            }

            for (int x = 0; x < _grid.Length; x++)
            {
                T[] list = _grid[x];

                for (int y = 0; y < list.Length; y++)
                {
                    T entry = list[y];

                    if (entry != null && predicate.Invoke(entry))
                    {
                        return new(x, y);
                    }
                }
            }

            return null;
        }

        public Coordinates[] GetAllCoordinates(Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                return null;
            }

            List<Coordinates> result = null;

            for (int x = 0; x < _grid.Length; x++)
            {
                T[] list = _grid[x];

                for (int y = 0; y < list.Length; y++)
                {
                    T entry = list[y];

                    if (entry != null && predicate.Invoke(entry))
                    {
                        result ??= new();

                        Coordinates found = new(x, y);
                        result.Add(found);
                    }
                }
            }

            if (result.IsNullOrEmpty())
            {
                return null;
            }

            return result.ToArray();
        }

        public void For(Action<int, int> callback)
        {
            if (callback == null)
            {
                return;
            }

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    callback.Invoke(x, y);
                }
            }
        }

        public void ForWithValue(Action<int, int, T> callback)
        {
            if (callback == null)
            {
                return;
            }

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    T model = _grid[x][y];
                    callback.Invoke(x, y, model);
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    yield return _grid[x][y];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void OnEdition(int x, int y, T previousValue, T newValue) { }

        private T Get(int x, int y)
        {
            if (x >= 0 && x < Width)
            {
                if (y >= 0 && y < Height)
                {
                    return _grid[x][y];
                }
            }

            return default;
        }

        private void Set(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                T previous = _grid[x][y];
                _grid[x][y] = value;
                OnEdition(x, y, previous, value);
            }
        }
    }
}
