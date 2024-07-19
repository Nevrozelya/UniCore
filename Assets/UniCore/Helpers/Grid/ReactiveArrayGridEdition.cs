namespace UniCore.Helpers.Grid
{
    public struct ReactiveArrayGridEdition<T>
    {
        public readonly int X;
        public readonly int Y;

        public readonly T PreviousValue;
        public readonly T NewValue;

        public ReactiveArrayGridEdition(int x, int y, T previousValue, T newValue)
        {
            X = x;
            Y = y;

            PreviousValue = previousValue;
            NewValue = newValue;
        }
    }
}
