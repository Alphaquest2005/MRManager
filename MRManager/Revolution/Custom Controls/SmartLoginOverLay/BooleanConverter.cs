//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: BooleanConverter.cs
//		namespace	: SoftArcs.WPFSmartLibrary.ValueConverter
//		class(es)	: BoolToBrushConverter
//						  BoolToOppositeBoolConverter
//						  BoolToVisibilityConverter
//						  BoolToVisibleOrHiddenConverter
//						  BoolToVisibleOrCollapsedConverter
//						  BoolToVisibleOrCollapsedOrHiddenConverter
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
	/// Converts a Boolean value into a Brush (and back)
	/// The Brushes for the true value and the false value can be declared separately
	/// </summary>
	[ValueConversion( typeof( bool ), typeof( Brush ) )]
	[MarkupExtensionReturnType( typeof( BoolToBrushConverter ) )]
	public class BoolToBrushConverter : MarkupExtension, IValueConverter
	{
		/// <summary>
		/// TrueValueBrush (default : MediumSpringGreen => see Constructor)
		/// </summary>
		public Brush TrueValueBrush { get; set; }

		/// <summary>
		/// FalseValueBrush (default : White => see Constructor)
		/// </summary>
		public Brush FalseValueBrush { get; set; }

		/// <summary>
		/// Initialize the 'True' and 'False' Brushes with standard values
		/// </summary>
		public BoolToBrushConverter()
		{
			TrueValueBrush = Brushes.MediumSpringGreen;
			FalseValueBrush = Brushes.White;
		}

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				return (bool)value ? TrueValueBrush : FalseValueBrush;
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
										  CultureInfo culture)
		{
			if (value is Brush)
			{
				return value.Equals( TrueValueBrush );
			}

			return value;
		}

		#endregion // IValueConverter Members

		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new BoolToBrushConverter
						 {
							 TrueValueBrush = this.TrueValueBrush,
							 FalseValueBrush = this.FalseValueBrush
						 };
		}

		#endregion
	}

	/// <summary>
	/// Converts a Boolean value into an opposite Boolean value (and back)
	/// </summary>
	[ValueConversion( typeof( bool ), typeof( bool ) )]
	[MarkupExtensionReturnType( typeof( BoolToOppositeBoolConverter ) )]
	public class BoolToOppositeBoolConverter : MarkupExtension, IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				return !(bool)value;
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				return !(bool)value;
			}

			return value;
		}

		#endregion

		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new BoolToOppositeBoolConverter();
		}

		#endregion
	}

	/// <summary>
	/// Converts a Boolean value into a Visibility enumeration (and back)
	/// </summary>
	[ValueConversion( typeof( bool ), typeof( Visibility ) )]
	[MarkupExtensionReturnType( typeof( BoolToVisibilityConverter ) )]
	public class BoolToVisibilityConverter : MarkupExtension, IValueConverter
	{
		/// <summary>
		/// FalseEquivalent (default : Visibility.Collapsed => see Constructor)
		/// </summary>
		public Visibility FalseEquivalent { get; set; }
		/// <summary>
		/// Define whether the opposite boolean value is crucial (default : false)
		/// </summary>
		public bool OppositeBooleanValue { get; set; }

		/// <summary>
		/// Initialize the properties with standard values
		/// </summary>
		public BoolToVisibilityConverter()
		{
			this.FalseEquivalent = Visibility.Collapsed;
			this.OppositeBooleanValue = false;
		}

		//+------------------------------------------------------------------------
		//+ Usage :
		//+ -------
		//+ 1. <conv:BoolToVisibilityConverter x:Key="BoolToVisibilityConverter" />
		//+ 2. {Binding ... Converter={StaticResource BoolToVisibilityConverter}
		//+------------------------------------------------------------------------
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool && targetType == typeof( Visibility ))
			{
				bool? booleanValue = (bool?)value;

				if (this.OppositeBooleanValue == true)
				{
					booleanValue = !booleanValue;
				}

				return booleanValue.GetValueOrDefault() ? Visibility.Visible : FalseEquivalent;
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility)
			{
				Visibility visibilityValue = (Visibility)value;

				if (this.OppositeBooleanValue == true)
				{
					visibilityValue = visibilityValue == Visibility.Visible ? FalseEquivalent : Visibility.Visible;
				}

				return (visibilityValue == Visibility.Visible);
			}

			return value;
		}

		#endregion // IValueConverter Members

		//+-----------------------------------------------------------------------------------
		//+ Usage :	(wpfsl: => XML Namespace mapping to the "BoolToVisibilityConverter" class)
		//+ -------
		//+ Use as follows : {Binding ... Converter={wpfsl:BoolToVisibilityConverter}
		//+ NO StaticResource required
		//+-----------------------------------------------------------------------------------
		#region MarkupExtension "overrides"

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new BoolToVisibilityConverter
			{
				FalseEquivalent = this.FalseEquivalent,
				OppositeBooleanValue = this.OppositeBooleanValue
			};
		}

		#endregion
	}

	/// <summary>
	/// Converts a Boolean value into a Visibility enumeration (and back)
	/// FalseEquivalent is always Visibility.Hidden
	/// ! Obsolete : It is recommended to use the more flexible 'BoolToVisibilityConverter' instead
	/// </summary>
	[ValueConversion( typeof( bool ), typeof( Visibility ) )]
	public class BoolToVisibleOrHiddenConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool && targetType == typeof( Visibility ))
			{
				bool bValue = (bool)value;

				if (bValue)
					return Visibility.Visible;

				return Visibility.Hidden;
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility)
			{
				Visibility visibility = (Visibility)value;

				if (visibility == Visibility.Visible)
					return true;

				return false;
			}

			return value;
		}

		#endregion
	}

	/// <summary>
	/// Converts a Boolean value into a Visibility enumeration (and back)
	/// FalseEquivalent is always Visibility.Collapsed
	/// ! Obsolete : It is recommended to use the more flexible 'BoolToVisibilityConverter' instead
	/// </summary>
	[ValueConversion( typeof( bool ), typeof( Visibility ) )]
	public class BoolToVisibleOrCollapsedConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool && targetType == typeof( Visibility ))
			{
				return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility)
			{
				return ((Visibility)value == Visibility.Visible);
			}
			return value;
		}

		#endregion
	}

	/// <summary>
	/// Converts a Boolean value into a Visibility enumeration (and back)
	/// FalseEquivalent can be defined with the property 'Collapse'
	/// ! Obsolete : It is recommended to use the more flexible 'BoolToVisibilityConverter' instead
	/// </summary>
	[ValueConversion( typeof( bool ), typeof( Visibility ) )]
	public class BoolToVisibleOrCollapsedOrHiddenConverter : IValueConverter
	{
		/// <summary>
		/// Collapse (default : Visibility.Hidden, because a bool is 'false' by default)
		/// true  => 'False-Equivalent' = Visibility.Collapsed
		/// false => 'False-Equivalent' = Visibility.Hidden
		/// </summary>
		public bool Collapse { get; set; }

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool && targetType == typeof( Visibility ))
			{
				bool bValue = (bool)value;

				if (bValue)
				{
					return Visibility.Visible;
				}

				if (Collapse)
				{
					return Visibility.Collapsed;
				}

				return Visibility.Hidden;
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility)
			{
				Visibility visibility = (Visibility)value;

				if (visibility == Visibility.Visible)
					return true;

				return false;
			}

			return value;
		}

		#endregion // IValueConverter Members
	}

}
