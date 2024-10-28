using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Threading;
using UniCore.Extensions.Language;
using UniCore.Utils;
using UnityEngine;

namespace UniCore.Systems.Persistance
{
    public class FilePersistanceSystem<T> : IDisposable
    {
        protected T _data;

        private string _path;
        private CancellationTokenSource _readToken;
        private CancellationTokenSource _writeToken;

        public bool IsInitialized { get; private set; }
        public bool IsValid => _data != null;

        public FilePersistanceSystem(string relativeFilePath)
        {
            // relativeFilePath must be containing filename & extension
            _path = Path.Combine(Application.persistentDataPath, relativeFilePath).SanitizeBackslashes();
            Read();
        }

        public void Dispose()
        {
            _readToken.CancelAndDispose();
            _writeToken.CancelAndDispose();
        }

        public T Get()
        {
            return _data;
        }

        public void Set(T data, bool write = true)
        {
            _data = data;

            if (write)
            {
                Write();
            }
        }

        private void Read()
        {
            _readToken = _readToken.Renew();
            ReadAsync(_readToken.Token).Forget();
        }

        private async UniTask ReadAsync(CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            _data = await FileUtils.ReadAndParseAsync<T>(_path, token);

            IsInitialized = true;
        }

        protected void Write()
        {
            _writeToken = _writeToken.Renew();
            WriteAsync(_writeToken.Token).Forget();
        }

        private async UniTask WriteAsync(CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            await FileUtils.SerializeAndWriteAsync(_path, _data, token);
        }
    }
}
