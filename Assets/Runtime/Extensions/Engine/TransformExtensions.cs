using System.Collections.Generic;
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
    }
}
