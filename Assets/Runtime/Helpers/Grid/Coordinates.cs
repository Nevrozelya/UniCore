using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UniCore.Helpers.Grid
{
    [TypeConverter(typeof(CoordinatesConverter))]
    public readonly struct Coordinates : IEquatable<Coordinates>, IComparable<Coordinates>
    {
        public static readonly Coordinates Zero = new(0, 0);
        public static readonly Coordinates One = new(1, 1);
        public static readonly Coordinates Right = new(1, 0);
        public static readonly Coordinates Left = new(-1, 0);
        public static readonly Coordinates Up = new(0, 1);
        public static readonly Coordinates Down = new(0, -1);

        public readonly int X;
        public readonly int Y;

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsValid(int xMaxExclusive, int yMaxExclusive)
        {
            bool validX = X >= 0 && X < xMaxExclusive;
            bool validY = Y >= 0 && Y < yMaxExclusive;

            return validX && validY;
        }

        public bool IsANeighbor(Coordinates other)
        {
            bool result = Math.Abs(X - other.X) + Math.Abs(Y - other.Y) == 1;
            return result;
        }

        public bool Equals(Coordinates other)
        {
            return X == other.X && Y == other.Y;
        }

        public List<Coordinates> GetDiagonals(int width, int height)
        {
            List<Coordinates> diagonals = new();

            if (X > 0 && Y > 0)
            {
                Coordinates bottomLeft = new(X - 1, Y - 1);
                diagonals.Add(bottomLeft);
            }

            if (X > 0 && Y < height - 1)
            {
                Coordinates topLeft = new(X - 1, Y + 1);
                diagonals.Add(topLeft);
            }

            if (X < width - 1 && Y > 0)
            {
                Coordinates bottomRight = new(X + 1, Y - 1);
                diagonals.Add(bottomRight);
            }

            if (X < width - 1 && Y < height - 1)
            {
                Coordinates topRight = new(X + 1, Y + 1);
                diagonals.Add(topRight);
            }

            return diagonals;
        }

        public List<Coordinates> GetNeighbors(int width, int height)
        {
            List<Coordinates> neighbors = new();

            if (Y > 0)
            {
                Coordinates bottom = new(X, Y - 1);
                neighbors.Add(bottom);
            }

            if (Y < height - 1)
            {
                Coordinates top = new(X, Y + 1);
                neighbors.Add(top);
            }

            if (X > 0)
            {
                Coordinates left = new(X - 1, Y);
                neighbors.Add(left);
            }

            if (X < width - 1)
            {
                Coordinates right = new(X + 1, Y);
                neighbors.Add(right);
            }

            return neighbors;
        }

        public Coordinates Rotate(bool isClockwise)
        {
            if (isClockwise)
            {
                return new Coordinates(Y, -X);
            }
            else
            {
                return new Coordinates(-Y, X);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinates coordinates)
            {
                return Equals(coordinates);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Y.GetHashCode() << 2);
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }

        public int CompareTo(Coordinates other)
        {
            if (this > other)
            {
                return 1;
            }

            if (this < other)
            {
                return -1;
            }

            return 0;
        }

        public static Coordinates Min(Coordinates a, Coordinates b)
        {
            return a < b ? a : b;
        }

        public static Coordinates Max(Coordinates a, Coordinates b)
        {
            return a > b ? a : b;
        }

        public static int Distance(Coordinates a, Coordinates b, bool allowDiagonals)
        {
            int deltaX = Math.Abs(a.X - b.X);
            int deltaY = Math.Abs(a.Y - b.Y);

            if (allowDiagonals)
            {
                return Math.Max(deltaX, deltaY);
            }
            else
            {
                return deltaX + deltaY;
            }
        }

        public static bool operator ==(Coordinates a, Coordinates b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Coordinates a, Coordinates b)
        {
            return !(a == b);
            // And not a != b, because that's precisely
            // what we are defining here...
        }

        public static bool operator >(Coordinates a, Coordinates b)
        {
            if (a.X == b.X)
            {
                return a.Y > b.Y;
            }

            return a.X > b.X;
        }

        public static bool operator <(Coordinates a, Coordinates b)
        {
            if (a.X == b.X)
            {
                return a.Y < b.Y;
            }

            return a.X < b.X;
        }

        public static bool operator <=(Coordinates a, Coordinates b)
        {
            if (a.X == b.X)
            {
                return a.Y <= b.Y;
            }

            return a.X <= b.X;
        }

        public static bool operator >=(Coordinates a, Coordinates b)
        {
            if (a.X == b.X)
            {
                return a.Y >= b.Y;
            }

            return a.X >= b.X;
        }

        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.X + b.X, a.Y + b.Y);
        }

        public static Coordinates operator -(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.X - b.X, a.Y - b.Y);
        }

        public static Coordinates operator *(Coordinates c, int i)
        {
            return new Coordinates(c.X * i, c.Y * i);
        }

        public static Coordinates operator /(Coordinates c, int i)
        {
            if (i == 0)
            {
                return c;
            }

            return new Coordinates(c.X / i, c.Y / i);
        }

        public static Coordinates operator %(Coordinates c, int i)
        {
            if (i == 0)
            {
                return c;
            }

            return new Coordinates(c.X % i, c.Y % i);
        }
    }
}
