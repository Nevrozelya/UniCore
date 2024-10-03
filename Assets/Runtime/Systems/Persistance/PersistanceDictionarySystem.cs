using System;
using System.Collections.Generic;
using System.Globalization;
using UniCore.Assets.Runtime.Systems.Persistance;
using UniCore.Extensions;

namespace UniCore.Systems.Persistance
{
    public class PersistanceDictionarySystem<T> : PersistanceFileSystem<Dictionary<T, string>>
    {
        public PersistanceDictionarySystem(string relativeFilePath) : base(relativeFilePath) { }

        // The write parameter permits to make multiple sequential sets
        // without triggering multiple file writing, but only for last call.
        public void Set(T setting, string value, bool write = true)
        {
            _data ??= new();
            _data[setting] = value;

            if (write)
            {
                Write();
            }
        }

        public void Set(T setting, object value, bool write = true)
        {
            string text = value == null ? string.Empty : value.ToString();
            Set(setting, text, write);
        }

        public string GetString(T setting)
        {
            if (_data.IsNullOrEmpty())
            {
                return null;
            }

            if (!_data.ContainsKey(setting))
            {
                return null;
            }

            return _data[setting];
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

            value = value.Replace(',', '.');

            if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out float parsed))
            {
                return parsed;
            }

            return null;
        }

        public TEnum? GetEnum<TEnum>(T setting) where TEnum : struct
        {
            string value = GetString(setting);

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (Enum.TryParse(value, out TEnum parsed))
            {
                return parsed;
            }

            return null;
        }

        public DateTime? GetDate(T setting)
        {
            string value = GetString(setting);

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (DateTime.TryParse(value, out DateTime parsed))
            {
                return parsed;
            }

            return null;
        }
    }
}
