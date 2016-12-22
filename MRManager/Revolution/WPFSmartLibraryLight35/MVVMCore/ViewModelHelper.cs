//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: ViewModelHelper.cs
//		namespace	: SoftArcs.WPFSmartLibrary.MVVMCore
//		class(es)	: ViewModelHelper
//							
//	--------------------------------------------------------------------
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace SoftArcs.WPFSmartLibrary.MVVMCore
{
	public class ViewModelHelper
	{
		// --------------------------------------------------------------------------------------------------------------
		// The origin of the following code snippet can be found here :
		// http://geekswithblogs.net/lbugnion/archive/2009/09/05/detecting-design-time-mode-in-wpf-and-silverlight.aspx
		// --------------------------------------------------------------------------------------------------------------
		//+-------------------------------------------- Code Snippet - START --------------------------------------------
		private static bool? isInDesignMode;

		/// <summary>
		/// Gets a value indicating whether the control is in design mode (running in Blend or Visual Studio)
		/// </summary>
		public static bool IsInDesignModeStatic
		{
			get
			{
				if (!isInDesignMode.HasValue)
				{
					DependencyProperty depProp = DesignerProperties.IsInDesignModeProperty;
					isInDesignMode = (bool)DependencyPropertyDescriptor
												  .FromProperty( depProp, typeof( FrameworkElement ) )
												  .Metadata.DefaultValue;
				}

				return isInDesignMode.Value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the control is in design mode (running under Blend or Visual Studio)
		/// </summary>
		[SuppressMessage(
		"Microsoft.Performance",
		"CA1822:MarkMembersAsStatic",
		Justification = "Non static member needed for data binding" )]
		public bool IsInDesignMode
		{
			get
			{
				return IsInDesignModeStatic;
			}
		}
		//+-------------------------------------------- Code Snippet - END --------------------------------------------
	}
}
