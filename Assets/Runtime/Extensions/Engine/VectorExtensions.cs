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

        public static Vector2 AddX(this Vector2 v, float x)
        {
            return new Vector2(v.x + x, v.y);
        }

        public static Vector2 AddY(this Vector2 v, float y)
        {
            return new Vector2(v.x, v.y + y);
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

        public static Vector3 AddX(this Vector3 v, float x)
        {
            return new Vector3(v.x + x, v.y, v.z);
        }

        public static Vector3 AddY(this Vector3 v, float y)
        {
            return new Vector3(v.x, v.y + y, v.z);
        }

        public static Vector3 AddZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, v.z + z);
        }

        // Helpers

        public static Vector2 ToUnity(this System.Numerics.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector3 ToUnity(this System.Numerics.Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static System.Numerics.Vector2 ToPure(this Vector2 vector)
        {
            return new System.Numerics.Vector2(vector.x, vector.y);
        }

        public static System.Numerics.Vector3 ToPure(this Vector3 vector)
        {
            return new System.Numerics.Vector3(vector.x, vector.y, vector.z);
        }

        public static Vector3 ToWorldStep(this Vector2 step, Camera camera)
        {
            Vector3 viewportStep = camera.ScreenToViewportPoint(step);
            Vector2 worldSize = camera.GetOrthographicWorldScreenSize();
            Vector3 worldStep = new(worldSize.x * viewportStep.x, worldSize.y * viewportStep.y, 0);

            return worldStep;
        }
    }
}
