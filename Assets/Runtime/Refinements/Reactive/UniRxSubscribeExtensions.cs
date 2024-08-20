using System;

namespace UniCore.Refinements.Reactive
{
    // Used to avoid System.ObservableExtensions.Subscribe & UniRx.ObservableExtensions.Subscribe collision
    public static class UniRxSubscribeExtensions
    {
        public static IDisposable SubscribeRx<T>(this IObservable<T> source)
        {
            return UniRx.ObservableExtensions.Subscribe(source);
        }

        public static IDisposable SubscribeRx<T>(this IObservable<T> source, Action<T> onNext)
        {
            return UniRx.ObservableExtensions.Subscribe(source, onNext);
        }

        public static IDisposable SubscribeRx<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError)
        {
            return UniRx.ObservableExtensions.Subscribe(source, onNext, onError);
        }

        public static IDisposable SubscribeRx<T>(this IObservable<T> source, Action<T> onNext, Action onCompleted)
        {
            return UniRx.ObservableExtensions.Subscribe(source, onNext, onCompleted);
        }

        public static IDisposable SubscribeRx<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            return UniRx.ObservableExtensions.Subscribe(source, onNext, onError, onCompleted);
        }
    }
}
