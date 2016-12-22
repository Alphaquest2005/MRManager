//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: BrushConverter.cs
//		namespace	: SoftArcs.WPFSmartLibrary.ValueConverter
//		class(es)	: BrushToColorConverter
//							
//	--------------------------------------------------------------------
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace SoftArcs.WPFSmartLibrary.ValueConverter
{
	/// <summary>
	/// Converts a SolidColorBrush into a Color (and back)
	/// </summary>
	[ValueConversion( typeof( Brush ), typeof( Color ) )]
	[MarkupExtensionReturnType( typeof( BrushToColorConverter ) )]
	public class BrushToColorConverter : MarkupExtension, IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is SolidColorBrush)
			{
				return (value as SolidColorBrush).Color;
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Color)
			{
				return new SolidColorBrush( (Color)value );
			}

			return value;
		}

		#endregion // IValueConverter Members

		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new BrushToColorConverter();
		}

		#endregion
	}
}
