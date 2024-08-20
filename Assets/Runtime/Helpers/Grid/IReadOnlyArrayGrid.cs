using System;
using System.Collections.Generic;

namespace UniCore.Helpers.Grid
{
    public interface IReadOnlyArrayGrid<T> : IEnumerable<T>
    {
        public abstract T this[int x, int y] { get; }
        public abstract T this[Coordinates c] { get; }
        public abstract Coordinates? GetFirstCoordinates(Func<T, bool> predicate);
        public abstract Coordinates[] GetAllCoordinates(Func<T, bool> predicate);
        public abstract void For(Action<int, int> callback);
        public abstract void ForWithValue(Action<int, int, T> callback);
    }
}
