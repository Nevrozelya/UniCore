using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using static UniCore.Utils.Easing;

namespace UniCore.Utils
{
    public static class Interpolation
    {
        public static async UniTask Async(
            Action<float> progress,
            float from,
            float to,
            float duration,
            EasingFunction ease = EasingFunction.EaseInOutCubic,
            CancellationToken token = default)
        {
            duration = duration < 0 ? 0 : duration;

            if (duration > 0)
            {
                float timeStart = Time.time;

                while (Time.time - timeStart < duration && !token.IsCancellationRequested)
                {
                    if (token != default && token.IsCancellationRequested)
                    {
                        return;
                    }

                    float currentNormalizedTime = EaseByFunction((Time.time - timeStart) / duration, ease);
                    progress(Mathf.Lerp(from, to, currentNormalizedTime));
                    await UniTask.Yield(token);
                }
            }

            if (!token.IsCancellationRequested)
            {
                progress(to);
            }
        }

        public static IEnumerator Coroutine(
            Action<float> progress,
            float from,
            float to,
            float duration,
            Action endCallback = null,
            EasingFunction ease = EasingFunction.EaseInOutCubic)
        {
            duration = duration < 0 ? 0 : duration;

            if (duration > 0)
            {
                float timeStart = Time.time;

                while (Time.time - timeStart < duration)
                {
                    float currentNormalizedTime = EaseByFunction((Time.time - timeStart) / duration, ease);
                    progress(Mathf.Lerp(from, to, currentNormalizedTime));
                    yield return true;
                }
            }
            progress(to);
            endCallback?.Invoke();
        }

        public static float EaseByFunction(float value, EasingFunction func)
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
    }
}
