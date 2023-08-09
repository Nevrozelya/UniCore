using System.Linq;
using System.Net.Mail;

namespace UniCore.Extensions
{
    public static class StringExtensions
    {
        public static string ToFirstLetterUppercase(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;
            else if (text.Length < 2)
                return text.ToUpper();

            return $"{char.ToUpper(text[0])}{text.Substring(1)}";
        }

        public static string ToUppercaseUnderscore(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            return text.ToUpper().Replace('-', '_');
        }

        public static string SanitizeBackslashes(this string text)
        {
            return text.Replace("\\", "/");
        }

        public static string SanitizeEnum(this object obj, bool useDash = true)
        {
            return SanitizeEnum(obj.ToString(), useDash);
        }

        public static string SanitizeEnum(this string text, bool useDash = true)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            char replacement = useDash ? '-' : ' ';
            return text.ToLower().Replace('_', replacement);
        }

        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                MailAddress mailAddress = new(email);
                string[] hostParts = mailAddress.Host.Split('.');

                if (hostParts.Length == 1) // No dot.
                {
                    return false;
                }

                if (hostParts.Any(p => p == string.Empty)) // Double dot.
                {
                    return false;
                }

                if (hostParts[^1].Length < 2) // TLD only one letter.
                {
                    return false;
                }

                if (mailAddress.User.Contains(' '))
                {
                    return false;
                }

                if (mailAddress.User.Split('.').Any(p => p == string.Empty)) // Double dot or dot at end of user part.
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string LastOfSplit(this string text, char separator = '.')
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            string[] split = text.Split(separator);

            if (split.IsNullOrEmpty())
            {
                return text;
            }

            return split[split.Length - 1];
        }
    }
}
