using UnityEngine;

namespace UniCore.Extensions.Engine
{
    public static class CameraExtensions
    {
        public static Vector2 GetOrthographicWorldScreenSize(this Camera camera)
        {
            float screenRatio = (float)Screen.width / Screen.height;
            float worldHeight = camera.orthographicSize * 2;
            float worldWidth = worldHeight * screenRatio;

            return new Vector2(worldWidth, worldHeight);
        }

        public static void SetOrthographicSizeFromWorldWidth(this Camera camera, float width, float maxRatio = 0)
        {
            camera.orthographicSize = GetOrthographicSizeFromWorldWidth(width, maxRatio);
        }

        public static float GetOrthographicSizeFromWorldWidth(float width, float maxRatio = 0)
        {
            float screenRatio = (float)Screen.width / Screen.height;

            if (maxRatio > 0 && screenRatio > 0)
            {
                screenRatio = maxRatio;
            }

            float height = width / screenRatio;
            float orthographicSize = height / 2f;

            return orthographicSize;
        }
    }
}
