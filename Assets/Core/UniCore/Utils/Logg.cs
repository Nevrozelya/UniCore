using System;
using UnityEngine;

namespace UniCore.Utils
{
    public class Logg
    {
        public const string PREFIX_FORMAT = "{0} | {1} | {2}";
        public const string DEFAULT_FORMAT = "{0} | {1}";

        #region Instanciable

        public bool IsEnabled { get; set; }
        private string _prefix;

        public Logg(string prefix, bool isEnabled = true)
        {
            _prefix = prefix;
            IsEnabled = isEnabled;
        }

        public void Info(string message)
        {
            if (IsEnabled)
                Info(message, _prefix);
        }

        public void Info(object obj)
        {
            if (IsEnabled)
                Info(obj, _prefix);
        }

        public void Warning(string message)
        {
            if (IsEnabled)
                Warning(message, _prefix);
        }

        public void Warning(object obj)
        {
            if (IsEnabled)
                Warning(obj, _prefix);
        }

        public void Error(string message)
        {
            if (IsEnabled)
                Error(message, _prefix);
        }

        public void Error(object obj)
        {
            if (IsEnabled)
                Error(obj, _prefix);
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

        private static string Format(string txt, string prefix)
        {
            string date = DateTime.UtcNow.ToString("o");

            if (string.IsNullOrWhiteSpace(prefix))
            {
                return string.Format(DEFAULT_FORMAT, date, txt);
            }
            else
            {
                return string.Format(PREFIX_FORMAT, date, prefix, txt);
            }
        }
        #endregion
    }
}
