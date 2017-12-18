using System;
using System.Collections.Generic;
using System.Linq;
using LinqLib.Properties;

namespace LinqLib.Operators
{
  /// <summary>
  /// Provides extension methods for calculating Frequency, probability, Average, variance and standard deviation on sequences
  /// </summary>
  public static class Statistical
  {
    #region Sequence Frequency and probability

    /// <summary>
    /// Calculates the frequency of each element in a sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the evaluated sequences.</typeparam>
    /// <param name="source">Source sequence.</param>
    /// <returns>A sequence of Key-Value pairs with an entry for each element in original sequence and its frequency.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<KeyValuePair<TSource, int>> Frequency<TSource>(this IEnumerable<TSource> source)
    {
      Helper.InvalidateNullParam(source, "source");

      return source.GroupBy(I => I).Select(K => new KeyValuePair<TSource, int>(K.Key, K.Count()));
    }

    /// <summary>
    /// Calculates the frequency of each element in a sequence based on a custom list of buckets.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the evaluated sequences.</typeparam>
    /// <typeparam name="TBucket">The type of the bucket elements.</typeparam>
    /// <param name="source">Source sequence.</param>
    /// <param name="buckets">A list of buckets to which each element in the source sequence may fit.</param>
    /// <param name="bucketSelector">A function that take a source element and the list of buckets and returns the bucket where the source element fits.</param>
    /// <returns>A sequence of Key-Value pairs with an entry for each bucket element and its frequency.</returns>
    /// <exception cref="System.ArgumentNullException">source or buckets or bucketSelector is null.</exception>
    public static IEnumerable<KeyValuePair<TBucket, int>> Frequency<TSource, TBucket>(this IEnumerable<TSource> source, IEnumerable<TBucket> buckets, Func<TSource, IEnumerable<TBucket>, TBucket> bucketSelector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(buckets, "buckets");
      Helper.InvalidateNullParam(bucketSelector, "bucketSelector");

      IEnumerable<KeyValuePair<TBucket, int>> rawFreq = source.GroupBy(I => bucketSelector(I, buckets)).Select(K => new KeyValuePair<TBucket, int>(K.Key, K.Count()));
      IEnumerable<KeyValuePair<TBucket, int>> freq = from bucket in buckets
                                                     join freqItem in rawFreq on bucket equals freqItem.Key into freqBucketList
                                                     from item in freqBucketList.DefaultIfEmpty(new KeyValuePair<TBucket, int>(bucket, 0))
                                                     select item;
      return freq;
    }

    /// <summary>
    /// Calculates the frequency of each element based on a custom transformation.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the evaluated sequences.</typeparam>
    /// <typeparam name="TBucket">The type of the bucket elements.</typeparam>
    /// <param name="source">Source sequence.</param>
    /// <param name="bucketSelector">A function that take a source element and returns the bucket where the source element fits.</param>
    /// <returns>A sequence of Key-Value pairs with an entry for each bucket element and its frequency.</returns>
    /// <exception cref="System.ArgumentNullException">source or bucketSelector is null.</exception>
    public static IEnumerable<KeyValuePair<TBucket, int>> Frequency<TSource, TBucket>(this IEnumerable<TSource> source, Func<TSource, TBucket> bucketSelector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(bucketSelector, "bucketSelector");

      return source.GroupBy(I => bucketSelector(I)).Select(K => new KeyValuePair<TBucket, int>(K.Key, K.Count()));
    }

    /// <summary>
    /// Calculates the probability of randomly matching an item in a sequence. 
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the evaluated sequences.</typeparam>
    /// <param name="source">Source sequence.</param>
    /// <param name="item">The item to match.</param>
    /// <returns>A double-precision floating-point value between 0 and 1.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static double Probability<TSource>(this IEnumerable<TSource> source, TSource item)
    {
      Helper.InvalidateNullParam(source, "source");

      double items = source.Count();
      double matches = source.Where(I => item.Equals(I)).Count();

      return matches / items;
    }


    /// <summary>
    /// Calculates the probability of randomly matching an item in a sequence using a custom comparer. 
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the evaluated sequences.</typeparam>
    /// <param name="source">Source sequence.</param>
    /// <param name="item">The item to match.</param>
    /// <param name="comparer">An IEqualityComparer instance to use when matching elements.</param>
    /// <returns>A double-precision floating-point value between 0 and 1.</returns>    
    /// <exception cref="System.ArgumentNullException">source or comparer is null.</exception>
    public static double Probability<TSource>(this IEnumerable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(comparer, "comparer");

      double items = source.Count();
      double matches = source.Where(I => comparer.Equals(item, I)).Count();

      return matches / items;
    }

    #endregion

    #region Moving Sum

    /// <summary>
    /// Computes the moving sum of a sequence of System.Double values.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the moving sum on.</param>
    /// <param name="blockSize">The number of elements in the moving sum block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double> MovingSum(this IEnumerable<double> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      double sum = 0;
      int block = blockSize;
      int nans = -1;
      double value;

      using (IEnumerator<double> left = source.GetEnumerator())
      using (IEnumerator<double> right = source.GetEnumerator())
      {
        //Add the first set of blockSize elements
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            // if any of the elements in the block were nan, yield a nan, otherwise yield the sum.
            if (nans > 0)
              yield return double.NaN;
            else
              yield return sum;
            yield break;
          }
          value = right.Current;
          if (double.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
        }

