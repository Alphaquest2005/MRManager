//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: SmartIndicator.xaml.cs
//		namespace	: SoftArcs.WPFSmartLibrary.SmartUserControls
//		class(es)	: SmartIndicator
//							
//	--------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SoftArcs.WPFSmartLibrary.SmartUserControls
{
	#region Enumerations

	/// <summary>
	/// Indicates the state of the indicator element
	/// </summary>
	public enum IndicatorLevel
	{
		Low,
		Middle,
		High
	}

	#endregion

	public partial class SmartIndicator
	{
		#region Fields

		private double oneThirdOfActualWidth;

		private DoubleAnimation moveAnimation;
		private DoubleAnimation onAnimation;
		private DoubleAnimation offAnimation;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the Style associated with the control's internal Path object.
		/// </summary>
		public Style IndicatorStyle
		{
			get { return this.Indicator.Style; }
			set { this.Indicator.Style = value; }
		}

		#endregion

		#region Dependency Properties

		#region DependencyProperty - AnimationDuration (of type "Double")

		/// <summary>
		/// Gets or sets the duration of the indicator state changed animation (in milliseconds). This is a dependency property. The default value is 350.
		/// </summary>
		public double AnimationDuration
		{
			get { return (double)GetValue( AnimationDurationProperty ); }
			set { SetValue( AnimationDurationProperty, value ); }
		}
		public static readonly DependencyProperty AnimationDurationProperty =
			DependencyProperty.Register( "AnimationDuration", typeof( double ), typeof( SmartIndicator ),
												 new UIPropertyMetadata( 350.0,
																				(dpo, ea) =>
																				{
																					// ReSharper disable ConvertToLambdaExpression
																					((SmartIndicator)dpo).OnAnimationDurationChanged(
																						(double)ea.OldValue, (double)ea.NewValue );
																					// ReSharper restore ConvertToLambdaExpression
																				} ) );

		private void OnAnimationDurationChanged(double oldValue, double newValue)
		{
			if (oldValue.Equals( newValue )) return;

			this.moveAnimation.Duration = new Duration( TimeSpan.FromMilliseconds( newValue ) );
			this.onAnimation.Duration = new Duration( TimeSpan.FromMilliseconds( newValue ) );
			this.offAnimation.Duration = new Duration( TimeSpan.FromMilliseconds( newValue ) );
		}

		#endregion

		#region DependencyProperty - IndicatorState (of type "IndicatorLevel")

		/// <summary>
		/// Gets or sets the current indicator state. This is a dependency property. The default value is IndicatorLevel.Middle.
		/// </summary>
		public IndicatorLevel IndicatorState
		{
			get { return (IndicatorLevel)GetValue( IndicatorStateProperty ); }
			set { SetValue( IndicatorStateProperty, value ); }
		}
		public static readonly DependencyProperty IndicatorStateProperty =
									  DependencyProperty.Register( "IndicatorState", typeof( IndicatorLevel ), typeof( SmartIndicator ),
											new UIPropertyMetadata( IndicatorLevel.Middle,
												(dpo, ea) =>
												{
													// ReSharper disable ConvertToLambdaExpression
													((SmartIndicator)dpo).OnIndicatorStateChanged( (IndicatorLevel)ea.OldValue, (IndicatorLevel)ea.NewValue );
													// ReSharper restore ConvertToLambdaExpression
												} ) );

		private void OnIndicatorStateChanged(IndicatorLevel oldValue, IndicatorLevel newValue)
		{
			if (oldValue.Equals( newValue )) return;

			this.changeIndicatorLevel( newValue );
		}

		#endregion

		#region DependencyProperty - ObservedValue (of type "Double")

		/// <summary>
		/// Gets or sets the value being observed. This is a dependency property. The default value is 5.
		/// The Range for each level can be set with the FirstRangePerimeter and the SecondRangePerimeter properties.
		/// (The default value is exactly the half of the Slider's Maximum default value).
		/// </summary>
		public double ObservedValue
		{
			get { return (double)GetValue( ObservedValueProperty ); }
			set { SetValue( ObservedValueProperty, value ); }
		}
		public static readonly DependencyProperty ObservedValueProperty =
									  DependencyProperty.Register( "ObservedValue", typeof( double ), typeof( SmartIndicator ),
											new UIPropertyMetadata( 5.0,
												(dpo, ea) =>
												{
													// ReSharper disable ConvertToLambdaExpression
													((SmartIndicator)dpo).OnObservedValueChanged( (double)ea.OldValue, (double)ea.NewValue );
													// ReSharper restore ConvertToLambdaExpression
												} ) );

		private void OnObservedValueChanged(double oldValue, double newValue)
		{
			if (oldValue.Equals( newValue )) return;

			this.validateObservedValue();
		}

		#endregion

		#region DependencyProperty - ObservedPassword (of type "String")

		/// <summary>
		/// Gets or sets the password being observed. This is a dependency property.
		/// </summary>
		public string ObservedPassword
		{
			get { return (string)GetValue( ObservedPasswordProperty ); }
			set { SetValue( ObservedPasswordProperty, value ); }
		}
		public static readonly DependencyProperty ObservedPasswordProperty =
									  DependencyProperty.Register( "ObservedPassword", typeof( string ), typeof( SmartIndicator ),
											new UIPropertyMetadata( String.Empty,
												(dpo, ea) =>
												{
													// ReSharper disable ConvertToLambdaExpression
													((SmartIndicator)dpo).OnObservedPasswordChanged( (string)ea.OldValue, (string)ea.NewValue );
													// ReSharper restore ConvertToLambdaExpression
												} ) );

		private void OnObservedPasswordChanged(string oldValue, string newValue)
		{
			if (oldValue == null && newValue == null || oldValue != null && oldValue.Equals( newValue )) return;

			this.validateObservedPassword();
		}

		#endregion

		#region DependencyProperty - FirstRangePerimeter (of type "Double")

		/// <summary>
		/// Gets or sets the upper bound of the first level. This is a dependency property. The default value is 3.33.
		/// (The default value is exactly the third of the Slider's Maximum default value).
		/// </summary>
		public double FirstRangePerimeter
		{
			get { return (double)GetValue( FirstRangePerimeterProperty ); }
			set { SetValue( FirstRangePerimeterProperty, value ); }
		}
		public static readonly DependencyProperty FirstRangePerimeterProperty =
									  DependencyProperty.Register( "FirstRangePerimeter", typeof( double ), typeof( SmartIndicator ),
											new PropertyMetadata( 3.33,
												(dpo, ea) =>
												{
													// ReSharper disable ConvertToLambdaExpression
													((SmartIndicator)dpo).OnFirstRangePerimeterChanged( (double)ea.OldValue, (double)ea.NewValue );
													// ReSharper restore ConvertToLambdaExpression
												} ) );

		private void OnFirstRangePerimeterChanged(double oldValue, double newValue)
		{
			if (oldValue.Equals( newValue )) return;

			this.validateObservedValue();
		}

		#endregion

		#region DependencyProperty - SecondRangePerimeter (of type "Double")

		/// <summary>
		/// Gets or sets the upper bound of the first level. This is a dependency property. The default value is 6.67.
		/// (The default value is exactly two thirds of the Slider's Maximum default value).
		/// </summary>
		public double SecondRangePerimeter
		{
			get { return (double)GetValue( SecondRangePerimeterProperty ); }
			set { SetValue( SecondRangePerimeterProperty, value ); }
		}
		public static readonly DependencyProperty SecondRangePerimeterProperty =
									  DependencyProperty.Register( "SecondRangePerimeter", typeof( double ), typeof( SmartIndicator ),
											new PropertyMetadata( 6.67,
												(dpo, ea) =>
												{
													// ReSharper disable ConvertToLambdaExpression
													((SmartIndicator)dpo).OnSecondRangePerimeterPropertyChanged( (double)ea.OldValue, (double)ea.NewValue );
													// ReSharper restore ConvertToLambdaExpression
												} ) );

		private void OnSecondRangePerimeterPropertyChanged(double oldValue, double newValue)
		{
			if (oldValue.Equals( newValue )) return;

			this.validateObservedValue();
		}

		#endregion

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the SmartIndicator class. 
		/// </summary>
		public SmartIndicator()
		{
			InitializeComponent();

			// Initialize all animation objects with the default values
			this.moveAnimation = new DoubleAnimation() { Duration = new Duration( TimeSpan.FromMilliseconds( this.AnimationDuration ) ) };
			this.onAnimation = new DoubleAnimation() { To = 0.9, Duration = new Duration( TimeSpan.FromMilliseconds( this.AnimationDuration ) ) };
			this.offAnimation = new DoubleAnimation() { To = 0.3, Duration = new Duration( TimeSpan.FromMilliseconds( this.AnimationDuration ) ) };
		}

		#endregion

		#region Events related to the Control

		/// <summary>
		/// Change the indicator level if the ObservedValueProperty was set on the control.
		/// </summary>
		private void SmartIndicator_Loaded(object sender, RoutedEventArgs e)
		{
			var observedValueSource = DependencyPropertyHelper.GetValueSource( this, ObservedValueProperty );

			if (observedValueSource.BaseValueSource != BaseValueSource.Default)
			{
				this.validateObservedValue();
			}

			var observedPasswordSource = DependencyPropertyHelper.GetValueSource( this, ObservedPasswordProperty );

			if (observedPasswordSource.BaseValueSource != BaseValueSource.Default)
			{
				this.validateObservedPassword();
			}
		}

		/// <summary>
		/// Calculate the third of the ActualWidth every time the size of the control is changed.
		/// </summary>
		private void SmartIndicator_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.oneThirdOfActualWidth = this.ActualWidth / 3.0;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Change the indicator state depending on the observed value.
		/// </summary>
		private void validateObservedValue()
		{
			if (this.ObservedValue <= this.FirstRangePerimeter)
			{
				this.changeIndicatorLevel( IndicatorLevel.Low );
			}
			else if (this.ObservedValue > this.FirstRangePerimeter && this.ObservedValue <= SecondRangePerimeter)
			{
				this.changeIndicatorLevel( IndicatorLevel.Middle );
			}
			else
			{
				this.changeIndicatorLevel( IndicatorLevel.High );
			}
		}

		/// <summary>
		/// Change the indicator state depending on the observed password.
		/// </summary>
		private void validateObservedPassword()
		{
			if (this.ObservedPassword == null || this.ObservedPassword.Length < 3)
			{
				this.changeIndicatorLevel( IndicatorLevel.Low );
			}
			else if (this.ObservedPassword.Length >= 3 && this.ObservedPassword.Length < 5)
			{
				this.changeIndicatorLevel( IndicatorLevel.Middle );
			}
			else
			{
				this.changeIndicatorLevel( IndicatorLevel.High );
			}
		}

		/// <summary>
		/// Change the indicator state to the given IndicatorLevel with a smooth animation.
		/// </summary>
		private void changeIndicatorLevel(IndicatorLevel newLevel)
		{
			switch (newLevel)
			{
				case IndicatorLevel.Low:
					this.LowIndicator.BeginAnimation( OpacityProperty, onAnimation );
					this.MiddleIndicator.BeginAnimation( OpacityProperty, offAnimation );
					this.HighIndicator.BeginAnimation( OpacityProperty, offAnimation );
					moveAnimation.To = -this.oneThirdOfActualWidth;
					break;
				case IndicatorLevel.Middle:
					this.LowIndicator.BeginAnimation( OpacityProperty, offAnimation );
					this.MiddleIndicator.BeginAnimation( OpacityProperty, onAnimation );
					this.HighIndicator.BeginAnimation( OpacityProperty, offAnimation );
					moveAnimation.To = 0;
					break;
				case IndicatorLevel.High:
					this.LowIndicator.BeginAnimation( OpacityProperty, offAnimation );
					this.MiddleIndicator.BeginAnimation( OpacityProperty, offAnimation );
					onAnimation.To = 1.0;
					this.HighIndicator.BeginAnimation( OpacityProperty, onAnimation );
					onAnimation.To = 0.9;
					moveAnimation.To = this.oneThirdOfActualWidth;
					break;
				default:
					throw new ArgumentOutOfRangeException( "newLevel" );
			}

			this.IndicatorTranslateTransform.BeginAnimation( TranslateTransform.XProperty, moveAnimation );
		}

		#endregion
	}
}
