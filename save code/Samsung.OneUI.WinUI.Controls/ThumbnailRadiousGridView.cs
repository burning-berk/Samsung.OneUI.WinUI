using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class ThumbnailRadiousGridView : GridView
{
	public static readonly DependencyProperty VisualizationModeProperty = DependencyProperty.Register("VisualizationMode", typeof(ThumbnailRadiousVisualizationMode), typeof(ThumbnailRadiousGridView), new PropertyMetadata(ThumbnailRadiousVisualizationMode.Large, VisualizationModeChangedCallback));

	public ThumbnailRadiousVisualizationMode VisualizationMode
	{
		get
		{
			return (ThumbnailRadiousVisualizationMode)GetValue(VisualizationModeProperty);
		}
		set
		{
			SetValue(VisualizationModeProperty, value);
		}
	}

	private static void VisualizationModeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		ThumbnailRadiousGridView thumbnailRadiousGridView = (ThumbnailRadiousGridView)d;
		if (thumbnailRadiousGridView != null)
		{
			thumbnailRadiousGridView.UpdateVisualizationMode();
		}
	}

	public ThumbnailRadiousGridView()
	{
		base.DefaultStyleKey = typeof(ThumbnailRadiousGridView);
	}

	private void UpdateVisualizationMode()
	{
		foreach (ThumbnailRadious item in FindVisualChildren<ThumbnailRadious>(this))
		{
			item.VisualizationMode = VisualizationMode;
		}
	}

	private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
	{
		if (depObj == null)
		{
			yield return null;
		}
		for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
		{
			DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
			if (child != null && child is T)
			{
				yield return (T)child;
			}
			foreach (T item in FindVisualChildren<T>(child))
			{
				yield return item;
			}
		}
	}
}
