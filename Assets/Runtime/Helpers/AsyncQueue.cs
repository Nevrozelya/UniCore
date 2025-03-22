using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UniCore.Extensions.Language;
using UniCore.Utils;

namespace UniCore.Helpers
{
    public class AsyncQueue<T> : IDisposable
    {
        private const string LOG = "AsyncQueue";

        public IReadOnlyCollection<T> Queue => _queue;
        public bool IsRunning => _task.Status == UniTaskStatus.Pending;

        private Queue<T> _queue;
        private Func<T, CancellationToken, UniTask> _treatment;

        private UniTask _task;
        private CancellationTokenSource _token;

        public AsyncQueue(Func<T, CancellationToken, UniTask> treatment)
        {
            _treatment = treatment;

            if (_treatment == null)
            {
                Logg.Error("Given treatment function is null!", LOG);
            }
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void Enqueue(T item)
        {
            if (_treatment == null)
            {
                return;
            }

            _queue ??= new();
            _queue.Enqueue(item);

            if (_task.Status != UniTaskStatus.Pending)
            {
                _token ??= new();
                _task = ClearQueueAsync(_token.Token);
            }
        }

        public void Clear()
        {
            _token.CancelAndDispose();
            _token = null;

            _queue?.Clear();
        }

        private async UniTask ClearQueueAsync(CancellationToken token)
        {
            while (_queue.TryDequeue(out T item) && !token.IsCancellationRequested)
            {
                await _treatment.Invoke(item, token);
            }
        }
    }
}
