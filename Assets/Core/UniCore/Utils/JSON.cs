﻿namespace CTA.Core.Utils
{
    public static class JSON
    {
        //public static bool TryDeserialize<T>(string json, out T result)
        //{
        //    try
        //    {
        //        result = JsonConvert.DeserializeObject<T>(json);
        //        if (result == null)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (System.Exception e)
        //    {
        //        Log.Error($"{e.Message}", "JSON");
        //        result = default(T);
        //        return false;
        //    }
        //}

        //public static bool TrySerialize(object obj, out string json, bool indent = true)
        //{
        //    Formatting formatting = indent ? Formatting.Indented : Formatting.None;
        //    try
        //    {
        //        json = JsonConvert.SerializeObject(obj, formatting, new JsonSerializerSettings
        //        {
        //            NullValueHandling = NullValueHandling.Ignore
        //        });

        //        if (string.IsNullOrWhiteSpace(json))
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch // (Exception e)
        //    {
        //        json = string.Empty;
        //        return false;
        //    }
        //}

        //public static bool TryRemap<T>(object input, out T remapped)
        //{
        //    remapped = default;

        //    if (TrySerialize(input, out string json, indent: false))
        //    {
        //        if (TryDeserialize(json, out remapped))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
    }
}