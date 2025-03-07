using System;

namespace UniCore.Helpers.Grid
{
    public interface IReadOnlyReactiveArrayGrid<T> : IReadOnlyArrayGrid<T>
    {
        public abstract IObservable<ReactiveArrayGridEditEvent<T>> EditEvent { get; }
        public abstract IObservable<ReactiveArrayGridMoveEvent<T>> MoveEvent { get; }
        public abstract IObservable<ReactiveArrayGridSwapEvent<T>> SwapEvent { get; }
    }
}
