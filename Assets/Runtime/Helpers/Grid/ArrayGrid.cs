using System;
using System.Collections;
using System.Collections.Generic;
using UniCore.Extensions.Language;

namespace UniCore.Helpers.Grid
{
    public class ArrayGrid<T> : IReadOnlyArrayGrid<T>
    {
        public readonly int Width;
        public readonly int Height;
        private readonly T[][] _grid;

        int IReadOnlyArrayGrid<T>.Width => Width;
        int IReadOnlyArrayGrid<T>.Height => Height;

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
            For(p => _grid[p.X][p.Y] = default);
        }

        public Coordinates? GetFirstCoordinates(Predicate<T> predicate)
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

                    if (entry != null && predicate(entry))
                    {
                        return new(x, y);
                    }
                }
            }

            return null;
        }

        public Coordinates[] GetAllCoordinates(Predicate<T> predicate)
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

                    if (entry != null && predicate(entry))
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

        public void For(Action<Coordinates> callback)
        {
            if (callback == null)
            {
                return;
            }

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Coordinates position = new(x, y);
                    callback(position);
                }
            }
        }

        public void ForWithValue(Action<Coordinates, T> callback)
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
                    Coordinates position = new(x, y);
                    callback(position, model);
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
            if (!AreValid(x, y))
            {
                return default;
            }

            return _grid[x][y];
        }

        private void Set(int x, int y, T value)
        {
            if (!AreValid(x, y))
            {
                return;
            }

            T previous = _grid[x][y];
            _grid[x][y] = value;
            OnEdition(x, y, previous, value);
        }

        private bool AreValid(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }
    }
}
