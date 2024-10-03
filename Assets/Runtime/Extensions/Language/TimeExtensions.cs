namespace UniCore.Extensions.Language
{
    public static class TimeExtensions
    {
        public static int ToMilliseconds(this float seconds)
        {
            return (int)(seconds * 1000f);
        }

        public static float ToSeconds(this int milliseconds)
        {
            return milliseconds / 1000f;
        }

        public static string FormatTime(this float seconds)
        {
            string format = "00";
            string formatedMinutes = ((int)seconds / 60).ToString(format);
            string formatedSeconds = ((int)seconds % 60).ToString(format);

            return $"{formatedMinutes}:{formatedSeconds}";
        }
    }
}
