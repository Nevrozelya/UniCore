using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace UniCore.Utils
{
    public static class JSON
    {
        private const string LOG = "JSON";

        public static bool TryParse<T>(string json, out T result)
        {
            result = default;

            if (string.IsNullOrWhiteSpace(json))
            {
                Logg.Error("Given json is null or empty!", LOG);
                return false;
            }

            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
                return result != null;
            }
            catch (Exception e)
            {
                Logg.Error(e.Message, LOG);
                return false;
            }
        }

        public static bool TrySerialize(object obj, out string json, bool indent = true, bool stringifyEnums = false)
        {
            json = string.Empty;

            if (obj == null)
            {
                Logg.Error("Given object is null or empty!", LOG);
                return false;
            }

            JsonSerializerSettings settings = new()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            if (indent)
            {
                settings.Formatting = Formatting.Indented;
            }

            if (stringifyEnums)
            {
                StringEnumConverter stringifier = new();
                List<JsonConverter> converters = new() { stringifier };
                settings.Converters = converters;
            }

            try
            {
                json = JsonConvert.SerializeObject(obj, settings);
                return !string.IsNullOrWhiteSpace(json);
            }
            catch (Exception e)
            {
                Logg.Error(e.Message, LOG);
                return false;
            }
        }

        public static bool TryRemap<T>(object input, out T remapped)
        {
            remapped = default;

            if (TrySerialize(input, out string json, indent: false))
            {
                return TryParse(json, out remapped);
            }

            return false;
        }
    }
}