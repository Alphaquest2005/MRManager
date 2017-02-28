using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Core.Common.UI
{
    public static class ExposeControl
    {
        static readonly object NoObject = new object();

        static readonly FrameworkPropertyMetadata AsMetadata
            = new FrameworkPropertyMetadata(
                NoObject,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                AsChanged);

        public static readonly DependencyProperty AsProperty
            = DependencyProperty.RegisterAttached(
                "As",
                typeof(object),
                typeof(ExposeControl),
                AsMetadata);

        public static object GetAs(DependencyObject obj)
        {
            return obj.GetValue(AsProperty);
        }

        public static void SetAs(DependencyObject obj, object value)
        {
            obj.SetValue(AsProperty, value);
        }

        static void AsChanged(
            DependencyObject obj,
            DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != obj)
            {
                obj.Dispatcher.BeginInvoke(new Action(() =>
                {
                    obj.SetCurrentValue(AsProperty, obj);
                }));
            }
        }
    }
}
