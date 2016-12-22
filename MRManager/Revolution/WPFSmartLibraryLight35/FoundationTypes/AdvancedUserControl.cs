//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: AdvancedUserControl.cs
//		namespace	: SoftArcs.WPFSmartLibrary.FoundationTypes
//		class(es)	: AdvancedUserControl
//							
//	--------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoftArcs.WPFSmartLibrary.FoundationTypes
{
	public class AdvancedUserControl : UserControl
	{
		#region Properties

		/// <summary>
		/// Gets or sets the parent element of the UserControl.
		/// </summary>
		protected DependencyObject ParentElement { get; set; }

		#endregion

		#region DependencyProperty - FullSpan (type of "SwitchState")

		/// <summary>
		/// Gets or sets the full (row and column) span state of the UserControl. This is a dependency property.
		/// The default value is Off. This applies only when the parent element is a Grid panel.
		/// </summary>
		public SwitchState FullSpan
		{
			get { return (SwitchState)GetValue( FullSpanProperty ); }
			set { SetValue( FullSpanProperty, value ); }
		}
		public static readonly DependencyProperty FullSpanProperty =
						DependencyProperty.Register( "FullSpan", typeof( SwitchState ), typeof( AdvancedUserControl ),
							new UIPropertyMetadata( SwitchState.Off,
								(dpo, ea) =>
								{
									// ReSharper disable ConvertToLambdaExpression
									((AdvancedUserControl)dpo).OnFullSpanChanged( (SwitchState)ea.OldValue, (SwitchState)ea.NewValue );
									// ReSharper restore ConvertToLambdaExpression
								} ) );

		protected virtual void OnFullSpanChanged(SwitchState oldValue, SwitchState newValue)
		{
			this.SetFullRowSpan( newValue );
			this.SetFullColumnSpan( newValue );
		}

		#endregion

		#region DependencyProperty - FullRowSpan (type of "SwitchState")

		/// <summary>
		/// Gets or sets the full row span state of the UserControl. This is a dependency property.
		/// The default value is Off. This applies only when the parent element is a Grid panel.
		/// </summary>
		public SwitchState FullRowSpan
		{
			get { return (SwitchState)GetValue( FullRowSpanProperty ); }
			set { SetValue( FullRowSpanProperty, value ); }
		}
		public static readonly DependencyProperty FullRowSpanProperty =
						DependencyProperty.Register( "FullRowSpan", typeof( SwitchState ), typeof( AdvancedUserControl ),
							new UIPropertyMetadata( SwitchState.Off,
								(dpo, ea) =>
								{
									// ReSharper disable ConvertToLambdaExpression
									((AdvancedUserControl)dpo).OnFullRowSpanChanged( (SwitchState)ea.OldValue, (SwitchState)ea.NewValue );
									// ReSharper restore ConvertToLambdaExpression
								} ) );

		protected virtual void OnFullRowSpanChanged(SwitchState oldValue, SwitchState newValue)
		{
			this.SetFullRowSpan( newValue );
		}

		#endregion

		#region DependencyProperty - FullColumnSpan (type of "SwitchState")

		/// <summary>
		/// Gets or sets the full column span state of the UserControl. This is a dependency property.
		/// The default value is Off. This applies only when the parent element is a Grid panel.
		/// </summary>
		public SwitchState FullColumnSpan
		{
			get { return (SwitchState)GetValue( FullColumnSpanProperty ); }
			set { SetValue( FullColumnSpanProperty, value ); }
		}
		public static readonly DependencyProperty FullColumnSpanProperty =
						DependencyProperty.Register( "FullColumnSpan", typeof( SwitchState ), typeof( AdvancedUserControl ),
							new UIPropertyMetadata( SwitchState.Off,
								(dpo, ea) =>
								{
									// ReSharper disable ConvertToLambdaExpression
									((AdvancedUserControl)dpo).OnFullColumnSpanChanged( (SwitchState)ea.OldValue, (SwitchState)ea.NewValue );
									// ReSharper restore ConvertToLambdaExpression
								} ) );

		protected virtual void OnFullColumnSpanChanged(SwitchState oldValue, SwitchState newValue)
		{
			this.SetFullColumnSpan( newValue );
		}

		#endregion

		#region DependencyProperty - Command (type of "ICommand")

		/// <summary>
		/// Gets or sets the command to invoke when the connected event occurs.
		/// This is a dependency property.
		/// </summary>
		public ICommand Command
		{
			get { return (ICommand)GetValue( CommandProperty ); }
			set { SetValue( CommandProperty, value ); }
		}
		public static readonly DependencyProperty CommandProperty =
									  DependencyProperty.Register( "Command", typeof( ICommand ), typeof( AdvancedUserControl ),
																			 new PropertyMetadata( null ) );
		#endregion

		#region DependencyProperty - CommandParameter (type of "Object")

		/// <summary>
		/// Gets or sets the parameter to pass to the Command property. This is a dependency property.
		/// </summary>
		public object CommandParameter
		{
			get { return GetValue( CommandParameterProperty ); }
			set { SetValue( CommandParameterProperty, value ); }
		}
		public static readonly DependencyProperty CommandParameterProperty =
									  DependencyProperty.Register( "CommandParameter", typeof( object ), typeof( AdvancedUserControl ),
																			 new PropertyMetadata( null ) );
		#endregion

		#region Protected Methods

		/// <summary>
		/// Initialize the ParentElement property with the parent of the inherited UserControl.
		/// </summary>
		/// <param name="userControl"></param>
		protected void InitializeBaseClass(UserControl userControl)
		{
			// Get the parent DependencyObject
			this.ParentElement = userControl.Parent;
		}

		/// <summary>
		/// Set the RowSpan AttachedProperty and the ColumnSpan AttachedProperty to all rows resp. all columns of the parent Grid.
		/// </summary>
		/// <param name="switchState"></param>
		protected void SetFullSpan(SwitchState switchState)
		{
			this.SetFullRowSpan( switchState );
			this.SetFullColumnSpan( switchState );
		}

		/// <summary>
		/// Set the RowSpan AttachedProperty to all rows of the parent Grid.
		/// </summary>
		/// <param name="switchState"></param>
		protected void SetFullRowSpan(SwitchState switchState)
		{
			// If the parent DependencyObject is a Grid, perform the RowSpan
			if (this.ParentElement is Grid)
			{
				Grid grid = this.ParentElement as Grid;

				// If there is more than one row ...
				if (grid.RowDefinitions.Count > 0)
				{
					if (switchState == SwitchState.On)
					{
						// ... and FullRowSpan is turned ON -> set the RowSpanProperty to all subsequent rows
						Grid.SetRowSpan( this, grid.RowDefinitions.Count - Grid.GetRow( this ) );
					}
					else
					{
						// ... otherwise clear the RowSpanProperty
						this.ClearValue( Grid.RowSpanProperty );
					}
				}
			}
		}

		/// <summary>
		/// Set the ColumnSpan AttachedProperty to all columns of the parent Grid.
		/// </summary>
		/// <param name="switchState"></param>
		protected void SetFullColumnSpan(SwitchState switchState)
		{
			// If the parent DependencyObject is a Grid, perform the ColumnSpan
			if (this.ParentElement is Grid)
			{
				Grid grid = this.ParentElement as Grid;

				// If there is more than one column ...
				if (grid.ColumnDefinitions.Count > 0)
				{
					if (switchState == SwitchState.On)
					{
						// ... and FullColumnSpan is turned ON -> set the ColumnSpanProperty to all subsequent columns
						Grid.SetColumnSpan( this, grid.ColumnDefinitions.Count - Grid.GetColumn( this ) );
					}
					else
					{
						// ... otherwise clear the ColumnSpanProperty 
						this.ClearValue( Grid.ColumnSpanProperty );
					}
				}
			}
		}

		#endregion
	}
}
