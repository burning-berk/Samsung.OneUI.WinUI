using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Samsung.OneUI.WinUI.Utils.Helpers;

internal static class UIExtensionsInternal
{
	public static T FindChildByName<T>(string name, DependencyObject startNode) where T : FrameworkElement
	{
		List<T> list = new List<T>();
		FindChildren(list, startNode);
		return list.FirstOrDefault((T occurrence) => occurrence.Name.Equals(name));
	}

	public static void FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
	{
		if (startNode == null)
		{
			return;
		}
		int childrenCount = VisualTreeHelper.GetChildrenCount(startNode);
		for (int i = 0; i < childrenCount; i++)
		{
			DependencyObject child = VisualTreeHelper.GetChild(startNode, i);
			if (child.GetType().Equals(typeof(T)) || child.GetType().GetTypeInfo().IsSubclassOf(typeof(T)))
			{
				T item = (T)child;
				results.Add(item);
			}
			FindChildren(results, child);
		}
	}

	public static T FindFirstChildOfType<T>(DependencyObject control) where T : DependencyObject
	{
		int childrenCount = VisualTreeHelper.GetChildrenCount(control);
		T val = null;
		for (int i = 0; i < childrenCount; i++)
		{
			DependencyObject child = VisualTreeHelper.GetChild(control, i);
			if (child is T result)
			{
				return result;
			}
			val = FindFirstChildOfType<T>(child);
			if (val != null)
			{
				break;
			}
		}
		return val;
	}

	public static T GetVisualParent<T>(DependencyObject child) where T : DependencyObject
	{
		T result = null;
		DependencyObject parent = VisualTreeHelper.GetParent(child);
		while (parent != null)
		{
			if (!(parent is T result2))
			{
				parent = VisualTreeHelper.GetParent(parent);
				continue;
			}
			return result2;
		}
		return result;
	}

	public static T FindChild<T>(FrameworkElement element) where T : StackPanel
	{
		int childrenCount = VisualTreeHelper.GetChildrenCount(element);
		for (int i = 0; i < childrenCount; i++)
		{
			FrameworkElement frameworkElement = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
			if (frameworkElement is StackPanel)
			{
				return (T)frameworkElement;
			}
		}
		return null;
	}

	public static List<T> GetAllChildren<T>(StackPanel father) where T : FrameworkElement
	{
		int childrenCount = VisualTreeHelper.GetChildrenCount(father);
		List<T> list = new List<T>();
		for (int i = 0; i < childrenCount; i++)
		{
			T val = VisualTreeHelper.GetChild(father, i) as T;
			if (val != null)
			{
				list.Add(val);
			}
		}
		return list;
	}

	public static UIElement GetFirstVisibleItem(this ItemsControl itemsControl)
	{
		for (int i = 0; i < itemsControl.Items.Count; i++)
		{
			UIElement uIElement = itemsControl.ContainerFromIndex(i) as UIElement;
			if (IsItemVisible(uIElement))
			{
				return uIElement;
			}
		}
		return null;
	}

	public static UIElement GetLastVisibleUIElement(this ItemsControl itemsControl)
	{
		for (int num = itemsControl.Items.Count - 1; num >= 0; num--)
		{
			UIElement uIElement = itemsControl.ContainerFromIndex(num) as UIElement;
			if (IsItemVisible(uIElement))
			{
				return uIElement;
			}
		}
		return null;
	}

	public static T FindLastVisualChild<T>(DependencyObject parent) where T : DependencyObject
	{
		if (parent == null)
		{
			return null;
		}
		for (int num = VisualTreeHelper.GetChildrenCount(parent) - 1; num >= 0; num--)
		{
			DependencyObject child = VisualTreeHelper.GetChild(parent, num);
			T val = FindLastVisualChild<T>(child);
			if (val != null)
			{
				return val;
			}
			if (child is T result)
			{
				return result;
			}
		}
		return null;
	}

	private static bool IsItemVisible(UIElement item)
	{
		if (item != null)
		{
			return item.Visibility == Visibility.Visible;
		}
		return false;
	}

	public static T GetTransform<T>(this FrameworkElement frameworkElement) where T : Transform
	{
		return (T)((!(frameworkElement.RenderTransform is T val)) ? (frameworkElement.RenderTransform = (T)Activator.CreateInstance(typeof(T))) : val);
	}

	public static void ExecuteWhenLoaded(FrameworkElement frameworkElement, Action action)
	{
		if (frameworkElement == null)
		{
			return;
		}
		if (!frameworkElement.IsLoaded)
		{
			RoutedEventHandler handler = null;
			handler = delegate
			{
				frameworkElement.Loaded -= handler;
				action?.Invoke();
			};
			frameworkElement.Loaded += handler;
		}
		else
		{
			action?.Invoke();
		}
	}

	public static bool IsDescendantOf(DependencyObject child, DependencyObject ancestor)
	{
		DependencyObject dependencyObject = child;
		while (dependencyObject != null)
		{
			if ((object)dependencyObject == ancestor)
			{
				return true;
			}
			dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
		}
		return false;
	}
}
