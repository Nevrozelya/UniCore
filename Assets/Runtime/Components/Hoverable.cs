using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniCore.Components
{
    public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool IsInteractable { get; set; } = true;

        public IReadOnlyReactiveProperty<bool> IsHovered => _isHovered;

        private ReactiveProperty<bool> _isHovered = new();

        private void OnDestroy()
        {
            _isHovered.Dispose();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            _isHovered.Value = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            _isHovered.Value = false;
        }
    }
}
