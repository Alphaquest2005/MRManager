//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: TextBoxBaseEnhancements.cs
//		namespace	: SoftArcs.WPFSmartLibrary.UIClassEnhancements
//		class(es)	: TextBoxBaseEnhancements
//							
//	--------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SoftArcs.WPFSmartLibrary.UIClassEnhancements
{
	public class TextBoxBaseEnhancements
	{
		public static void AddAutoSelectToAllTextBoxes(bool includePasswordBoxes = false)
		{
			EventManager.RegisterClassHandler( typeof( TextBoxBase ), UIElement.GotFocusEvent,
														  new RoutedEventHandler( TextBoxBase_GotFocus ) );

			if (includePasswordBoxes == true)
			{
				PasswordBoxEnhancements.AddAutoSelectToAllPasswordBoxes();
			}
		}

		private static void TextBoxBase_GotFocus(object sender, RoutedEventArgs e)
		{
			var textBoxBase = sender as TextBoxBase;

			if (textBoxBase != null)
			{
				textBoxBase.SelectAll();
			}
		}
	}
}
