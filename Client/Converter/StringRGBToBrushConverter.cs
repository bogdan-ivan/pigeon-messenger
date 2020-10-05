using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Client
{
    public class StringRGBToBrushConverter : BaseValueConverter<StringRGBToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue && value is string && !String.IsNullOrWhiteSpace((string)value))
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom($"#{value}"));
            }
            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
