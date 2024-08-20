using UnityEngine;

namespace UniCore.Extensions
{
    // https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextMark.html

    public enum RichTag
    {
        Bold, Italic, Underlined, Crossed, Marked, Color
    }

    public static class RichTextExtensions
    {
        public static string Format(this char character, RichTag tag, string value)
        {
            return character.ToString().Format(tag, value);
        }

        public static string Format(this string txt, RichTag tag, string value)
        {
            switch (tag)
            {
                case RichTag.Bold:
                    return txt.Bold();
                case RichTag.Italic:
                    return txt.Italic();
                case RichTag.Underlined:
                    return txt.Underline();
                case RichTag.Crossed:
                    return txt.Cross();
                case RichTag.Marked:
                    return txt.Mark(value);
                case RichTag.Color:
                    return txt.Color(value);
            }
            return txt;
        }

        public static string Bold(this char txt)
        {
            return $"<b>{txt}</b>";
        }

        public static string Bold(this string txt)
        {
            return $"<b>{txt}</b>";
        }

        public static string Italic(this char txt)
        {
            return $"<i>{txt}</i>";
        }

        public static string Italic(this string txt)
        {
            return $"<i>{txt}</i>";
        }

        public static string Underline(this char txt)
        {
            return $"<u>{txt}</u>";
        }

        public static string Underline(this string txt)
        {
            return $"<u>{txt}</u>";
        }

        public static string Cross(this char txt)
        {
            return $"<s>{txt}</s>";
        }

        public static string Cross(this string txt)
        {
            return $"<s>{txt}</s>";
        }

        public static string Color(this string txt, string color)
        {
            return $"<color={color}>{txt}</color>";
        }

        public static string Color(this char txt, string color)
        {
            return $"<color={color}>{txt}</color>";
        }

        public static string Color(this char txt, Color color)
        {
            string hexa = color.ToHexa(true);
            return txt.Color(hexa);
        }

        public static string Color(this string txt, Color color)
        {
            string hexa = color.ToHexa(true);
            return txt.Color(hexa);
        }

        public static string Mark(this string txt, string color)
        {
            return $"<mark={color}>{txt}</mark>";
        }

        public static string Mark(this char txt, string color)
        {
            return $"<mark={color}>{txt}</mark>";
        }

        public static string Mark(this char txt, Color color)
        {
            string hexa = color.ToHexa(true);
            return txt.Color(hexa);
        }

        public static string Mark(this string txt, Color color)
        {
            string hexa = color.ToHexa(true);
            return txt.Color(hexa);
        }
    }
}