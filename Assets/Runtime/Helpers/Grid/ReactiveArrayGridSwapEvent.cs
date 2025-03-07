namespace UniCore.Helpers.Grid
{
    public readonly struct ReactiveArrayGridSwapEvent<T>
    {
        public readonly Coordinates From;
        public readonly Coordinates To;

        public readonly T FromPreviousValue;
        public readonly T FromNewValue;

        public readonly T ToPreviousValue => FromNewValue;
        public readonly T ToNewValue => FromPreviousValue;

        public ReactiveArrayGridSwapEvent(Coordinates from, Coordinates to, T fromPreviousValue, T fromNewValue)
        {
            From = from;
            To = to;

            FromPreviousValue = fromPreviousValue;
            FromNewValue = fromNewValue;
        }
    }
}
