//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: DateTimeConverter.cs
//		namespace	: SoftArcs.WPFSmartLibrary.ValueConverter
//		class(es)	: DateTimeToEmptyStringConverter
//							
//	--------------------------------------------------------------------
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SoftArcs.WPFSmartLibrary.ValueConverter
{
	/// <summary>
	/// Converts a DateTime object into a String resp. an empty String if DateTime is not initialized (and back)
	/// The format of the DateTime-String can be declared separately
	/// </summary>
	[ValueConversion( typeof( DateTime ), typeof( string ) )]
	[MarkupExtensionReturnType( typeof( DateTimeToEmptyStringConverter ) )]
	public class DateTimeToEmptyStringConverter : MarkupExtension, IValueConverter
	{
		/// <summary>
		/// FormatString (default : "dd.MM.yyyy" => see Constructor)
		/// </summary>
		public string FormatString { get; set; }

		/// <summary>
		/// Initialize the 'FormatString' with a standard value
		/// </summary>
		public DateTimeToEmptyStringConverter()
		{
			FormatString = "dd.MM.yyyy";
		}

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DateTime)
			{
				DateTime d = (DateTime)value;

				if (d.Equals( new DateTime() ))
				{
					return String.Empty;
				}
				
				if (parameter is string)
				{
					return d.ToString( parameter.ToString() );
				}
				
				return d.ToString( FormatString );
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string)
			{
				DateTime d;
				string str = value.ToString();

				if (DateTime.TryParse( str, out d ) == true)
				{
					return System.Convert.ToDateTime( str );
				}
			}

			return value;
		}

		#endregion

		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new DateTimeToEmptyStringConverter();
		}

		#endregion
	}
}
