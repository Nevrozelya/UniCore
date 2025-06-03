using System;
using System.Collections.Generic;
using System.Threading;
using UniCore.Extensions.Language;

namespace UniCore.Helpers
{
    public class CancellationGroup<T> : IDisposable
    {
        private Dictionary<T, CancellationTokenSource> _tokens;

        public void Dispose()
        {
            if (_tokens.IsNullOrEmpty())
            {
                return;
            }

            foreach (KeyValuePair<T, CancellationTokenSource> pair in _tokens)
            {
                pair.Value.CancelAndDispose();
            }
        }

        public CancellationTokenSource Renew(T key)
        {
            _tokens ??= new();

            if (_tokens.ContainsKey(key))
            {
                _tokens[key].CancelAndDispose();
            }

            CancellationTokenSource token = new();
            _tokens[key] = token;

            return token;
        }

        public bool Cancel(T key)
        {
            if (_tokens.IsNullOrEmpty())
            {
                return false;
            }

            if (!_tokens.ContainsKey(key))
            {
                return false;
            }

            _tokens[key].CancelAndDispose();

            return _tokens.Remove(key);
        }

        public bool Contains(T key)
        {
            if (_tokens.IsNullOrEmpty())
            {
                return false;
            }

            return _tokens.ContainsKey(key);
        }
    }
}
