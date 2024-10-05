using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

namespace UniCore.Components
{
    public enum ClickPhase
    {
        Press, Release
    }

    public readonly struct ClickEvent
    {
        public readonly ClickPhase Phase;
        public readonly InputButton Button;

        public readonly bool IsLeftButton => Button == InputButton.Left;

        public ClickEvent(ClickPhase phase, InputButton button)
        {
            Phase = phase;
            Button = button;
        }
    }

    public class Clickable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsInteractable { get; set; } = true;

        // Will happen for both left & right clicks, at press & release states
        public IObservable<ClickEvent> ExhaustiveClickEvent => _click.AsObservable();
        public IObservable<ClickEvent> ClickEvent => ExhaustiveClickEvent.Where(e => e.Phase == ClickPhase.Release && e.IsLeftButton);

        private Subject<ClickEvent> _click = new();
        private bool _isCancelledByDrag;
        private IDisposable _cancellationDisposable;

        private void Awake()
        {
            bool dragFound = TryGetComponent(out Draggable drag);

            if (!dragFound)
            {
                return;
            }

            _cancellationDisposable = drag.DragEvent
                .Where(e => e.Phase == DragPhase.Dragging)
                .Subscribe(e => _isCancelledByDrag = true);
        }

        private void OnDestroy()
        {
            _click.Dispose();
            _cancellationDisposable?.Dispose();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            _isCancelledByDrag = false;

            ClickEvent pressEvent = new(ClickPhase.Press, eventData.button);
            _click.OnNext(pressEvent);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            if (_isCancelledByDrag)
            {
                return;
            }

            ClickEvent releaseEvent = new(ClickPhase.Release, eventData.button);
            _click.OnNext(releaseEvent);
        }
    }
}
