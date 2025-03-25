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

        int IReadOnlyArrayGrid<T>.Width => Width;
        int IReadOnlyArrayGrid<T>.Height => Height;

        private readonly T[][] _grid;

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

        public T this[Coordinates coordinates]
        {
            get => Get(coordinates);
            set => Set(coordinates, value);
        }

        public bool Move(Coordinates from, Coordinates to)
        {
            if (!AreValid(from) || !AreValid(to))
            {
                return false;
            }

            T fromValue = _grid[from.X][from.Y];
            if (fromValue == null)
            {
                return false;
            }

            T toValue = _grid[to.X][to.Y];
            if (toValue != null)
            {
                return false;
            }

            _grid[from.X][from.Y] = default;
            _grid[to.X][to.Y] = fromValue;

            OnMove(from, to, fromValue);
            return true;
        }

        public bool Swap(Coordinates from, Coordinates to)
        {
            if (!AreValid(from) || !AreValid(to))
            {
                return false;
            }

            T fromValue = _grid[from.X][from.Y];
            if (fromValue == null)
            {
                return false;
            }

            T toValue = _grid[to.X][to.Y];
            if (toValue == null)
            {
                return false;
            }

            _grid[from.X][from.Y] = toValue;
            _grid[to.X][to.Y] = fromValue;

            OnSwap(from, to, fromValue, toValue);
            return true;
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

                    if (predicate(entry))
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

                    if (predicate(entry))
                    {
                        Coordinates found = new(x, y);

                        result ??= new();
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

        public void ForEach(Action<T> callback)
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
                    callback(model);
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

        public override string ToString()
        {
            return ToString(null);
        }

        public string ToString(Func<T, string> formatter)
        {
            if (formatter == null)
            {
                formatter = e => e.ToString();
            }

            string result = string.Empty;

            for (int y = Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    T model = _grid[x][y];

                    string entry = model == null ? "_" : formatter(model);

                    result += entry;

                    if (x < Width - 1)
                    {
                        result += " | ";
                    }
                }

                result += "\n";
            }

            return result;
        }

        protected virtual void OnEdit(Coordinates position, T previousValue, T newValue) { }
        protected virtual void OnMove(Coordinates from, Coordinates to, T movedValue) { }
        protected virtual void OnSwap(Coordinates from, Coordinates to, T swappedFromPreviousValue, T swappedFromNewValue) { }

        private T Get(Coordinates position)
        {
            if (!AreValid(position))
            {
                return default;
            }

            return _grid[position.X][position.Y];
        }

        private void Set(Coordinates position, T value)
        {
            if (!AreValid(position))
            {
                return;
            }

            T previous = _grid[position.X][position.Y];
            _grid[position.X][position.Y] = value;

            OnEdit(position, previous, value);
        }

        private bool AreValid(Coordinates position)
        {
            return position.X >= 0 && position.Y >= 0 && position.X < Width && position.Y < Height;
        }
    }
}
