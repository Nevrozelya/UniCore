using UnityEngine;

namespace UniCore.Components
{
    public class Annotation : MonoBehaviour
    {
#if UNITY_EDITOR
        [TextArea(10, 50)][SerializeField] private string _annotation;
#endif
    }
}
