using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

namespace UniCore.Components
{
    public enum DragPhase
    {
        PointerDown, Start, Dragging, End
    }

    public readonly struct DragEvent
    {
        public readonly DragPhase Phase;
        public readonly Vector2 Step;
        public readonly InputButton Button;

        public readonly bool IsLeftButton => Button == InputButton.Left;

        public DragEvent(DragPhase phase, Vector2 step, InputButton button)
        {
            Phase = phase;
            Step = step;
            Button = button;
        }
    }

    public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public bool IsInteractable { get; set; } = true;
        public bool IsDragging { get; private set; }
        public IObservable<DragEvent> DragEvent => _drag;

        private Subject<DragEvent> _drag = new();
        private int? _pointerId;

        private void OnDestroy()
        {
            _drag.Dispose();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            if (_pointerId.HasValue)
            {
                return;
            }

            _pointerId = eventData.pointerId;
            IsDragging = false; // Should be useless, just in case

            DragEvent pointerDownEvent = new(DragPhase.PointerDown, default, eventData.button);
            _drag.OnNext(pointerDownEvent);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            if (!IsDragging)
            {
                // Drag didn't start, so it cannot end
                return;
            }

            if (!_pointerId.HasValue || eventData.pointerId != _pointerId.Value)
            {
                return;
            }

            _pointerId = null;
            IsDragging = false;

            DragEvent endEvent = new(DragPhase.End, default, eventData.button);
            _drag.OnNext(endEvent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            if (!_pointerId.HasValue)
            {
                return; // Should never happen, just in case
            }

            if (!IsDragging)
            {
                DragEvent startEvent = new(DragPhase.Start, default, eventData.button);
                _drag.OnNext(startEvent);
            }

            IsDragging = true;

            DragEvent dragEvent = new(DragPhase.Dragging, eventData.delta, eventData.button);
            _drag.OnNext(dragEvent);
        }
    }
}
