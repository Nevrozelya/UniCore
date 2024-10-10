using System;
using System.ComponentModel;
using System.Globalization;

namespace UniCore.Helpers.Grid
{
    // Example : https://stackoverflow.com/questions/24504245/not-ableto-serialize-dictionary-with-complex-key-using-json-net
    // Note: The CanConvertTo() implementation is not needed for Dictionary<K,T> usage,
    // because Dictionary uses string as destinationType by design.
    // We provided the CanConvertTo method anyway in case it is needed in other use cases.

    public class CoordinatesConverter : TypeConverter
    {
        private const char SEPARATOR = ':';

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string serialization = value as string;

            if (string.IsNullOrWhiteSpace(serialization))
            {
                return base.ConvertFrom(context, culture, value);
            }

            string[] split = serialization.Split(SEPARATOR);

            int x = int.Parse(split[0]);
            int y = int.Parse(split[1]);

            return new Coordinates(x, y);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            Coordinates casted = (Coordinates)value;
            return string.Join(SEPARATOR, casted.X, casted.Y);
        }
    }
}
