using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace Samsung.OneUI.WinUI.Controls;

internal class ShowVerticalScrollableIndicatorBehavior : Behavior<DependencyObject>
{
	private const int VERTICAL_OFFSET_SAFETY_SPACING = 2;

	private long _scrollableHeightToken;

	public static readonly DependencyProperty TargetScrollViewerProperty = DependencyProperty.Register("TargetScrollViewer", typeof(ScrollViewer), typeof(ShowVerticalScrollableIndicatorBehavior), new PropertyMetadata(null));

	public static readonly DependencyProperty TopIndicatorProperty = DependencyProperty.Register("TopIndicator", typeof(UIElement), typeof(ShowVerticalScrollableIndicatorBehavior), new PropertyMetadata(null, OnTopBottomIndicatorValueChanged));

	public static readonly DependencyProperty BottomIndicatorProperty = DependencyProperty.Register("BottomIndicator", typeof(UIElement), typeof(ShowVerticalScrollableIndicatorBehavior), new PropertyMetadata(null, OnTopBottomIndicatorValueChanged));

	public ScrollViewer TargetScrollViewer
	{
		get
		{
			return (ScrollViewer)GetValue(TargetScrollViewerProperty);
		}
		set
		{
			SetValue(TargetScrollViewerProperty, value);
		}
	}

	public UIElement TopIndicator
	{
		get
		{
			return (UIElement)GetValue(TopIndicatorProperty);
		}
		set
		{
			SetValue(TopIndicatorProperty, value);
		}
	}

	public UIElement BottomIndicator
	{
		get
		{
			return (UIElement)GetValue(BottomIndicatorProperty);
		}
		set
		{
			SetValue(BottomIndicatorProperty, value);
		}
	}

	protected override void OnAttached()
	{
		base.OnAttached();
		if (TargetScrollViewer != null)
		{
			TargetScrollViewer.ViewChanged += TargetScrollViewer_ViewChanged;
			_scrollableHeightToken = TargetScrollViewer.RegisterPropertyChangedCallback(ScrollViewer.ScrollableHeightProperty, ForegroundElement_ScrollableHeightChanged);
		}
	}

	protected override void OnDetaching()
	{
		base.OnDetaching();
		if (TargetScrollViewer != null)
		{
			TargetScrollViewer.ViewChanged -= TargetScrollViewer_ViewChanged;
			TargetScrollViewer.UnregisterPropertyChangedCallback(ScrollViewer.ScrollableHeightProperty, _scrollableHeightToken);
		}
	}

	private static void OnTopBottomIndicatorValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ShowVerticalScrollableIndicatorBehavior showVerticalScrollableIndicatorBehavior)
		{
			showVerticalScrollableIndicatorBehavior.SetIndicatorsOpacity();
		}
	}

	private void ForegroundElement_ScrollableHeightChanged(DependencyObject sender, DependencyProperty dp)
	{
		SetIndicatorsOpacity();
		UpdateScrollViewTabStop();
	}

	private void TargetScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
	{
		if (!(e == null))
		{
			SetIndicatorsOpacity();
		}
	}

	private void SetIndicatorsOpacity()
	{
		if (TopIndicator != null)
		{
			TopIndicator.Visibility = ComputeTopIndicatorVisibility();
		}
		if (BottomIndicator != null)
		{
			BottomIndicator.Visibility = ComputeBottomIndicatorVisibility();
		}
	}

	private void UpdateScrollViewTabStop()
	{
		if (!(TargetScrollViewer == null))
		{
			TargetScrollViewer.IsTabStop = TargetScrollViewer.ScrollableHeight > 0.0 || TargetScrollViewer.ScrollableWidth > 0.0;
		}
	}

	private Visibility ComputeTopIndicatorVisibility()
	{
		if (!CanScrollUp())
		{
			return Visibility.Collapsed;
		}
		return Visibility.Visible;
	}

	private bool CanScrollUp()
	{
		if (TargetScrollViewer != null)
		{
			return TargetScrollViewer.VerticalOffset - 2.0 > 0.0;
		}
		return false;
	}

	private Visibility ComputeBottomIndicatorVisibility()
	{
		if (!CanScrollDown())
		{
			return Visibility.Collapsed;
		}
		return Visibility.Visible;
	}

	private bool CanScrollDown()
	{
		if (TargetScrollViewer != null)
		{
			double num = TargetScrollViewer.ScrollableHeight - 2.0;
			if (num > 0.0)
			{
				return TargetScrollViewer.VerticalOffset < num;
			}
			return false;
		}
		return false;
	}
}
