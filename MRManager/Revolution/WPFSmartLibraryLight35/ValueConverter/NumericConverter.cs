//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: NumericConverter.cs
//		namespace	: SoftArcs.WPFSmartLibrary.ValueConverter
//		class(es)	: DoubleToEmptyStringConverter
//						  IntToEmptyStringConverter
//						  IntToStringConverter
//						  IntToBrushConverter
//							
//	--------------------------------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace SoftArcs.WPFSmartLibrary.ValueConverter
{
	/// <summary>
	/// Converts a Double value into a String resp. an empty String if the Double value is zero (and back)
	/// </summary>
	[ValueConversion( typeof( double ), typeof( string ) )]
	[MarkupExtensionReturnType( typeof( DoubleToEmptyStringConverter ) )]
	public class DoubleToEmptyStringConverter : MarkupExtension, IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double)
			{
				double doubleValue = (double)value;

				if (doubleValue.Equals( 0.0 ))
				{
					return String.Empty;
				}

				if (parameter is string)
				{
					return doubleValue.ToString( parameter.ToString() );
				}

				return doubleValue.ToString( "N1" );
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string)
			{
				double doubleValue;
				string str = value.ToString();

				if (Double.TryParse( str, out doubleValue ) == true)
				{
					return System.Convert.ToDouble( str );
				}
			}

			return value;
		}

		#endregion

		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new DoubleToEmptyStringConverter();
		}

		#endregion
	}

	/// <summary>
	/// Converts an Integer value into a String resp. an empty String if the Integer value is zero (and back)
	/// ! Obsolete : It is recommended to use the more flexible "IntToStringConverter" instead
	/// </summary>
	[ValueConversion( typeof( int ), typeof( string ) )]
	[MarkupExtensionReturnType( typeof( IntToEmptyStringConverter ) )]
	public class IntToEmptyStringConverter : MarkupExtension, IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int)
			{
				int intValue = (int)value;

				if (intValue.Equals( 0 ))
				{
					return String.Empty;
				}

				return intValue.ToString();
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string)
			{
				int intValue;
				string str = value.ToString();

				if (Int32.TryParse( str, out intValue ) == true)
				{
					return System.Convert.ToInt32( str );
				}
			}

			return value;
		}

		#endregion

		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new IntToEmptyStringConverter();
		}

		#endregion
	}

	/// <summary>
	/// Converts an Integer value into a String resp. an empty String if the Integer value is zero (and back)
	/// The string for the zero value can be declared with the "ZeroValueString" property
	/// </summary>
	[ValueConversion( typeof( int ), typeof( string ) )]
	[MarkupExtensionReturnType( typeof( IntToStringConverter ) )]
	public class IntToStringConverter : MarkupExtension, IValueConverter
	{
		/// <summary>
		/// ZeroValueString (default : String.Empty => see Constructor)
		/// </summary>
		public string ZeroValueString { get; set; }

		/// <summary>
		/// Constructor : Initialize all properties
		/// </summary>
		public IntToStringConverter()
		{
			this.ZeroValueString = String.Empty;
		}

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int)
			{
				int intValue = (int)value;

				if (intValue.Equals( 0 ))
				{
					return this.ZeroValueString;
				}

				return intValue.ToString();
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string)
			{
				int intValue;
				string str = value.ToString();

				if (Int32.TryParse( str, out intValue ) == true)
				{
					return System.Convert.ToInt32( str );
				}
			}

			return value;
		}

		#endregion

		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new IntToStringConverter()
				       {
					       ZeroValueString = this.ZeroValueString
				       };
		}

		#endregion
	}

	/// <summary>
	/// Converts an Integer value into a Brush (and back)
	/// The Brushes for the zero value and the non zero value can be declared separately
	/// </summary>
	[ValueConversion( typeof( int ), typeof( Brush ) )]
	[MarkupExtensionReturnType( typeof( IntToBrushConverter ) )]
	public class IntToBrushConverter : MarkupExtension, IValueConverter
	{
		/// <summary>
		/// ZeroValueBrush (default : Transparent => see Constructor)
		/// </summary>
		public Brush ZeroValueBrush { get; set; }

		/// <summary>
		/// NonZeroValueBrush (default : FromRgb(255,200,200) -> FromRgb(255,150,150) => see Constructor)
		/// </summary>
		public Brush NonZeroValueBrush { get; set; }

		/// <summary>
		/// Initialize all Brushes with standard values
		/// </summary>
		public IntToBrushConverter()
		{
			this.ZeroValueBrush = Brushes.Transparent;
			LinearGradientBrush lgb = new LinearGradientBrush(
														new GradientStopCollection()
				                                 {
					                                 new GradientStop(){ Color = Color.FromRgb(255,200,200), Offset = 0},
					                                 new GradientStop(){ Color = Color.FromRgb(255,150,150), Offset = 1},
				                                 } )
												  {
													  StartPoint = new Point( 0, 0 ),
													  EndPoint = new Point( 0, 1 )
												  };
			this.NonZeroValueBrush = lgb;
		}

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int)
			{
				return (int)value == 0 ? this.ZeroValueBrush : this.NonZeroValueBrush;
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
										  CultureInfo culture)
		{
			if (value is Brush)
			{
				return (value.Equals( this.ZeroValueBrush )) ? 0 : value;
			}

			return value;
		}

		#endregion // IValueConverter Members

		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new IntToBrushConverter()
				       {
					       NonZeroValueBrush = this.NonZeroValueBrush
				       };
		}

		#endregion
	}
}
