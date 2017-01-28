using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    public class HasValueTypeVisiblityConverter : IMultiValueConverter
    {
        Dictionary<string,Func<object,bool>> valueChecks = new Dictionary<string, Func<object,bool>>()
        {
            {"TextBox", x => x != null },
            {"CheckBox", x => x != null },
            {"DatePicker", x => System.Convert.ToDateTime(x) != DateTime.MinValue }
        };
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter == null) return Visibility.Collapsed;
            var isTypeCheck = !(values[0] != null && values[0].ToString() != parameter.ToString());
            if (!isTypeCheck) return Visibility.Collapsed;

            var res = (valueChecks[parameter.ToString()].Invoke(values[1]))
                      ? Visibility.Visible : Visibility.Hidden ;
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}