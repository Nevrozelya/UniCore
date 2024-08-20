using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniCore.Components
{
    public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool IsHovered { get; private set; }

        public IObservable<bool> HoverEvent => _hover;

        private Subject<bool> _hover = new();

        private void OnDestroy()
        {
            _hover.Dispose();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHovered = true;
            _hover.OnNext(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHovered = false;
            _hover.OnNext(false);
        }
    }
}
