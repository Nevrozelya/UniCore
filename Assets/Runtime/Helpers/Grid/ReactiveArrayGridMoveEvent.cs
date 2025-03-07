namespace UniCore.Helpers.Grid
{
    public readonly struct ReactiveArrayGridMoveEvent<T>
    {
        public readonly Coordinates From;
        public readonly Coordinates To;

        public readonly T MovedValue;

        public ReactiveArrayGridMoveEvent(Coordinates from, Coordinates to, T movedValue)
        {
            From = from;
            To = to;

            MovedValue = movedValue;
        }
    }
}
