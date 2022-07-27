using System;
using System.Globalization;
using System.Windows.Data;

namespace UI_Layer.Converters
{
    public class DashboradReturnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var borrow = DateTime.Parse(value.ToString());
            if (borrow.Day - DateTime.Now.Day >= 7)
            {
                return "Late";
            }
            else return "Normal";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
