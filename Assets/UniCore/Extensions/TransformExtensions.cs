using System.Collections.Generic;
using UnityEngine;

namespace UniCore.Extensions
{
    public static class TransformExtensions
    {
        public static string GetHierarchyPath(this Transform transform)
        {
            List<string> names = new List<string>() { transform.name };

            Transform current = transform;
            while (current.parent != null)
            {
                names.Insert(0, current.parent.name);
                current = current.parent;
            }

            return string.Join("/", names);
        }
    }
}
