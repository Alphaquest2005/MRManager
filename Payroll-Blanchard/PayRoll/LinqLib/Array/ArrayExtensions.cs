using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LinqLib.Properties;
using LinqLib.Sequence;

namespace LinqLib.Array
{
  /// <summary>
  /// Adds two specified T type values
  /// </summary>
  /// <typeparam name="T">The type of the values to add.</typeparam>
  /// <param name="x">First T type value to add.</param>
  /// <param name="y">Second T type value to add.</param>
  /// <returns>A T value that is the sum of x and y.</returns>
  public delegate T Add<T>(T x, T y);

  /// <summary>
  /// Subtracts two specified T type values
  /// </summary>
  /// <typeparam name="T">The type of the values to subtract.</typeparam>
  /// <param name="x">The T type value to subtract from.</param>
  /// <param name="y">The T type value to subtract.</param>
  /// <returns>A T value that is the difference between x and y.</returns>
  public delegate T Subtract<T>(T x, T y);

  /// <summary>
  /// Multiplies a specified T type value by the specified double value.
  /// </summary>
  /// <typeparam name="T">The type of the value to multiply.</typeparam>
  /// <param name="x">The type of the values to multiply.</param>
  /// <param name="y">A double value used to multiply the x value.</param>
  /// <returns>A T value that is the result of multiplying x and y.</returns>
  public delegate T Multiply<T>(T x, double y);

  /// <summary>
  /// Divides a specified T type value by the specified double value.
  /// </summary>
  /// <typeparam name="T">The type of the value to divide.</typeparam>
  /// <param name="x">The type of the values to divide.</param>
  /// <param name="y">A double value used to divide the x value.</param>
  /// <returns>A T value that is the result of dividing x by y.</returns>
  public delegate T Divide<T>(T x, double y);

  /// <summary>
  /// Provides array manipulation methods.
  /// </summary>
  public static class ArrayExtensions
  {
    #region Slice and Fuse (up to 4D - primitives only)

    /// <summary>
    /// Slices a two dimensional array into a sequence of single dimensional arrays.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The array to slice.</param>
    /// <returns>An enumerable of one dimensional arrays.</returns>
    /// <remarks>Slicing occurs on the Y axis. Each array represents a row from source array.</remarks>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.InvalidOperationException">T is not a primitive.</exception>
    public static IEnumerable<T[]> Slice<T>(this T[,] source)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNonPremitive<T>("Slice");

      int elementSize = Marshal.SizeOf(typeof(T));

      int yLen = source.GetLength(0);
      int xLen = source.GetLength(1);

      int xSize = elementSize * xLen;

      for (int y = 0; y < yLen; y++)
      {
        T[] outputArray = new T[xLen];
        System.Buffer.BlockCopy(source, y * xSize, outputArray, 0, xSize);
        yield return outputArray;
      }
    }

    /// <summary>
    /// Slices a three dimensional array into a sequence of two dimensional arrays.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The array to slice.</param>
    /// <returns>An enumerable of two dimensional arrays.</returns>
    /// <remarks>Slicing occurs on the Z axis. Each array represents a sheet from source array.</remarks>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.InvalidOperationException">T is not a primitive.</exception>
    public static IEnumerable<T[,]> Slice<T>(this T[, ,] source)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNonPremitive<T>("Slice");

      int elementSize = Marshal.SizeOf(typeof(T));

      int zLen = source.GetLength(0);
      int yLen = source.GetLength(1);
      int xLen = source.GetLength(2);

      int xySize = elementSize * xLen * yLen;