        //Add a value from the left and subtract from the right.
        //If a nan is encountered, set the nans to the block size and yield nanas until it clears.
        while (right.MoveNext())
        {
          value = right.Current;
          if (double.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
          if (nans > 0)
            yield return double.NaN;
          else
            yield return sum;

          left.MoveNext();
          value = left.Current;
          if (!double.IsNaN(value))
            sum -= value;
        }
      }
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Double&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double?> MovingSum(this IEnumerable<double?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      double sum = 0;
      int block = blockSize;
      int nans = -1;
      double value;

      using (IEnumerator<double?> left = source.GetEnumerator())
      using (IEnumerator<double?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (nans > 0)
              yield return double.NaN;
            else
              yield return sum;
            yield break;
          }
          value = right.Current.GetValueOrDefault();
          if (double.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
        }

        while (right.MoveNext())
        {
          value = right.Current.GetValueOrDefault();
          if (double.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
          if (nans > 0)
            yield return double.NaN;
          else
            yield return sum;

          left.MoveNext();
          value = left.Current.GetValueOrDefault();
          if (!double.IsNaN(value))
            sum -= value;
        }
      }
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of System.Singel values.
    /// </summary>
    /// <param name="source">A sequence of System.Singel values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Singel values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<float> MovingSum(this IEnumerable<float> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      float sum = 0;
      int block = blockSize;
      int nans = -1;
      float value;

      using (IEnumerator<float> left = source.GetEnumerator())
      using (IEnumerator<float> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (nans > 0)
              yield return float.NaN;
            else
              yield return sum;
            yield break;
          }
          value = right.Current;
          if (float.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
        }

        while (right.MoveNext())
        {
          value = right.Current;
          if (float.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
          if (nans > 0)
            yield return float.NaN;
          else
            yield return sum;

          left.MoveNext();
          value = left.Current;
          if (!float.IsNaN(value))
            sum -= value;
        }
      }
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Singel&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Singel&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Singel&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<float?> MovingSum(this IEnumerable<float?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      float sum = 0;
      int block = blockSize;
      int nans = -1;
      float value;

      using (IEnumerator<float?> left = source.GetEnumerator())
      using (IEnumerator<float?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (nans > 0)
              yield return float.NaN;
            else
              yield return sum;
            yield break;
          }
          value = right.Current.GetValueOrDefault();
          if (float.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
        }

        while (right.MoveNext())
        {
          value = right.Current.GetValueOrDefault();
          if (float.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
          if (nans > 0)
            yield return float.NaN;
          else
            yield return sum;

          left.MoveNext();
          value = left.Current.GetValueOrDefault();
          if (!float.IsNaN(value))
            sum -= value;
        }
      }
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of System.Decimal values.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Decimal values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<decimal> MovingSum(this IEnumerable<decimal> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      decimal sum = 0;
      int block = blockSize;
      using (IEnumerator<decimal> left = source.GetEnumerator())
      using (IEnumerator<decimal> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            yield return sum;
            yield break;
          }
          sum += right.Current;
        }

        while (right.MoveNext())
        {
          sum += right.Current;
          yield return sum;
          left.MoveNext();
          sum -= left.Current;
        }
      }
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Decimal&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Decimal&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<decimal?> MovingSum(this IEnumerable<decimal?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      decimal sum = 0;
      int block = blockSize;
      using (IEnumerator<decimal?> left = source.GetEnumerator())
      using (IEnumerator<decimal?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            yield return sum;
            yield break;
          }
          sum += right.Current.GetValueOrDefault();
        }

        while (right.MoveNext())
        {
          sum += right.Current.GetValueOrDefault();
          yield return sum;
          left.MoveNext();
          sum -= left.Current.GetValueOrDefault();
        }
      }
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of System.Int64 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<long> MovingSum(this IEnumerable<long> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      long sum = 0;
      int block = blockSize;
      using (IEnumerator<long> left = source.GetEnumerator())
      using (IEnumerator<long> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            yield return sum;
            yield break;
          }
          sum += right.Current;
        }

        while (right.MoveNext())
        {
          sum += right.Current;
          yield return sum;
          left.MoveNext();
          sum -= left.Current;
        }
      }
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Int64&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<long?> MovingSum(this IEnumerable<long?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      long sum = 0;
      int block = blockSize;
      using (IEnumerator<long?> left = source.GetEnumerator())
      using (IEnumerator<long?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            yield return sum;
            yield break;
          }
          sum += right.Current.GetValueOrDefault();
        }

