namespace UniCore.Extensions
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
    }
}
