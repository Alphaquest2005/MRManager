//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: TextBoxBaseAnimation.cs
//		namespace	: SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
//		class(es)	: TextBoxBaseAnimation
//							
//	---------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
{
	/// <summary>
	/// Supplies attached properties that provides animation functionality to the background property of a
	/// control that inherits from TextBoxBase (TextBox, RichTextBox)
	/// ! Obsolete : Use the BrushPropertyAnimation class instead
	/// </summary>
	public class TextBoxBaseAnimation : AttachedPropertiesManager
	{
		#region Constructor

		/// <summary>
		/// TextBoxBaseAnimation static constructor
		/// </summary>
		static TextBoxBaseAnimation()
		{
			// Here we add the 'TextBoxBase' type as an owner of the 'BackgroundProperty'
			// So it is possible to attach this functionality either to a TextBox or a RichTextBox
			Control.BackgroundProperty.AddOwner( typeof( TextBoxBase ),
						new FrameworkPropertyMetadata( Brushes.Transparent, BGColorChanged, CoerceBGColorChange ) );
		}

		#endregion // Constructor

		#region Attached Properties

		#region DependencyProperty - AnimateBackground ("bool")

		public static bool GetAnimateBackground(DependencyObject dpo)
		{
			return (bool)dpo.GetValue( AnimateBackgroundProperty );
		}
		public static void SetAnimateBackground(DependencyObject dpo, bool value)
		{
			dpo.SetValue( AnimateBackgroundProperty, value );
		}
		/// <summary>
		/// Gets or sets whether the animation of the Background property should take place or not.
		/// This is an attached dependency property.
		/// </summary>
		public static readonly DependencyProperty AnimateBackgroundProperty =
					DependencyProperty.RegisterAttached( "AnimateBackground", typeof( bool ),
																	 typeof( TextBoxBaseAnimation ),
																	 new FrameworkPropertyMetadata( false,
																											  onAnimateBackgroundChanged ) );
		/// <summary>
		/// Handles changes to the 'AnimateBackground' attached property.
		/// </summary>
		private static void onAnimateBackgroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var control = sender as Control;

			if (control != null)
			{
				if (GetAnimateBackground( control ) == true)
				{
					HookElement( control );
				}
				else
				{
					UnhookElement( control );
				}
			}
		}

		#endregion

		#region DependencyProperty - AnimationDuration ("double")

		public static double GetAnimationDuration(DependencyObject dpo)
		{
			return (double)dpo.GetValue( AnimationDurationProperty );
		}
		public static void SetAnimationDuration(DependencyObject dpo, double value)
		{
			dpo.SetValue( AnimationDurationProperty, value );
		}
		/// <summary>
		/// Gets or sets the duration of the animation. This is an attached dependency property.
		/// The default value is 350.0.
		/// </summary>
		public static readonly DependencyProperty AnimationDurationProperty =
					DependencyProperty.RegisterAttached( "AnimationDuration", typeof( double ),
																	 typeof( TextBoxBaseAnimation ),
																	 new FrameworkPropertyMetadata( 350.0 ) );
		#endregion

		#endregion

		#region Background Property changed handling

		/// <summary>
		/// 'Background Color' changed
		/// </summary>
		private static void BGColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			//System.Diagnostics.Debug.WriteLine("Color changed from " + e.OldValue + " to " + e.NewValue);
		}

		/// <summary>
		/// Coerce 'Background Color' change
		/// </summary>
		private static object CoerceBGColorChange(DependencyObject sender, object baseValue)
		{
			var control = sender as Control;

			// Make sure that the object is a control and hooked by our attached property
			if (control != null && IsHookedElement( control ) == true)
			{
				Brush newBGBrush = baseValue as Brush;

				// Update the animation flag
				// If animation already started - don't restart it (otherwise it will result in an infinite loop)
				if (UpdateAnimationStartedFlag( control ) == true)
				{
					return baseValue;
				}

				// If 'Background' value hasn't changed, do nothing.
				// This can happen if the Background Property is set using data binding 
				// and the binding source has changed but the new 'Background' value hasn't changed.
				// ReSharper disable PossibleUnintendedReferenceComparison
				if (control.Background == newBGBrush)
				// ReSharper restore PossibleUnintendedReferenceComparison
				{
					return baseValue;
				}

				// This is our 'Animation Brush'
				SolidColorBrush animationBrush = new SolidColorBrush();

				// This is the ColorAnimation
				ColorAnimation colorAnimation = new ColorAnimation()
				{
					Duration = new Duration( TimeSpan.FromMilliseconds( GetAnimationDuration( control ) ) )
				};

				// When animation completes, set the background brush to the requested value (baseValue)
				// or to the existing binding
				colorAnimation.Completed += (objectSender, eventArgs) =>
				{
					UpdateAnimationStartedFlag( control );

					// This will trigger value coercion again 
					// but UpdateAnimationStartedFlag() function will return true this time, 
					// thus animation will not be triggered 
					if (BindingOperations.IsDataBound( control, Control.BackgroundProperty ))
					{
						Binding bindingValue = BindingOperations.GetBinding( control, Control.BackgroundProperty );
						BindingOperations.SetBinding( control, Control.BackgroundProperty, bindingValue );
					}
					else
					{
						control.Background = newBGBrush;
					}
				};

				// It is very important to animate from the previous background color
				// otherwise the animation would always start from 'Brushes.Transparent'
				// ReSharper disable PossibleNullReferenceException
				colorAnimation.From = (control.Background as SolidColorBrush).Color;
				colorAnimation.To = (newBGBrush as SolidColorBrush).Color;
				// ReSharper restore PossibleNullReferenceException

				// Till the animation ends the background brush is our 'Animation Brush'
				control.Background = animationBrush;

				// Start the animation of the 'Animation Brush'
				animationBrush.BeginAnimation( SolidColorBrush.ColorProperty, colorAnimation );

				return animationBrush;
			}

			return baseValue;
		}

		#endregion // Background Property changed handling
	}
}
