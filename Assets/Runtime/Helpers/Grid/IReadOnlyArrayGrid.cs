using System;
using System.Collections.Generic;

namespace UniCore.Helpers.Grid
{
    public interface IReadOnlyArrayGrid<T> : IEnumerable<T>
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract T this[Coordinates c] { get; }
        public abstract Coordinates? GetFirstCoordinates(Predicate<T> predicate);
        public abstract Coordinates[] GetAllCoordinates(Predicate<T> predicate);
        public abstract void For(Action<Coordinates> callback);
        public abstract void ForWithValue(Action<Coordinates, T> callback);
        public abstract void ForEach(Action<T> callback);
        public abstract string ToString();
        public abstract string ToString(Func<T, string> formatter);
    }
}
