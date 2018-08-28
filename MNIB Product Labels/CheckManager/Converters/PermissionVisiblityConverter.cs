using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using MNIB_Distribution_Manager;

namespace CheckManager.Converters
{
    public class PermissionVisiblityConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            if (value != null && string.IsNullOrEmpty(value.ToString())) return Visibility.Collapsed;
            if (CheckViewModel.Instance.User == null) return Visibility.Collapsed;
            return CheckViewModel.Instance.User.UserPermissions.Any(x => x.Permission.Name != null && x.Permission.Name == value.ToString()) ? Visibility.Visible: Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Visibility.Collapsed;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class PermissionEnabledConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (parameter != null && string.IsNullOrEmpty(parameter.ToString())) return false;
            
            if (CheckViewModel.Instance.User == null) return false;

            return CheckViewModel.Instance.User.UserPermissions.Any(x => x.Permission.Name != null && x.Permission.Name == parameter.ToString());
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class UserEnabledConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value != null && string.IsNullOrEmpty(value.ToString())) return false;

            if (CheckViewModel.Instance.User == null) return false;

            return CheckViewModel.Instance.User.LoginName == value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
