using Cysharp.Threading.Tasks;
using System.Threading;
using UniCore.Utils;
using UnityEngine;

namespace UniCore.Extensions.Engine
{
    public static class CanvasGroupExtensions
    {
        public static void SetVisible(this CanvasGroup group, bool isVisible)
        {
            if (group == null)
            {
                return;
            }

            group.interactable = isVisible;
            group.blocksRaycasts = isVisible;
            group.alpha = isVisible ? 1 : 0;
            group.gameObject.SetActive(isVisible);
        }

        public static void Show(this CanvasGroup group)
        {
            group.SetVisible(true);
        }

        public static void Hide(this CanvasGroup group)
        {
            group.SetVisible(false);
        }

        public static async UniTask SetVisibleAsync(this CanvasGroup group, bool isVisible, float duration, CancellationToken token)
        {
            if (group == null)
            {
                return;
            }

            float start = group.alpha;
            float end = isVisible ? 1 : 0;

            if (!isVisible)
            {
                group.interactable = false;
                group.blocksRaycasts = false;
            }
            else
            {
                group.gameObject.SetActive(true);
            }

            if (start != end)
            {
                await Interpolation.CubicAsync(p =>
                {
                    group.alpha = p;
                }, start, end, duration, token);
            }

            if (isVisible)
            {
                group.interactable = true;
                group.blocksRaycasts = true;
            }
            else
            {
                group.gameObject.SetActive(false);
            }
        }

        public static async UniTask ShowAsync(this CanvasGroup group, float duration, CancellationToken token)
        {
            await group.SetVisibleAsync(true, duration, token);
        }

        public static async UniTask HideAsync(this CanvasGroup group, float duration, CancellationToken token)
        {
            await group.SetVisibleAsync(false, duration, token);
        }
    }
}
