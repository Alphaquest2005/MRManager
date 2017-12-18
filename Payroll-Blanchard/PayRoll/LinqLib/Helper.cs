using System;
using System.Collections.Generic;
using System.Linq;
using LinqLib.Properties;

namespace LinqLib
{
  internal static class Helper
  {
    #region Declarations

    // static hash set that holds primitive numeric types for fast lookup.   
    private static HashSet<string> numericTypes = new HashSet<string> {"Byte",
                                                                       "Int16",
                                                                       "Int32",
                                                                       "Int64",
                                                                       "SByte",
                                                                       "UInt16",
                                                                       "UInt32",
                                                                       "UInt64",
                                                                       "Decimal",
                                                                       "Double",
                                                                       "Single"};

    #endregion

    /// <summary>
    /// Wraps null validation.
    /// </summary>
    /// <param name="parameter">object to validate.</param>
    /// <param name="parameterName">Name of validated parameter.</param>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal static void InvalidateNullParam(object parameter, string parameterName)
    {
      if (parameter == null)
        throw new ArgumentNullException(parameterName);
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal static void InvalidateEnumeratedParam<T>(IEnumerable<T> parameter, int count, string parameterName)
    {
      if (parameter == null)
        throw new ArgumentNullException(parameterName);

      if (parameter.Count() != count)
        throw new ArgumentException(string.Format(Resources.excptionInvalidItemsCount, count), parameterName);

      int idx = 0;
      foreach (T element in parameter)
      {
        if (element == null)
          throw new ArgumentNullException(string.Format(Resources.excptionSourceSequenceIsNull, idx), parameterName);
        else
          idx++;
      }
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal static void InvalidateEmptySequence<T>(IEnumerable<T> parameter, string parameterName)
    {
      if (parameter == null)
        throw new ArgumentNullException(parameterName);

      if (!parameter.Any())
        throw new ArgumentException(Resources.excptionSequenceMinOne, parameterName);
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal static void InvalidateNonNumeric<T>(string caller)
    {
      Type tt = typeof(T);
      if (!Helper.IsNumeric(tt))
        throw new InvalidOperationException(string.Format(Resources.exceptionApplyAttempt, caller, tt.Name));
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal static void InvalidateNonPremitive<T>(string caller)
    {
      Type tt = typeof(T);
      if (!tt.IsPrimitive)
        throw new InvalidOperationException(string.Format(Resources.exceptionApplyAttempt, caller, tt.Name));
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal static void InvalidateNumericRange(double value, double min, double max, string parameterName)
    {
      if (value < min)
        throw new ArgumentException(string.Format(Resources.excptionParamMinRange, parameterName, min));
      if (value > max)
        throw new ArgumentException(string.Format(Resources.excptionParamMaxRange, parameterName, max));
    }

    /// <summary>
    /// Evaluates if a type is of numeric type or of nullable numeric type. 
    /// </summary>
    /// <param name="value">A System.Type to evaluate.</param>
    /// <returns>true if type is numeric or generic nullable of numeric type, false otherwise.</returns>
    public static bool IsNumeric(Type value)
    {
      string name = value.Name;
      if (value.IsValueType && numericTypes.Contains(name)) //if type is primitive and name is found in lookup table
        return true;
      else if (name == "Nullable`1") //if type is nullable<T> and T is numeric.
        return IsNumeric(value.GetGenericArguments()[0]);
      else
        return false;
    }

    /// <summary>
    /// Swaps two references.
    /// </summary>
    /// <typeparam name="T">The data type of the references to swap.</typeparam>
    /// <param name="a">First reference</param>
    /// <param name="b">Second reference</param>
    public static void Swap<T>(ref T a, ref T b)
    {
      T temp = b;
      b = a;
      a = temp;
    }
  }
}
