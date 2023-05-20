using UnityEngine;

namespace UniCore.Utils
{
    public static class MathUtils
    {
        public const float TAU = 2f * Mathf.PI;

        public static float Normalize(float value, float sourceScaleMin, float sourceScaleMax)
        {
            if (sourceScaleMax - sourceScaleMin == 0)
                return 0;

            return (value - sourceScaleMin) / (sourceScaleMax - sourceScaleMin);
        }

        public static float GetScaleToScaleValue(float value, float sourceScaleMin, float sourceScaleMax, float targetScaleMin, float targetScaleMax)
        {
            float sourceSize = sourceScaleMax - sourceScaleMin;
            float targetSize = targetScaleMax - targetScaleMin;

            float valuePercentage = sourceSize / (value - sourceScaleMin);
            float targetValue = targetScaleMin + (targetSize / valuePercentage);
            return targetValue;
        }

        public static float GetRadianAngle(Vector2 me, Vector2 target)
        {
            float angle = Mathf.Atan2(target.y - me.y, target.x - me.x);
            return angle;
        }

        public static float GetRadianAngle(Vector2 direction)
        {
            return GetRadianAngle(new Vector2(0, 0), direction);
        }

        public static Vector2 GetDirection(float radianAngle)
        {
            float x = Mathf.Cos(radianAngle);
            float y = Mathf.Sin(radianAngle);

            // Result is normalized by default
            // (ofc, because it uses the trigo circle)
            return new Vector2(x, y);
        }
    }
}
