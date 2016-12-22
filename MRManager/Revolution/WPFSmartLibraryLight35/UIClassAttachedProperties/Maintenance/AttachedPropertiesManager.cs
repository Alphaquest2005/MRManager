//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: AttachedPropertiesManager.cs
//		namespace	: SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
//		class(es)	: AttachedPropertiesManager
//							
//	--------------------------------------------------------------------
using System.Collections.Generic;
using System.Windows;

namespace SoftArcs.WPFSmartLibrary.UIClassAttachedProperties
{
	public class AttachedPropertiesManager
	{
		private static readonly Dictionary<UIElement, bool> hookedElements;

		static AttachedPropertiesManager()
		{
			hookedElements = new Dictionary<UIElement, bool>();
		}

		/// <summary>
		/// Add an UIElement to the list of hooked UIElements
		/// </summary>
		public static void HookElement(UIElement uiElement)
		{
			hookedElements.Add( uiElement, false );
		}

		/// <summary>
		/// Remove an UIElement from the list of hooked UIElements
		/// </summary>
		public static void UnhookElement(UIElement uiElement)
		{
			if (hookedElements.ContainsKey( uiElement ))
			{
				hookedElements.Remove( uiElement );
			}
		}

		/// <summary>
		/// Check whether the given UIElement is hooked
		/// </summary>
		public static bool IsHookedElement(UIElement uiElement)
		{
			return hookedElements.ContainsKey( uiElement );
		}

		/// <summary>
		/// Update animation started flag for a given UIElement
		/// </summary>
		public static bool UpdateAnimationStartedFlag(UIElement uiElement)
		{
			var animationStarted = hookedElements[uiElement];
			hookedElements[uiElement] = !animationStarted;

			return animationStarted;
		}
	}
}
