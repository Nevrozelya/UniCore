using UnityEngine;

namespace UniCore.Extensions
{
    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color c, float a)
        {
            return new Color(c.r, c.g, c.b, a);
        }

        public static string ToHexa(this Color color, bool withAlpha = false)
        {
            string hex = withAlpha ? ColorUtility.ToHtmlStringRGBA(color) : ColorUtility.ToHtmlStringRGB(color);
            return $"#{hex}";
        }
    }
}
