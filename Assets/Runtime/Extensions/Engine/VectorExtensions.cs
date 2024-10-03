using UnityEngine;

namespace UniCore.Extensions.Engine
{
    public static class VectorExtensions
    {
        // Vector 2

        public static Vector2 WithX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }

        public static Vector2 WithY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }

        // Vector 3

        public static Vector3 WithX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        public static Vector3 WithZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        public static Vector3 WithXY(this Vector3 v, float x, float y)
        {
            return new Vector3(x, y, v.z);
        }

        public static Vector3 WithXZ(this Vector3 v, float x, float z)
        {
            return new Vector3(x, v.y, z);
        }

        public static Vector3 WithYZ(this Vector3 v, float y, float z)
        {
            return new Vector3(v.x, y, z);
        }
    }
}