      for (int z = 0; z < zLen; z++)
      {
        T[,] outputArray = new T[yLen, xLen];
        System.Buffer.BlockCopy(source, z * xySize, outputArray, 0, xySize);
        yield return outputArray;
      }
    }

    /// <summary>
    /// Slices a four dimensional array into a sequence of three dimensional arrays.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The array to slice.</param>
    /// <returns>An enumerable of three dimensional arrays.</returns>
    /// <remarks>Slicing occurs on the A axis. Each array represents a cube from source array.</remarks>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.InvalidOperationException">T is not a primitive.</exception>
    public static IEnumerable<T[, ,]> Slice<T>(this T[, , ,] source)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNonPremitive<T>("Slice");

      int elementSize = Marshal.SizeOf(typeof(T));

      int aLen = source.GetLength(0);
      int zLen = source.GetLength(1);
      int yLen = source.GetLength(2);
      int xLen = source.GetLength(3);

      int xyzSize = elementSize * xLen * yLen * zLen;

      for (int a = 0; a < aLen; a++)
      {
        T[, ,] outputArray = new T[zLen, yLen, xLen];
        System.Buffer.BlockCopy(source, a * xyzSize, outputArray, 0, xyzSize);
        yield return outputArray;
      }
    }

    /// <summary>
    /// Fuses a two dimensional array from a sequence of single dimensional arrays.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The sequence to fuse.</param>
    /// <returns>An two dimensional array.</returns>    
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.InvalidOperationException">T is not a primitive.</exception>
    public static T[,] Fuse<T>(this IEnumerable<T[]> source)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNonPremitive<T>("Fuse");

      int elementSize = Marshal.SizeOf(typeof(T));

      int y = 0;
      int x = 0;
      T[][] temp = source.ToArray();
      foreach (T[] item in temp)
      {
        int xLen = item.GetLength(0);
        if (xLen > x)
          x = xLen;
        y++;
      }

      T[,] outputArray = new T[y, x];

      y = 0;
      foreach (T[] item in temp)
      {
        int xSize = item.Length * elementSize;
        System.Buffer.BlockCopy(item, 0, outputArray, y * xSize, xSize);
        y++;
      }
      return outputArray;
    }

    /// <summary>
    /// Fuses a three dimensional array from a sequence of two dimensional arrays.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The sequence to fuse.</param>
    /// <returns>An three dimensional array.</returns>    
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.InvalidOperationException">T is not a primitive.</exception>
    public static T[, ,] Fuse<T>(this IEnumerable<T[,]> source)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNonPremitive<T>("Fuse");

      int elementSize = Marshal.SizeOf(typeof(T));

      int z = 0;
      int y = 0;
      int x = 0;
      foreach (T[,] item in source)
      {
        int yLen = item.GetLength(0);
        if (yLen > y)
          y = yLen;
        int xLen = item.GetLength(1);
        if (xLen > x)
          x = xLen;
        z++;
      }

      T[, ,] outputArray = new T[z, y, x];

      z = 0;
      foreach (T[,] item in source)
      {
        int xySize = item.Length * elementSize;
        System.Buffer.BlockCopy(item, 0, outputArray, z * xySize, xySize);
        z++;
      }
      return outputArray;
    }

    /// <summary>
    /// Fuses a four dimensional array from a sequence of three dimensional arrays.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The sequence to fuse.</param>
    /// <returns>An four dimensional array.</returns>    
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.InvalidOperationException">T is not a primitive.</exception>
    public static T[, , ,] Fuse<T>(this IEnumerable<T[, ,]> source)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNonPremitive<T>("Fuse");

      int elementSize = Marshal.SizeOf(typeof(T));

      int a = 0;
      int z = 0;
      int y = 0;
      int x = 0;
      foreach (T[, ,] item in source)
      {
        int zLen = item.GetLength(0);
        if (zLen > z)
          z = zLen;
        int yLen = item.GetLength(1);
        if (yLen > y)
          y = yLen;
        int xLen = item.GetLength(2);
        if (xLen > x)
          x = xLen;

        a++;
      }

      T[, , ,] outputArray = new T[a, z, y, x];

      a = 0;
      foreach (T[, ,] item in source)
      {
        int xyzSize = item.Length * elementSize;
        System.Buffer.BlockCopy(item, 0, outputArray, a * xyzSize, xyzSize);
        a++;
      }
      return outputArray;
    }

    #endregion

    #region Circular Shift (up to 4D - any type)

    /// <summary>
    /// Splits and shifts an array into a sequence of smaller arrays. Each resulting array is a contiguous subset of the source array and moves forward one element at the time.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>        
    /// <param name="x">The size of each new array.</param>
    /// <returns>A sequence of arrays, each represents a section of the source array.</returns>
    public static IEnumerable<T[]> CircularShift<T>(this T[] source, int x)
    {
      return CircularShift(source, x, 1);
    }

    /// <summary>
    /// Splits and shifts an array into a sequence of smaller arrays. Each resulting array is a contiguous subset of the source array but can overlap other arrays in the sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="x">The size of each new array.</param>    
    /// <param name="xStep">The number of elements to progress when creating the next array.</param>
    /// <returns>A sequence of arrays, each represents a section of the source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x or xStep are out of valid range.</exception>    
    public static IEnumerable<T[]> CircularShift<T>(this T[] source, int x, int xStep)
    {
      Helper.InvalidateNullParam(source, "source");

      int xLen = source.GetLength(0);

      Helper.InvalidateNumericRange(x, 1, xLen, "x");
      Helper.InvalidateNumericRange(xStep, 1, xLen, "xStep");

      int xLoops = xLen - x;

      for (int x1 = 0; x1 <= xLoops; x1 += xStep)
      {
        T[] outputArray = new T[x];
        System.Array.Copy(source, x1, outputArray, 0, x);
        yield return outputArray;
      }
    }

    /// <summary>
    /// Splits and shifts an array into a sequence of smaller arrays. Each resulting array is a contiguous subset of the source array and moves forward one element at the time on each axis.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>        
    /// <param name="y">The Y size of each new array.</param>
    /// <param name="x">The X size of each new array.</param>
    /// <returns>A sequence of arrays, each represents a section of the source array.</returns>
    public static IEnumerable<T[,]> CircularShift<T>(this T[,] source, int y, int x)
    {
      return CircularShift(source, x, y, 1, 1);
    }

    /// <summary>
    /// Splits and shifts an array into a sequence of smaller arrays. Each resulting array is a contiguous subset of the source array but can overlap other arrays in the sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="y">The Y size of each new array.</param>
    /// <param name="x">The X size of each new array.</param>
    /// <param name="yStep">The number of elements to progress on the Y axis when creating the next array.</param>
    /// <param name="xStep">The number of elements to progress on the X axis when creating the next array.</param>
    /// <returns>A sequence of arrays, each represents a section of the source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x, y, xStep or yStep is out of range.</exception>    
    public static IEnumerable<T[,]> CircularShift<T>(this T[,] source, int y, int x, int yStep, int xStep)
    {
      Helper.InvalidateNullParam(source, "source");

      int yLen = source.GetLength(0);
      int xLen = source.GetLength(1);

      Helper.InvalidateNumericRange(x, 1, xLen, "x");
      Helper.InvalidateNumericRange(y, 1, yLen, "y");
      Helper.InvalidateNumericRange(xStep, 1, xLen, "xStep");
      Helper.InvalidateNumericRange(yStep, 1, yLen, "yStep");

      int xLoops = xLen - x;
      int yLoops = yLen - y;

      for (int y1 = 0; y1 <= yLoops; y1 += yStep)
      {
        int yOffset = y1 * xLen;
        for (int x1 = 0; x1 <= xLoops; x1 += xStep)
        {
          int xOffset = yOffset + x1;
          T[,] outputArray = new T[y, x];
          for (int ySection = 0; ySection < y; ySection++)
          {
            int srcOffset = ySection * xLen + xOffset;
            int destOffset = ySection * x;
            System.Array.Copy(source, srcOffset, outputArray, destOffset, x);
          }
          yield return outputArray;
        }
      }
    }

    /// <summary>
    /// Splits and shifts an array into a sequence of smaller arrays. Each resulting array is a contiguous subset of the source array and moves forward one element at the time on each axis.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>        
    /// <param name="z">The Z size of each new array.</param>
    /// <param name="y">The Y size of each new array.</param>
    /// <param name="x">The X size of each new array.</param>
    /// <returns>A sequence of arrays, each represents a section of the source array.</returns>
    public static IEnumerable<T[, ,]> CircularShift<T>(this T[, ,] source, int z, int y, int x)
    {
      return CircularShift(source, z, x, y, 1, 1, 1);
    }

    /// <summary>
    /// Splits and shifts an array into a sequence of smaller arrays. Each resulting array is a contiguous subset of the source array but can overlap other arrays in the sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="z">The Z size of each new array.</param>
    /// <param name="y">The Y size of each new array.</param>
    /// <param name="x">The X size of each new array.</param>
    /// <param name="zStep">The number of elements to progress on the Z axis when creating the next array.</param>
    /// <param name="yStep">The number of elements to progress on the Y axis when creating the next array.</param>
    /// <param name="xStep">The number of elements to progress on the X axis when creating the next array.</param>
    /// <returns>A sequence of arrays, each represents a section of the source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x, y, z, xStep, yStep or zStep is out of range.</exception>    
    public static IEnumerable<T[, ,]> CircularShift<T>(this T[, ,] source, int z, int y, int x, int zStep, int yStep, int xStep)
    {
      Helper.InvalidateNullParam(source, "source");

      int zLen = source.GetLength(0);
      int yLen = source.GetLength(1);
      int xLen = source.GetLength(2);

      Helper.InvalidateNumericRange(x, 1, xLen, "x");
      Helper.InvalidateNumericRange(y, 1, yLen, "y");
      Helper.InvalidateNumericRange(z, 1, yLen, "z");
      Helper.InvalidateNumericRange(xStep, 1, xLen, "xStep");
      Helper.InvalidateNumericRange(yStep, 1, yLen, "yStep");
      Helper.InvalidateNumericRange(zStep, 1, zLen, "zStep");

      int xyArea = xLen * yLen;

      int xLoops = xLen - x;
      int yLoops = yLen - y;
      int zLoops = zLen - z;

      for (int z1 = 0; z1 <= zLoops; z1 += zStep)
      {
        int zOffset = z1 * xyArea;
        for (int y1 = 0; y1 <= yLoops; y1 += yStep)
        {
          int yOffset = y1 * xLen + zOffset;
          for (int x1 = 0; x1 <= xLoops; x1 += xStep)
          {
            int xOffset = yOffset + x1;
            T[, ,] outputArray = new T[z, y, x];
            for (int zSection = 0; zSection < z; zSection++)
            {
              int zSectionOffset = zSection * xyArea;
              for (int ySection = 0; ySection < y; ySection++)
              {
                int srcOffset = zSectionOffset + ySection * xLen + xOffset;
                int destOffset = zSection * x * y + ySection * x;
                System.Array.Copy(source, srcOffset, outputArray, destOffset, x);
              }
            }
            yield return outputArray;
          }
        }
      }
    }

    /// <summary>
    /// Splits and shifts an array into a sequence of smaller arrays. Each resulting array is a contiguous subset of the source array and moves forward one element at the time on each axis.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>    
    /// <param name="a">The A size of each new array.</param>
    /// <param name="z">The Z size of each new array.</param>
    /// <param name="y">The Y size of each new array.</param>
    /// <param name="x">The X size of each new array.</param>
    /// <returns>A sequence of arrays, each represents a section of the source array.</returns>
    public static IEnumerable<T[, , ,]> CircularShift<T>(this T[, , ,] source, int a, int z, int y, int x)
    {
      return CircularShift(source, a, z, x, y, 1, 1, 1, 1);
    }

    /// <summary>
    /// Splits and shifts an array into a sequence of smaller arrays. Each resulting array is a contiguous subset of the source array but can overlap other arrays in the sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="a">The A size of each new array.</param>
    /// <param name="z">The Z size of each new array.</param>
    /// <param name="y">The Y size of each new array.</param>
    /// <param name="x">The X size of each new array.</param>
    /// <param name="aStep">The number of elements to progress on the A axis when creating the next array.</param>
    /// <param name="zStep">The number of elements to progress on the Z axis when creating the next array.</param>
    /// <param name="yStep">The number of elements to progress on the Y axis when creating the next array.</param>
    /// <param name="xStep">The number of elements to progress on the X axis when creating the next array.</param>
    /// <returns>A sequence of arrays, each represents a section of the source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x, y, z, a, xStep, yStep, zStep or aStep is out of range.</exception>    
    public static IEnumerable<T[, , ,]> CircularShift<T>(this T[, , ,] source, int a, int z, int y, int x, int aStep, int zStep, int yStep, int xStep)
    {
      Helper.InvalidateNullParam(source, "source");

      int aLen = source.GetLength(0);
      int zLen = source.GetLength(1);
      int yLen = source.GetLength(2);
      int xLen = source.GetLength(3);

      Helper.InvalidateNumericRange(x, 1, xLen, "x");
      Helper.InvalidateNumericRange(y, 1, yLen, "y");
      Helper.InvalidateNumericRange(z, 1, yLen, "z");
      Helper.InvalidateNumericRange(a, 1, aLen, "a");
      Helper.InvalidateNumericRange(xStep, 1, xLen, "xStep");
      Helper.InvalidateNumericRange(yStep, 1, yLen, "yStep");
      Helper.InvalidateNumericRange(zStep, 1, zLen, "zStep");
      Helper.InvalidateNumericRange(aStep, 1, aLen, "aStep");

      int xyArea = xLen * yLen;
      int xyzVol = xyArea * zLen;

      int xLoops = xLen - x;
      int yLoops = yLen - y;
      int zLoops = zLen - z;
      int aLoops = aLen - a;

      for (int a1 = 0; a1 <= aLoops; a1 += aStep)
      {
        int aOffset = a1 * xyzVol;
        for (int z1 = 0; z1 <= zLoops; z1 += zStep)
        {
          int zOffset = aOffset + z1 * xLen * yLen;
          for (int y1 = 0; y1 <= yLoops; y1 += yStep)
          {
            int yOffset = zOffset + y1 * xLen;
            for (int x1 = 0; x1 <= xLoops; x1 += xStep)
            {
              int xOffset = yOffset + x1;
              T[, , ,] outputArray = new T[a, z, y, x];

              for (int aSection = 0; aSection < a; aSection++)
              {
                int aSectionOffset = aSection * xyzVol;
                for (int zSection = 0; zSection < z; zSection++)
                {
                  int zSectionOffset = zSection * xyArea;
                  for (int ySection = 0; ySection < y; ySection++)
                  {
                    int srcOffset = aSectionOffset + zSectionOffset + ySection * xLen + xOffset;
                    int destOffset = aSection * z * x * y + zSection * x * y + ySection * x;
                    System.Array.Copy(source, srcOffset, outputArray, destOffset, x);
                  }
                }
              }
              yield return outputArray;
            }
          }
        }
      }
    }

    #endregion

    #region Split (up to 4D - any type)

    /// <summary>
    /// Splits an array into a sequence of smaller arrays. Each array starts where previous array ends. 
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="x">Size of each new array.</param>
    /// <returns>A sequence of arrays, each represents a unique, Contiguous section of the source array.</returns>
    public static IEnumerable<T[]> Split<T>(this T[] source, int x)
    {
      return source.CircularShift(x, x);
    }

    /// <summary>
    /// Splits an array into a sequence of smaller arrays. Each array starts where previous array ends. 
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="y">The Y size of each new array.</param>
    /// <param name="x">The X size of each new array.</param>
    /// <returns>A sequence of arrays, each represents a unique, Contiguous section of the source array.</returns>    
    public static IEnumerable<T[,]> Split<T>(this T[,] source, int y, int x)
    {
      return source.CircularShift(y, x, y, x);
    }

    /// <summary>
    /// Splits an array into a sequence of smaller arrays. Each array starts where previous array ends. 
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>    
    /// <param name="z">The Z size of each new array.</param>
    /// <param name="y">The Y size of each new array.</param>
    /// <param name="x">The X size of each new array.</param>
    /// <returns>A sequence of arrays, each represents a unique, Contiguous section of the source array.</returns>    
    public static IEnumerable<T[, ,]> Split<T>(this T[, ,] source, int z, int y, int x)
    {
      return source.CircularShift(z, y, x, z, y, x);
    }

    /// <summary>
    /// Splits an array into a sequence of smaller arrays. Each array starts where previous array ends. 
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="a">The A size of each new array.</param>
    /// <param name="z">The Z size of each new array.</param>
    /// <param name="y">The Y size of each new array.</param>
    /// <param name="x">The X size of each new array.</param>
    /// <returns>A sequence of arrays, each represents a unique, Contiguous section of the source array.</returns>    
    public static IEnumerable<T[, , ,]> Split<T>(this T[, , ,] source, int a, int z, int y, int x)
    {
      return source.CircularShift(a, z, y, x, a, z, y, x);
    }

    #endregion

    #region ToArray (up to 4D - any type)

    /// <summary>
    /// Converts a sequence of elements into an array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">Source sequence to convert.</param>
    /// <param name="x">Size of the new array.</param>    
    /// <returns>A single dimension array.</returns>
    public static T[] ToArray<T>(this IEnumerable<T> source, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");

      Type tt = typeof(T);
      T[] dest = new T[x];
      if (tt.IsPrimitive)
      {
        int itemSize = Marshal.SizeOf(typeof(T));
        T[] temp = source.ToArray();

        int elements = temp.Length;
        int copyLen = x;
        if (copyLen > elements)
          copyLen = elements;

        System.Buffer.BlockCopy(temp, 0, dest, 0, copyLen * itemSize);
      }
      else
      {
        int x1 = 0;
        foreach (T item in source)
        {
          dest[x1] = item;
          x1++;
          if (x1 == x)
            break;
        }
      }
      return dest;
    }

    /// <summary>
    /// Converts a sequence of elements into a two dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">Source sequence to convert.</param>
    /// <param name="y">Size of the first dimension.</param>
    /// <param name="x">Size of the second dimension.</param>    
    /// <returns>A two dimensional array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x or y is out of range.</exception>    
    public static T[,] ToArray<T>(this IEnumerable<T> source, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");

      Type tt = typeof(T);
      T[,] dest = new T[y, x];
      if (tt.IsPrimitive)
      {
        int itemSize = Marshal.SizeOf(typeof(T));
        T[] temp = source.ToArray();

        int elements = temp.Length;
        int copyLen = x * y;
        if (copyLen > elements)
          copyLen = elements;

        System.Buffer.BlockCopy(temp, 0, dest, 0, copyLen * itemSize);
      }
      else
      {
        int x1 = 0;
        int y1 = 0;
        foreach (T item in source)
        {
          dest[y1, x1] = item;
          x1++;
          if (x1 == x)
          {
            x1 = 0;
            y1++;
            if (y1 == y)
              break;
          }
        }
      }
      return dest;
    }

    /// <summary>
    /// Converts a sequence of elements into a three dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">Source sequence to convert.</param>
    /// <param name="z">Size of the first dimension.</param>
    /// <param name="y">Size of the second dimension.</param>
    /// <param name="x">Size of the third dimension.</param>
    /// <returns>A three dimensional array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x, y or z is out of range.</exception>    
    public static T[, ,] ToArray<T>(this IEnumerable<T> source, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");
      Helper.InvalidateNumericRange(z, 1, int.MaxValue, "z");

      Type tt = typeof(T);

      T[, ,] dest = new T[z, y, x];
      if (tt.IsPrimitive)
      {
        int itemSize = Marshal.SizeOf(typeof(T));
        T[] temp = source.ToArray();

        int elements = temp.Length;
        int copyLen = x * y * z;
        if (copyLen > elements)
          copyLen = elements;

        System.Buffer.BlockCopy(temp, 0, dest, 0, copyLen * itemSize);
      }
      else
      {
        int x1 = 0;
        int y1 = 0;
        int z1 = 0;
        foreach (T item in source)
        {
          dest[z1, y1, x1] = item;
          x1++;
          if (x1 == x)
          {
            y1++;
            x1 = 0;
            if (y1 == y)
            {
              z1++;
              y1 = 0;
              if (z1 == z)
                break;
            }
          }
        }
      }
      return dest;
    }

    /// <summary>
    /// Converts a sequence of elements into a four dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">Source sequence to convert.</param>
    /// <param name="a">Size of the first dimension.</param>
    /// <param name="z">Size of the second dimension.</param>
    /// <param name="y">Size of the third dimension.</param>
    /// <param name="x">Size of the fourth dimension.</param>
    /// <returns>A four dimensional array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x, y, z or a is out of range.</exception>    
    public static T[, , ,] ToArray<T>(this IEnumerable<T> source, int a, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "a");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");
      Helper.InvalidateNumericRange(z, 1, int.MaxValue, "z");

      Type tt = typeof(T);

      T[, , ,] dest = new T[a, z, y, x];
      if (tt.IsPrimitive)
      {
        int itemSize = Marshal.SizeOf(typeof(T));
        T[] temp = source.ToArray();

        int elements = temp.Length;
        int copyLen = a * x * y * z;
        if (copyLen > elements)
          copyLen = elements;

        System.Buffer.BlockCopy(temp, 0, dest, 0, copyLen * itemSize);
      }
      else
      {
        int x1 = 0;
        int y1 = 0;
        int z1 = 0;
        int a1 = 0;
        foreach (T item in source)
        {
          dest[a1, z1, y1, x1] = item;
          x1++;
          if (x1 == x)
          {
            x1 = 0;
            y1++;
            if (y1 == y)
            {
              y1 = 0;
              z1++;
              if (z1 == z)
              {
                z1 = 0;
                a1++;
                if (a1 == a)
                  break;
              }
            }
          }
        }
      }
      return dest;
    }

    #endregion

    #region Resize (up to 4D - double and float types only)
    /// <summary>
    /// Resizes an array of doubles while preserving its content and trends. 
    /// </summary>
    /// <param name="source">The source array.</param>
    /// <param name="newSize">The size of the new array.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">newSize is out of range.</exception>    
    public static double[] Resize(this double[] source, int newSize)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(newSize, 1, int.MaxValue, "newSize");

      int originalBoundX = source.GetUpperBound(0);
      int originalSizeX = originalBoundX + 1;
      double[] destArr = new double[newSize];
      int newBoundX = newSize - 1;

      if (newSize == originalSizeX)
      {
        //Nothing to Resize, simple copy will do the magic.        
        System.Array.Copy(source, 0, destArr, 0, originalSizeX);
      }
      else if (originalSizeX < newSize) //Expand
      {
        for (int x = 0; x < newSize; x++)
        {
          double pos = (double)(x * originalBoundX) / newBoundX;
          int leftCell = NumericHelper.IntegerPart(pos);
          if (NumericHelper.AreClose(pos, leftCell) || NumericHelper.AreClose(pos, originalBoundX))
          {
            destArr[x] = source[leftCell];
          }
          else
          {
            int rightCell = leftCell + 1;
            destArr[x] = source[leftCell] + (source[rightCell] - source[leftCell]) * NumericHelper.FractionPart(pos);
          }
        }
      }
      else //shrink
      {
        double ratio = (double)(originalSizeX) / newSize;

        for (int x = 0; x < newSize; x++)
        {
          double start1 = ratio * x;
          double end1 = start1 + ratio;
          double acc = 0;
          int start = NumericHelper.IntegerPart(start1);
          int end = NumericHelper.IntegerPart(end1);

          if (!NumericHelper.AreClose(start1, start))
          {
            acc += source[start] * (1 - NumericHelper.FractionPart(start1));
            start++;
          }

          if (!NumericHelper.AreClose(end1, end))
          {
            acc += source[end] * NumericHelper.FractionPart(end1);
          }

          for (int xSection = start; xSection < end; xSection++)
            acc += source[xSection];

          destArr[x] = acc / ratio;
        }
      }

      return destArr;
    }

    /// <summary>
    /// Resizes an array of doubles while preserving its content and trends. 
    /// </summary>
    /// <param name="source">The source array.</param>
    /// <param name="y">New size of the Y axis.</param>
    /// <param name="x">New size of the X axis.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x or y is out of range.</exception>    
    public static double[,] Resize(this double[,] source, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");

      return source.Slice()
            .Select(X => X.Resize(x))
            .Fuse()
            .Rotate(270)
            .Slice()
            .Select(X => X.Resize(y))
            .Fuse()
            .Rotate(90);
    }

    /// <summary>
    /// Resizes an array of doubles while preserving its content and trends. 
    /// </summary>
    /// <param name="source">The source array.</param>
    /// <param name="z">New size of the Z axis.</param>
    /// <param name="y">New size of the Y axis.</param>
    /// <param name="x">New size of the X axis.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x, y or z is out of range.</exception>    
    public static double[, ,] Resize(this double[, ,] source, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");
      Helper.InvalidateNumericRange(z, 1, int.MaxValue, "z");

      return source.Slice()
            .Select(X => X.Resize(y, x))
            .Fuse()
            .Rotate(RotateAxis.RotateY, -90)
            .Slice()
            .Select(X => X.Slice()
                          .Select(X1 => X1.Resize(z))
                          .Fuse())
            .Fuse()
            .Rotate(RotateAxis.RotateY, 90);
    }

    /// <summary>
    /// Resizes an array of doubles while preserving its content and trends. 
    /// </summary>
    /// <param name="source">The source array.</param>
    /// <param name="a">New size of the A axis.</param>
    /// <param name="z">New size of the Z axis.</param>
    /// <param name="y">New size of the Y axis.</param>
    /// <param name="x">New size of the X axis.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x, y, z or a is out of range.</exception>    
    public static double[, , ,] Resize(this double[, , ,] source, int a, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");
      Helper.InvalidateNumericRange(z, 1, int.MaxValue, "z");
      Helper.InvalidateNumericRange(a, 1, int.MaxValue, "a");

      return source.Slice()
            .Select(X => X.Resize(z, y, x))
            .Fuse()
            .Rotate(RotateAxis.RotateY, 90)
            .Slice()
            .Select(X => X.Slice()
                          .Select(X1 => X1.Slice()
                                          .Select(X3 => X3.Resize(a))
                                          .Fuse())
                           .Fuse())
            .Fuse()
            .Rotate(RotateAxis.RotateY, -90);
    }

    /// <summary>
    /// Resizes an array of floats while preserving its content and trends. 
    /// </summary>
    /// <param name="source">The source array.</param>
    /// <param name="newSize">The size of the new array.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">newSize is out of range.</exception>    
    public static float[] Resize(this float[] source, int newSize)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(newSize, 1, int.MaxValue, "newSize");

      int originalBoundX = source.GetUpperBound(0);
      int originalSizeX = originalBoundX + 1;
      float[] destArr = new float[newSize];
      int newBoundX = newSize - 1;

      if (newSize == originalSizeX)
      {
        //Nothing to Resize, simple copy will do the magic.        
        System.Array.Copy(source, 0, destArr, 0, originalSizeX);
      }
      else if (originalSizeX < newSize) //Expand
      {
        for (int x = 0; x < newSize; x++)
        {
          double pos = (double)(x * originalBoundX) / newBoundX;
          int leftCell = NumericHelper.IntegerPart(pos);
          if (NumericHelper.AreClose(pos, leftCell) || NumericHelper.AreClose(pos, originalBoundX))
          {
            destArr[x] = source[leftCell];
          }
          else
          {
            int rightCell = leftCell + 1;
            destArr[x] = (float)(source[leftCell] + (source[rightCell] - source[leftCell]) * NumericHelper.FractionPart(pos));
          }
        }
      }
      else //shrink
      {
        double ratio = (double)(originalSizeX) / newSize;

        for (int x = 0; x < newSize; x++)
        {
          double start1 = ratio * x;
          double end1 = start1 + ratio;
          double acc = 0;
          int start = NumericHelper.IntegerPart(start1);
          int end = NumericHelper.IntegerPart(end1);

          if (!NumericHelper.AreClose(start1, start))
          {
            acc += source[start] * (1 - NumericHelper.FractionPart(start1));
            start++;
          }

          if (!NumericHelper.AreClose(end1, end))
          {
            acc += source[end] * NumericHelper.FractionPart(end1);
          }

          for (int xSection = start; xSection < end; xSection++)
            acc += source[xSection];

          destArr[x] = (float)(acc / ratio);
        }
      }

      return destArr;
    }

    /// <summary>
    /// Resizes an array of floats while preserving its content and trends. 
    /// </summary>
    /// <param name="source">The source array.</param>
    /// <param name="y">New size of the Y axis.</param>
    /// <param name="x">New size of the X axis.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x or y is out of range.</exception>    
    public static float[,] Resize(this float[,] source, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");

      return source.Slice()
            .Select(X => X.Resize(x))
            .Fuse()
            .Rotate(270)
            .Slice()
            .Select(X => X.Resize(y))
            .Fuse()
            .Rotate(90);
    }

    /// <summary>
    /// Resizes an array of floats while preserving its content and trends. 
    /// </summary>
    /// <param name="source">The source array.</param>
    /// <param name="z">New size of the Z axis.</param>
    /// <param name="y">New size of the Y axis.</param>
    /// <param name="x">New size of the X axis.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x, y or z is out of range.</exception>    
    public static float[, ,] Resize(this float[, ,] source, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");
      Helper.InvalidateNumericRange(z, 1, int.MaxValue, "z");

      return source.Slice()
            .Select(X => X.Resize(y, x))
            .Fuse()
            .Rotate(RotateAxis.RotateY, -90)
            .Slice()
            .Select(X => X.Slice()
                          .Select(X1 => X1.Resize(z))
                          .Fuse())
            .Fuse()
            .Rotate(RotateAxis.RotateY, 90);
    }

    /// <summary>
    /// Resizes an array of floats while preserving its content and trends. 
    /// </summary>
    /// <param name="source">The source array.</param>
    /// <param name="a">New size of the A axis.</param>
    /// <param name="z">New size of the Z axis.</param>
    /// <param name="y">New size of the Y axis.</param>
    /// <param name="x">New size of the X axis.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">x, y, z or a is out of range.</exception>    
    public static float[, , ,] Resize(this float[, , ,] source, int a, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");
      Helper.InvalidateNumericRange(z, 1, int.MaxValue, "z");
      Helper.InvalidateNumericRange(a, 1, int.MaxValue, "a");

      return source.Slice()
            .Select(X => X.Resize(z, y, x))
            .Fuse()
            .Rotate(RotateAxis.RotateY, 90)
            .Slice()
            .Select(X => X.Slice()
                          .Select(X1 => X1.Slice()
                                          .Select(X3 => X3.Resize(a))
                                          .Fuse())
                           .Fuse())
            .Fuse()
            .Rotate(RotateAxis.RotateY, -90);
    }

    #endregion

    #region Resize (up to 4D - generic)

    /// <summary>
    /// Resizes an array of elements while preserving its content and trends. 
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="adder">A function to use when adding two T elements.</param>
    /// <param name="subtractor">A function to use when subtracting two T elements.</param>
    /// <param name="multiplier">A function to use when multiplying a T element.</param>
    /// <param name="divider">A function to use when dividing a T element.</param>
    /// <param name="newSize">the new size of the array.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source, adder, subtractor, multiplier or divider is null.</exception>
    /// <exception cref="System.ArgumentException">newSize is out of range.</exception>    
    public static T[] Resize<T>(this T[] source, Add<T> adder, Subtract<T> subtractor, Multiply<T> multiplier, Divide<T> divider, int newSize)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(adder, "adder");
      Helper.InvalidateNullParam(subtractor, "subtractor");
      Helper.InvalidateNullParam(multiplier, "multiplier");
      Helper.InvalidateNullParam(divider, "divider");
      Helper.InvalidateNumericRange(newSize, 1, int.MaxValue, "newSize");

      int originalBoundX = source.GetUpperBound(0);
      int originalSizeX = originalBoundX + 1;
      T[] destArr = new T[newSize];
      int newBoundX = newSize - 1;

      if (newSize == originalSizeX)
      {
        //Nothing to Resize, simple copy will do the magic.        
        System.Array.Copy(source, 0, destArr, 0, originalSizeX);
      }
      else if (originalSizeX < newSize) //Expand
      {
        for (int x = 0; x < newSize; x++)
        {
          double pos = (double)(x * originalBoundX) / newBoundX;
          int leftCell = NumericHelper.IntegerPart(pos);
          if (NumericHelper.AreClose(pos, leftCell) || NumericHelper.AreClose(pos, originalBoundX))
          {
            destArr[x] = source[leftCell];
          }
          else
          {
            int rightCell = leftCell + 1;
            destArr[x] = adder(source[leftCell], multiplier(subtractor(source[rightCell], source[leftCell]), NumericHelper.FractionPart(pos)));
          }
        }
      }
      else //shrink
      {
        double ratio = (double)(originalSizeX) / newSize;

        for (int x = 0; x < newSize; x++)
        {
          double start1 = ratio * x;
          double end1 = start1 + ratio;
          T acc = default(T);
          int start = NumericHelper.IntegerPart(start1);
          int end = NumericHelper.IntegerPart(end1);

          if (!NumericHelper.AreClose(start1, start))
          {
            acc = adder(acc, multiplier(source[start], (1 - NumericHelper.FractionPart(start1))));
            start++;
          }

          if (!NumericHelper.AreClose(end1, end))
          {
            acc = adder(acc, multiplier(source[end], NumericHelper.FractionPart(end1)));
          }

          for (int xSection = start; xSection < end; xSection++)
            acc = adder(acc, source[xSection]);

          destArr[x] = divider(acc, ratio);
        }
      }

      return destArr;
    }

    /// <summary>
    /// Resizes an array of elements while preserving its content and trends. 
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="adder">A function to use when adding two T elements.</param>
    /// <param name="subtractor">A function to use when subtracting two T elements.</param>
    /// <param name="multiplier">A function to use when multiplying a T element.</param>
    /// <param name="divider">A function to use when dividing a T element.</param>
    /// <param name="y">New size of the Y axis.</param>
    /// <param name="x">New size of the X axis.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source, adder, subtractor, multiplier or divider is null.</exception>
    /// <exception cref="System.ArgumentException">x or y is out of range.</exception>        
    public static T[,] Resize<T>(this T[,] source, Add<T> adder, Subtract<T> subtractor, Multiply<T> multiplier, Divide<T> divider, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(adder, "adder");
      Helper.InvalidateNullParam(subtractor, "subtractor");
      Helper.InvalidateNullParam(multiplier, "multiplier");
      Helper.InvalidateNullParam(divider, "divider");

      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");

      return source.Slice()
            .Select(X => X.Resize(adder, subtractor, multiplier, divider, x))
            .Fuse()
            .Rotate(270)
            .Slice()
            .Select(X => X.Resize(adder, subtractor, multiplier, divider, y))
            .Fuse()
            .Rotate(90);
    }

    /// <summary>
    /// Resizes an array of elements while preserving its content and trends. 
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="adder">A function to use when adding two T elements.</param>
    /// <param name="subtractor">A function to use when subtracting two T elements.</param>
    /// <param name="multiplier">A function to use when multiplying a T element.</param>
    /// <param name="divider">A function to use when dividing a T element.</param>    
    /// <param name="z">New size of the Z axis.</param>
    /// <param name="y">New size of the Y axis.</param>
    /// <param name="x">New size of the X axis.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source, adder, subtractor, multiplier or divider is null.</exception>
    /// <exception cref="System.ArgumentException">x, y or z is out of range.</exception>    
    public static T[, ,] Resize<T>(this T[, ,] source, Add<T> adder, Subtract<T> subtractor, Multiply<T> multiplier, Divide<T> divider, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(adder, "adder");
      Helper.InvalidateNullParam(subtractor, "subtractor");
      Helper.InvalidateNullParam(multiplier, "multiplier");
      Helper.InvalidateNullParam(divider, "divider");

      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");
      Helper.InvalidateNumericRange(z, 1, int.MaxValue, "z");

      return source.Slice()
            .Select(X => X.Resize(adder, subtractor, multiplier, divider, y, x))
            .Fuse()
            .Rotate(RotateAxis.RotateY, -90)
            .Slice()
            .Select(X => X.Slice()
                          .Select(X1 => X1.Resize(adder, subtractor, multiplier, divider, z))
                          .Fuse())
            .Fuse()
            .Rotate(RotateAxis.RotateY, 90);
    }

    /// <summary>
    /// Resizes an array of elements while preserving its content and trends. 
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="adder">A function to use when adding two T elements.</param>
    /// <param name="subtractor">A function to use when subtracting two T elements.</param>
    /// <param name="multiplier">A function to use when multiplying a T element.</param>
    /// <param name="divider">A function to use when dividing a T element.</param>
    /// <param name="a">New size of the A axis.</param>
    /// <param name="z">New size of the Z axis.</param>
    /// <param name="y">New size of the Y axis.</param>
    /// <param name="x">New size of the X axis.</param>
    /// <returns>The newly resized array.</returns>
    /// <exception cref="System.ArgumentNullException">source, adder, subtractor, multiplier or divider is null.</exception>    
    /// <exception cref="System.ArgumentException">x, y, z or a is out of range.</exception>    
    public static T[, , ,] Resize<T>(this T[, , ,] source, Add<T> adder, Subtract<T> subtractor, Multiply<T> multiplier, Divide<T> divider, int a, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(adder, "adder");
      Helper.InvalidateNullParam(subtractor, "subtractor");
      Helper.InvalidateNullParam(multiplier, "multiplier");
      Helper.InvalidateNullParam(divider, "divider");

      Helper.InvalidateNumericRange(x, 1, int.MaxValue, "x");
      Helper.InvalidateNumericRange(y, 1, int.MaxValue, "y");
      Helper.InvalidateNumericRange(z, 1, int.MaxValue, "z");
      Helper.InvalidateNumericRange(a, 1, int.MaxValue, "a");

      return source.Slice()
            .Select(X => X.Resize(adder, subtractor, multiplier, divider, z, y, x))
            .Fuse()
            .Rotate(RotateAxis.RotateY, 90)
            .Slice()
            .Select(X => X.Slice()
                          .Select(X1 => X1.Slice()
                                          .Select(X3 => X3.Resize(adder, subtractor, multiplier, divider, a))
                                          .Fuse())
                           .Fuse())
            .Fuse()
            .Rotate(RotateAxis.RotateY, -90);
    }

    #endregion

    #region Rotate (up to 4D - any type)

    /// <summary>
    /// Rotates a two dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array to rotate</param>
    /// <param name="angle">Degrees of rotation.</param>
    /// <returns>a rotated copy of source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Invalid angle.</exception>    
    public static T[,] Rotate<T>(this T[,] source, int angle)
    {
      Helper.InvalidateNullParam(source, "source");
      if (angle % 90 != 0)
        throw new ArgumentException(string.Format(Resources.excptionInvalidAngle, angle), "angle");

      int yBound = source.GetUpperBound(0);
      int xBound = source.GetUpperBound(1);
      int yLen = yBound + 1;
      int xLen = xBound + 1;

      T[,] dest = null;
      Action<int, int> cellSetter = null;

      angle = angle % 360;

      if (angle == 0)
      {
        dest = new T[yLen, xLen];
        System.Array.Copy(source, dest, source.Length);
        return dest;
      }

      switch (angle)
      {
        case 90:
        case -270:
          dest = new T[xLen, yLen];
          cellSetter = (y, x) => dest[x, yBound - y] = source[y, x];
          break;
        case 180:
        case -180:
          dest = new T[yLen, xLen];
          cellSetter = (y, x) => dest[yBound - y, xBound - x] = source[y, x];
          break;
        case 270:
        case -90:
          dest = new T[xLen, yLen];
          cellSetter = (y, x) => dest[xBound - x, y] = source[y, x];
          break;
      }

      for (int y = 0; y < yLen; y++)
        for (int x = 0; x < xLen; x++)
          cellSetter(y, x);

      return dest;
    }

    /// <summary>
    /// Rotates a three dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array to rotate</param>
    /// <param name="axis">The axis to perform the rotation on.</param>
    /// <param name="angle">Degrees of rotation.</param>
    /// <returns>a rotated copy of source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Invalid axis.</exception>    
    /// <exception cref="System.ArgumentException">Invalid angle.</exception>    
    public static T[, ,] Rotate<T>(this T[, ,] source, RotateAxis axis, int angle)
    {
      Helper.InvalidateNullParam(source, "source");
      if (axis != RotateAxis.RotateNone && axis != RotateAxis.RotateX && axis != RotateAxis.RotateY && axis != RotateAxis.RotateZ)
        throw new ArgumentException(string.Format(Resources.excption3DRotation, axis), "axis");
      if (angle % 90 != 0)
        throw new ArgumentException(string.Format(Resources.excptionInvalidAngle, angle), "angle");

      angle = angle % 360;

      if (angle == 0 || axis == RotateAxis.RotateNone)
      {
        T[, ,] dest = new T[source.GetLength(0), source.GetLength(1), source.GetLength(2)];
        System.Array.Copy(source, dest, source.Length);
        return dest;
      }

      switch (angle)
      {
        case 90:
        case -270:
          return Rotate3D090(source, axis);
        case 180:
        case -180:
          return Rotate3D180(source, axis);
        case 270:
        case -90:
          return Rotate3D270(source, axis);
        default:
          return null;
      }
    }

    #region Rotate 3D xxx

    private static T[, ,] Rotate3D090<T>(this T[, ,] source, RotateAxis axis)
    {
      int zBound = source.GetUpperBound(0);
      int yBound = source.GetUpperBound(1);
      int xBound = source.GetUpperBound(2);
      int zLen = zBound + 1;
      int yLen = yBound + 1;
      int xLen = xBound + 1;

      T[, ,] dest = null;
      Action<int, int, int> cellSetter = null;

      switch (axis)
      {
        case RotateAxis.RotateX:
          dest = new T[yLen, zLen, xLen];
          cellSetter = (z, y, x) => dest[y, zBound - z, x] = source[z, y, x];
          break;
        case RotateAxis.RotateY:
          dest = new T[xLen, yLen, zLen];
          cellSetter = (z, y, x) => dest[xBound - x, y, z] = source[z, y, x];
          break;
        case RotateAxis.RotateZ:
          dest = new T[zLen, xLen, yLen];
          cellSetter = (z, y, x) => dest[z, x, yBound - y] = source[z, y, x];
          break;
      }

      for (int z = 0; z < zLen; z++)
        for (int y = 0; y < yLen; y++)
          for (int x = 0; x < xLen; x++)
            cellSetter(z, y, x);

      return dest;
    }

    private static T[, ,] Rotate3D180<T>(this T[, ,] source, RotateAxis axis)
    {
      int zBound = source.GetUpperBound(0);
      int yBound = source.GetUpperBound(1);
      int xBound = source.GetUpperBound(2);
      int zLen = zBound + 1;
      int yLen = yBound + 1;
      int xLen = xBound + 1;

      T[, ,] dest = new T[zLen, yLen, xLen];
      Action<int, int, int> cellSetter = null;

      switch (axis)
      {
        case RotateAxis.RotateX:
          cellSetter = (z, y, x) => dest[zBound - z, yBound - y, x] = source[z, y, x];
          break;
        case RotateAxis.RotateY:
          cellSetter = (z, y, x) => dest[zBound - z, y, xBound - x] = source[z, y, x];
          break;
        case RotateAxis.RotateZ:
          cellSetter = (z, y, x) => dest[z, yBound - y, xBound - x] = source[z, y, x];
          break;
      }

      for (int z = 0; z < zLen; z++)
        for (int y = 0; y < yLen; y++)
          for (int x = 0; x < xLen; x++)
            cellSetter(z, y, x);

      return dest;
    }

    private static T[, ,] Rotate3D270<T>(this T[, ,] source, RotateAxis axis)
    {
      int zBound = source.GetUpperBound(0);
      int yBound = source.GetUpperBound(1);
      int xBound = source.GetUpperBound(2);
      int zLen = zBound + 1;
      int yLen = yBound + 1;
      int xLen = xBound + 1;

      T[, ,] dest = null;
      Action<int, int, int> cellSetter = null;


      switch (axis)
      {
        case RotateAxis.RotateX:
          dest = new T[yLen, zLen, xLen];
          cellSetter = (z, y, x) => dest[yBound - y, z, x] = source[z, y, x];
          break;
        case RotateAxis.RotateY:
          dest = new T[xLen, yLen, zLen];
          cellSetter = (z, y, x) => dest[x, y, zBound - z] = source[z, y, x];
          break;
        case RotateAxis.RotateZ:
          dest = new T[zLen, xLen, yLen];
          cellSetter = (z, y, x) => dest[z, xBound - x, y] = source[z, y, x];
          break;
      }

      for (int z = 0; z < zLen; z++)
        for (int y = 0; y < yLen; y++)
          for (int x = 0; x < xLen; x++)
            cellSetter(z, y, x);

      return dest;
    }

    #endregion

    /// <summary>
    /// Rotates a four dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array to rotate</param>
    /// <param name="axis">The axis to perform the rotation on.</param>
    /// <param name="angle">Degrees of rotation.</param>
    /// <returns>a rotated copy of source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Invalid axis.</exception>    
    /// <exception cref="System.ArgumentException">Invalid angle.</exception>    
    public static T[, , ,] Rotate<T>(this T[, , ,] source, RotateAxis axis, int angle)
    {
      Helper.InvalidateNullParam(source, "source");
      if (axis != RotateAxis.RotateNone && axis != RotateAxis.RotateX && axis != RotateAxis.RotateY && axis != RotateAxis.RotateZ && axis != RotateAxis.RotateA)
        throw new ArgumentException(string.Format(Resources.excption4DRotate, axis), "axis");
      if (angle % 90 != 0)
        throw new ArgumentException(string.Format(Resources.excptionInvalidAngle, angle), "angle");

      angle = angle % 540;

      if (angle == 0 || axis == RotateAxis.RotateNone)
      {
        T[, , ,] dest = new T[source.GetLength(0), source.GetLength(1), source.GetLength(2), source.GetLength(3)];
        System.Array.Copy(source, dest, source.Length);
        return dest;
      }

      switch (angle)
      {
        case 90:
        case -450:
          return Rotate4D090(source, axis);
        case 180:
        case -360:
          return Rotate4D180(source, axis);
        case 270:
        case -270:
          return Rotate4D270(source, axis);
        case 360:
        case -180:
          return Rotate4D360(source, axis);
        case 450:
        case -90:
          return Rotate4D450(source, axis);
        default:
          return null;
      }
    }

    #region Rotate 4D xxx

    private static T[, , ,] Rotate4D090<T>(this T[, , ,] source, RotateAxis axis)
    {
      int aBound = source.GetUpperBound(0);
      int zBound = source.GetUpperBound(1);
      int yBound = source.GetUpperBound(2);
      int xBound = source.GetUpperBound(3);
      int aLen = aBound + 1;
      int zLen = zBound + 1;
      int yLen = yBound + 1;
      int xLen = xBound + 1;

      T[, , ,] dest = null;
      Action<int, int, int, int> cellSetter = null;

      switch (axis)
      {
        case RotateAxis.RotateX:
          dest = new T[zLen, yLen, aLen, xLen];
          cellSetter = (a, z, y, x) => dest[z, y, aBound - a, x] = source[a, z, y, x];
          break;
        case RotateAxis.RotateY:
          dest = new T[zLen, xLen, yLen, aLen];
          cellSetter = (a, z, y, x) => dest[z, x, y, aBound - a] = source[a, z, y, x];
          break;
        case RotateAxis.RotateZ:
          dest = new T[yLen, zLen, xLen, aLen];
          cellSetter = (a, z, y, x) => dest[y, z, x, aBound - a] = source[a, z, y, x];
          break;
        case RotateAxis.RotateA:
          dest = new T[aLen, yLen, xLen, zLen];
          cellSetter = (a, z, y, x) => dest[a, y, x, zBound - z] = source[a, z, y, x];
          break;
      }

      for (int a = 0; a < aLen; a++)
        for (int z = 0; z < zLen; z++)
          for (int y = 0; y < yLen; y++)
            for (int x = 0; x < xLen; x++)
              cellSetter(a, z, y, x);

      return dest;
    }

    private static T[, , ,] Rotate4D180<T>(this T[, , ,] source, RotateAxis axis)
    {
      int aBound = source.GetUpperBound(0);
      int zBound = source.GetUpperBound(1);
      int yBound = source.GetUpperBound(2);
      int xBound = source.GetUpperBound(3);
      int aLen = aBound + 1;
      int zLen = zBound + 1;
      int yLen = yBound + 1;
      int xLen = xBound + 1;

      T[, , ,] dest = null;
      Action<int, int, int, int> cellSetter = null;

      switch (axis)
      {
        case RotateAxis.RotateX:
          dest = new T[yLen, aLen, zLen, xLen];
          cellSetter = (a, z, y, x) => dest[y, aBound - a, zBound - z, x] = source[a, z, y, x];
          break;
        case RotateAxis.RotateY:
          dest = new T[xLen, aLen, yLen, zLen];
          cellSetter = (a, z, y, x) => dest[x, aBound - a, y, zBound - z] = source[a, z, y, x];
          break;
        case RotateAxis.RotateZ:
          dest = new T[xLen, zLen, aLen, yLen];
          cellSetter = (a, z, y, x) => dest[x, z, aBound - a, yBound - y] = source[a, z, y, x];
          break;
        case RotateAxis.RotateA:
          dest = new T[aLen, xLen, zLen, yLen];
          cellSetter = (a, z, y, x) => dest[a, x, zBound - z, yBound - y] = source[a, z, y, x];
          break;
      }

      for (int a = 0; a < aLen; a++)
        for (int z = 0; z < zLen; z++)
          for (int y = 0; y < yLen; y++)
            for (int x = 0; x < xLen; x++)
              cellSetter(a, z, y, x);

      return dest;
    }

    private static T[, , ,] Rotate4D270<T>(this T[, , ,] source, RotateAxis axis)
    {
      int aBound = source.GetUpperBound(0);
      int zBound = source.GetUpperBound(1);
      int yBound = source.GetUpperBound(2);
      int xBound = source.GetUpperBound(3);
      int aLen = aBound + 1;
      int zLen = zBound + 1;
      int yLen = yBound + 1;
      int xLen = xBound + 1;

      T[, , ,] dest = new T[aLen, zLen, yLen, xLen];
      Action<int, int, int, int> cellSetter = null;

      switch (axis)
      {
        case RotateAxis.RotateX:
          cellSetter = (a, z, y, x) => dest[aBound - a, zBound - z, yBound - y, x] = source[a, z, y, x];
          break;
        case RotateAxis.RotateY:
          cellSetter = (a, z, y, x) => dest[aBound - a, zBound - z, y, xBound - x] = source[a, z, y, x];
          break;
        case RotateAxis.RotateZ:
          cellSetter = (a, z, y, x) => dest[aBound - a, z, yBound - y, xBound - x] = source[a, z, y, x];
          break;
        case RotateAxis.RotateA:
          cellSetter = (a, z, y, x) => dest[a, zBound - z, yBound - y, xBound - x] = source[a, z, y, x];
          break;
      }

      for (int a = 0; a < aLen; a++)
        for (int z = 0; z < zLen; z++)
          for (int y = 0; y < yLen; y++)
            for (int x = 0; x < xLen; x++)
              cellSetter(a, z, y, x);

      return dest;
    }

    private static T[, , ,] Rotate4D360<T>(this T[, , ,] source, RotateAxis axis)
    {
      int aBound = source.GetUpperBound(0);
      int zBound = source.GetUpperBound(1);
      int yBound = source.GetUpperBound(2);
      int xBound = source.GetUpperBound(3);
      int aLen = aBound + 1;
      int zLen = zBound + 1;
      int yLen = yBound + 1;
      int xLen = xBound + 1;

      T[, , ,] dest = null;
      Action<int, int, int, int> cellSetter = null;

      switch (axis)
      {
        case RotateAxis.RotateX:
          dest = new T[zLen, yLen, aLen, xLen];
          cellSetter = (a, z, y, x) => dest[zBound - z, yBound - y, a, x] = source[a, z, y, x];
          break;
        case RotateAxis.RotateY:
          dest = new T[zLen, xLen, yLen, aLen];
          cellSetter = (a, z, y, x) => dest[zBound - z, xBound - x, y, a] = source[a, z, y, x];
          break;
        case RotateAxis.RotateZ:
          dest = new T[yLen, zLen, xLen, aLen];
          cellSetter = (a, z, y, x) => dest[yBound - y, z, xBound - x, a] = source[a, z, y, x];
          break;
        case RotateAxis.RotateA:
          dest = new T[aLen, yLen, xLen, zLen];
          cellSetter = (a, z, y, x) => dest[a, yBound - y, xBound - x, z] = source[a, z, y, x];
          break;
      }

      for (int a = 0; a < aLen; a++)
        for (int z = 0; z < zLen; z++)
          for (int y = 0; y < yLen; y++)
            for (int x = 0; x < xLen; x++)
              cellSetter(a, z, y, x);

      return dest;
    }

    private static T[, , ,] Rotate4D450<T>(this T[, , ,] source, RotateAxis axis)
    {
      int aBound = source.GetUpperBound(0);
      int zBound = source.GetUpperBound(1);
      int yBound = source.GetUpperBound(2);
      int xBound = source.GetUpperBound(3);
      int aLen = aBound + 1;
      int zLen = zBound + 1;
      int yLen = yBound + 1;
      int xLen = xBound + 1;

      T[, , ,] dest = null;
      Action<int, int, int, int> cellSetter = null;

      switch (axis)
      {
        case RotateAxis.RotateX:
          dest = new T[yLen, aLen, zLen, xLen];
          cellSetter = (a, z, y, x) => dest[yBound - y, a, z, x] = source[a, z, y, x];
          break;
        case RotateAxis.RotateY:
          dest = new T[xLen, aLen, yLen, zLen];
          cellSetter = (a, z, y, x) => dest[xBound - x, a, y, z] = source[a, z, y, x];
          break;
        case RotateAxis.RotateZ:
          dest = new T[xLen, zLen, aLen, yLen];
          cellSetter = (a, z, y, x) => dest[xBound - x, z, a, y] = source[a, z, y, x];
          break;
        case RotateAxis.RotateA:
          dest = new T[aLen, xLen, zLen, yLen];
          cellSetter = (a, z, y, x) => dest[a, xBound - x, z, y] = source[a, z, y, x];
          break;
      }

      for (int a = 0; a < aLen; a++)
        for (int z = 0; z < zLen; z++)
          for (int y = 0; y < yLen; y++)
            for (int x = 0; x < xLen; x++)
              cellSetter(a, z, y, x);

      return dest;
    }

    #endregion

    #endregion

    #region Flip (up to 3D - any type)

    /// <summary>
    /// Flips elements in an array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array to flip</param>
    /// <returns>A flipped copy of the source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static T[] Flip<T>(this T[] source)
    {
      Helper.InvalidateNullParam(source, "source");

      int len = source.Length;
      T[] dest = new T[len];

      System.Array.Copy(source, dest, source.Length);
      System.Array.Reverse(dest);

      return dest;
    }

    /// <summary>
    /// Flips elements in an array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array to flip</param>
    /// <param name="axis">The axis to flip on.  Axis flag can combine multiple valid axis</param>
    /// <returns>A flipped copy of the source array.</returns> 
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Invalid axis.</exception>    
    public static T[,] Flip<T>(this T[,] source, FlipAxis axis)
    {
      Helper.InvalidateNullParam(source, "source");

      int axisValue = (int)axis;
      if (axisValue < 0 || axisValue > 3)
        throw new ArgumentException(string.Format(Resources.excption2DFlip, axis), "axis");

      T[,] dest;

      switch (axis)
      {
        case FlipAxis.FlipX:
          dest = source.Slice().Select(x => x.Flip()).Fuse();
          break;
        case FlipAxis.FlipY:
          dest = source.Rotate(90).Flip(FlipAxis.FlipX).Rotate(-90);
          break;
        case FlipAxis.FlipXY:
          dest = source.Flip(FlipAxis.FlipX).Flip(FlipAxis.FlipY);
          break;
        default:
          dest = (T[,])System.Array.CreateInstance(typeof(T), source.GetDimensions());
          System.Array.Copy(source, dest, source.Length);
          break;
      }
      return dest;
    }

    /// <summary>
    /// Flips elements in an array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array to flip</param>
    /// <param name="axis">The axis to flip on.  Axis flag can combine multiple valid axis</param>
    /// <returns>A flipped copy of the source array.</returns>    
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">Invalid axis.</exception>    
    public static T[, ,] Flip<T>(this T[, ,] source, FlipAxis axis)
    {
      Helper.InvalidateNullParam(source, "source");

      int axisValue = (int)axis;
      if (axisValue < 0 || axisValue > 7)
        throw new ArgumentException(string.Format(Resources.excption3DFlip, axis), "axis");

      T[, ,] dest;

      switch (axis)
      {
        case FlipAxis.FlipX:
          dest = source.Slice().Select(x => x.Slice().Select(x1 => x1.Flip()).Fuse()).Fuse();
          break;
        case FlipAxis.FlipY:
          dest = source.Rotate(RotateAxis.RotateZ, 90).Flip(FlipAxis.FlipX).Rotate(RotateAxis.RotateZ, -90);
          break;
        case FlipAxis.FlipXY:
          dest = source.Flip(FlipAxis.FlipX).Flip(FlipAxis.FlipY);
          break;
        case FlipAxis.FlipZ:
          dest = source.Rotate(RotateAxis.RotateY, 90).Flip(FlipAxis.FlipX).Rotate(RotateAxis.RotateY, -90);
          break;
        case FlipAxis.FlipXZ:
          dest = source.Flip(FlipAxis.FlipX).Flip(FlipAxis.FlipZ);
          break;
        case FlipAxis.FlipYZ:
          dest = source.Flip(FlipAxis.FlipY).Flip(FlipAxis.FlipZ);
          break;
        case FlipAxis.FlipXYZ:
          dest = source.Flip(FlipAxis.FlipX).Flip(FlipAxis.FlipY).Flip(FlipAxis.FlipZ);
          break;
        default:
          dest = (T[, ,])System.Array.CreateInstance(typeof(T), source.GetDimensions());
          System.Array.Copy(source, dest, source.Length);
          break;
      }

      return dest;
    }

    #endregion

    #region Replace and Extract (up to 4D - any type)

    /// <summary>
    /// Replaces a section in one array with content from another array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="newSection">An array with values used to replace content of source array.</param>
    /// <param name="offset">Zero based offset to start the replacement.</param>
    /// <exception cref="System.ArgumentNullException">source or newSection is null.</exception>
    /// <exception cref="System.ArgumentException">offset is out of range.</exception>        
    public static void Replace<T>(this T[] source, T[] newSection, int offset)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(source, "newSection");

      int xNewSectionLen = newSection.GetLength(0);
      int xSourceLen = source.GetLength(0);

      Helper.InvalidateNumericRange(offset, 0, xSourceLen - xNewSectionLen, "offset");

      System.Array.Copy(newSection, 0, source, offset, xNewSectionLen);
    }

    /// <summary>
    /// Replaces a section in one array with content from another array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="newSection">An array with values used to replace content of source array.</param> 
    /// <param name="yOffset">Zero based offset from the Y origin to start the replacement.</param>
    /// <param name="xOffset">Zero based offset from the X origin to start the replacement.</param>
    /// <exception cref="System.ArgumentNullException">source or newSection is null.</exception>
    /// <exception cref="System.ArgumentException">yOffset or xOffset is out of range.</exception>        
    public static void Replace<T>(this T[,] source, T[,] newSection, int yOffset, int xOffset)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(source, "newSection");

      int yNewSectionLen = newSection.GetLength(0);
      int xNewSectionLen = newSection.GetLength(1);

      int ySourceLen = source.GetLength(0);
      int xSourceLen = source.GetLength(1);

      Helper.InvalidateNumericRange(yOffset, 0, ySourceLen - yNewSectionLen, "yOffset");
      Helper.InvalidateNumericRange(xOffset, 0, xSourceLen - xNewSectionLen, "xOffset");

      int newOffset = yOffset * xSourceLen + xOffset;

      for (int y = 0; y < yNewSectionLen; y++)
        System.Array.Copy(newSection, y * xNewSectionLen, source, y * xSourceLen + newOffset, xNewSectionLen);
    }

    /// <summary>
    /// Replaces a section in one array with content from another array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="newSection">An array with values used to replace content of source array.</param> 
    /// <param name="zOffset">Zero based offset from the Z origin to start the replacement.</param>
    /// <param name="yOffset">Zero based offset from the Y origin to start the replacement.</param>
    /// <param name="xOffset">Zero based offset from the X origin to start the replacement.</param>
    /// <exception cref="System.ArgumentNullException">source or newSection is null.</exception>
    /// <exception cref="System.ArgumentException">zOffset, yOffset or xOffset is out of range.</exception>        
    public static void Replace<T>(this T[, ,] source, T[, ,] newSection, int zOffset, int yOffset, int xOffset)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(source, "newSection");

      int zNewSectionLen = newSection.GetLength(0);
      int yNewSectionLen = newSection.GetLength(1);
      int xNewSectionLen = newSection.GetLength(2);

      int zSourceLen = source.GetLength(0);
      int ySourceLen = source.GetLength(1);
      int xSourceLen = source.GetLength(2);

      Helper.InvalidateNumericRange(zOffset, 0, zSourceLen - zNewSectionLen, "zOffset");
      Helper.InvalidateNumericRange(yOffset, 0, ySourceLen - yNewSectionLen, "yOffset");
      Helper.InvalidateNumericRange(xOffset, 0, xSourceLen - xNewSectionLen, "xOffset");

      int xySourceArea = ySourceLen * xSourceLen;
      int newOffset = zOffset * xySourceArea + yOffset * xSourceLen + xOffset;

      int xyNewArea = yNewSectionLen * xNewSectionLen;

      for (int z = 0; z < zNewSectionLen; z++)
        for (int y = 0; y < yNewSectionLen; y++)
          System.Array.Copy(newSection, z * xyNewArea + y * xNewSectionLen, source, z * xySourceArea + y * xSourceLen + newOffset, xNewSectionLen);
    }

    /// <summary>
    /// Replaces a section in one array with content from another array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="newSection">An array with values used to replace content of source array.</param> 
    /// <param name="zOffset">Zero based offset from the Z origin to start the replacement.</param>
    /// <param name="aOffset">Zero based offset from the A origin to start the replacement.</param>
    /// <param name="yOffset">Zero based offset from the Y origin to start the replacement.</param>
    /// <param name="xOffset">Zero based offset from the X origin to start the replacement.</param>
    /// <exception cref="System.ArgumentNullException">source or newSection is null.</exception>
    /// <exception cref="System.ArgumentException">aOffset, zOffset, yOffset or xOffset is out of range.</exception>        
    public static void Replace<T>(this T[, , ,] source, T[, , ,] newSection, int aOffset, int zOffset, int yOffset, int xOffset)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(source, "newSection");

      int aNewSectionLen = newSection.GetLength(0);
      int zNewSectionLen = newSection.GetLength(1);
      int yNewSectionLen = newSection.GetLength(2);
      int xNewSectionLen = newSection.GetLength(3);

      int aSourceLen = source.GetLength(0);
      int zSourceLen = source.GetLength(1);
      int ySourceLen = source.GetLength(2);
      int xSourceLen = source.GetLength(3);

      Helper.InvalidateNumericRange(aOffset, 0, aSourceLen - aNewSectionLen, "aOffset");
      Helper.InvalidateNumericRange(zOffset, 0, zSourceLen - zNewSectionLen, "zOffset");
      Helper.InvalidateNumericRange(yOffset, 0, ySourceLen - yNewSectionLen, "yOffset");
      Helper.InvalidateNumericRange(xOffset, 0, xSourceLen - xNewSectionLen, "xOffset");

      int xySourceArea = ySourceLen * xSourceLen;
      int xyzSourceVol = zSourceLen * xySourceArea;

      int newOffset = aOffset * xyzSourceVol + zOffset * xySourceArea + yOffset * xSourceLen + xOffset;

      int xyNewArea = yNewSectionLen * xNewSectionLen;
      int xyzNewVol = zNewSectionLen * xyNewArea;

      for (int a = 0; a < aNewSectionLen; a++)
        for (int z = 0; z < zNewSectionLen; z++)
          for (int y = 0; y < yNewSectionLen; y++)
            System.Array.Copy(newSection, a * xyzNewVol + z * xyNewArea + y * xNewSectionLen, source, a * xyzSourceVol + z * xySourceArea + y * xSourceLen + newOffset, xNewSectionLen);
    }

    /// <summary>
    /// Extract an array of values from the source array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="offset">Zero based offset to start extraction from.</param>
    /// <param name="length">Number of elements to extract.</param>
    /// <returns>An array with values from the source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">offset is out of range.</exception>        
    public static T[] Extract<T>(this T[] source, int offset, int length)
    {
      Helper.InvalidateNullParam(source, "source");

      int xSourceLen = source.GetLength(0);
      Helper.InvalidateNumericRange(offset, 0, xSourceLen - length, "offset");

      T[] dest = new T[length];

      System.Array.Copy(source, offset, dest, 0, length);

      return dest;
    }

    /// <summary>
    /// Extract an array of values from the source array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>    
    /// <param name="yOffset">Zero based offset to start extraction from Y origin.</param>
    /// <param name="xOffset">Zero based offset to start extraction from X origin.</param>    
    /// <param name="yLength">Number of elements to extract on the Y axis.</param>
    /// <param name="xLength">Number of elements to extract on the X axis.</param> 
    /// <returns>An array with values from the source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">yOffset or xOffset is out of range.</exception>        
    public static T[,] Extract<T>(this T[,] source, int yOffset, int xOffset, int yLength, int xLength)
    {
      Helper.InvalidateNullParam(source, "source");

      int ySourceLen = source.GetLength(0);
      int xSourceLen = source.GetLength(1);
      Helper.InvalidateNumericRange(yOffset, 0, ySourceLen - yLength, "yOffset");
      Helper.InvalidateNumericRange(xOffset, 0, xSourceLen - xLength, "xOffset");

      T[,] dest = new T[yLength, xLength];

      int offset = yOffset * xSourceLen + xOffset;

      for (int y = 0; y < yLength; y++)
        System.Array.Copy(source, offset + y * xSourceLen, dest, y * xLength, xLength);

      return dest;
    }

    /// <summary>
    /// Extract an array of values from the source array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>                
    /// <param name="zOffset">Zero based offset to start extraction from Z origin.</param>
    /// <param name="yOffset">Zero based offset to start extraction from Y origin.</param>
    /// <param name="xOffset">Zero based offset to start extraction from X origin.</param>        
    /// <param name="zLength">Number of elements to extract on the Z axis.</param>
    /// <param name="yLength">Number of elements to extract on the Y axis.</param>
    /// <param name="xLength">Number of elements to extract on the X axis.</param>
    /// <returns>An array with values from the source array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">zOffset, yOffset or xOffset is out of range.</exception>        
    public static T[, ,] Extract<T>(this T[, ,] source, int zOffset, int yOffset, int xOffset, int zLength, int yLength, int xLength)
    {
      Helper.InvalidateNullParam(source, "source");

      int zSourceLen = source.GetLength(0);
      int ySourceLen = source.GetLength(1);
      int xSourceLen = source.GetLength(2);

      Helper.InvalidateNumericRange(zOffset, 0, zSourceLen - zLength, "zOffset");
      Helper.InvalidateNumericRange(yOffset, 0, ySourceLen - yLength, "yOffset");
      Helper.InvalidateNumericRange(xOffset, 0, xSourceLen - xLength, "xOffset");

      T[, ,] dest = new T[zLength, yLength, xLength];

      int xySourceArea = ySourceLen * xSourceLen;
      int xyDestArea = yLength * xLength;
      int offset = zOffset * xySourceArea + yOffset * xSourceLen + xOffset;

      for (int z = 0; z < zLength; z++)
        for (int y = 0; y < yLength; y++)
          System.Array.Copy(source, offset + z * xySourceArea + y * xSourceLen, dest, z * xyDestArea + y * xLength, xLength);

      return dest;
    }

    /// <summary>
    /// Extract an array of values from the source array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">The source array.</param>            
    /// <param name="aOffset">Zero based offset to start extraction from A origin.</param>
    /// <param name="zOffset">Zero based offset to start extraction from Z origin.</param>
    /// <param name="yOffset">Zero based offset to start extraction from Y origin.</param>
    /// <param name="xOffset">Zero based offset to start extraction from X origin.</param>    
    /// <param name="aLength">Number of elements to extract on the A axis.</param>
    /// <param name="zLength">Number of elements to extract on the Z axis.</param>
    /// <param name="yLength">Number of elements to extract on the Y axis.</param>
    /// <param name="xLength">Number of elements to extract on the X axis.</param>
    /// <returns>An array with values from the source array.</returns>    
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    /// <exception cref="System.ArgumentException">aOffset, zOffset, yOffset or xOffset is out of range.</exception>        
    public static T[, , ,] Extract<T>(this T[, , ,] source, int aOffset, int zOffset, int yOffset, int xOffset, int aLength, int zLength, int yLength, int xLength)
    {
      Helper.InvalidateNullParam(source, "source");

      int aSourceLen = source.GetLength(0);
      int zSourceLen = source.GetLength(1);
      int ySourceLen = source.GetLength(2);
      int xSourceLen = source.GetLength(3);

      Helper.InvalidateNumericRange(aOffset, 0, aSourceLen - aLength, "aOffset");
      Helper.InvalidateNumericRange(zOffset, 0, zSourceLen - zLength, "zOffset");
      Helper.InvalidateNumericRange(yOffset, 0, ySourceLen - yLength, "yOffset");
      Helper.InvalidateNumericRange(xOffset, 0, xSourceLen - xLength, "xOffset");

      T[, , ,] dest = new T[aLength, zLength, yLength, xLength];

      int xySourceArea = ySourceLen * xSourceLen;
      int xyzSourceVol = zSourceLen * xySourceArea;
      int xyDestArea = yLength * xLength;
      int xyzDestVol = zLength * xyDestArea;
      int offset = aOffset * xyzSourceVol + zOffset * xySourceArea + yOffset * xSourceLen + xOffset;

      for (int a = 0; a < aLength; a++)
        for (int z = 0; z < zLength; z++)
          for (int y = 0; y < yLength; y++)
            System.Array.Copy(source, offset + a * xyzSourceVol + z * xySourceArea + y * xSourceLen, dest, a * xyzDestVol + z * xyDestArea + y * xLength, xLength);

      return dest;
    }

    #endregion

    #region Enumerable and Equality extensions

    /// <summary>
    /// Converts an array to a sequence implementing IEnumerable. 
    /// </summary>
    /// <param name="source">The array to convert.</param>
    /// <returns>A sequence of all array elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>    
    public static IEnumerable AsEnumerable(this System.Array source)
    {
      Helper.InvalidateNullParam(source, "source");

      IEnumerator enumerator = source.GetEnumerator();
      while (enumerator.MoveNext())
        if (enumerator.Current.GetType().IsArray)
          foreach (object item in ((System.Array)enumerator.Current).AsEnumerable())
            yield return item;
        else
          yield return enumerator.Current;
    }

    /// <summary>
    /// Converts an array to a sequence implementing IEnumerable. 
    /// </summary>
    /// <typeparam name="T">The type of the elements of the source array.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <returns>A generic sequence of all array elements.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<T> AsEnumerable<T>(this System.Array source)
    {
      Helper.InvalidateNullParam(source, "source");

      IEnumerator enumerator = source.GetEnumerator();
      while (enumerator.MoveNext())
        if (enumerator.Current.GetType().IsArray)
          foreach (T item in ((System.Array)enumerator.Current).AsEnumerable<T>())
            yield return item;
        else
          yield return (T)enumerator.Current;
    }

    /// <summary>
    /// Checks if members form one array are equals to members from a second array.
    /// </summary>
    /// <param name="source">First Array to compare.</param>
    /// <param name="other">Second Array to compare.</param>
    /// <returns>True if arrays size and content are identical; false otherwise.</returns>
    /// <exception cref="System.ArgumentNullException">source or other is null.</exception>    
    public static bool ArrayEquals(this System.Array source, System.Array other)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(other, "other");

      return source.ArrayEquals(other, new SimpleEqualityComparer<object>());
    }

    /// <summary>
    /// Checks if members form one array are equals to members from a second array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source array.</typeparam>
    /// <param name="source">First Array to compare.</param>
    /// <param name="other">Second Array to compare.</param>
    /// <param name="comparer">An equality comparer used to compare the array elements.</param>
    /// <returns>True if arrays size and content are identical; false otherwise.</returns>
    /// <exception cref="System.ArgumentNullException">source, other or comparer is null.</exception>    
    public static bool ArrayEquals<T>(this System.Array source, System.Array other, IEqualityComparer<T> comparer)
    {
      Helper.InvalidateNullParam(source, "source");
      Helper.InvalidateNullParam(other, "other");
      Helper.InvalidateNullParam(comparer, "comparer");

      int rank = source.Rank;

      if (other.Rank != rank)
        return false;

      for (int i = 0; i < rank; i++)
        if (source.GetUpperBound(i) != other.GetUpperBound(i) || source.GetLowerBound(i) != other.GetLowerBound(i))
          return false;

      IEnumerator xRator = source.GetEnumerator();
      IEnumerator yRator = other.GetEnumerator();

      while (xRator.MoveNext())
      {
        yRator.MoveNext();
        if (!comparer.Equals((T)xRator.Current, (T)yRator.Current))
          return false;
      }

      return true;
    }

    #endregion

    #region Jagged / Fixed Converters

    /// <summary>
    /// Returns an array with the size of each dimension of the supplied array.
    /// </summary>
    /// <param name="array">The array to operate on.</param>
    /// <returns>Array of dimensions.</returns>
    /// <exception cref="System.ArgumentNullException">array is null.</exception>        
    public static int[] GetDimensions(this System.Array array)
    {
      Helper.InvalidateNullParam(array, "array");
      int rank = array.Rank;
      int[] dimensions = new int[rank];
      for (int i = 0; i < rank; i++)
        dimensions[i] = array.GetLength(i);

      return dimensions;
    }

    /// <summary>
    /// Converts a two dimensional array into a jagged array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <returns>A jagged array.</returns>
    public static T[][] ToJagged<T>(this T[,] source)
    {
      return ToJagged(source, true);
    }

    /// <summary>
    /// Converts a two dimensional array into a jagged array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <param name="stopOnDefault">Indicates if trailing default value elements should be included.</param>
    /// <returns>A jagged array.</returns>
    public static T[][] ToJagged<T>(this T[,] source, bool stopOnDefault)
    {
      Helper.InvalidateNullParam(source, "source");

      int y = source.GetLength(0);
      T[][] result = new T[y][];
      int y1 = 0;

      if (stopOnDefault)
      {
        T defaultValue = default(T);
        foreach (T[] item in source.Slice())
        {
          result[y1] = item.Reverse().SkipWhile(X => object.Equals(X, defaultValue)).Reverse().ToArray();
          y1++;
        }
      }
      else
        foreach (T[] item in source.Slice())
        {
          result[y1] = item.ToArray();
          y1++;
        }
      return result;
    }

    /// <summary>
    /// Converts a three dimensional array into a jagged array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <returns>A jagged array.</returns>
    public static T[][][] ToJagged<T>(this T[, ,] source)
    {
      return ToJagged(source, true);
    }

    /// <summary>
    /// Converts a three dimensional array into a jagged array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <param name="stopOnDefault">Indicates if trailing default value elements should be included.</param>
    /// <returns>A jagged array.</returns>
    public static T[][][] ToJagged<T>(this T[, ,] source, bool stopOnDefault)
    {
      Helper.InvalidateNullParam(source, "source");

      int z = source.GetLength(0);
      T[][][] result = new T[z][][];
      int z1 = 0;

      if (stopOnDefault)
      {
        T defaultValue = default(T);
        foreach (T[,] item in source.Slice())
        {
          var itemSliced = item.Slice().ToArray();
          int y = itemSliced.Reverse().SkipWhile(X => X.All(X1 => object.Equals(X1, defaultValue))).Count();
          result[z1] = new T[y][];
          for (int y1 = 0; y1 < y; y1++)
            result[z1][y1] = itemSliced[y1].Reverse().SkipWhile(X => object.Equals(X, defaultValue)).Reverse().ToArray();

          z1++;
        }
      }
      else
        foreach (T[,] item in source.Slice())
        {
          result[z1] = item.ToJagged(false);
          z1++;
        }
      return result;
    }

    /// <summary>
    /// Converts a four dimensional array into a jagged array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <returns>A jagged array.</returns>
    public static T[][][][] ToJagged<T>(this T[, , ,] source)
    {
      return ToJagged(source, true);
    }

    /// <summary>
    /// Converts a four dimensional array into a jagged array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <param name="stopOnDefault">Indicates if trailing default value elements should be included.</param>
    /// <returns>A jagged array.</returns>
    public static T[][][][] ToJagged<T>(this T[, , ,] source, bool stopOnDefault)
    {
      Helper.InvalidateNullParam(source, "source");

      int a = source.GetLength(0);
      T[][][][] result = new T[a][][][];
      int a1 = 0;

      if (stopOnDefault)
      {
        T defaultValue = default(T);
        foreach (T[, ,] item in source.Slice())
        {
          var itemSliced = item.Slice().ToArray();
          int z = itemSliced.Reverse().SkipWhile(X => X.AsEnumerable<T>().All(X1 => object.Equals(X1, defaultValue))).Count();
          result[a1] = new T[z][][];
          for (int z1 = 0; z1 < z; z1++)
          {
            var itemSliced2 = itemSliced[z1].Slice().ToArray();
            int y = itemSliced2.Reverse().SkipWhile(X => X.All(X1 => object.Equals(X1, defaultValue))).Count();
            result[a1][z1] = new T[y][];
            for (int y1 = 0; y1 < y; y1++)
            {
              result[a1][z1][y1] = itemSliced2[y1].Reverse().SkipWhile(X => object.Equals(X, defaultValue)).Reverse().ToArray();
            }
          }
          a1++;
        }
      }
      else
        foreach (T[, ,] item in source.Slice())
        {
          result[a1] = item.ToJagged(false);
          a1++;
        }
      return result;
    }

    /// <summary>
    /// Converts a jagged array into a two dimensional array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>    
    /// <returns>A two dimensional array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>    
    public static T[,] FromJagged<T>(this T[][] source)
    {
      Helper.InvalidateNullParam(source, "source");

      int y = source.Length;
      int x = source.Where(S => S != null).Max(S => S.Length);

      return source.FromJagged(y, x, y, x);
    }

    /// <summary>
    /// Converts a jagged array into a two dimensional array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>    
    /// <param name="y">Max number of elements to convert on the Y axis.</param>
    /// <param name="x">Max number of elements to convert on the X axis.</param>
    /// <returns>A two dimensional array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    [CLSCompliant(false)]
    public static T[,] FromJagged<T>(this T[][] source, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");

      int inputY = source.Length;
      int inputX = source.Where(S => S != null).Max(S => S.Length);

      return source.FromJagged(y, x, inputY, inputX);
    }

    private static T[,] FromJagged<T>(this T[][] source, int outputY, int outputX, int inputY, int inputX)
    {
      Helper.InvalidateNullParam(source, "source");

      T defaultValue = default(T);

      T[,] result = Enumerator.Generate(outputY * outputX, () => defaultValue).ToArray(outputY, outputX);

      if (inputY > outputY)
        inputY = outputY;

      for (int y1 = 0; y1 < inputY; y1++)
        if (source[y1] != null)
        {
          int tempLenX = source[y1].Length;
          if (tempLenX > outputX)
            tempLenX = outputX;
          if (tempLenX > inputX)
            tempLenX = inputX;

          for (int x1 = 0; x1 < tempLenX; x1++)
            result[y1, x1] = source[y1][x1];
        }
      return result;
    }

    /// <summary>
    /// Converts a jagged array into a three dimensional array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <returns>A three dimensional array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>            
    [CLSCompliant(false)]
    public static T[, ,] FromJagged<T>(this T[][][] source)
    {
      Helper.InvalidateNullParam(source, "source");

      int z = source.Length;
      int y = source.Where(S => S != null).Max(S => S.Length);
      int x = (from s3 in source.Where(S => S != null)
               from s2 in s3.Where(S => S != null)
               select s2.Length).Max();

      return source.FromJagged(z, y, x, z, y, x);
    }

    /// <summary>
    /// Converts a jagged array into a three dimensional array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>    
    /// <param name="z">Max number of elements to convert on the Z axis.</param>
    /// <param name="y">Max number of elements to convert on the Y axis.</param>
    /// <param name="x">Max number of elements to convert on the X axis.</param>
    /// <returns>A three dimensional array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>            
    public static T[, ,] FromJagged<T>(this T[][][] source, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");

      int inputZ = source.Length;
      int inputY = source.Where(S => S != null).Max(S => S.Length);
      int inputX = (from s3 in source.Where(S => S != null)
                    from s2 in s3.Where(S => S != null)
                    select s2.Length).Max();

      return source.FromJagged(z, y, x, inputZ, inputY, inputX);
    }

    private static T[, ,] FromJagged<T>(this T[][][] source, int outputZ, int outputY, int outputX, int inputZ, int inputY, int inputX)
    {
      Helper.InvalidateNullParam(source, "source");

      T defaultValue = default(T);

      T[, ,] result = Enumerator.Generate(outputZ * outputY * outputX, () => defaultValue).ToArray(outputZ, outputY, outputX);

      if (inputZ > outputZ)
        inputZ = outputZ;

      for (int z1 = 0; z1 < inputZ; z1++)
        if (source[z1] != null)
        {
          int tempLenY = source[z1].Length;
          if (tempLenY > outputY)
            tempLenY = outputY;
          if (tempLenY > inputY)
            tempLenY = inputY;

          for (int y1 = 0; y1 < tempLenY; y1++)
            if (source[z1][y1] != null)
            {
              int tempLenX = source[z1][y1].Length;
              if (tempLenX > outputX)
                tempLenX = outputX;
              if (tempLenX > inputX)
                tempLenX = inputX;

              for (int x1 = 0; x1 < tempLenX; x1++)
                result[z1, y1, x1] = source[z1][y1][x1];
            }
        }
      return result;
    }

    /// <summary>
    /// Converts a jagged array into a four dimensional array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <returns>A four dimensional array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>    
    [CLSCompliant(false)]
    public static T[, , ,] FromJagged<T>(this T[][][][] source)
    {
      Helper.InvalidateNullParam(source, "source");

      int a = source.Length;
      int z = source.Where(S => S != null).Max(S => S.Length);
      int y = (from s3 in source.Where(S => S != null)
               from s2 in s3.Where(S => S != null)
               select s2.Length).Max();
      int x = (from s4 in source.Where(S => S != null)
               from s3 in s4.Where(S => S != null)
               from s2 in s3.Where(S => S != null)
               select s2.Length).Max();

      return source.FromJagged(a, z, y, x, a, z, y, x);
    }

    /// <summary>
    /// Converts a jagged array into a four dimensional array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="source">The array to convert.</param>
    /// <param name="a">Max number of elements to convert on the A axis.</param>
    /// <param name="z">Max number of elements to convert on the Z axis.</param>
    /// <param name="y">Max number of elements to convert on the Y axis.</param>
    /// <param name="x">Max number of elements to convert on the X axis.</param>
    /// <returns>A four dimensional array.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>    
    public static T[, , ,] FromJagged<T>(this T[][][][] source, int a, int z, int y, int x)
    {
      Helper.InvalidateNullParam(source, "source");

      int inputA = source.Length;
      int inputZ = source.Where(S => S != null).Max(S => S.Length);
      int inputY = (from s3 in source.Where(S => S != null)
                    from s2 in s3.Where(S => S != null)
                    select s2.Length).Max();
      int inputX = (from s4 in source.Where(S => S != null)
                    from s3 in s4.Where(S => S != null)
                    from s2 in s3.Where(S => S != null)
                    select s2.Length).Max();

      return source.FromJagged(a, z, y, x, inputA, inputZ, inputY, inputX);
    }

    private static T[, , ,] FromJagged<T>(this T[][][][] source, int outputA, int outputZ, int outputY, int outputX, int inputA, int inputZ, int inputY, int inputX)
    {
      Helper.InvalidateNullParam(source, "source");

      T defaultValue = default(T);

      T[, , ,] result = Enumerator.Generate(outputA * outputZ * outputY * outputX, () => defaultValue).ToArray(outputA, outputZ, outputY, outputX);

      if (inputA > outputA)
        inputA = outputA;

      for (int a1 = 0; a1 < inputA; a1++)
        if (source[a1] != null)
        {
          int tempLenZ = source[a1].Length;
          if (tempLenZ > outputZ)
            tempLenZ = outputZ;
          if (tempLenZ > inputZ)
            tempLenZ = inputZ;

          for (int z1 = 0; z1 < tempLenZ; z1++)
            if (source[a1][z1] != null)
            {
              int tempLenY = source[a1][z1].Length;
              if (tempLenY > outputY)
                tempLenY = outputY;
              if (tempLenY > inputY)
                tempLenY = inputY;

              for (int y1 = 0; y1 < tempLenY; y1++)
                if (source[a1][z1][y1] != null)
                {
                  int tempLenX = source[a1][z1][y1].Length;
                  if (tempLenX > outputX)
                    tempLenX = outputX;
                  if (tempLenX > inputX)
                    tempLenX = inputX;

                  for (int x1 = 0; x1 < tempLenX; x1++)
                    result[a1, z1, y1, x1] = source[a1][z1][y1][x1];
                }
            }
        }
      return result;
    }

    //
    //public static T[, , ,] FromJagged<T>(this T[][][][] source)
    //{
    //  Helper.InvalidateNullParam(source, "source");
    //  T defaultValue = default(T);

    //  int a = source.Length;
    //  int z = source.Max(S => S.Length);
    //  int y = (from A in source
    //           from A1 in A
    //           select A1).Max(X => X.Length);
    //  int x = (from A in source
    //           from A1 in A
    //           from A2 in A1
    //           select A2).Max(X => X.Length);

    //  T[, , ,] result = Enumerator.Generate(a * z * y * x, () => defaultValue).ToArray(a, z, y, x);

    //  for (int a1 = 0; a1 < a; a1++)
    //  {
    //    int tempLenZ = source[a1].Length;
    //    for (int z1 = 0; z1 < tempLenZ; z1++)
    //    {
    //      int tempLenY = source[a1][z1].Length;
    //      for (int y1 = 0; y1 < tempLenY; y1++)
    //      {
    //        int tempLenX = source[a1][z1][y1].Length;
    //        for (int x1 = 0; x1 < tempLenX; x1++)
    //          result[a1, z1, y1, x1] = source[a1][z1][y1][x1];
    //      }
    //    }
    //  }

    //  return result;
    //}
    #endregion

    #region Helper Class

    private static class NumericHelper
    {
      internal static int IntegerPart(double value)
      {
        return (int)value;
      }

      internal static double FractionPart(double value)
      {
        return value - (int)value;
      }

      internal static bool AreClose(double value1, double value2)
      {
        return value1 + .000001 > value2 && value1 - .000001 < value2;
      }
    }

    #endregion
  }
}