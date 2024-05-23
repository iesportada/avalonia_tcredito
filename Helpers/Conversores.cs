using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace MVVM_TarjetaCredito.Helpers
{
    public class BoolRadioConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;
            else
                return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;
            else
                return value;
        }
    }
}