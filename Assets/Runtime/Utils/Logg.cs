using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UniCore.Extensions;
using UnityEngine;

namespace UniCore.Utils
{
    public class Logg
    {
        public const string DEFAULT_JOIN_SEPARATOR = ", ";
        public const string DEFAULT_DUMP_FILE_PREFIXE = "dump";

        public const string PREFIXED_FORMAT = "{0} | {1} | {2}";
        public const string DEFAULT_FORMAT = "{0} | {1}";

        #region Instanciable
        public bool IsEnabled { get; set; }
        private string _prefix;

        public Logg(string prefix, bool isEnabled = true)
        {
            _prefix = prefix;
            IsEnabled = isEnabled;
        }

        public Logg(object source, bool isEnabled = true)
        {
            if (source != null)
            {
                _prefix = source.GetType().ToString().LastOfSplit();
            }

            IsEnabled = isEnabled;
        }

        public void Info(string message)
        {
            if (IsEnabled)
            {
                Info(message, _prefix);
            }
        }

        public void Info(object obj)
        {
            if (IsEnabled)
            {
                Info(obj, _prefix);
            }
        }

        public void Warning(string message)
        {
            if (IsEnabled)
            {
                Warning(message, _prefix);
            }
        }

        public void Warning(object obj)
        {
            if (IsEnabled)
            {
                Warning(obj, _prefix);
            }
        }

        public void Error(string message)
        {
            if (IsEnabled)
            {
                Error(message, _prefix);
            }
        }

        public void Error(object obj)
        {
            if (IsEnabled)
            {
                Error(obj, _prefix);
            }
        }

        public void Jsonify(object obj)
        {
            if (IsEnabled)
            {
                Jsonify(obj, _prefix);
            }
        }

        public void Dump(object obj)
        {
            if (IsEnabled)
            {
                Dump(obj, _prefix);
            }
        }

        public void Join<T>(IEnumerable<T> enumerable, string separator = DEFAULT_JOIN_SEPARATOR)
        {
            if (IsEnabled)
            {
                Join(enumerable, separator, _prefix);
            }
        }

        public void Join<T>(ICollection<T> collection, string separator = DEFAULT_JOIN_SEPARATOR)
        {
            if (IsEnabled)
            {
                Join(collection, separator, _prefix);
            }
        }

        public void Join<T>(T[] array, string separator = DEFAULT_JOIN_SEPARATOR)
        {
            if (IsEnabled)
            {
                Join(array, separator, _prefix);
            }
        }
        #endregion

        #region Static
        public static void Info(string txt, string prefix = null)
        {
            string formated = Format(txt, prefix);
            Debug.Log(formated);
        }

        public static void Info(object obj, string prefix = null)
        {
            Info(obj?.ToString(), prefix);
        }

        public static void Warning(string txt, string prefix = null)
        {
            string formated = Format(txt, prefix);
            Debug.LogWarning(formated);
        }

        public static void Warning(object obj, string prefix = null)
        {
            Warning(obj?.ToString(), prefix);
        }

        public static void Error(string txt, string prefix = null)
        {
            string formated = Format(txt, prefix);
            Debug.LogError(formated);
        }

        public static void Error(object obj, string prefix = null)
        {
            Error(obj?.ToString(), prefix);
        }

        public static void Jsonify(object obj, string prefix = null)
        {
            if (JSON.TrySerialize(obj, out string json))
            {
                Info(json, prefix);
            }
        }

        public static string Dump(object obj, string filenamePrefix = DEFAULT_DUMP_FILE_PREFIXE)
        {
            if (Application.isEditor && JSON.TrySerialize(obj, out string json))
            {
                string date = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
                string filename;

                if (string.IsNullOrWhiteSpace(filenamePrefix))
                {
                    filename = $"{date}.json";
                }
                else
                {
                    filename = $"{filenamePrefix}-{date}.json";
                }

                string path = Path.Combine(Application.persistentDataPath, filename);
                FileUtils.TryWriteAsync(path, json, CancellationToken.None).Forget();

                return path;
            }

            return null;
        }

        public static void Join<T>(IEnumerable<T> enumerable, string separator = DEFAULT_JOIN_SEPARATOR, string prefix = null)
        {
            if (enumerable.IsNullOrEmpty())
            {
                return;
            }

            string joined = string.Join(separator, enumerable);
            Info(joined, prefix);
        }

        public static void Join<T>(ICollection<T> collection, string separator = DEFAULT_JOIN_SEPARATOR, string prefix = null)
        {
            if (collection.IsNullOrEmpty())
            {
                return;
            }

            string joined = string.Join(separator, collection);
            Info(joined, prefix);
        }

        public static void Join<T>(T[] array, string separator = DEFAULT_JOIN_SEPARATOR, string prefix = null)
        {
            if (array.IsNullOrEmpty())
            {
                return;
            }

            string joined = string.Join(separator, array);
            Info(joined, prefix);
        }

        private static string Format(string txt, string prefix)
        {
            string date = DateTime.Now.ToString("o");

            if (string.IsNullOrWhiteSpace(prefix))
            {
                return string.Format(DEFAULT_FORMAT, date, txt);
            }
            else
            {
                return string.Format(PREFIXED_FORMAT, date, prefix, txt);
            }
        }
        #endregion
    }
}