        while (right.MoveNext())
        {
          sum += right.Current.GetValueOrDefault();
          yield return sum;
          left.MoveNext();
          sum -= left.Current.GetValueOrDefault();
        }
      }
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of System.Int32 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<int> MovingSum(this IEnumerable<int> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int sum = 0;
      int block = blockSize;
      using (IEnumerator<int> left = source.GetEnumerator())
      using (IEnumerator<int> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            yield return sum;
            yield break;
          }
          sum += right.Current;
        }

        while (right.MoveNext())
        {
          sum += right.Current;
          yield return sum;
          left.MoveNext();
          sum -= left.Current;
        }
      }
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Int32&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<int?> MovingSum(this IEnumerable<int?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int sum = 0;
      int block = blockSize;
      using (IEnumerator<int?> left = source.GetEnumerator())
      using (IEnumerator<int?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            yield return sum;
            yield break;
          }
          sum += right.Current.GetValueOrDefault();
        }

        while (right.MoveNext())
        {
          sum += right.Current.GetValueOrDefault();
          yield return sum;
          left.MoveNext();
          sum -= left.Current.GetValueOrDefault();
        }
      }
    }


    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Double values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double?> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Single values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Single values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<float> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Single&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<float?> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Decimal values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Decimal values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<decimal> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Decimal&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<decimal?> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Int64 values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<long> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<long?> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Int32 values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<int> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving variance in a sequence of a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the moving variance in a sequence on.</param>
    /// <param name="blockSize">The number of elements in the moving variance in a sequence block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<int?> MovingSum<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return MovingSum(source.Select(S => selector(S)), blockSize);
    }

    #endregion

    #region Standard Moving Average

    /// <summary>
    /// Computes the moving average of a sequence of System.Double values.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double> StandardMovingAverage(this IEnumerable<double> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      double sum = 0;
      int block = blockSize;
      int nans = -1;
      double value;

      using (IEnumerator<double> left = source.GetEnumerator())
      using (IEnumerator<double> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (nans > 0)
              yield return double.NaN;
            else
              yield return sum / (blockSize - block - 1);
            yield break;
          }
          value = right.Current;
          if (double.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
        }

        while (right.MoveNext())
        {
          value = right.Current;
          if (double.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
          if (nans > 0)
            yield return double.NaN;
          else
            yield return sum / blockSize;

          left.MoveNext();
          value = left.Current;
          if (!double.IsNaN(value))
            sum -= value;
        }
      }
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Double&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double?> StandardMovingAverage(this IEnumerable<double?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int elements = 0;
      int nans = -1;
      double sum = 0;
      double? curr;
      using (IEnumerator<double?> left = source.GetEnumerator())
      using (IEnumerator<double?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (nans > 0)
              yield return double.NaN;
            else if (elements > 0)
              yield return (double)sum / elements;
            else
              yield return null;
            yield break;
          }
          curr = right.Current;
          if (curr.HasValue)
          {
            if (double.IsNaN(curr.Value))
              nans = blockSize;
            else
            {
              sum += curr.Value;
              nans--;
            }
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          if (curr.HasValue)
          {
            if (double.IsNaN(curr.Value))
              nans = blockSize;
            else
            {
              sum += curr.Value;
              nans--;
            }
            elements++;
          }

          if (nans > 0)
            yield return double.NaN;
          else if (elements > 0)
            yield return (double)sum / elements;
          else
            yield return null;
          left.MoveNext();
          curr = left.Current;
          if (curr.HasValue)
          {
            if (!double.IsNaN(curr.Value))
              sum -= curr.Value;
            elements--;
          }
        }
      }
    }

    /// <summary>
    /// Computes the moving average of a sequence of System.Singel values.
    /// </summary>
    /// <param name="source">A sequence of System.Singel values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Singel values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<float> StandardMovingAverage(this IEnumerable<float> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      float sum = 0;
      int block = blockSize;
      int nans = -1;
      float value;

      using (IEnumerator<float> left = source.GetEnumerator())
      using (IEnumerator<float> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (nans > 0)
              yield return float.NaN;
            else
              yield return sum / (blockSize - block - 1);
            yield break;
          }
          value = right.Current;
          if (float.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
        }

        while (right.MoveNext())
        {
          value = right.Current;
          if (float.IsNaN(value))
            nans = blockSize;
          else
          {
            sum += value;
            nans--;
          }
          if (nans > 0)
            yield return float.NaN;
          else
            yield return sum / blockSize;

          left.MoveNext();
          value = left.Current;
          if (!float.IsNaN(value))
            sum -= value;
        }
      }
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Singel&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Singel&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Singel&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<float?> StandardMovingAverage(this IEnumerable<float?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int elements = 0;
      int nans = -1;
      float sum = 0;
      float? curr;
      using (IEnumerator<float?> left = source.GetEnumerator())
      using (IEnumerator<float?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (nans > 0)
              yield return float.NaN;
            else if (elements > 0)
              yield return (float)sum / elements;
            else
              yield return null;
            yield break;
          }
          curr = right.Current;
          if (curr.HasValue)
          {
            if (float.IsNaN(curr.Value))
              nans = blockSize;
            else
            {
              sum += curr.Value;
              nans--;
            }
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          if (curr.HasValue)
          {
            if (float.IsNaN(curr.Value))
              nans = blockSize;
            else
            {
              sum += curr.Value;
              nans--;
            }
            elements++;
          }

          if (nans > 0)
            yield return float.NaN;
          else if (elements > 0)
            yield return (float)sum / elements;
          else
            yield return null;
          left.MoveNext();
          curr = left.Current;
          if (curr.HasValue)
          {
            if (!float.IsNaN(curr.Value))
              sum -= curr.Value;
            elements--;
          }
        }
      }
    }

    /// <summary>
    /// Computes the moving average of a sequence of System.Decimal values.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Decimal values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<decimal> StandardMovingAverage(this IEnumerable<decimal> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      decimal sum = 0;
      using (IEnumerator<decimal> left = source.GetEnumerator())
      using (IEnumerator<decimal> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            yield return sum / (blockSize - block - 1);
            yield break;
          }
          sum += right.Current;
        }

        while (right.MoveNext())
        {
          sum += right.Current;
          yield return sum / blockSize;
          left.MoveNext();
          sum -= left.Current;
        }
      }
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Decimal&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Decimal&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<decimal?> StandardMovingAverage(this IEnumerable<decimal?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int elements = 0;
      decimal sum = 0;
      decimal? curr;
      using (IEnumerator<decimal?> left = source.GetEnumerator())
      using (IEnumerator<decimal?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements > 0)
              yield return sum / elements;
            else
              yield return null;
            yield break;
          }
          curr = right.Current;
          if (curr.HasValue)
          {
            sum += curr.Value;
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          if (curr.HasValue)
          {
            sum += curr.Value;
            elements++;
          }

          if (elements > 0)
            yield return sum / elements;
          else
            yield return null;
          left.MoveNext();
          curr = left.Current;
          if (curr.HasValue)
          {
            sum -= curr.Value;
            elements--;
          }
        }
      }
    }

    /// <summary>
    /// Computes the moving average of a sequence of System.Int64 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double> StandardMovingAverage(this IEnumerable<long> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      long sum = 0;
      using (IEnumerator<long> left = source.GetEnumerator())
      using (IEnumerator<long> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            yield return (double)sum / (blockSize - block - 1);
            yield break;
          }
          sum += right.Current;
        }

        while (right.MoveNext())
        {
          sum += right.Current;
          yield return (double)sum / blockSize;
          left.MoveNext();
          sum -= left.Current;
        }
      }
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Int64&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double?> StandardMovingAverage(this IEnumerable<long?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int elements = 0;
      long sum = 0;
      long? curr;
      using (IEnumerator<long?> left = source.GetEnumerator())
      using (IEnumerator<long?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements > 0)
              yield return (double)sum / elements;
            else
              yield return null;
            yield break;
          }
          curr = right.Current;
          if (curr.HasValue)
          {
            sum += curr.Value;
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          if (curr.HasValue)
          {
            sum += curr.Value;
            elements++;
          }

          if (elements > 0)
            yield return (double)sum / elements;
          else
            yield return null;
          left.MoveNext();
          curr = left.Current;
          if (curr.HasValue)
          {
            sum -= curr.Value;
            elements--;
          }
        }
      }
    }

    /// <summary>
    /// Computes the moving average of a sequence of System.Int32 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double> StandardMovingAverage(this IEnumerable<int> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int sum = 0;
      using (IEnumerator<int> left = source.GetEnumerator())
      using (IEnumerator<int> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            yield return (double)sum / (blockSize - block - 1);
            yield break;
          }
          sum += right.Current;
        }

        while (right.MoveNext())
        {
          sum += right.Current;
          yield return (double)sum / blockSize;
          left.MoveNext();
          sum -= left.Current;
        }
      }
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Int32&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double?> StandardMovingAverage(this IEnumerable<int?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int elements = 0;
      int sum = 0;
      int? curr;
      using (IEnumerator<int?> left = source.GetEnumerator())
      using (IEnumerator<int?> right = source.GetEnumerator())
      {
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements > 0)
              yield return (double)sum / elements;
            else
              yield return null;
            yield break;
          }
          curr = right.Current;
          if (curr.HasValue)
          {
            sum += curr.Value;
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          if (curr.HasValue)
          {
            sum += curr.Value;
            elements++;
          }

          if (elements > 0)
            yield return (double)sum / elements;
          else
            yield return null;
          left.MoveNext();
          curr = left.Current;
          if (curr.HasValue)
          {
            sum -= curr.Value;
            elements--;
          }
        }
      }
    }



    /// <summary>
    /// Computes the moving average of a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Double values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double?> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving average of a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Single values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Single values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<float> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Single&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<float?> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving average of a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Decimal values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Decimal values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<decimal> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Decimal&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<decimal?> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving average of a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Int64 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double?> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving average of a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Int32 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the moving average of a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double?> StandardMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return StandardMovingAverage(source.Select(S => selector(S)), blockSize);
    }

    #endregion

    #region Cumulative Moving Average

    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Double values.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the moving average on.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    public static IEnumerable<double> CumulativeMovingAverage(this IEnumerable<double> source)
    {
      Helper.InvalidateNullParam(source, "source");

      double sum = 0;
      int idx = 0;

      foreach (double item in source)
        yield return (sum += item) / ++idx;
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Double&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the moving average on.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<double?> CumulativeMovingAverage(this IEnumerable<double?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      double sum = 0;
      int idx = 0;

      foreach (double? item in source)
        if (item.HasValue)
        {
          idx++;
          sum += item.Value;
          if (idx > 0)
            yield return sum / idx;
        }
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Singel values.
    /// </summary>
    /// <param name="source">A sequence of System.Singel values to calculate the moving average on.</param>
    /// <returns>A sequence of System.Singel values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<float> CumulativeMovingAverage(this IEnumerable<float> source)
    {
      Helper.InvalidateNullParam(source, "source");

      float sum = 0;
      int idx = 0;

      foreach (float item in source)
        yield return (sum += item) / ++idx;
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Singel&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Singel&gt; values to calculate the moving average on.</param>
    /// <returns>A sequence of Nullable&lt;System.Singel&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<float?> CumulativeMovingAverage(this IEnumerable<float?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      float sum = 0;
      int idx = 0;

      foreach (float? item in source)
        if (item.HasValue)
        {
          idx++;
          sum += item.Value;
          if (idx > 0)
            yield return sum / idx;
        }
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Decimal values.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the moving average on.</param>    
    /// <returns>A sequence of System.Decimal values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<decimal> CumulativeMovingAverage(this IEnumerable<decimal> source)
    {
      Helper.InvalidateNullParam(source, "source");

      decimal sum = 0;
      int idx = 0;

      foreach (decimal item in source)
        yield return (sum += item) / ++idx;
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Decimal&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the moving average on.</param>
    /// <returns>A sequence of Nullable&lt;System.Decimal&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<decimal?> CumulativeMovingAverage(this IEnumerable<decimal?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      decimal sum = 0;
      int idx = 0;

      foreach (decimal? item in source)
        if (item.HasValue)
        {
          idx++;
          sum += item.Value;
          if (idx > 0)
            yield return sum / idx;
        }
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Int64 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the moving average on.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<double> CumulativeMovingAverage(this IEnumerable<long> source)
    {
      Helper.InvalidateNullParam(source, "source");

      double sum = 0;
      int idx = 0;

      foreach (long item in source)
        yield return (sum += item) / ++idx;
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Int64&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the moving average on.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<double?> CumulativeMovingAverage(this IEnumerable<long?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      double sum = 0;
      int idx = 0;

      foreach (long? item in source)
        if (item.HasValue)
        {
          idx++;
          sum += item.Value;
          if (idx > 0)
            yield return sum / idx;
        }
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Int32 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the moving average on.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<double> CumulativeMovingAverage(this IEnumerable<int> source)
    {
      Helper.InvalidateNullParam(source, "source");

      double sum = 0;
      int idx = 0;

      foreach (int item in source)
        yield return (sum += item) / ++idx;
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Int32&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the moving average on.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<double?> CumulativeMovingAverage(this IEnumerable<int?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      double sum = 0;
      int idx = 0;

      foreach (int? item in source)
        if (item.HasValue)
        {
          idx++;
          sum += item.Value;
          if (idx > 0)
            yield return sum / idx;
        }
    }


    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Double values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<double> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<double?> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Single values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Single values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<float> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Single&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<float?> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Decimal values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Decimal values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<decimal> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Decimal&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<decimal?> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Int64 values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<double> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<double?> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of System.Int32 values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<double> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative moving average of a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the moving average on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    public static IEnumerable<double?> CumulativeMovingAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeMovingAverage(source.Select(S => selector(S)));
    }

    #endregion

    #region  Weighted Moving Average

    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Double values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>    
    public static IEnumerable<double> WeightedMovingAverage(this IEnumerable<double> source, int blockSize, Func<int, double> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<double> current = new Queue<double>();
      double[] factor = new double[blockSize];

      using (IEnumerator<double> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        double factorSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          yield return current.Multiply(factor).Sum() / factorSum;
          current.Dequeue();
        }
      }
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Double&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double?> WeightedMovingAverage(this IEnumerable<double?> source, int blockSize, Func<int, double> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<double?> current = new Queue<double?>();
      double?[] factor = new double?[blockSize];
      IEnumerable<double?> map;
      double? factorFullSum;
      double? tempSum;

      using (IEnumerator<double?> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        factorFullSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          if (current.All(V => !V.HasValue))
            tempSum = null;
          else if (current.Any(V => !V.HasValue))
          {
            map = current.Divide(current); // Convert to ones / nulls
            tempSum = map.Multiply(factor).Sum();
          }
          else
            tempSum = factorFullSum;

          yield return current.Multiply(factor).Sum() / tempSum;
          current.Dequeue();
        }
      }
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Single values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of System.Single values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<float> WeightedMovingAverage(this IEnumerable<float> source, int blockSize, Func<int, float> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<float> current = new Queue<float>();
      float[] factor = new float[blockSize];

      using (IEnumerator<float> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        float factorSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          yield return current.Multiply(factor).Sum() / factorSum;
          current.Dequeue();
        }
      }
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Single&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of Nullable&lt;System.Single&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<float?> WeightedMovingAverage(this IEnumerable<float?> source, int blockSize, Func<int, float> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<float?> current = new Queue<float?>();
      float?[] factor = new float?[blockSize];
      IEnumerable<float?> map;
      float? factorFullSum;
      float? tempSum;

      using (IEnumerator<float?> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        factorFullSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          if (current.All(V => !V.HasValue))
            tempSum = null;
          else if (current.Any(V => !V.HasValue))
          {
            map = current.Divide(current); // Convert to ones / nulls
            tempSum = map.Multiply(factor).Sum();
          }
          else
            tempSum = factorFullSum;

          yield return current.Multiply(factor).Sum() / tempSum;
          current.Dequeue();
        }
      }
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Decimal values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of System.Decimal values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<decimal> WeightedMovingAverage(this IEnumerable<decimal> source, int blockSize, Func<int, decimal> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<decimal> current = new Queue<decimal>();
      decimal[] factor = new decimal[blockSize];

      using (IEnumerator<decimal> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        decimal factorSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          yield return current.Multiply(factor).Sum() / factorSum;
          current.Dequeue();
        }
      }
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Decimal&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of Nullable&lt;System.Decimal&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<decimal?> WeightedMovingAverage(this IEnumerable<decimal?> source, int blockSize, Func<int, decimal> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<decimal?> current = new Queue<decimal?>();
      decimal?[] factor = new decimal?[blockSize];
      IEnumerable<decimal?> map;
      decimal? factorFullSum;
      decimal? tempSum;

      using (IEnumerator<decimal?> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        factorFullSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          if (current.All(V => !V.HasValue))
            tempSum = null;
          else if (current.Any(V => !V.HasValue))
          {
            map = current.Divide(current); // Convert to ones / nulls
            tempSum = map.Multiply(factor).Sum();
          }
          else
            tempSum = factorFullSum;

          yield return current.Multiply(factor).Sum() / tempSum;
          current.Dequeue();
        }
      }
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Int64 values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double> WeightedMovingAverage(this IEnumerable<long> source, int blockSize, Func<int, double> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<double> current = new Queue<double>();
      double[] factor = new double[blockSize];

      using (IEnumerator<long> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        double factorSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          yield return current.Multiply(factor).Sum() / factorSum;
          current.Dequeue();
        }
      }
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Int64&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double?> WeightedMovingAverage(this IEnumerable<long?> source, int blockSize, Func<int, double> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<double?> current = new Queue<double?>();
      double?[] factor = new double?[blockSize];
      IEnumerable<double?> map;
      double? factorFullSum;
      double? tempSum;

      using (IEnumerator<long?> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        factorFullSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          if (current.All(V => !V.HasValue))
            tempSum = null;
          else if (current.Any(V => !V.HasValue))
          {
            map = current.Divide(current); // Convert to ones / nulls
            tempSum = map.Multiply(factor).Sum();
          }
          else
            tempSum = factorFullSum;

          yield return current.Multiply(factor).Sum() / tempSum;
          current.Dequeue();
        }
      }
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Int32 values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double> WeightedMovingAverage(this IEnumerable<int> source, int blockSize, Func<int, double> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<double> current = new Queue<double>();
      double[] factor = new double[blockSize];

      using (IEnumerator<int> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        double factorSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          yield return current.Multiply(factor).Sum() / factorSum;
          current.Dequeue();
        }
      }
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Int32&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double?> WeightedMovingAverage(this IEnumerable<int?> source, int blockSize, Func<int, double> weight)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(weight, "weight");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      Queue<double?> current = new Queue<double?>();
      double?[] factor = new double?[blockSize];
      IEnumerable<double?> map;
      double? factorFullSum;
      double? tempSum;

      using (IEnumerator<int?> se = source.GetEnumerator())
      {
        for (int i = 0; i < blockSize - 1; i++)
        {
          if (!se.MoveNext())
            throw new ArgumentException(Resources.excptionValueMinBlockSize, "source");
          current.Enqueue(se.Current);
          factor[i] = weight(i + 1);
        }
        factor[blockSize - 1] = weight(blockSize);
        factorFullSum = factor.Sum();

        while (se.MoveNext())
        {
          current.Enqueue(se.Current);
          if (current.All(V => !V.HasValue))
            tempSum = null;
          else if (current.Any(V => !V.HasValue))
          {
            map = current.Divide(current); // Convert to ones / nulls
            tempSum = map.Multiply(factor).Sum();
          }
          else
            tempSum = factorFullSum;

          yield return current.Multiply(factor).Sum() / tempSum;
          current.Dequeue();
        }
      }
    }


    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Double values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<double> WeightedMovingAverage(this IEnumerable<double> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Double&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<double?> WeightedMovingAverage(this IEnumerable<double?> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Single values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of System.Single values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<float> WeightedMovingAverage(this IEnumerable<float> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Single&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of Nullable&lt;System.Single&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<float?> WeightedMovingAverage(this IEnumerable<float?> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Decimal values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of System.Decimal values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<decimal> WeightedMovingAverage(this IEnumerable<decimal> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Decimal&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of Nullable&lt;System.Decimal&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<decimal?> WeightedMovingAverage(this IEnumerable<decimal?> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Int64 values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<double> WeightedMovingAverage(this IEnumerable<long> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Int64&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<double?> WeightedMovingAverage(this IEnumerable<long?> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of System.Int32 values using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<double> WeightedMovingAverage(this IEnumerable<int> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Int32&gt; values using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>    
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    /// <remarks>Elements position within the block is used as weight. In a block of for items, the first (oldest) element will have the 1/4 of the weight of the last element.</remarks>
    public static IEnumerable<double?> WeightedMovingAverage(this IEnumerable<int?> source, int blockSize)
    {
      return WeightedMovingAverage(source, blockSize, X => X);
    }


    /// <summary> 
    /// Computes the weighted moving average of a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, double> weight, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>    
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double?> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, double> weight, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    /// <summary> 
    /// Computes the weighted moving average of a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Single values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<float> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, float> weight, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Single&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>    
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<float?> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, float> weight, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    /// <summary> 
    /// Computes the weighted moving average of a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Decimal values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<decimal> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, decimal> weight, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Decimal&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>    
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<decimal?> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, decimal> weight, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    /// <summary> 
    /// Computes the weighted moving average of a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, double> weight, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>    
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double?> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, double> weight, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    /// <summary> 
    /// Computes the weighted moving average of a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items. 
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of System.Double values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, double> weight, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    /// <summary>
    /// Computes the weighted moving average of a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence using lower weight for 'older' items.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the moving average on.</param>
    /// <param name="blockSize">The number of elements in the moving average block. Block size must be two or larger.</param>
    /// <param name="weight">A function that compute the weight of an element based on its index in the block.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of Nullable&lt;System.Double&gt; values.</returns>
    /// <exception cref="System.ArgumentNullException">source or weight or selector is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>    
    /// <exception cref="System.ArgumentException">source must be larger than block size.</exception>
    public static IEnumerable<double?> WeightedMovingAverage<TSource>(this IEnumerable<TSource> source, int blockSize, Func<int, double> weight, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return source.Select(S => selector(S)).WeightedMovingAverage(blockSize, weight);
    }

    #endregion

    #region Variance

    /// <summary>
    /// Computes the variance in a sequence of System.Double values.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Variance(this IEnumerable<double> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      double sum = 0;
      double sumSqr = 0;

      using (IEnumerator<double> src = source.GetEnumerator())
        while (src.MoveNext())
        {
          double curr = src.Current;
          i++;
          sum += curr;
          sumSqr += curr * curr;
        }
      return (sumSqr - sum * (sum / i)) / (i - 1);
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Double&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Variance(this IEnumerable<double?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      double sum = 0;
      double sumSqr = 0;

      using (IEnumerator<double?> src = source.GetEnumerator())
        while (src.MoveNext())
        {
          if (src.Current.HasValue)
          {
            double curr = src.Current.Value;
            i++;
            sum += curr;
            sumSqr += curr * curr;
          }
        }
      return (sumSqr - sum * (sum / i)) / (i - 1);
    }

    /// <summary>
    /// Computes the variance in a sequence of System.Single values.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static float Variance(this IEnumerable<float> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      float sum = 0;
      float sumSqr = 0;

      using (IEnumerator<float> src = source.GetEnumerator())
        while (src.MoveNext())
        {
          float curr = src.Current;
          i++;
          sum += curr;
          sumSqr += curr * curr;
        }
      return (sumSqr - sum * (sum / i)) / (i - 1);
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Single&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static float? Variance(this IEnumerable<float?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      float sum = 0;
      float sumSqr = 0;

      using (IEnumerator<float?> src = source.GetEnumerator())
        while (src.MoveNext())
        {
          if (src.Current.HasValue)
          {
            float curr = src.Current.Value;
            i++;
            sum += curr;
            sumSqr += curr * curr;
          }
        }
      return (sumSqr - sum * (sum / i)) / (i - 1);
    }

    /// <summary>
    /// Computes the variance in a sequence of System.Decimal values.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static decimal Variance(this IEnumerable<decimal> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      decimal sum = 0;
      decimal sumSqr = 0;

      using (IEnumerator<decimal> src = source.GetEnumerator())
        while (src.MoveNext())
        {
          decimal curr = src.Current;
          i++;
          sum += curr;
          sumSqr += curr * curr;
        }
      return (sumSqr - sum * (sum / i)) / (i - 1);
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Decimal&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static decimal? Variance(this IEnumerable<decimal?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      decimal sum = 0;
      decimal sumSqr = 0;

      using (IEnumerator<decimal?> src = source.GetEnumerator())
        while (src.MoveNext())
          if (src.Current.HasValue)
          {
            decimal curr = src.Current.Value;
            i++;
            sum += curr;
            sumSqr += curr * curr;
          }

      return (sumSqr - sum * (sum / i)) / (i - 1);
    }

    /// <summary>
    /// Computes the variance in a sequence of System.Int64 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Variance(this IEnumerable<long> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      double sum = 0;
      double sumSqr = 0;

      using (IEnumerator<long> src = source.GetEnumerator())
        while (src.MoveNext())
        {
          double curr = src.Current;
          i++;
          sum += curr;
          sumSqr += curr * curr;
        }
      return (double)(sumSqr - sum * (sum / i)) / (i - 1);
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Int64&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Variance(this IEnumerable<long?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      double sum = 0;
      double sumSqr = 0;

      using (IEnumerator<long?> src = source.GetEnumerator())
        while (src.MoveNext())
          if (src.Current.HasValue)
          {
            long curr = src.Current.Value;
            i++;
            sum += curr;
            sumSqr += curr * curr;
          }

      return (sumSqr - sum * (sum / i)) / (i - 1);
    }

    /// <summary>
    /// Computes the variance in a sequence of System.Int32 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Variance(this IEnumerable<int> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      double sum = 0;
      double sumSqr = 0;

      using (IEnumerator<int> src = source.GetEnumerator())
        while (src.MoveNext())
        {
          double curr = src.Current;
          i++;
          sum += curr;
          sumSqr += curr * curr;
        }
      return (double)(sumSqr - sum * (sum / i)) / (i - 1);
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Int32&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Variance(this IEnumerable<int?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      int i = 0;
      double sum = 0;
      double sumSqr = 0;

      using (IEnumerator<int?> src = source.GetEnumerator())
        while (src.MoveNext())
        {
          if (src.Current.HasValue)
          {
            double curr = src.Current.Value;
            i++;
            sum += curr;
            sumSqr += curr * curr;
          }
        }

      return (sumSqr - sum * (sum / i)) / (i - 1);
    }



    /// <summary>
    /// Computes the variance in a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the variance in a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static float Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static float? Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the variance in a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static decimal Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static decimal? Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the variance in a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the variance in a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the variance in a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the variance of.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)));
    }

    #endregion

    #region Variance (Cumulative)

    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Double values.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeVariance(this IEnumerable<double> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<double> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i = 1;
          double sum = src.Current;
          double sumSqr = sum * sum;
          while (src.MoveNext())
          {
            double curr = src.Current;
            i++;
            sum += curr;
            sumSqr += curr * curr;
            yield return (sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Double&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeVariance(this IEnumerable<double?> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<double?> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i;
          double sum = src.Current.GetValueOrDefault();
          double sumSqr = sum * sum;
          i = src.Current.HasValue ? 1 : 0;
          while (src.MoveNext())
          {
            if (src.Current.HasValue)
            {
              double curr = src.Current.Value;
              i++;
              sum += curr;
              sumSqr += curr * curr;
            }
            yield return (sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Single values.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float> CumulativeVariance(this IEnumerable<float> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<float> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i = 1;
          float sum = src.Current;
          float sumSqr = sum * sum;
          while (src.MoveNext())
          {
            float curr = src.Current;
            i++;
            sum += curr;
            sumSqr += curr * curr;
            yield return (sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Single&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float?> CumulativeVariance(this IEnumerable<float?> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<float?> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i;
          float sum = src.Current.GetValueOrDefault();
          float sumSqr = sum * sum;
          i = src.Current.HasValue ? 1 : 0;
          while (src.MoveNext())
          {
            if (src.Current.HasValue)
            {
              float curr = src.Current.Value;
              i++;
              sum += curr;
              sumSqr += curr * curr;
            }
            yield return (sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Decimal values.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal> CumulativeVariance(this IEnumerable<decimal> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<decimal> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i = 1;
          decimal sum = src.Current;
          decimal sumSqr = sum * sum;
          while (src.MoveNext())
          {
            decimal curr = src.Current;
            i++;
            sum += curr;
            sumSqr += curr * curr;
            yield return (sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Decimal&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal?> CumulativeVariance(this IEnumerable<decimal?> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<decimal?> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i;
          decimal sum = src.Current.GetValueOrDefault();
          decimal sumSqr = sum * sum;
          i = src.Current.HasValue ? 1 : 0;
          while (src.MoveNext())
          {

            if (src.Current.HasValue)
            {
              decimal curr = src.Current.Value;
              i++;
              sum += curr;
              sumSqr += curr * curr;
            }
            if (i <= 1)
              throw new DivideByZeroException();
            else
              yield return (sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Int64 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeVariance(this IEnumerable<long> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<long> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i = 1;
          double sum = src.Current;
          double sumSqr = sum * sum;
          while (src.MoveNext())
          {
            double curr = src.Current;
            i++;
            sum += curr;
            sumSqr += curr * curr;
            yield return (double)(sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Int64&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeVariance(this IEnumerable<long?> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<long?> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i;
          double sum = src.Current.GetValueOrDefault();
          double sumSqr = sum * sum;
          i = src.Current.HasValue ? 1 : 0;
          while (src.MoveNext())
          {
            if (src.Current.HasValue)
            {
              long curr = src.Current.Value;
              i++;
              sum += curr;
              sumSqr += curr * curr;
            }
            if (i <= 1)
              throw new DivideByZeroException();
            else
              yield return (sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Int32 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeVariance(this IEnumerable<int> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<int> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i = 1;
          double sum = src.Current;
          double sumSqr = sum * sum;
          while (src.MoveNext())
          {
            double curr = src.Current;
            i++;
            sum += curr;
            sumSqr += curr * curr;
            yield return (double)(sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Int32&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeVariance(this IEnumerable<int?> source)
    {
      Helper.InvalidateNullParam(source, "source");
      if (source.Where(V => V.HasValue).Take(2).Count() < 2)
        throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");

      using (IEnumerator<int?> src = source.GetEnumerator())
        if (src.MoveNext())
        {
          int i;
          double sum = src.Current.GetValueOrDefault();
          double sumSqr = sum * sum;
          i = src.Current.HasValue ? 1 : 0;
          while (src.MoveNext())
          {
            if (src.Current.HasValue)
            {
              double curr = src.Current.Value;
              i++;
              sum += curr;
              sumSqr += curr * curr;
            }
            if (i <= 1)
              throw new DivideByZeroException();
            else
              yield return (sumSqr - sum * (sum / i)) / (i - 1);
          }
        }
    }


    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float?> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal?> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the variance of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative variance in a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the variance of.</param>
    /// <returns>A sequence of values that are the computed variance for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeVariance<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeVariance(source.Select(S => selector(S)));
    }

    #endregion

    #region Variance (Block)

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Double values.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Variance(this IEnumerable<double> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int blockSize2 = blockSize - 1;
      double sum = 0;
      double sumSqr = 0;
      int nans = -1;
      double curr;
      using (IEnumerator<double> left = source.GetEnumerator())
      using (IEnumerator<double> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");
            else if (nans > 0)
              yield return double.NaN;
            else
              yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current;
          if (double.IsNaN(curr))
            nans = blockSize;
          else
          {
            sum += curr;
            sumSqr += curr * curr;
            nans--;
          }
          elements++;
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          if (double.IsNaN(curr))
            nans = blockSize;
          else
          {
            sum += curr;
            sumSqr += curr * curr;
            nans--;
          }
          if (nans > 0)
            yield return double.NaN;
          else
            yield return (sumSqr - sum * (sum / blockSize)) / blockSize2;
          left.MoveNext();
          curr = left.Current;
          if (!double.IsNaN(curr))
          {
            sum -= curr;
            sumSqr -= curr * curr;
          }
        }
      }
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Double&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Variance(this IEnumerable<double?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      double sum = 0;
      double sumSqr = 0;
      int nans = -1;
      double curr;
      using (IEnumerator<double?> left = source.GetEnumerator())
      using (IEnumerator<double?> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwoNotNull  , "source");
            else if (nans > 0)
              yield return double.NaN;
            else
              yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current.GetValueOrDefault();
          nans--;
          if (right.Current.HasValue)
          {
            if (double.IsNaN(curr))
              nans = blockSize;
            else
            {
              sum += curr;
              sumSqr += curr * curr;
            }
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current.GetValueOrDefault();
          nans--;
          if (right.Current.HasValue)
          {
            if (double.IsNaN(curr))
              nans = blockSize;
            else
            {
              sum += curr;
              sumSqr += curr * curr;
            }
            elements++;
          }
          if (elements <= 1)
            yield return null;
          else if (nans > 0)
            yield return double.NaN;
          else
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);

          left.MoveNext();
          curr = left.Current.GetValueOrDefault();
          if (left.Current.HasValue)
          {
            if (!double.IsNaN(curr))
            {
              sum -= curr;
              sumSqr -= curr * curr;
            }
            elements--;
          }
        }
      }
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Single values.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float> Variance(this IEnumerable<float> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");
                                     
      int block = blockSize;
      int blockSize2 = blockSize - 1;
      float sum = 0;
      float sumSqr = 0;
      int nans = -1;
      float curr;
      using (IEnumerator<float> left = source.GetEnumerator())
      using (IEnumerator<float> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");
            else if (nans > 0)
              yield return float.NaN;
            else
              yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current;
          if (float.IsNaN(curr))
            nans = blockSize;
          else
          {
            sum += curr;
            sumSqr += curr * curr;
            nans--;
          }
          elements++;
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          if (float.IsNaN(curr))
            nans = blockSize;
          else
          {
            sum += curr;
            sumSqr += curr * curr;
            nans--;
          }
          if (nans > 0)
            yield return float.NaN;
          else
            yield return (sumSqr - sum * (sum / blockSize)) / blockSize2;
          left.MoveNext();
          curr = left.Current;
          if (!float.IsNaN(curr))
          {
            sum -= curr;
            sumSqr -= curr * curr;
          }
        }
      }
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Single&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float?> Variance(this IEnumerable<float?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      float sum = 0;
      float sumSqr = 0;
      int nans = -1;
      float curr;
      using (IEnumerator<float?> left = source.GetEnumerator())
      using (IEnumerator<float?> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwoNotNull, "source");
            else if (nans > 0)
              yield return float.NaN;
            else
              yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current.GetValueOrDefault();
          nans--;
          if (right.Current.HasValue)
          {
            if (float.IsNaN(curr))
              nans = blockSize;
            else
            {
              sum += curr;
              sumSqr += curr * curr;
            }
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current.GetValueOrDefault();
          nans--;
          if (right.Current.HasValue)
          {
            if (float.IsNaN(curr))
              nans = blockSize;
            else
            {
              sum += curr;
              sumSqr += curr * curr;
            }
            elements++;
          }
          if (elements <= 1)
            yield return null;
          else if (nans > 0)
            yield return float.NaN;
          else
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);

          left.MoveNext();
          curr = left.Current.GetValueOrDefault();
          if (left.Current.HasValue)
          {
            if (!float.IsNaN(curr))
            {
              sum -= curr;
              sumSqr -= curr * curr;
            }
            elements--;
          }
        }
      }
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Decimal values.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal> Variance(this IEnumerable<decimal> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int blockSize2 = blockSize - 1;
      decimal sum = 0;
      decimal sumSqr = 0;
      decimal curr;
      using (IEnumerator<decimal> left = source.GetEnumerator())
      using (IEnumerator<decimal> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current;
          sum += curr;
          sumSqr += curr * curr;
          elements++;
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          sum += curr;
          sumSqr += curr * curr;
          yield return (sumSqr - sum * (sum / blockSize)) / blockSize2;
          left.MoveNext();
          curr = left.Current;
          sum -= curr;
          sumSqr -= curr * curr;
        }
      }
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Decimal&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal?> Variance(this IEnumerable<decimal?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      decimal sum = 0;
      decimal sumSqr = 0;
      int nans = -1;
      decimal curr;
      using (IEnumerator<decimal?> left = source.GetEnumerator())
      using (IEnumerator<decimal?> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwoNotNull, "source");
            else
              yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current.GetValueOrDefault();
          nans--;
          if (right.Current.HasValue)
          {
            sum += curr;
            sumSqr += curr * curr;
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current.GetValueOrDefault();
          nans--;
          if (right.Current.HasValue)
          {
            sum += curr;
            sumSqr += curr * curr;
            elements++;
          }
          if (elements <= 1)
            yield return null;
          else
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);

          left.MoveNext();
          curr = left.Current.GetValueOrDefault();
          if (left.Current.HasValue)
          {
            sum -= curr;
            sumSqr -= curr * curr;
            elements--;
          }
        }
      }
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Int64 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Variance(this IEnumerable<long> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int blockSize2 = blockSize - 1;
      double sum = 0;
      double sumSqr = 0;
      long curr;
      using (IEnumerator<long> left = source.GetEnumerator())
      using (IEnumerator<long> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current;
          sum += curr;
          sumSqr += curr * curr;
          elements++;
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          sum += curr;
          sumSqr += curr * curr;
          yield return (sumSqr - sum * (sum / blockSize)) / blockSize2;
          left.MoveNext();
          curr = left.Current;
          sum -= curr;
          sumSqr -= curr * curr;
        }
      }
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Int64&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Variance(this IEnumerable<long?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      double sum = 0;
      double sumSqr = 0;
      long curr;
      using (IEnumerator<long?> left = source.GetEnumerator())
      using (IEnumerator<long?> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwoNotNull, "source");
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current.GetValueOrDefault();
          if (right.Current.HasValue)
          {
            sum += curr;
            sumSqr += curr * curr;
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current.GetValueOrDefault();
          if (right.Current.HasValue)
          {
            sum += curr;
            sumSqr += curr * curr;
            elements++;
          }

          if (elements <= 1)
            yield return null;
          else
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);

          left.MoveNext();
          curr = left.Current.GetValueOrDefault();
          if (left.Current.HasValue)
          {
            sum -= curr;
            sumSqr -= curr * curr;
            elements--;
          }
        }
      }
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Int32 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Variance(this IEnumerable<int> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      int blockSize2 = blockSize - 1;
      double sum = 0;
      double sumSqr = 0;
      int curr;
      using (IEnumerator<int> left = source.GetEnumerator())
      using (IEnumerator<int> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwo, "source");
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current;
          sum += curr;
          sumSqr += curr * curr;
          elements++;
        }

        while (right.MoveNext())
        {
          curr = right.Current;
          sum += curr;
          sumSqr += curr * curr;
          yield return (sumSqr - sum * (sum / blockSize)) / blockSize2;
          left.MoveNext();
          curr = left.Current;
          sum -= curr;
          sumSqr -= curr * curr;
        }
      }
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Int32&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Variance(this IEnumerable<int?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");
      if (blockSize < 2)
        throw new ArgumentException(Resources.excptionValueMinTwo, "blockSize");

      int block = blockSize;
      double sum = 0;
      double sumSqr = 0;
      int curr;
      using (IEnumerator<int?> left = source.GetEnumerator())
      using (IEnumerator<int?> right = source.GetEnumerator())
      {
        int elements = 0;
        while (block > 1)
        {
          block--;
          if (!right.MoveNext())
          {
            if (elements < 2)
              throw new ArgumentException(Resources.excptionSequenceMinTwoNotNull, "source");
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);
            yield break;
          }
          curr = right.Current.GetValueOrDefault();
          if (right.Current.HasValue)
          {
            sum += curr;
            sumSqr += curr * curr;
            elements++;
          }
        }

        while (right.MoveNext())
        {
          curr = right.Current.GetValueOrDefault();
          if (right.Current.HasValue)
          {
            sum += curr;
            sumSqr += curr * curr;
            elements++;
          }

          if (elements <= 1)
            yield return null;
          else
            yield return (sumSqr - sum * (sum / elements)) / (elements - 1);

          left.MoveNext();
          curr = left.Current.GetValueOrDefault();
          if (left.Current.HasValue)
          {
            sum -= curr;
            sumSqr -= curr * curr;
            elements--;
          }
        }
      }
    }



    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float?> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal?> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the variance of blocks of elements in a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the variance of.</param>
    /// <param name="blockSize">The number of elements in the variance block. Block size must be two or larger.</param>
    /// <returns>The variance in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Variance<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Variance(source.Select(S => selector(S)), blockSize);
    }

    #endregion

    #region Standard Deviation

    /// <summary>
    /// Computes the standard deviation of a sequence of System.Double values.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Stdev(this IEnumerable<double> source)
    {
      Helper.InvalidateNullParam(source, "source");

      return Math.Sqrt(source.Variance());
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Double&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Stdev(this IEnumerable<double?> source)
    {
      double? variance = source.Variance();
      return (double?)Math.Sqrt(variance.Value);
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of System.Single values.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static float Stdev(this IEnumerable<float> source)
    {
      return (float)Math.Sqrt(source.Variance());
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Single&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static float? Stdev(this IEnumerable<float?> source)
    {
      float? variance = source.Variance();
      return (float?)Math.Sqrt(variance.Value);
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of System.Decimal values.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static decimal Stdev(this IEnumerable<decimal> source)
    {
      return (decimal)Math.Sqrt((double)source.Variance());
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Decimal&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static decimal? Stdev(this IEnumerable<decimal?> source)
    {
      decimal? variance = source.Variance();
      return (decimal?)Math.Sqrt((double)variance.Value);

    }

    /// <summary>
    /// Computes the standard deviation of a sequence of System.Int64 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Stdev(this IEnumerable<long> source)
    {
      return Math.Sqrt(source.Variance());
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Int64&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Stdev(this IEnumerable<long?> source)
    {
      double? variance = source.Variance();
      return (double?)Math.Sqrt(variance.Value);
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of System.Int32 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Stdev(this IEnumerable<int> source)
    {
      return Math.Sqrt(source.Variance());
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Int32&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Stdev(this IEnumerable<int?> source)
    {
      double? variance = source.Variance();
      return (double?)Math.Sqrt(variance.Value);
    }



    /// <summary>
    /// Computes the standard deviation of a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static float Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static float? Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static decimal Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static decimal? Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception> 
    public static double Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the standard deviation of a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the standard deviation of.</param>
    /// <returns>The standard deviation of the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static double? Stdev<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)));
    }

    #endregion

    #region Standard Deviation (Cumulative)

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Double values.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeStdev(this IEnumerable<double> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double item in source.CumulativeVariance())
        yield return Math.Sqrt(item);
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Double&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeStdev(this IEnumerable<double?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double? item in source.CumulativeVariance())
        yield return Math.Sqrt(item.GetValueOrDefault());
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Single values.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float> CumulativeStdev(this IEnumerable<float> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (float item in source.CumulativeVariance())
        yield return (float)Math.Sqrt(item);
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Single&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float?> CumulativeStdev(this IEnumerable<float?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (float? item in source.CumulativeVariance())
        yield return (float?)Math.Sqrt(item.GetValueOrDefault());
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Decimal values.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal> CumulativeStdev(this IEnumerable<decimal> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (decimal item in source.CumulativeVariance())
        yield return (decimal)Math.Sqrt((double)item);
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Decimal&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal?> CumulativeStdev(this IEnumerable<decimal?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (decimal? item in source.CumulativeVariance())
        yield return (decimal?)Math.Sqrt((double)item.GetValueOrDefault());
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Int64 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeStdev(this IEnumerable<long> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double item in source.CumulativeVariance())
        yield return Math.Sqrt(item);
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Int64&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeStdev(this IEnumerable<long?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double? item in source.CumulativeVariance())
        yield return Math.Sqrt(item.GetValueOrDefault());
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Int32 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeStdev(this IEnumerable<int> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double item in source.CumulativeVariance())
        yield return Math.Sqrt(item);
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Int32&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeStdev(this IEnumerable<int?> source)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double? item in source.CumulativeVariance())
        yield return Math.Sqrt(item.GetValueOrDefault());
    }



    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float?> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal?> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the standard deviation of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    /// <summary>
    /// Computes the cumulative standard deviation in a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the standard deviation of.</param>
    /// <returns>A sequence of values that are the computed standard deviation for the first 2,3,4,5....N elements.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> CumulativeStdev<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return CumulativeStdev(source.Select(S => selector(S)));
    }

    #endregion

    #region Standard Deviation (Block)

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Double values.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Stdev(this IEnumerable<double> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double item in source.Variance(blockSize))
        yield return Math.Sqrt(item);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Double&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Stdev(this IEnumerable<double?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double? item in source.Variance(blockSize))
        if (item.HasValue)
          yield return Math.Sqrt(item.GetValueOrDefault());
        else
          yield return null;
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Single values.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float> Stdev(this IEnumerable<float> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (float item in source.Variance(blockSize))
        yield return (float)Math.Sqrt(item);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Single&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float?> Stdev(this IEnumerable<float?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (float? item in source.Variance(blockSize))
        if (item.HasValue)
          yield return (float?)Math.Sqrt(item.GetValueOrDefault());
        else
          yield return null;
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Decimal values.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal> Stdev(this IEnumerable<decimal> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (decimal item in source.Variance(blockSize))
        yield return (decimal)Math.Sqrt((double)item);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Decimal&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal?> Stdev(this IEnumerable<decimal?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (decimal? item in source.Variance(blockSize))
        if (item.HasValue)
          yield return (decimal?)Math.Sqrt((double)item.Value);
        else
          yield return null;
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Int64 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Stdev(this IEnumerable<long> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double item in source.Variance(blockSize))
        yield return Math.Sqrt(item);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Int64&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Stdev(this IEnumerable<long?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double? item in source.Variance(blockSize))
        if (item.HasValue)
          yield return Math.Sqrt(item.Value);
        else
          yield return null;
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Int32 values.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Stdev(this IEnumerable<int> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double item in source.Variance(blockSize))
        yield return Math.Sqrt(item);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Int32&gt; values.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">blockSize must be two or larger.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Stdev(this IEnumerable<int?> source, int blockSize)
    {
      Helper.InvalidateNullParam(source, "source");

      foreach (double? item in source.Variance(blockSize))
        if (item.HasValue)
          yield return Math.Sqrt(item.Value);
        else
          yield return null;
    }



    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Double values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, double> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Double&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Double&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, double?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Single values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, float> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Single&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Single&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<float?> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, float?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Decimal values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<decimal> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, decimal> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Decimal&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Decimal&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>  
    public static IEnumerable<decimal?> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, decimal?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int64 values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, long> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Int64&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int64&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, long?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of System.Int32 values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, int> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    /// <summary>
    /// Computes the standard deviation of blocks of elements in a sequence of Nullable&lt;System.Int32&gt; values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <param name="source">A sequence of Nullable&lt;System.Int32&gt; values to calculate the standard deviation of.</param>
    /// <param name="blockSize">The number of elements in the standard deviation block. Block size must be two or larger.</param>
    /// <returns>The standard deviation in the sequence of values.</returns>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
    /// <exception cref="System.ArgumentException">Sequence must have at least two values.</exception>
    public static IEnumerable<double?> Stdev<TSource>(this IEnumerable<TSource> source, int blockSize, Func<TSource, int?> selector)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(selector, "selector");

      return Stdev(source.Select(S => selector(S)), blockSize);
    }

    #endregion
  }
}