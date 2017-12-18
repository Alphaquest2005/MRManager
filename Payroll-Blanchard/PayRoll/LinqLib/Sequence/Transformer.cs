using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using LinqLib.DynamicCodeGenerator;
using LinqLib.Properties;
using System.Data.Entity;

namespace LinqLib.Sequence
{
  /// <summary>
  /// Provides methods that return a the pivoted transformation of sequences and their sub sequences.  /// 
  /// </summary>
  public static class Transformer
  {
    private static Dictionary<string, DynamicClassGenerator> generatedClasses = new Dictionary<string, DynamicClassGenerator>();
    private static Dictionary<string, string> safeNames = new Dictionary<string, string>();

    private static string safeNamePrefix = "_";

    /// <summary>
    /// Prefix to use when converting fields content to CLR names. If field content starts with a digit, the prefix will be added to make the name CLR compatible. default prefix is an underscore.
    /// </summary>
    public static string SafeNamePrefix
    {
      get { return safeNamePrefix; }
      set { safeNamePrefix = value; }
    }

    /// <summary>
    /// Applies a pivot on a sequence property (sub sequence) in a sequence of objects.
    /// </summary>
    /// <typeparam name="TSource">Type of items in the source sequence.</typeparam>
    /// <typeparam name="TPivot">Type of items in the sequence to pivot.</typeparam>
    /// <param name="source">Source sequence to operate on.</param>
    /// <param name="sequenceToPivot">The sub sequence to pivot.</param>
    /// <param name="nameColumn">The property name in the sub sequence type to use for the pivot column name. The content of that property will become a property in to pivoted output.</param>
    /// <param name="jaggedSequence">A Boolean indicating if all sub sequence have same number of members with the same type of content.</param>
    /// <returns>A sequence of objects representing the pivoted values of the original sequence inline with the sub sequence.</returns>
    public static IEnumerable<object> Pivot<TSource, TPivot>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TPivot>> sequenceToPivot, Func<TPivot, string> nameColumn, bool jaggedSequence)
    {
      return Pivot<TSource, TPivot>(source, sequenceToPivot, nameColumn, jaggedSequence, null);
    }

