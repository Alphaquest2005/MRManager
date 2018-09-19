using System;
using System.Windows.Data;
using System.Windows.Media;

namespace CashSummaryManager.Converters
{
    public class ColorConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var text = (double)value;
            return Math.Abs(text) < 0.001 ? Colors.Green : Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}