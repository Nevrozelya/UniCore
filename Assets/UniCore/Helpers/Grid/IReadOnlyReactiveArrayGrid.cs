using System;

namespace UniCore.Helpers.Grid
{
    public interface IReadOnlyReactiveArrayGrid<T> : IReadOnlyArrayGrid<T>
    {
        public abstract IObservable<ReactiveArrayGridEdition<T>> EditEvent { get; }
    }
}
