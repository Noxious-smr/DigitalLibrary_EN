using System;
using System.Globalization;
using System.Windows.Data;

namespace UI_Layer.Converters
{
    public class ISBNConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                if (double.TryParse(value.ToString(), out double newValue))
                {
                    return value.ToString();
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
