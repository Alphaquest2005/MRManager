//	--------------------------------------------------------------------
//		Member of the WPFSmartLibrary
//		For more information see : http://wpfsmartlibrary.codeplex.com/
//		(by DotNetMastermind)
//
//		filename		: SmartVisualTreeHelper.cs
//		namespace	: SoftArcs.WPFSmartLibrary.CommonHelper
//		class(es)	: SmartVisualTreeHelper
//							
//	--------------------------------------------------------------------
using System.Windows;
using System.Windows.Media;

namespace SoftArcs.WPFSmartLibrary.CommonHelper
{
	public class SmartVisualTreeHelper
	{
		/// <summary>
		/// Find the "nth" visual child of the given type in the visual tree (traverse down).
		/// </summary>
		/// <typeparam name="Type">The type of the searched visual item.</typeparam>
		/// <param name="dependencyObject">A direct parent of the searched visual item.</param>
		/// <param name="nthItem">The sequence number of the child in the visual tree.</param>
		/// <returns>The found visual child or null if no appropriate child was found.</returns>
		public static Type FindVisualChild<Type>(DependencyObject dependencyObject, int nthItem = 1)
				  where Type : DependencyObject
		{
			int itemCount = 0;

			for (int i = 0 ; i < VisualTreeHelper.GetChildrenCount( dependencyObject ) ; i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild( dependencyObject, i );

				if (child is Type)
				{
					itemCount++;
					if (itemCount == nthItem)
					{
						return (Type)child;
					}
				}
				else
				{
					var childOfChild = FindVisualChild<Type>( child );

					if (childOfChild != null)
					{
						return childOfChild;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Find the first visual child of the given type with the given name in the visual tree (traverse down). 
		/// </summary>
		/// <typeparam name="Type">The type of the searched visual item.</typeparam>
		/// <param name="dependencyObject">A direct parent of the searched visual item.</param>
		/// <param name="childName">The name of the child.</param>
		/// <returns>The first child item that matches the type and nameor null if no appropriate child was found.</returns>
		public static Type FindVisualChildByName<Type>(DependencyObject dependencyObject, string childName)
				  where Type : DependencyObject
		{
			// Confirm parent and childName are valid. 
			if (dependencyObject == null) return null;

			Type foundChild = null;

			int childrenCount = VisualTreeHelper.GetChildrenCount( dependencyObject );
			for (int i = 0 ; i < childrenCount ; i++)
			{
				var child = VisualTreeHelper.GetChild( dependencyObject, i );
				// If the child is not of the request child type child
				Type childType = child as Type;
				if (childType == null)
				{
					// recursively drill down the tree
					foundChild = FindVisualChildByName<Type>( child, childName );

					// If the child is found, break so we do not overwrite the found child. 
					if (foundChild != null) break;
				}
				else if (!string.IsNullOrEmpty( childName ))
				{
					var frameworkElement = child as FrameworkElement;
					// If the child's name is set for search
					if (frameworkElement != null && frameworkElement.Name == childName)
					{
						// if the child's name is of the request name
						foundChild = (Type)child;
						break;
					}
				}
				else
				{
					// child element found.
					foundChild = (Type)child;
					break;
				}
			}

			return foundChild;
		}

		/// <summary>
		/// Find the first ancestor element of the given type in the visual tree (traverse up).
		/// </summary>
		/// <typeparam name="Type">The type of the searched visual item.</typeparam>
		/// <param name="dependencyObject">A direct child of the searched visual item.</param>
		/// <returns>The found visual ancestor or null if no appropriate ancestor was found.</returns>
		public static Type FindAncestor<Type>(DependencyObject dependencyObject)
				  where Type : class
		{
			DependencyObject target = dependencyObject;
			do
			{
				target = VisualTreeHelper.GetParent( target );
			}
			while (target != null && !(target is Type));

			return target as Type;
		}
	}
}
