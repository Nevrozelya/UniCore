using System.Collections.Generic;
using UniCore.Extensions.Language;
using UnityEngine;

namespace UniCore.Extensions.Engine
{
    public static class CollectionExtensions
    {
        public static void DestroyAndClear<T>(this T[] instances) where T : Component
        {
            if (instances.IsNullOrEmpty())
            {
                return;
            }

            for (int i = 0; i < instances.Length; i++)
            {
                T instance = instances[i];
                GameObject.Destroy(instance.gameObject);
                instances[i] = null;
            }
        }

        public static void DestroyAndClear<T>(this List<T> instances) where T : Component
        {
            if (instances.IsNullOrEmpty())
            {
                return;
            }

            foreach (T instance in instances)
            {
                GameObject.Destroy(instance.gameObject);
            }

            instances.Clear();
        }

        public static void DestroyAndClear<TKey, TValue>(this Dictionary<TKey, TValue> instances) where TValue : Component
        {
            if (instances.IsNullOrEmpty())
            {
                return;
            }

            foreach (KeyValuePair<TKey, TValue> pair in instances)
            {
                GameObject.Destroy(pair.Value.gameObject);
            }

            instances.Clear();
        }
    }
}
