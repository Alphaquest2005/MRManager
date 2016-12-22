//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: EnumConverter.cs
//		namespace	: SoftArcs.WPFSmartLibrary.ValueConverter
//		class(es)	: EnumMatchToBooleanConverter
//							
//	--------------------------------------------------------------------
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SoftArcs.WPFSmartLibrary.ValueConverter
{
	/// <summary>
	/// Converts an Enum match into a Boolean value (and back)
	/// </summary>
	[ValueConversion( typeof( object ), typeof( bool ) )]
	[MarkupExtensionReturnType( typeof( EnumMatchToBooleanConverter ) )]
	public class EnumMatchToBooleanConverter : MarkupExtension, IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null)
			{
				return false;
			}

			string sourceValue = value.ToString();
			string targetValue = parameter.ToString();

			return sourceValue.Equals( targetValue, StringComparison.InvariantCultureIgnoreCase );
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null)
			{
				return null;
			}

			bool useValue = (bool)value;
			string targetValue = parameter.ToString();

			return useValue ? Enum.Parse( targetType, targetValue ) : null;
		}

		#endregion

		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new EnumMatchToBooleanConverter();
		}

		#endregion
	}
}
