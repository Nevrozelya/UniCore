using UnityEngine;

namespace UniCore.Extensions.Engine
{
    public static class RendererExtensions
    {
        public const string EMISSION_COLOR = "_EmissionColor";

        public static void SetEmission(this Renderer renderer, Color color)
        {
            renderer.material.SetColor(EMISSION_COLOR, color);
        }
    }
}
