using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    public class StringDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime res;
            if (value != null && DateTime.TryParse(value.ToString(), out res)) return res;
            return DateTime.MinValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}
