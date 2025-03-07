namespace UniCore.Helpers.Grid
{
    public readonly struct ReactiveArrayGridEditEvent<T>
    {
        public readonly Coordinates Position;

        public readonly T PreviousValue;
        public readonly T NewValue;

        public ReactiveArrayGridEditEvent(Coordinates position, T previousValue, T newValue)
        {
            Position = position;

            PreviousValue = previousValue;
            NewValue = newValue;
        }
    }
}
