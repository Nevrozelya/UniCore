using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using UniCore.Extensions.Language;
using UniCore.Refinements.Reactive;
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
        [field: SerializeField] public bool LongAsRightClick { get; set; }
        [field: SerializeField] public float LongClickDelay { get; set; } = .25f;

        public bool IsInteractable { get; set; } = true;

        public IObservable<ClickEvent> CompleteClickEvent => _click.AsObservable(); // Will happen for both left & right clicks, at press & release states
        public IObservable<ClickEvent> LeftClickEvent => CompleteClickEvent.Where(e => e.Phase == ClickPhase.Release && e.Button == InputButton.Left);
        public IObservable<ClickEvent> RightClickEvent => CompleteClickEvent.Where(e => e.Phase == ClickPhase.Release && e.Button == InputButton.Right);
        public IObservable<ClickEvent> MiddleClickEvent => CompleteClickEvent.Where(e => e.Phase == ClickPhase.Release && e.Button == InputButton.Middle);
        public IObservable<ClickEvent> LeftOrRightClickEvent => CompleteClickEvent.Where(e => e.Phase == ClickPhase.Release && e.Button != InputButton.Middle);
        public IObservable<ClickEvent> ClickEvent => LeftClickEvent; // Only for naming convenience

        private Subject<ClickEvent> _click = new();
        private bool _isCancelledByDrag;
        private IDisposable _cancellationDisposable;

        private bool _longClickTriggered;
        private UniTask _longClickTask;
        private CancellationTokenSource _longClickToken;

        private void Awake()
        {
            bool dragFound = TryGetComponent(out Draggable drag);

            if (!dragFound)
            {
                return;
            }

            _cancellationDisposable = drag.ExhaustiveDragEvent
                .Where(e => e.Phase == DragPhase.Dragging)
                .SubscribeRx(OnDrag);
        }

        private void OnDestroy()
        {
            _click.Dispose();
            _cancellationDisposable?.Dispose();
            _longClickToken.CancelAndDispose();
        }

        private void OnDrag(DragEvent evt)
        {
            _isCancelledByDrag = true;
            _longClickToken.CancelAndDispose();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            _isCancelledByDrag = false;
            _longClickTriggered = false;

            ClickEvent pressEvent = new(ClickPhase.Press, eventData.button);
            _click.OnNext(pressEvent);

            if (eventData.button == InputButton.Left)
            {
                if (LongAsRightClick && LongClickDelay > 0)
                {
                    _longClickToken = _longClickToken.Renew();
                    _longClickTask = DelayAsync(_longClickToken.Token);
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!IsInteractable)
            {
                return;
            }

            if (eventData.button == InputButton.Left && _longClickTask.Status == UniTaskStatus.Pending)
            {
                _longClickToken.CancelAndDispose();
            }

            if (_isCancelledByDrag)
            {
                return;
            }

            if (_longClickTriggered)
            {
                return;
            }

            ClickEvent releaseEvent = new(ClickPhase.Release, eventData.button);
            _click.OnNext(releaseEvent);
        }

        private async UniTask DelayAsync(CancellationToken token)
        {
            await UniTask.WaitForSeconds(LongClickDelay, cancellationToken: token);

            _longClickTriggered = true;

            ClickEvent longClickEvent = new(ClickPhase.Release, InputButton.Right);
            _click.OnNext(longClickEvent);
        }
    }
}
