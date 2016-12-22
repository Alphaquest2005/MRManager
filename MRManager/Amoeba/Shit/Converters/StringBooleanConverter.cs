using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    public class StringBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool res;
            if (value != null && Boolean.TryParse(value.ToString(), out res)) return res;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}
