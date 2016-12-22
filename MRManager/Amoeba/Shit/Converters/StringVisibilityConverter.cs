using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    public class StringVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = parameter != null && (value != null && value.ToString() != parameter.ToString()) ? Visibility.Collapsed : Visibility.Visible;
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}
