namespace UniCore.Helpers.Grid
{
    public struct ReactiveArrayGridEdition<T>
    {
        public readonly Coordinates Position;
        public readonly T PreviousValue;
        public readonly T NewValue;

        public ReactiveArrayGridEdition(Coordinates position, T previousValue, T newValue)
        {
            Position = position;
            PreviousValue = previousValue;
            NewValue = newValue;
        }
    }
}
