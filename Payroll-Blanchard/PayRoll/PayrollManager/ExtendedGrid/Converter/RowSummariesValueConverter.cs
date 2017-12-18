using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ExtendedGrid.Classes;
using ExtendedGrid.ExtendedGridControl;

namespace ExtendedGrid.Converter
{
    internal class RowSummariesValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            UIElement currentStackPanel=null;
            object value=null;
            try
            {
                if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
                    return "";
                var currentCoulumn = (ExtendedGrid.Microsoft.Windows.Controls.Primitives.DataGridColumnHeader)values[0];
                //string currentColumnName = RowSummariesHelper.CurrentColumn;
                //if (currentColumnName == null) return "";
                var dt = ((DataTable)values[1]);

                if (currentCoulumn.Column == null)
                {
                    return "";
                }
                string computation = parameter.ToString();
                //if (currentCoulumn.Column.SortMemberPath != RowSummariesHelper.CurrentColumn)
                //{
                //    switch (computation)
                //    {
                //        case "Count":
                //            return dt.Rows[ExtendedDataGrid.Count][currentCoulumn.Column.SortMemberPath];
                //        case "Sum":
                //            return dt.Rows[ExtendedDataGrid.Sum][currentCoulumn.Column.SortMemberPath];
                //        case "Average":
                //            return dt.Rows[ExtendedDataGrid.Average][currentCoulumn.Column.SortMemberPath];
                //        case "Min":
                //            return dt.Rows[ExtendedDataGrid.Min][currentCoulumn.Column.SortMemberPath];
                //        case "Max":
                //            return dt.Rows[ExtendedDataGrid.Max][currentCoulumn.Column.SortMemberPath];
                //        case "Smallest":
                //            return dt.Rows[ExtendedDataGrid.Smallest][currentCoulumn.Column.SortMemberPath];
                //        case "Largest":
                //            return dt.Rows[ExtendedDataGrid.Lasrgest][currentCoulumn.Column.SortMemberPath];
                //    }
                //}




                var stackpanel = FindControls.FindChild<System.Windows.Controls.StackPanel>(currentCoulumn, null);
                var currentColumnName = currentCoulumn.Column.SortMemberPath;
                switch (computation)
                {
                    case "Count":
                        value = dt.Rows[ExtendedDataGrid.Count][currentColumnName];
                         currentStackPanel = stackpanel == null ? null : stackpanel.Children[ExtendedDataGrid.Count];
                        return value;
                    case "Sum":
                         value = dt.Rows[ExtendedDataGrid.Sum][currentColumnName];
                         currentStackPanel = stackpanel == null ? null : stackpanel.Children[ExtendedDataGrid.Sum];
                        return value;
                    case "Average":
                        value = dt.Rows[ExtendedDataGrid.Average][currentColumnName];
                         currentStackPanel = stackpanel == null ? null : stackpanel.Children[ExtendedDataGrid.Average];
                        return value;
                    case "Min":
                        value = dt.Rows[ExtendedDataGrid.Min][currentColumnName];
                         currentStackPanel = stackpanel == null ? null : stackpanel.Children[ExtendedDataGrid.Min];
                        return value;
                    case "Max":
                        value = dt.Rows[ExtendedDataGrid.Max][currentColumnName];
                         currentStackPanel = stackpanel == null ? null : stackpanel.Children[ExtendedDataGrid.Max];
                        return value;
                    case "Smallest":
                        value = dt.Rows[ExtendedDataGrid.Smallest][currentColumnName];
                         currentStackPanel = stackpanel == null ? null : stackpanel.Children[ExtendedDataGrid.Smallest];
                        return value;
                    case "Largest":
                        value = dt.Rows[ExtendedDataGrid.Lasrgest][currentColumnName];
                         currentStackPanel = stackpanel == null ? null : stackpanel.Children[ExtendedDataGrid.Lasrgest];
                        return value;
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                if (currentStackPanel!=null)
                {
                    currentStackPanel.Visibility = string.IsNullOrEmpty(System.Convert.ToString(value)) ? Visibility.Collapsed : Visibility.Visible;
                }
              
            }
         
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


