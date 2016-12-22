//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: WindowKeysHandling.cs
//		namespace	: SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
//		class(es)	: WindowKeysHandling
//							
//	--------------------------------------------------------------------
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
{
	public static class WindowKeysHandling
	{
		#region DependencyProperty - EscapeClosesWindow ("bool")

		public static bool GetEscapeClosesWindow(DependencyObject dpo)
		{
			return (bool)dpo.GetValue( EscapeClosesWindowProperty );
		}
		public static void SetEscapeClosesWindow(DependencyObject dpo, bool value)
		{
			dpo.SetValue( EscapeClosesWindowProperty, value );
		}
		/// <summary>
		/// Gets or sets a boolean value whether to close the associated Window or not.
		/// This is an attached dependency property.
		/// </summary>
		public static readonly DependencyProperty EscapeClosesWindowProperty =
					DependencyProperty.RegisterAttached( "EscapeClosesWindow", typeof( bool ),
																	 typeof( WindowKeysHandling ),
																	 new FrameworkPropertyMetadata( false,
																	 new PropertyChangedCallback( OnEscapeClosesWindowChanged ) ) );

		/// <summary>
		/// Handles changes to the 'EscapeClosesWindow' attached property.
		/// </summary>
		private static void OnEscapeClosesWindowChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			// Prevent the following WPF Designer Warning :
			// "Microsoft.Expression.Platform.WPF.InstanceBuilders.WindowInstance"
			// can't be converted into "System.Windows.Window"
			if (DesignerProperties.GetIsInDesignMode( new DependencyObject() ) == false)
			{
				Window targetWindow = sender as Window;

				if (targetWindow != null)
				{
					if ((bool)e.NewValue == true)
					{
						targetWindow.PreviewKeyDown += new KeyEventHandler( Window_PreviewKeyDown );
					}
					else
					{
						targetWindow.PreviewKeyDown -= new KeyEventHandler( Window_PreviewKeyDown );
					}
				}
			}
		}

		/// <summary>
		/// Handle the 'PreviewKeyDown'-event on the Window
		/// </summary>
		private static void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			// If the escape key was pressed, close the window
			if (e.Key == Key.Escape)
			{
				(sender as Window).Close();
			}
		}

		#endregion
	}
}