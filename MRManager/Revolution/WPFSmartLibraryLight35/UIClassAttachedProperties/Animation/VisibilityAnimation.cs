//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: VisibilityAnimation.cs
//		namespace	: SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
//		class(es)	: VisibilityAnimation
//							
//	---------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
{
	/// <summary>
	/// Supplies attached properties that provides animation functionality to visibility properties
	/// </summary>
	public class VisibilityAnimation : AttachedPropertiesManager
	{
		#region Enumerations

		public enum AnimationType
		{
			/// <summary>
			/// No animation
			/// </summary>
			None,
			/// <summary>
			/// Fade in / Fade out
			/// </summary>
			Fade
		}

		#endregion

		#region Constructor

		/// <summary>
		/// VisibilityAnimation static constructor
		/// </summary>
		static VisibilityAnimation()
		{
			// Here we add the 'FrameworkElement' type as an owner of the 'VisibilityProperty'
			// So it is possible to attach this functionality to all framework elements
			UIElement.VisibilityProperty.AddOwner( typeof( FrameworkElement ),
					  new FrameworkPropertyMetadata( Visibility.Visible, VisibilityChanged, CoerceVisibility ) );
		}

		#endregion // Constructor

		#region DependencyProperty - AnimationType (of type "AnimationType")

		public static AnimationType GetAnimationType(DependencyObject dpo)
		{
			return (AnimationType)dpo.GetValue( AnimationTypeProperty );
		}
		public static void SetAnimationType(DependencyObject dpo, AnimationType value)
		{
			dpo.SetValue( AnimationTypeProperty, value );
		}
		/// <summary>
		/// Gets or sets the type of the animation. This is an attached dependency property.
		/// The default value is AnimationType.None.
		/// </summary>
		public static readonly DependencyProperty AnimationTypeProperty =
									  DependencyProperty.RegisterAttached( "AnimationType", typeof( AnimationType ),
																						typeof( VisibilityAnimation ),
																						new FrameworkPropertyMetadata( AnimationType.None,
																						new PropertyChangedCallback( OnAnimationTypePropertyChanged ) ) );
		/// <summary>
		/// Handles changes to the 'AnimationType' attached property.
		/// </summary>
		private static void OnAnimationTypePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var frameworkElement = sender as FrameworkElement;

			if (frameworkElement != null)
			{
				if (GetAnimationType( frameworkElement ) != AnimationType.None)
				{
					HookElement( frameworkElement );
				}
				else
				{
					UnhookElement( frameworkElement );
				}
			}
		}

		#endregion

		#region DependencyProperty - IgnoreFirstTime (of type "bool")

		public static bool GetIgnoreFirstTime(DependencyObject dpo)
		{
			return (bool)dpo.GetValue( IgnoreFirstTimeProperty );
		}
		public static void SetIgnoreFirstTime(DependencyObject dpo, bool value)
		{
			dpo.SetValue( IgnoreFirstTimeProperty, value );
		}
		/// <summary>
		/// Gets or sets whether the first property change should be validated or not.
		/// This is an attached dependency property. The default value is false.
		/// </summary>
		public static readonly DependencyProperty IgnoreFirstTimeProperty =
									  DependencyProperty.RegisterAttached( "IgnoreFirstTime", typeof( bool ),
																						typeof( VisibilityAnimation ),
																						new UIPropertyMetadata( false ) );
		#endregion

		#region DependencyProperty - AnimationDuration (of type "double")

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
																						typeof( VisibilityAnimation ),
																						new UIPropertyMetadata( 350.0 ) );
		#endregion

		#region Visibility changed handling

		/// <summary>
		/// Visibility changed
		/// </summary>
		private static void VisibilityChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			// Ignore
		}

		/// <summary>
		/// Coerce visibility change
		/// </summary>
		private static object CoerceVisibility(DependencyObject dependencyObject, object baseValue)
		{
			// Make sure object is a framework element
			var frameworkElement = dependencyObject as FrameworkElement;

			if (frameworkElement == null)
			{
				return baseValue;
			}

			// Cast to type safe value
			var visibility = (Visibility)baseValue;

			// If Visibility value hasn't change, do nothing.
			// This can happen if the Visibility property is set using data binding and
			// the binding source has changed but the new visibility value hasn't changed.
			if (visibility == frameworkElement.Visibility)
			{
				return baseValue;
			}

			// If element is not hooked by our attached property, stop here
			if (IsHookedElement( frameworkElement ) == false)
			{
				return baseValue;
			}

			// if element has IgnoreFirstTime flag set, then ignore the first time 
			// the property is coerced.
			if (GetIgnoreFirstTime( frameworkElement ) == true)
			{
				SetIgnoreFirstTime( frameworkElement, false );
				return baseValue;
			}

			// Update animation flag
			// If animation already started - don't restart it (otherwise it will result in an infinite loop)
			if (UpdateAnimationStartedFlag( frameworkElement ) == true)
			{
				return baseValue;
			}

			// If we get here, it means we have to start fade in or fade out animation. 
			// In any case return value of this method will be Visibility.Visible, to allow the animation.
			var doubleAnimation = new DoubleAnimation
			{
				Duration = new Duration( TimeSpan.FromMilliseconds( GetAnimationDuration( frameworkElement ) ) )
			};

			// When animation completes, set the visibility value to the requested 
			// value (baseValue)
			doubleAnimation.Completed += (sender, eventArgs) =>
			{
				if (visibility == Visibility.Visible)
				{
					// In case we change into Visibility.Visible, the correct value 
					// is already set. So just update the animation started flag
					UpdateAnimationStartedFlag( frameworkElement );
				}
				else
				{
					// This will trigger value coercion again 
					// but UpdateAnimationStartedFlag() function will return true this time, 
					// thus animation will not be triggered. 
					if (BindingOperations.IsDataBound( frameworkElement, UIElement.VisibilityProperty ))
					{
						// Set visiblity using bounded value
						Binding bindingValue = BindingOperations.GetBinding( frameworkElement, UIElement.VisibilityProperty );
						BindingOperations.SetBinding( frameworkElement, UIElement.VisibilityProperty, bindingValue );
					}
					else
					{
						// No binding, just assign the value
						frameworkElement.Visibility = visibility;
					}
				}
			};

			if (visibility == Visibility.Visible)
			{
				// Fade in by animating opacity
				doubleAnimation.To = 1.0;
			}
			else
			{
				// Fade out by animating opacity
				doubleAnimation.To = 0.0;
			}

			// Start animation
			frameworkElement.BeginAnimation( UIElement.OpacityProperty, doubleAnimation );

			// Make sure the element remains visible during the animation
			// The original requested value will be set in the completed event of 
			// the animation
			return Visibility.Visible;
		}

		#endregion // Visibility changed handling
	}
}
