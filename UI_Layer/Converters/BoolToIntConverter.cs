using System;
using System.Globalization;
using System.Windows.Data;

namespace UI_Layer.Converters
{
    public class BoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ? 0 : 1; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 1:
                    return false;
                default:
                    return true;
            }
        }
    }
}
