using System;
using UniRx;

namespace UniCore.Helpers.Grid
{
    public class ReactiveArrayGrid<T> : ArrayGrid<T>, IReadOnlyReactiveArrayGrid<T>, IDisposable
    {
        public IObservable<ReactiveArrayGridEdition<T>> EditEvent => _editSubject;

        private Subject<ReactiveArrayGridEdition<T>> _editSubject;

        public ReactiveArrayGrid(int width, int height) : base(width, height)
        {
            _editSubject = new();
        }

        public void Dispose()
        {
            _editSubject.Dispose();
        }

        protected override void OnEdition(int x, int y, T previousValue, T newValue)
        {
            Coordinates position = new(x, y);
            ReactiveArrayGridEdition<T> edition = new(position, previousValue, newValue);
            _editSubject.OnNext(edition);
        }
    }
}
