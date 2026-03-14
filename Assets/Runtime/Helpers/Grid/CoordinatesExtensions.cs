using System.Collections.Generic;

namespace UniCore.Helpers.Grid
{
    public static class CoordinatesExtensions
    {
        public static List<Coordinates> GetNeighbors(this Coordinates coordinates, int width, int height, int range = 1)
        {
            if (range < 1)
            {
                range = 1;
            }

            int x = coordinates.X;
            int y = coordinates.Y;

            List<Coordinates> neighbors = new();

            for (int i = 1; i <= range; i++)
            {
                int bottomY = y - i;
                int topY = y + i;
                int leftX = x - i;
                int rightX = x + i;

                if (bottomY >= 0)
                {
                    Coordinates bottom = new(x, bottomY);
                    neighbors.Add(bottom);
                }

                if (topY < height)
                {
                    Coordinates top = new(x, topY);
                    neighbors.Add(top);
                }

                if (leftX >= 0)
                {
                    Coordinates left = new(leftX, y);
                    neighbors.Add(left);
                }

                if (rightX < width)
                {
                    Coordinates right = new(rightX, y);
                    neighbors.Add(right);
                }
            }

            return neighbors;
        }

        public static List<Coordinates> GetDiagonals(this Coordinates coordinates, int width, int height, int range = 1)
        {
            if (range < 1)
            {
                range = 1;
            }

            int x = coordinates.X;
            int y = coordinates.Y;

            List<Coordinates> neighbors = new();

            for (int i = 1; i <= range; i++)
            {
                int bottomY = y - i;
                int topY = y + i;
                int leftX = x - i;
                int rightX = x + i;

                if (leftX >= 0 && bottomY >= 0)
                {
                    Coordinates bottomLeft = new(leftX, bottomY);
                    neighbors.Add(bottomLeft);
                }

                if (leftX >= 0 && topY < height)
                {
                    Coordinates topLeft = new(leftX, topY);
                    neighbors.Add(topLeft);
                }

                if (rightX < width && bottomY >= 0)
                {
                    Coordinates bottomRight = new(rightX, bottomY);
                    neighbors.Add(bottomRight);
                }

                if (rightX < width && topY < height)
                {
                    Coordinates topRight = new(rightX, topY);
                    neighbors.Add(topRight);
                }
            }

            return neighbors;
        }
    }
}
