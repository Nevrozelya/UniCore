using UnityEngine;

namespace UniCore.Extensions.Engine
{
    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static Color WithIntensity(this Color color, float intensity)
        {
            //return color * Mathf.LinearToGammaSpace(intensity);

            float factor = Mathf.Pow(2, intensity);
            return color * factor;
        }

        public static string ToHexa(this Color color, bool withAlpha = false)
        {
            string hex = withAlpha ? ColorUtility.ToHtmlStringRGBA(color) : ColorUtility.ToHtmlStringRGB(color);
            return $"#{hex}";
        }
    }
}
