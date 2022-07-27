using System;
using System.Globalization;
using System.Windows.Data;

namespace UI_Layer.Converters
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double newValue))
            {
                return newValue == 0 ? null : newValue;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double newValue))
            {
                return newValue ;
            }
            else
            {
                return 0;
            }

        }
    }
}
