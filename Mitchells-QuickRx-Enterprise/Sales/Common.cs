using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using RMSDataAccessLayer;

using System.Globalization;

namespace SalesRegion
{

    public class ArithmeticConverter : IValueConverter
    {
        private const string ArithmeticParseExpression = "([+\\-*/]{1,1})\\s{0,}(\\-?[\\d\\.]+)";
        private Regex arithmeticRegex = new Regex(ArithmeticParseExpression);

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value is double && parameter != null)
            {
                string param = parameter.ToString();

                if (param.Length > 0)
                {
                    Match match = arithmeticRegex.Match(param);
                    if (match != null && match.Groups.Count == 3)
                    {
                        string operation = match.Groups[1].Value.Trim();
                        string numericValue = match.Groups[2].Value;

                        double number = 0;
                        if (double.TryParse(numericValue, out number)) // this should always succeed or our regex is broken
                        {
                            double valueAsDouble = (double)value;
                            double returnValue = 0;

                            switch (operation)
                            {
                                case "+":
                                    returnValue = valueAsDouble + number;
                                    break;

                                case "-":
                                    returnValue = valueAsDouble - number;
                                    break;

                                case "*":
                                    returnValue = valueAsDouble * number;
                                    break;

                                case "/":
                                    returnValue = valueAsDouble / number;
                                    break;
                            }

                            return returnValue;
                        }
                    }
                }
            }

            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

    public class TimeSpanValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() == typeof(TimeSpan))
            {
                TimeSpan t = (TimeSpan)value;
                return String.Format("{0} Hours:{1} Min:{2} Sec", t.Hours * -1, t.Minutes * -1, t.Seconds * -1);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NotNullValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.ToString() == "")
            {
               return "Null";
            }
            else
            {
                 return "Not Null";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsPharmacistVisibleConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Cashier c = SalesVM.Instance.CashierEx; 
            Cashier tc = (values[0] as Cashier);
            Cashier p = (values[1] as Cashier);
            //if (c == null || c.Role == "Pharmacist")
            if (parameter.ToString() == "Button")
            {
                if (c != null)
                {
                    if (c.Role == "Pharmacist")
                    {
                        if (p != null && p.Id == c.Id)
                        {
                            return Visibility.Visible;
                        }
                        else
                        {
                            return Visibility.Hidden;
                        }
                    }
                    else
                    {
                        if (tc != null && c.Id == tc.Id && SalesVM.Instance.Pharmacists != null && SalesVM.Instance.Pharmacists.Any() == true)
                        {
                            return Visibility.Visible;
                        }

                    }
                }


                if (tc != null && c.Id == tc.Id && p != null && p.Role == "Pharmacist")
                {
                    return Visibility.Visible;
                }
                return Visibility.Hidden;
            }
            else
            {
                if (c != null && c.Role == "Pharmacist" || tc != null && c.Id != tc.Id )
                {
                   // if(p != null && p.Id == c.Id)
                    return Visibility.Hidden;
                }
                //if (SalesVM._pharmacists == null || SalesVM._pharmacists.Count == 0)
                //{
                //    return Visibility.Hidden;
                //}
                
                    return Visibility.Visible;
                
            }
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsPharmacistNotVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Cashier c = (value as Cashier);
            if (c != null && c.Role == "Pharmacist")
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DosageSourceConverter : IValueConverter
    {
        RMSDataAccessLayer.RMSModel db = new RMSDataAccessLayer.RMSModel();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var dosagelst = (from pe in db.TransactionEntryBase.OfType<PrescriptionEntry>()
                                where pe.Item == value
                                select pe.Dosage);
                return dosagelst;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TransPreZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value != null && value.ToString().Replace("0","") != "")
            return value.ToString().Remove(0, value.ToString().IndexOfAny("123456789".ToCharArray()));
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToNullableDateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            DateTime? d = (DateTime?)value;
            if (d.HasValue && d.Value > DateTime.Parse("1/1/2009"))
                return "Product Expires:" + d.Value.ToString("MMM-dd-yyyy");
            else
                return String.Empty;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            string s = (string)value;
            if (String.IsNullOrEmpty(s))
                return null;
            else
                return (DateTime?)DateTime.Parse(s, culture);
        }
    }

    public enum Sex
    {
        Male,
        Female
    }

    public class EnumMatchToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return null;
           // int integer = (int)value;
            //if (integer == int.Parse(parameter.ToString()))
            if(System.Convert.ToBoolean(value) == System.Convert.ToBoolean(parameter))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToBoolean(parameter);
        }
    } 


    public enum SalesPadTransState
    {
        Transaction,
        Receipt,
    }

   
    public class AutoCompleteEntry
    {
        private string[] keywordStrings;
        private string displayString;

        public string[] KeywordStrings
        {
            get
            {
                if (keywordStrings == null)
                {
                    keywordStrings = new string[] { displayString };
                }
                return keywordStrings;
            }
        }

        public string DisplayName
        {
            get { return displayString; }
            set { displayString = value; }
        }

        public AutoCompleteEntry(string name, params string[] keywords)
        {
            displayString = name;
            keywordStrings = keywords;
        }

        public override string ToString()
        {
            return displayString;
        }


    }


    public class Common
    {
        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        /// 

        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        //public static T CopyEntity<T>(RMSModel ctx, T entity, bool copyKeys = false) where T : class
        //{
        //    T clone = ctx.CreateObject<T>();
        //    PropertyInfo[] pis = entity.GetType().GetProperties();

        //    foreach (PropertyInfo pi in pis)
        //    {
        //        EdmScalarPropertyAttribute[] attrs = (EdmScalarPropertyAttribute[])pi.GetCustomAttributes(typeof(EdmScalarPropertyAttribute), false);

        //        foreach (EdmScalarPropertyAttribute attr in attrs)
        //        {
        //            if (!copyKeys && attr.EntityKeyProperty)
        //                continue;

        //            pi.SetValue(clone, pi.GetValue(entity, null), null);
        //        }
        //    }

        //    return clone;
        //}

    }
}
