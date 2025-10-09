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
        public readonly Vector2 Position;
        public readonly InputButton Button;

        public readonly bool IsLeftButton => Button == InputButton.Left;

        public DragEvent(DragPhase phase, Vector2 step, Vector2 position, InputButton button)
        {
            Phase = phase;
            Step = step;
            Position = position;
            Button = button;
        }
    }

    public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [field: SerializeField] public bool IsInteractable { get; set; } = true;

        public bool IsDragging { get; private set; }
        public IObservable<DragEvent> CompleteDragEvent => _drag;
        public IObservable<DragEvent> DragEvent => _drag.Where(e => e.IsLeftButton);

        private Subject<DragEvent> _drag = new();

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

            IsDragging = false; // Should be useless, just in case

            DragEvent pointerDownEvent = new(DragPhase.PointerDown, Vector2.zero, eventData.position, eventData.button);
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

            IsDragging = false;

            DragEvent endEvent = new(DragPhase.End, Vector2.zero, eventData.position, eventData.button);
            _drag.OnNext(endEvent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            if (!IsDragging)
            {
                DragEvent startEvent = new(DragPhase.Start, Vector2.zero, eventData.position, eventData.button);
                _drag.OnNext(startEvent);
            }

            IsDragging = true;

            DragEvent dragEvent = new(DragPhase.Dragging, eventData.delta, eventData.position, eventData.button);
            _drag.OnNext(dragEvent);
        }
    }
}
