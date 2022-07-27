using DomainLayer.Models;
using UI_Layer.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace UI_Layer.Converters
{
    public class DashboardTemplateSelector : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var borrow = value as BorrowViewModel;
            if (borrow != null)
            {
                if (borrow.ActualReturnDate != DateTime.MinValue)
                {
                    return "Returned";
                }
                else
                {
                    if (borrow.ReturnDate.Day - DateTime.Now.Day <= 7)
                    {
                        return "Late";
                    }
                    else
                    {
                        return "Normal";
                    }
                } 
            }
            return "Normal";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
