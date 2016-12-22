//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: PasswordBoxEnhancements.cs
//		namespace	: SoftArcs.WPFSmartLibrary.UIClassEnhancements
//		class(es)	: PasswordBoxEnhancements
//							
//	--------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls;

namespace SoftArcs.WPFSmartLibrary.UIClassEnhancements
{
	class PasswordBoxEnhancements
	{
		public static void AddAutoSelectToAllPasswordBoxes()
		{
			EventManager.RegisterClassHandler( typeof( PasswordBox ), UIElement.GotFocusEvent,
														  new RoutedEventHandler( PasswordBox_GotFocus ) );
		}

		private static void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
		{
			var passwordBox = sender as PasswordBox;

			if (passwordBox != null)
			{
				passwordBox.SelectAll();
			}
		}
	}
}
