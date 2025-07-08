using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace UniCore.Utils
{
    public static class Interpolation
    {
        public static async UniTask LinearNormalizedAsync(
            Action<float> progress,
            float duration,
            CancellationToken token)
        {
            duration = duration < 0 ? 0 : duration;

            if (duration > 0)
            {
                float timeStart = Time.time;

                while (Time.time - timeStart < duration && !token.IsCancellationRequested)
                {
                    float linear = (Time.time - timeStart) / duration;
                    progress?.Invoke(linear);
                    await UniTask.Yield(token);
                }
            }

            if (!token.IsCancellationRequested)
            {
                progress(1);
            }
        }

        public static async UniTask EasedNormalizedAsync(
            Action<float> progress,
            float duration,
            EasingFunction easing,
            CancellationToken token)
        {
            await LinearNormalizedAsync(p =>
            {
                float eased = easing.Ease(p);
                progress?.Invoke(eased);
            }, duration, token);
        }

        public static async UniTask EasedNormalizedAsync(
            Action<float> progress,
            float duration,
            AnimationCurve easing,
            CancellationToken token)
        {
            await LinearNormalizedAsync(p =>
            {
                float eased = easing?.Evaluate(p) ?? p;
                progress?.Invoke(eased);
            }, duration, token);
        }

        public static async UniTask LinearAsync(
            Action<float> progress,
            float from,
            float to,
            float duration,
            CancellationToken token)
        {
            await LinearNormalizedAsync(p =>
            {
                float scaled = Mathf.Lerp(from, to, p);
                progress?.Invoke(scaled);
            }, duration, token);
        }

        public static async UniTask EasedAsync(
            Action<float> progress,
            float from,
            float to,
            float duration,
            EasingFunction easing,
            CancellationToken token)
        {
            await LinearNormalizedAsync(p =>
            {
                float eased = easing.Ease(p);
                float scaled = Mathf.Lerp(from, to, eased);
                progress?.Invoke(scaled);
            }, duration, token);
        }

        public static async UniTask EasedAsync(
            Action<float> progress,
            float from,
            float to,
            float duration,
            AnimationCurve easing,
            CancellationToken token)
        {
            await LinearNormalizedAsync(p =>
            {
                float eased = easing?.Evaluate(p) ?? p;
                float scaled = Mathf.Lerp(from, to, eased);
                progress?.Invoke(scaled);
            }, duration, token);
        }

        public static async UniTask CubicNormalizedAsync(
            Action<float> progress,
            float duration,
            CancellationToken token)
        {
            await EasedNormalizedAsync(
                progress,
                duration,
                EasingFunction.EaseInOutCubic,
                token);
        }

        public static async UniTask CubicAsync(
            Action<float> progress,
            float from,
            float to,
            float duration,
            CancellationToken token)
        {
            await EasedAsync(
                progress,
                from,
                to,
                duration,
                EasingFunction.EaseInOutCubic,
                token);
        }

        [Obsolete("Use EasedAsync instead, kept for retro-compatibility only.")]
        public static async UniTask Async(
            Action<float> progress,
            float from,
            float to,
            float duration,
            EasingFunction ease = EasingFunction.EaseInOutCubic,
            CancellationToken token = default)
        {
            await EasedAsync(progress, from, to, duration, ease, token);
        }
    }
}
