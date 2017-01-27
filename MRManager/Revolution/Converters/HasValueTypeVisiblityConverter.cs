using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    public class HasValueTypeVisiblityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var res = (parameter != null && (values[0] != null && values[0].ToString() != parameter.ToString()))
                      && (values[1] == null)
                      ? Visibility.Collapsed : Visibility.Visible;
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}