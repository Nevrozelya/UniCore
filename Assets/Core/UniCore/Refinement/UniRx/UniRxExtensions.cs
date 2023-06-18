using System;
using System.Collections.Generic;
using UniRx;

namespace UniCore.Refinement.UniRx
{
    public static class UniRxExtensions
    {
        public static IObservable<IEnumerable<TValue>> BufferWithCondition<TValue>(this IObservable<TValue> observable, Func<TValue, bool> condition)
        {
            List<TValue> buffer = new();

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

                IDisposable subscription = observable.Subscribe(value =>
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