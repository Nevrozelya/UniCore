using UnityEngine;

namespace UniCore.Extensions
{
    public static class CameraExtensions
    {
        public static Vector2 OthographicWorldScreenSize(this Camera camera)
        {
            float screenRatio = (float)Screen.width / Screen.height;
            float worldHeight = camera.orthographicSize * 2;
            float worldWidth = worldHeight * screenRatio;

            return new Vector2(worldWidth, worldHeight);
        }
    }
}
