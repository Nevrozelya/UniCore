using System;
using System.Collections.Generic;
using UniRx;

namespace UniCore.Utils
{
    public static class UniRxOperators
    {
        public static IObservable<IEnumerable<TValue>> BufferWithCondition<TValue>(this IObservable<TValue> @this, Func<TValue, bool> condition)
        {
            List<TValue> buffer = new List<TValue>();

            return Observable.Create<IEnumerable<TValue>>(observer =>
            {
                void Clear()
                {
                    buffer.Clear();
                }

                void OnNext()
                {
                    if (buffer.Count > 0)
                    {
                        observer.OnNext(buffer);
                        Clear();
                    }
                }

                IDisposable subscription = @this.Subscribe(value =>
                {
                    if (condition(value))
                    {
                        OnNext();
                    }
                    else
                    {
                        buffer.Add(value);
                    }
                });

                return Disposable.Create(() =>
                {
                    Clear();
                    subscription.Dispose();
                });
            });
        }
    }
}