    /// <summary>
    /// Applies a pivot on a sequence property (sub sequence) in a sequence of objects.
    /// </summary>
    /// <typeparam name="TSource">Type of items in the source sequence.</typeparam>
    /// <typeparam name="TPivot">Type of items in the sequence to pivot.</typeparam>
    /// <param name="source">Source sequence to operate on.</param>
    /// <param name="sequenceToPivot">The sub sequence to pivot.</param>
    /// <param name="nameColumn">The property name in the sub sequence type to use for the pivot column name. The content of that property will become a property in to pivoted output.</param>
    /// <param name="jaggedSequence">A Boolean indicating if all sub sequence have same number of members with the same type of content.</param>
    /// <param name="classGenerationEventHandler">The name of the event handler that will handle the ClassGenerationEventArgs passed from the class generator.</param>
    /// <returns>A sequence of objects representing the pivoted values of the original sequence inline with the sub sequence.</returns>
    public static IEnumerable<object> Pivot<TSource, TPivot>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TPivot>> sequenceToPivot, Func<TPivot, string> nameColumn, bool jaggedSequence, EventHandler<ClassGenerationEventArgs> classGenerationEventHandler)
    {
      return Pivot(source, sequenceToPivot, nameColumn, X => X, jaggedSequence, classGenerationEventHandler);
    }

    /// <summary>
    /// Applies a pivot on a sequence property (sub sequence) in a sequence of objects.
    /// </summary>
    /// <typeparam name="TSource">Type of items in the source sequence.</typeparam>
    /// <typeparam name="TPivot">Type of items in the sequence to pivot.</typeparam>
    /// <typeparam name="TResult">Type of result element.</typeparam>
    /// <param name="source">Source sequence to operate on.</param>
    /// <param name="sequenceToPivot">The sub sequence to pivot.</param>
    /// <param name="nameColumn">The property name in the sub sequence type to use for the pivot column name. The content of that property will become a property in to pivoted output.</param>
    /// <param name="valueColumn">An expression used on members in the sub sequence type to set the pivot column value. The output of this expression will become the value of the pivoted column.</param>
    /// <param name="jaggedSequence">A Boolean indicating if all sub sequence have same number of members with the same type of content.</param>
    /// <returns>A sequence of objects representing the pivoted values of the original sequence inline with the sub sequence.</returns>    
    public static IEnumerable<object> Pivot<TSource, TPivot, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TPivot>> sequenceToPivot, Func<TPivot, string> nameColumn, Func<TPivot, TResult> valueColumn, bool jaggedSequence)
    {
      return Pivot<TSource, TPivot, TResult>(source, sequenceToPivot, nameColumn, valueColumn, jaggedSequence, null);
    }

    /// <summary>
    /// Applies a pivot on a sequence property (sub sequence) in a sequence of objects.
    /// </summary>
    /// <typeparam name="TSource">Type of items in the source sequence.</typeparam>
    /// <typeparam name="TPivot">Type of items in the sequence to pivot.</typeparam>
    /// <typeparam name="TResult">Type of result element.</typeparam>
    /// <param name="source">Source sequence to operate on.</param>
    /// <param name="sequenceToPivot">The sub sequence to pivot.</param>
    /// <param name="nameColumn">The property name in the sub sequence type to use for the pivot column name. The content of that property will become a property in to pivoted output.</param>
    /// <param name="valueColumn">An expression used on members in the sub sequence type to set the pivot column value. The output of this expression will become the value of the pivoted column.</param>
    /// <param name="jaggedSequence">A Boolean indicating if all sub sequence have same number of members with the same type of content.</param>
    /// <param name="classGenerationEventHandler">The name of the event handler that will handle the ClassGenerationEventArgs passed from the class generator.</param>
    /// <returns>A sequence of objects representing the pivoted values of the original sequence inline with the sub sequence.</returns>    
    public static IEnumerable<object> Pivot<TSource, TPivot, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TPivot>> sequenceToPivot, Func<TPivot, string> nameColumn, Func<TPivot, TResult> valueColumn, bool jaggedSequence, EventHandler<ClassGenerationEventArgs> classGenerationEventHandler)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(sequenceToPivot, "sequenceToPivot");
      Helper.InvalidateNullParam(nameColumn, "nameColumn");
      Helper.InvalidateNullParam(valueColumn, "valueColumn");

      Helper.InvalidateEmptySequence(source, "source");
      Helper.InvalidateNullParam(sequenceToPivot(source.First()), "resultOfSequenceToPivot");

      IEnumerable<string> fields;
      if (jaggedSequence)
        fields = GetFields<TSource, TPivot>(source, sequenceToPivot, nameColumn);
      else
        fields = GetFields<TSource, TPivot>(source.First(), sequenceToPivot, nameColumn);

      if (fields.Count() == 0)
      {
        foreach (TSource item in source)
          yield return item;

        yield break;
      }
      

      PivotInfo pivotInfo = PrepairPivot<TSource, TPivot, TResult>(fields, classGenerationEventHandler);

      foreach (TSource sourceItem in source)
        yield return GetPivotedInstance<TSource, TPivot, TResult>(sourceItem, sequenceToPivot, nameColumn, valueColumn, pivotInfo);

        
    }

    /// <summary>
    /// Applies a pivot on a sequence property in the source object.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TPivot">The type of the pivot.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="sequenceToPivot">The sequence to pivot.</param>
    /// <param name="nameColumn">The property name in the sub sequence type to use for the pivot column name. The content of that property will become a property in to pivoted output.</param>
    /// <param name="valueColumn">An expression used on members in the sub sequence type to set the pivot column value. The output of this expression will become the value of the pivoted column.</param>    
    /// <returns>A pivoted object where rows of a collection become properties.</returns>
    public static object Pivot<TSource, TPivot, TResult>(this TSource source, Func<TSource, IEnumerable<TPivot>> sequenceToPivot, Func<TPivot, string> nameColumn, Func<TPivot, TResult> valueColumn)
    {
      return Pivot(source, sequenceToPivot, nameColumn, valueColumn, null);
    }

    /// <summary>
    /// Applies a pivot on a sequence property of the source object.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TPivot">The type of the pivot.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="sequenceToPivot">The sequence to pivot.</param>
    /// <param name="nameColumn">The property name in the sub sequence type to use for the pivot column name. The content of that property will become a property in to pivoted output.</param>
    /// <param name="valueColumn">An expression used on members in the sequence type to set the pivot column value. The output of this expression will become the value of the pivoted column.</param>
    /// <param name="classGenerationEventHandler">The name of the event handler that will handle the ClassGenerationEventArgs passed from the class generator.</param>
    /// <returns>A pivoted object where rows of a collection become properties.</returns>
    public static object Pivot<TSource, TPivot, TResult>(this TSource source, Func<TSource, IEnumerable<TPivot>> sequenceToPivot, Func<TPivot, string> nameColumn, Func<TPivot, TResult> valueColumn, EventHandler<ClassGenerationEventArgs> classGenerationEventHandler)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(sequenceToPivot, "sequenceToPivot");
      Helper.InvalidateNullParam(nameColumn, "nameColumn");
      Helper.InvalidateNullParam(valueColumn, "valueColumn");

      Helper.InvalidateNullParam(sequenceToPivot(source), "resultOfSequenceToPivot");

      IEnumerable<string> fields = GetFields<TSource, TPivot>(source, sequenceToPivot, nameColumn);
      if (fields.Count() == 0)
        return source;

      PivotInfo pivotInfo = PrepairPivot<TSource, TPivot, TResult>(fields, classGenerationEventHandler);
      return GetPivotedInstance<TSource, TPivot, TResult>(source, sequenceToPivot, nameColumn, valueColumn, pivotInfo);

    }

    private static PivotInfo PrepairPivot<TSource, TPivot, TResult>(IEnumerable<string> fields, EventHandler<ClassGenerationEventArgs> classGenerationEventHandler)
    {
      PivotInfo pivotInfo = new PivotInfo();
      Type pivotedType;

      Type sourceType = typeof(TSource);
      Type resultType = typeof(TResult);

      pivotInfo.pivotedProperties = new Dictionary<string, PropertyInfo>();

      string className = string.Concat(sourceType.Name, "WithPivotOn", typeof(TPivot).Name);
      string keyName = string.Concat(sourceType.Namespace, ".", className, ".", fields.Aggregate((F1, F2) => string.Concat(F1, ".", F2)), ".", sourceType.Name, ".", resultType.Name);

      if (generatedClasses.ContainsKey(keyName))
        pivotInfo.pivotedClass = generatedClasses[keyName];
      else
      {
        pivotInfo.pivotedClass = new DynamicClassGenerator(className, sourceType, fields, resultType, classGenerationEventHandler);
        generatedClasses[keyName] = pivotInfo.pivotedClass;
      }

      pivotedType = pivotInfo.pivotedClass.GetInstance().GetType();
      foreach (string field in fields)
        pivotInfo.pivotedProperties.Add(field, pivotedType.GetProperty(field));

      return pivotInfo;
    }

    private static IEnumerable<string> GetFields<TSource, TPivot>(TSource source, Func<TSource, IEnumerable<TPivot>> sequenceToPivot, Func<TPivot, string> nameColumn)
    {
      return sequenceToPivot(source)
                   .Where(X => X != null)
                   .Select(X => GetSafeRuntimeName(nameColumn(X)))
                   .Where(F => !string.IsNullOrEmpty(F))
                   .Distinct();
    }

    private static IEnumerable<string> GetFields<TSource, TPivot>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TPivot>> sequenceToPivot, Func<TPivot, string> nameColumn)
    {
      return (from item in source
              where sequenceToPivot(item) != null
              from name in sequenceToPivot(item).Where(X => X != null).Select(X => GetSafeRuntimeName(nameColumn(X)))
              select name)
              .Where(F => !string.IsNullOrEmpty(F))
              .Distinct();
    }

    private static object GetPivotedInstance<TSource, TPivot, TResult>(TSource sourceItem, Func<TSource, IEnumerable<TPivot>> sequenceToPivot, Func<TPivot, string> nameColumn, Func<TPivot, TResult> valueColumn, PivotInfo pivotInfo)
    {
      object pivot = pivotInfo.pivotedClass.GetInstance();
      Type sourceType = typeof(TSource);

      foreach (PropertyInfo prop in sourceType.GetProperties().Where(P => P.CanWrite && P.CanRead))
      {
          try
          {
              prop.SetValue(pivot, prop.GetValue(sourceItem, null), null);
          }
          catch
          {
          }
      }
        

      foreach (FieldInfo fld in sourceType.GetFields())
        fld.SetValue(pivot, fld.GetValue(sourceItem));

      IEnumerable<TPivot> pivotItems = sequenceToPivot(sourceItem);
      if (pivotItems != null)
        foreach (TPivot pivotItem in pivotItems.Where(X => X != null))
          try
          {
            pivotInfo.pivotedProperties[GetSafeRuntimeName(nameColumn(pivotItem))].SetValue(pivot, valueColumn(pivotItem), null);
          }
          catch (Exception)
          {
           // throw new ArgumentException(string.Format(Resources.excptionDynamicPropertyAccess, nameColumn(pivotItem)));
          }
      return pivot;
    }

    /// <summary>
    /// Creates a CLR safe element name.
    /// </summary>
    /// <param name="name">Raw element name.</param>
    /// <returns>CLR safe element name.</returns>
    private static string GetSafeRuntimeName(string name)
    {
        var key = name.Replace(" ", "_");
        if (!safeNames.ContainsKey(key))
        {
            char[] safeChars = key.Where(C => char.IsLetterOrDigit(C) || C == '_').ToArray();
            string safeName = new string(safeChars);
            if (char.IsDigit(safeChars[0])) safeName = safeNamePrefix + safeName;
             
            Debug.WriteLine(key);
            safeNames.Add(key, safeName);
        }
        return safeNames[key];
    }

    /// <summary>
    /// Returns the original name of the property / field name.
    /// </summary>
    /// <param name="safeRuntimeName">Property / field name.</param>
    /// <returns>The original name of the property / field name.</returns>
    /// <exception cref="System.ArgumentException">
    /// The Safe Runtime Name cannot be found.
    /// </exception>
    /// <remarks>
    /// When pivoting a sub collection, the new properties of the pivoted type gets their names from the content of elements in the sub collection. The content might not contain valid CLR names.
    /// THe Pivot function will correct the name to adhere to the .NET requirements.  This method will lookup the original name based on the property / field name. 
    /// </remarks>
    public static string GetName(string safeRuntimeName)
    {
      if (!safeNames.ContainsValue(safeRuntimeName))
        throw new ArgumentException(string.Format(Resources.exceptionMissingFieldOrProperty, safeRuntimeName), "safeRuntimeName");
      else
        return safeNames.First(N => N.Value == safeRuntimeName).Key;
    }

    private class PivotInfo
    {
      public Dictionary<string, PropertyInfo> pivotedProperties;
      public DynamicClassGenerator pivotedClass;
    }
  }
}

