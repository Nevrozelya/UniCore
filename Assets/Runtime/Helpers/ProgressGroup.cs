using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UniCore.Extensions.Language;
using UnityEngine;

namespace UniCore.Helpers
{
    public class ProgressGroup : IDisposable
    {
        private CancellationTokenSource _token;
        private UniTask _task;

        private List<ProgressEntry> _entries;

        public bool IsRunning => _task.Status == UniTaskStatus.Pending;

        private class ProgressEntry
        {
            public Action<float> Action;

            public float ElapsedDelay;
            public float Delay;

            public float ElapsedDuration;
            public float Duration;

            public ProgressEntry(Action<float> action, float duration, float delay)
            {
                Action = action;

                Delay = delay;
                ElapsedDelay = 0;

                Duration = duration;
                ElapsedDuration = 0;
            }

            public float Progress(float deltaTime)
            {
                if (Delay > 0 && ElapsedDelay < Delay)
                {
                    ElapsedDelay += deltaTime;

                    if (ElapsedDelay >= Delay)
                    {
                        ElapsedDuration = ElapsedDelay - Delay;

                        if (Duration <= 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return ElapsedDuration / Duration;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }

                if (Duration <= 0)
                {
                    return 1;
                }

                ElapsedDuration += deltaTime;

                if (ElapsedDuration > Duration)
                {
                    ElapsedDuration = Duration;
                }

                return ElapsedDuration / Duration;
            }
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void Fire(Action<float> action, float duration, float delay = 0)
        {
            ProgressEntry entry = new(action, duration, delay);

            _entries ??= new();
            _entries.Add(entry);

            if (_task.Status == UniTaskStatus.Pending)
            {
                return;
            }

            _token = _token.Renew();
            _task = RunAsync(_token.Token);
        }

        private async UniTask RunAsync(CancellationToken token)
        {
            while (!_entries.IsNullOrEmpty())
            {
                float deltaTime = Time.deltaTime;

                for (int i = 0; i < _entries.Count; i++)
                {
                    ProgressEntry entry = _entries[i];
                    float progress = entry.Progress(deltaTime);

                    if (progress != -1)
                    {
                        entry.Action?.Invoke(progress);
                    }

                    if (progress >= 1)
                    {
                        _entries.RemoveAt(i);
                        i--;
                    }
                }

                await UniTask.Yield(token);
            }
        }
    }
}
