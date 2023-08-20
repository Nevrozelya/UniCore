using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using UniCore.Extensions;
using UniCore.Utils;
using UnityEngine;

namespace UniCore.Systems.Persistance
{
    public class PersistanceSystem<T> : IDisposable
    {
        public bool IsReady => _dictionary != null;

        private string _path;
        private Dictionary<T, string> _dictionary;
        private CancellationTokenSource _readToken;
        private CancellationTokenSource _writeToken;

        public PersistanceSystem(string relativeFilePath)
        {
            // Note: relativeFilePath must be containing filename & extension
            _path = Path.Combine(Application.persistentDataPath, relativeFilePath).SanitizeBackslashes();
            Read();
        }

        public void Dispose()
        {
            _readToken.CancelAndDispose();
            _writeToken.CancelAndDispose();
        }

        public bool Set(T setting, string value)
        {
            if (_dictionary == null)
            {
                return false;
            }

            _dictionary[setting] = value;
            Write();

            return true;
        }

        public bool Set(T setting, object value)
        {
            if (value == null)
            {
                return false;
            }

            return Set(setting, value.ToString());
        }

        public string GetString(T setting)
        {
            if (_dictionary.IsNullOrEmpty())
            {
                return null;
            }

            if (!_dictionary.ContainsKey(setting))
            {
                return null;
            }

            return _dictionary[setting];
        }

        public bool? GetBool(T setting)
        {
            string value = GetString(setting);

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (bool.TryParse(value, out bool parsed))
            {
                return parsed;
            }

            return null;
        }

        public int? GetInt(T setting)
        {
            string value = GetString(setting);

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out int parsed))
            {
                return parsed;
            }

            return null;
        }

        public float? GetFloat(T setting)
        {
            string value = GetString(setting);

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out float parsed))
            {
                return parsed;
            }

            return null;
        }

        public TEnum? GetEnum<TEnum>(T setting) where TEnum : struct
        {
            string value = GetString(setting);

            if (Enum.TryParse(value, out TEnum parsed))
            {
                return parsed;
            }

            return null;
        }

        public DateTime? GetDate(T setting)
        {
            string value = GetString(setting);

            if (DateTime.TryParse(value, out DateTime parsed))
            {
                return parsed;
            }

            return null;
        }

        private void Read()
        {
            _readToken.CancelAndDispose();
            _readToken = new();
            ReadAsync(_readToken.Token).Forget();
        }

        private async UniTask ReadAsync(CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            _dictionary = await FileUtils.ReadAndParseAsync<Dictionary<T, string>>(_path, token);
            _dictionary ??= new();
        }

        private void Write()
        {
            _writeToken.CancelAndDispose();
            _writeToken = new();
            WriteAsync(_writeToken.Token).Forget();
        }

        private async UniTask WriteAsync(CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            if (_dictionary == null)
            {
                return;
            }

            await FileUtils.SerializeAndWriteAsync(_path, _dictionary, token);
        }
    }
}
