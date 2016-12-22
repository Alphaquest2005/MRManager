//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: PasswordBoxManager.cs
//		namespace	: SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
//		class(es)	: PasswordBoxManager
//							
//	--------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls;

namespace SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
{
	public static class PasswordBoxManager
	{
		#region DependencyProperty - AddAutoSelect (type of "bool")

		public static bool GetAddAutoSelect(DependencyObject obj)
		{
			return (bool)obj.GetValue( AddAutoSelectProperty );
		}
		public static void SetAddAutoSelect(DependencyObject obj, bool value)
		{
			obj.SetValue( AddAutoSelectProperty, value );
		}
		/// <summary>
		/// Gets or sets the auto selection to the associated PasswordBox. This is a dependency property.
		/// </summary>
		public static readonly DependencyProperty AddAutoSelectProperty =
					DependencyProperty.RegisterAttached( "AddAutoSelect", typeof( bool ),
																	 typeof( PasswordBoxManager ),
																	 new UIPropertyMetadata( false,
																	 new PropertyChangedCallback( OnAddAutoSelectChanged ) ) );

		private static void OnAddAutoSelectChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			EventManager.RegisterClassHandler( typeof( PasswordBox ), PasswordBox.GotFocusEvent,
														  new RoutedEventHandler( PasswordBox_GotFocus ) );
		}

		private static void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
		{
			(sender as PasswordBox).SelectAll();
		}

		#endregion
	}
}
