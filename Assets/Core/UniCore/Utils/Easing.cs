using System;

namespace UniCore.Utils
{
    public enum EasingFunction
    {
        Linear,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInQuart,
        EaseOutQuart,
        EaseInOutQuart,
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint,
        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic,
        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce
    }

    public static class Easing
    {
        public static float Ease(this EasingFunction func, float value)
        {
            return value.EaseByFunction(func);
        }

        public static float EaseByFunction(this float value, EasingFunction func)
        {
            switch (func)
            {
                case EasingFunction.Linear:
                    return Linear(value);
                case EasingFunction.EaseInQuad:
                    return EaseInQuad(value);
                case EasingFunction.EaseOutQuad:
                    return EaseOutQuad(value);
                case EasingFunction.EaseInOutQuad:
                    return EaseInOutQuad(value);
                case EasingFunction.EaseInCubic:
                    return EaseInCubic(value);
                case EasingFunction.EaseOutCubic:
                    return EaseOutCubic(value);
                case EasingFunction.EaseInOutCubic:
                    return EaseInOutCubic(value);
                case EasingFunction.EaseInQuart:
                    return EaseInQuart(value);
                case EasingFunction.EaseOutQuart:
                    return EaseOutQuart(value);
                case EasingFunction.EaseInOutQuart:
                    return EaseInOutQuart(value);
                case EasingFunction.EaseInQuint:
                    return EaseInQuint(value);
                case EasingFunction.EaseOutQuint:
                    return EaseOutQuint(value);
                case EasingFunction.EaseInOutQuint:
                    return EaseInOutQuint(value);
                case EasingFunction.EaseInElastic:
                    return EaseInElastic(value);
                case EasingFunction.EaseOutElastic:
                    return EaseOutElastic(value);
                case EasingFunction.EaseInOutElastic:
                    return EaseInOutElastic(value);
                case EasingFunction.EaseInBounce:
                    return EaseInBounce(value);
                case EasingFunction.EaseOutBounce:
                    return EaseOutBounce(value);
                case EasingFunction.EaseInOutBounce:
                    return EaseInOutBounce(value);
                default:
                    return Linear(value);
            }
        }

        public static float Linear(float t)
        {
            return t;
        }

        public static float EaseInQuad(float t)
        {
            return t * t;
        }

        public static float EaseOutQuad(float t)
        {
            return t * (2 - t);
        }

        public static float EaseInOutQuad(float t)
        {
            return t < .5 ? 2 * t * t : -1 + (4 - 2 * t) * t;
        }

        public static float EaseInCubic(float t)
        {
            return t * t * t;
        }

        public static float EaseOutCubic(float t)
        {
            return (--t) * t * t + 1;
        }

        public static float EaseInOutCubic(float t)
        {
            return t < .5 ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
        }

        public static float EaseInQuart(float t)
        {
            return t * t * t * t;
        }

        public static float EaseOutQuart(float t)
        {
            return 1 - (--t) * t * t * t;
        }

        public static float EaseInOutQuart(float t)
        {
            return t < .5 ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;
        }

        public static float EaseInQuint(float t)
        {
            return t * t * t * t * t;
        }

        public static float EaseOutQuint(float t)
        {
            return 1 + (--t) * t * t * t * t;
        }

        public static float EaseInOutQuint(float t)
        {
            return t < .5 ? 16 * t * t * t * t * t : 1 + 16 * (--t) * t * t * t * t;
        }

        public static float EaseInElastic(float t)
        {
            if (t == 0)
            {
                return 0;
            }
            else if (t == 1)
            {
                return 1;
            }
            else
            {
                float helper = (2f * (float)Math.PI) / 3f;
                return -(float)(Math.Pow(2, 10 * t - 10) * Math.Sin(t * 10f - 10.75f) * helper);
            }
        }

        public static float EaseOutElastic(float t)
        {
            if (t == 0)
            {
                return 0;
            }
            else if (t == 1)
            {
                return 1;
            }
            else
            {
                float helper = (2f * (float)Math.PI) / 3f;
                return (float)(Math.Pow(2, -10 * t) * Math.Sin(t * 10f - 0.75f) * helper + 1);
            }
        }

        public static float EaseInOutElastic(float t)
        {
            if (t == 0)
            {
                return 0;
            }
            else if (t == 1)
            {
                return 1;
            }
            else if (t < .5f)
            {
                float helper = (2f * (float)Math.PI) / 4.5f;
                return -(float)(Math.Pow(2, 20 * t - 10) * Math.Sin(t * 20f - 11.125f) * helper) / 2f;
            }
            else
            {
                float helper = (2f * (float)Math.PI) / 4.5f;
                return (float)(Math.Pow(2, -20 * t + 10) * Math.Sin(t * 20f - 11.125f) * helper) / 2f + 1f;
            }
        }

        public static float EaseInBounce(float t)
        {
            return 1 - EaseOutBounce(1 - t);
        }

        public static float EaseOutBounce(float t)
        {
            float n1 = 7.5625f;
            float d1 = 2.75f;

            if (t < 1 / d1)
            {
                return n1 * t * t;
            }
            else if (t < 2 / d1)
            {
                return n1 * (t -= 1.5f / d1) * t + 0.75f;
            }
            else if (t < 2.5f / d1)
            {
                return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            }
            else
            {
                return n1 * (t -= 2.625f / d1) * t + 0.984375f;
            }
        }

        public static float EaseInOutBounce(float t)
        {
            return t < 0.5f ? (1 - EaseOutBounce(1 - 2 * t)) / 2 : (1 + EaseOutBounce(2 * t - 1)) / 2;
        }
    }
}
