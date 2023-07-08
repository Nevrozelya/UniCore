using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniCore.Components
{
    public enum DragPhase
    {
        Start, Dragging, End
    }

    public struct DragEvent
    {
        public DragPhase Phase { get; private set; }
        public Vector2 Step { get; private set; }
        public bool IsLeftButton { get; private set; }

        public DragEvent(DragPhase phase, Vector2 step, bool isLeftButton)
        {
            Phase = phase;
            Step = step;
            IsLeftButton = isLeftButton;
        }
    }

    public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public IObservable<DragEvent> DragEvent => _drag.AsObservable();

        private Subject<DragEvent> _drag = new();

        private void OnDestroy()
        {
            _drag.Dispose();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            bool isLeftButton = eventData.button == PointerEventData.InputButton.Left;
            DragEvent evt = new(DragPhase.Start, default, isLeftButton);
            _drag.OnNext(evt);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            bool isLeftButton = eventData.button == PointerEventData.InputButton.Left;
            DragEvent evt = new(DragPhase.End, default, isLeftButton);
            _drag.OnNext(evt);
        }

        public void OnDrag(PointerEventData eventData)
        {
            bool isLeftButton = eventData.button == PointerEventData.InputButton.Left;
            DragEvent evt = new(DragPhase.Dragging, eventData.delta, isLeftButton);
            _drag.OnNext(evt);
        }
    }
}
