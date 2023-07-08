using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniCore.Components
{
    public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public IObservable<bool> HoverEvent => _hover.AsObservable();

        private Subject<bool> _hover = new();

        private void OnDestroy()
        {
            _hover.Dispose();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hover.OnNext(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hover.OnNext(false);
        }
    }
}
