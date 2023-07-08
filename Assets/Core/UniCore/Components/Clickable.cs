using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniCore.Components
{
    public struct ClickEvent
    {
        public bool IsLeftButton { get; private set; }
        public bool IsPressed { get; private set; }

        public ClickEvent(bool isLeft, bool isPressed)
        {
            IsLeftButton = isLeft;
            IsPressed = isPressed;
        }
    }

    public class Clickable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // Will happen for both left & right clicks, at press & release states
        public IObservable<ClickEvent> ExhaustiveClickEvent => _click.AsObservable();

        public IObservable<ClickEvent> ClickEvent => ExhaustiveClickEvent.Where(e => !e.IsPressed && e.IsLeftButton);

        private Subject<ClickEvent> _click = new();
        private bool _isCancelledByDrag;
        private IDisposable _cancellationDisposable;

        private void Awake()
        {
            Draggable drag = GetComponent<Draggable>();

            if (drag != null)
            {
                _cancellationDisposable = drag.DragEvent
                    .Where(e => e.Phase == DragPhase.Dragging)
                    .Subscribe(e =>
                    {
                        _isCancelledByDrag = true;
                    });
            }
        }

        private void OnDestroy()
        {
            _click.Dispose();
            _cancellationDisposable?.Dispose();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isCancelledByDrag = false;

            bool isLeft = eventData.button == PointerEventData.InputButton.Left;
            ClickEvent evt = new(isLeft, isPressed: true);
            _click.OnNext(evt);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isCancelledByDrag)
            {
                bool isLeft = eventData.button == PointerEventData.InputButton.Left;
                ClickEvent evt = new(isLeft, isPressed: false);
                _click.OnNext(evt);
            }
        }
    }
}
