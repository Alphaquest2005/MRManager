using System;
using System.Windows.Data;

namespace BarCodes
{
    public class TransPreZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.ToString().Replace("0", "") != "")
                return value.ToString().Remove(0, value.ToString().IndexOfAny("123456789".ToCharArray()));
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
