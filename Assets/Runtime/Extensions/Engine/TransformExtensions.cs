using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace UniCore.Extensions.Engine
{
    public static class TransformExtensions
    {
        public static string GetHierarchyPath(this Transform transform)
        {
            if (transform == null)
            {
                return string.Empty;
            }

            List<string> names = new() { transform.name };
            Transform current = transform;

            while (current.parent != null)
            {
                names.Insert(0, current.parent.name);
                current = current.parent;
            }

            return string.Join('/', names);
        }

        public static void DestroyChildren(this Transform transform)
        {
            if (transform == null)
            {
                return;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                GameObject.Destroy(child);
            }
        }

        public static async UniTask RawShakeAsync(this Transform transform, Vector3 direction, float duration, float magnitude, CancellationToken token)
        {
            float elapsedTime = 0f;
            Vector3 originPosition = transform.position;

            while (elapsedTime < duration && !token.IsCancellationRequested)
            {
                elapsedTime += Time.deltaTime;

                transform.position = originPosition + magnitude * Random.Range(-1f, 1f) * direction;

                await UniTask.Yield(token);
            }

            transform.position = originPosition;
        }

        public static async UniTask PerlinShakeAsync(this Transform transform, float duration, float frequency, float magnitude, bool applyDamping, CancellationToken token)
        {
            float elapsedTime = 0f;
            Vector3 originPosition = transform.position;

            float offsetX = Random.Range(0f, 1000f);
            float offsetY = Random.Range(0f, 1000f);

            while (elapsedTime < duration && !token.IsCancellationRequested)
            {
                elapsedTime += Time.deltaTime;

                float noiseX = Mathf.PerlinNoise(elapsedTime * frequency + offsetX, 0f) * 2f - 1f;
                float noiseY = Mathf.PerlinNoise(elapsedTime * frequency + offsetY, 0f) * 2f - 1f;

                float damping = applyDamping ? 1f - (elapsedTime / duration) : 1;
                Vector3 shakeOffset = new Vector3(noiseX, noiseY, 0f) * magnitude * damping;

                transform.position = originPosition + shakeOffset;
                await UniTask.Yield(token);
            }

            transform.position = originPosition;
        }
    }
}
