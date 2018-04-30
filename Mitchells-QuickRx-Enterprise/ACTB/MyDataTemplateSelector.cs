using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Test
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(
            object item,
            DependencyObject container)
        {
            Window wnd = Application.Current.MainWindow;
            if (item is string)
            {
                if (wnd != null) return wnd.FindResource("WaitTemplate") as DataTemplate;
            }
            else if (wnd != null) return wnd.FindResource("TheItemTemplate") as DataTemplate;
            return null;
        }
    }
}
