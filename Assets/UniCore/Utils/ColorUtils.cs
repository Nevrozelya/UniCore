using UnityEngine;

namespace UniCore.Utils
{
    public static class ColorUtils
    {
        public static Color Random()
        {
            float r = UnityEngine.Random.Range(0f, 1f);
            float g = UnityEngine.Random.Range(0f, 1f);
            float b = UnityEngine.Random.Range(0f, 1f);

            return new Color(r, g, b);
        }
    }
}
