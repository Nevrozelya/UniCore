using System;
using UniRx;

namespace UniCore.Helpers.Grid
{
    public class ReactiveArrayGrid<T> : ArrayGrid<T>, IReadOnlyReactiveArrayGrid<T>, IDisposable
    {
        public IObservable<ReactiveArrayGridEditEvent<T>> EditEvent => _editEvent;
        public IObservable<ReactiveArrayGridMoveEvent<T>> MoveEvent => _moveEvent;
        public IObservable<ReactiveArrayGridSwapEvent<T>> SwapEvent => _swapEvent;

        private Subject<ReactiveArrayGridEditEvent<T>> _editEvent;
        private Subject<ReactiveArrayGridMoveEvent<T>> _moveEvent;
        private Subject<ReactiveArrayGridSwapEvent<T>> _swapEvent;

        public ReactiveArrayGrid(int width, int height) : base(width, height)
        {
            _editEvent = new();
            _moveEvent = new();
            _swapEvent = new();
        }

        public void Dispose()
        {
            _editEvent.Dispose();
            _moveEvent.Dispose();
            _swapEvent.Dispose();
        }

        protected override void OnEdit(Coordinates position, T previousValue, T newValue)
        {
            ReactiveArrayGridEditEvent<T> edit = new(position, previousValue, newValue);
            _editEvent.OnNext(edit);
        }

        protected override void OnMove(Coordinates from, Coordinates to, T movedValue)
        {
            ReactiveArrayGridMoveEvent<T> move = new(from, to, movedValue);
            _moveEvent.OnNext(move);
        }

        protected override void OnSwap(Coordinates from, Coordinates to, T swappedFromPreviousValue, T swappedFromNewValue)
        {
            ReactiveArrayGridSwapEvent<T> swap = new(from, to, swappedFromPreviousValue, swappedFromNewValue);
            _swapEvent.OnNext(swap);
        }
    }
}
