using UnityEngine;

namespace UniCore.Utils
{
    public static class MathUtils
    {
        public const float TAU = 2f * Mathf.PI;

        public static float Normalize(float value, float sourceScaleMin, float sourceScaleMax)
        {
            float sourceSize = sourceScaleMax - sourceScaleMin;

            if (sourceSize == 0)
            {
                return 0;
            }

            return (value - sourceScaleMin) / sourceSize;
        }

        public static float Denormalize(float normalizedValue, float targetScaleMin, float targetScaleMax)
        {
            float clamped = Mathf.Clamp01(normalizedValue);

            return GetScaleToScaleValue(clamped, 0, 1, targetScaleMin, targetScaleMax);
        }

        public static float GetScaleToScaleValue(float value, float sourceScaleMin, float sourceScaleMax, float targetScaleMin, float targetScaleMax)
        {
            float delta = value - sourceScaleMin;

            if (delta == 0)
            {
                return targetScaleMin;
            }

            float sourceSize = sourceScaleMax - sourceScaleMin;
            float targetSize = targetScaleMax - targetScaleMin;

            if (sourceSize == 0)
            {
                return targetScaleMin;
            }

            float valuePercentage = sourceSize / delta;
            float targetValue = targetScaleMin + (targetSize / valuePercentage);

            return targetValue;
        }

        public static float GetRadianAngle(Vector2 me, Vector2 target)
        {
            return Mathf.Atan2(target.y - me.y, target.x - me.x);
        }

        public static float GetRadianAngle(Vector2 direction)
        {
            return GetRadianAngle(Vector2.zero, direction);
        }

        public static Vector2 GetDirection(float radianAngle)
        {
            float x = Mathf.Cos(radianAngle);
            float y = Mathf.Sin(radianAngle);

            // Result is normalized by default
            // (ofc, because it uses the trigonometric circle)
            return new Vector2(x, y);
        }
    }
}
