using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.AttachedProperties;

public class ScrollViewer
{
	private const string VECTICAL_SCROLL_BAR = "VerticalScrollBar";

	private const string HORIZONTAL_SCROLL_BAR = "HorizontalScrollBar";

	private const string VECTICAL_THUMB = "VerticalThumb";

	private const string HORIZONTAL_THUMB = "HorizontalThumb";

	private const string CONTENT_GRID = "ContentGrid";

	public static readonly DependencyProperty IsMaskingRoundCornerProperty = DependencyProperty.RegisterAttached("IsMaskingRoundCorner", typeof(bool?), typeof(ScrollViewer), new PropertyMetadata(false, OnIsMaskingRoundCornerPropertyChanged));

	public static readonly DependencyProperty IsFirstScrollAnimationProperty = DependencyProperty.RegisterAttached("IsFirstScrollAnimation", typeof(bool?), typeof(ScrollViewer), new PropertyMetadata(false, OnIsFirstScrollAnimationPropertyChanged));

	public static readonly DependencyProperty VerticalScrollBarMarginProperty = DependencyProperty.RegisterAttached("VerticalScrollBarMargin", typeof(double), typeof(ScrollViewer), new PropertyMetadata(0));

	public static readonly DependencyProperty VerticalScrollBarSpacingFromContentProperty = DependencyProperty.RegisterAttached("VerticalScrollBarSpacingFromContent", typeof(GridLength), typeof(ScrollViewer), new PropertyMetadata(GridLength.Auto));

	public static readonly DependencyProperty MaskingRoundElementReferenceProperty = DependencyProperty.RegisterAttached("MaskingRoundElementReference", typeof(FrameworkElement), typeof(ScrollViewer), new PropertyMetadata(null, OnMaskingRoundElementReferencePropertyChanged));

	private static readonly DependencyProperty AutoHideVerticalScrollBarProperty = DependencyProperty.RegisterAttached("AutoHideVerticalScrollBar", typeof(bool?), typeof(ScrollViewer), new PropertyMetadata(true, OnAutoHideVerticalScrollBarPropertyChanged));

	private static readonly DependencyProperty AutoHideHorizontalScrollBarProperty = DependencyProperty.RegisterAttached("AutoHideHorizontalScrollBar", typeof(bool?), typeof(ScrollViewer), new PropertyMetadata(true, OnAutoHideHorizontalScrollBarPropertyChanged));

	public static bool? GetIsMaskingRoundCorner(DependencyObject obj)
	{
		return (bool?)obj.GetValue(IsMaskingRoundCornerProperty);
	}

	public static void SetIsMaskingRoundCorner(DependencyObject obj, bool? value)
	{
		obj.SetValue(IsMaskingRoundCornerProperty, value);
	}

	private static void OnIsMaskingRoundCornerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		Control control = d as Control;
		if ((object)control != null)
		{
			UIExtensionsInternal.ExecuteWhenLoaded(control, delegate
			{
				ApplyMaskingRoundCorner(control);
			});
		}
	}

	public static bool? GetIsFirstScrollAnimation(DependencyObject obj)
	{
		return (bool?)obj.GetValue(IsFirstScrollAnimationProperty);
	}

	public static void SetIsFirstScrollAnimation(DependencyObject obj, bool? value)
	{
		obj.SetValue(IsFirstScrollAnimationProperty, value);
	}

	private static void OnIsFirstScrollAnimationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		Microsoft.UI.Xaml.Controls.ScrollViewer scrollViewer = d as Microsoft.UI.Xaml.Controls.ScrollViewer;
		if ((object)scrollViewer == null || scrollViewer == null)
		{
			return;
		}
		bool isFirstScroll = false;
		EventHandler<ScrollViewerViewChangedEventArgs> ScrollViewer_ViewChanged = null;
		ScrollViewer_ViewChanged = delegate
		{
			if (!isFirstScroll)
			{
				AddFirstScrollAnimation(scrollViewer);
				isFirstScroll = true;
			}
		};
		scrollViewer.Loaded += delegate
		{
			ScrollBar scrollBar = GetScrollBar(scrollViewer, "VerticalScrollBar");
			ScrollBar scrollBar2 = GetScrollBar(scrollViewer, "HorizontalScrollBar");
			PointerEventHandler ScrollBar_PointerEntered = null;
			ScrollBar_PointerEntered = delegate
			{
				scrollViewer.ViewChanged -= ScrollViewer_ViewChanged;
			};
			PointerEventHandler ScrollBar_PointerExited = null;
			ScrollBar_PointerExited = delegate
			{
				scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
			};
			ScrollEventHandler ScrollBar_Scroll = null;
			ScrollBar_Scroll = delegate
			{
				isFirstScroll = true;
			};
			PointerEventHandler ScrollBar_PointerWheelChanged = null;
			ScrollBar_PointerWheelChanged = delegate
			{
				isFirstScroll = true;
			};
			RoutedEventHandler ScrollBar_Unloaded = null;
			ScrollBar_Unloaded = delegate(object sender, RoutedEventArgs e3)
			{
				if (sender is ScrollBar scrollBar3)
				{
					scrollBar3.PointerEntered -= ScrollBar_PointerEntered;
					scrollBar3.PointerExited -= ScrollBar_PointerExited;
					scrollBar3.PointerWheelChanged -= ScrollBar_PointerWheelChanged;
					scrollBar3.Scroll -= ScrollBar_Scroll;
					scrollBar3.Unloaded -= ScrollBar_Unloaded;
					ScrollBar_PointerEntered = null;
					ScrollBar_PointerExited = null;
					ScrollBar_PointerWheelChanged = null;
					ScrollBar_Scroll = null;
					ScrollBar_Unloaded = null;
				}
			};
			if (scrollBar != null)
			{
				scrollBar.PointerEntered += ScrollBar_PointerEntered;
				scrollBar.PointerExited += ScrollBar_PointerExited;
				scrollBar.PointerWheelChanged += ScrollBar_PointerWheelChanged;
				scrollBar.Scroll += ScrollBar_Scroll;
				scrollBar.Unloaded += ScrollBar_Unloaded;
			}
			if (scrollBar2 != null)
			{
				scrollBar2.PointerEntered += ScrollBar_PointerEntered;
				scrollBar2.PointerExited += ScrollBar_PointerExited;
				scrollBar2.PointerWheelChanged += ScrollBar_PointerWheelChanged;
				scrollBar2.Scroll += ScrollBar_Scroll;
				scrollBar2.Unloaded += ScrollBar_Unloaded;
			}
		};
		scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
		RoutedEventHandler ScrollViewer_Unloaded = null;
		ScrollViewer_Unloaded = delegate
		{
			scrollViewer.ViewChanged -= ScrollViewer_ViewChanged;
			scrollViewer.Unloaded -= ScrollViewer_Unloaded;
			ScrollViewer_ViewChanged = null;
			ScrollViewer_Unloaded = null;
		};
		scrollViewer.Unloaded += ScrollViewer_Unloaded;
	}

	private static void AddFirstScrollAnimation(Microsoft.UI.Xaml.Controls.ScrollViewer scrollViewer)
	{
		ScrollBar scrollBar = GetScrollBar(scrollViewer, "VerticalScrollBar");
		ScrollBar scrollBar2 = GetScrollBar(scrollViewer, "HorizontalScrollBar");
		CompositeTransform thumbTransform = GetThumbTransform(scrollBar, "VerticalThumb");
		CompositeTransform thumbTransform2 = GetThumbTransform(scrollBar2, "HorizontalThumb");
		new ScrollViewerAnimationService(thumbTransform, thumbTransform2).CreateFirstScrollAnimation();
	}

	private static ScrollBar GetScrollBar(Microsoft.UI.Xaml.Controls.ScrollViewer scrollViewer, string scrollBarName)
	{
		Grid grid = UIExtensionsInternal.FindChildByName<Grid>("ContentGrid", scrollViewer);
		if (grid != null)
		{
			foreach (UIElement child in grid.Children)
			{
				if (child is ScrollBar scrollBar && scrollBar.Name.Equals(scrollBarName))
				{
					return scrollBar;
				}
			}
		}
		return null;
	}

	private static CompositeTransform GetThumbTransform(ScrollBar scrollBar, string thumbName)
	{
		Thumb thumb = UIExtensionsInternal.FindChildByName<Thumb>(thumbName, scrollBar);
		if (thumb == null)
		{
			return null;
		}
		return (CompositeTransform)thumb.RenderTransform;
	}

	public static void SetVerticalScrollBarMargin(UIElement element, double value)
	{
		element.SetValue(VerticalScrollBarMarginProperty, value);
	}

	public static double GetVerticalScrollBarMargin(UIElement element)
	{
		return (double)element.GetValue(VerticalScrollBarMarginProperty);
	}

	public static GridLength GetVerticalScrollBarSpacingFromContent(DependencyObject obj)
	{
		return (GridLength)obj.GetValue(VerticalScrollBarSpacingFromContentProperty);
	}

	public static void SetVerticalScrollBarSpacingFromContent(DependencyObject obj, GridLength value)
	{
		obj.SetValue(VerticalScrollBarSpacingFromContentProperty, value);
	}

	public static FrameworkElement GetMaskingRoundElementReference(DependencyObject obj)
	{
		return (FrameworkElement)obj.GetValue(MaskingRoundElementReferenceProperty);
	}

	public static void SetMaskingRoundElementReference(DependencyObject obj, FrameworkElement value)
	{
		obj.SetValue(MaskingRoundElementReferenceProperty, value);
	}

	private static void OnMaskingRoundElementReferencePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		Control control = d as Control;
		if ((object)control != null)
		{
			UIExtensionsInternal.ExecuteWhenLoaded(GetMaskingRoundElementReference(control), delegate
			{
				UpdateMaskingRoundElementReference(control);
			});
		}
	}

	public static bool? GetAutoHideVerticalScrollBar(DependencyObject obj)
	{
		return (bool?)obj.GetValue(AutoHideVerticalScrollBarProperty);
	}

	public static void SetAutoHideVerticalScrollBar(DependencyObject obj, bool? value)
	{
		obj.SetValue(AutoHideVerticalScrollBarProperty, value);
	}

	private static void OnAutoHideVerticalScrollBarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		Microsoft.UI.Xaml.Controls.ScrollViewer scrollViewer = d as Microsoft.UI.Xaml.Controls.ScrollViewer;
		if ((object)scrollViewer == null || scrollViewer == null)
		{
			return;
		}
		scrollViewer.Loaded += delegate
		{
			ScrollBar scrollBar = GetScrollBar(scrollViewer, "VerticalScrollBar");
			Thumb thumb = UIExtensionsInternal.FindChildByName<Thumb>("VerticalThumb", scrollBar);
			if (thumb != null)
			{
				thumb.Opacity = ((!(bool)e.NewValue) ? 1 : 0);
			}
		};
	}

	public static bool? GetAutoHideHorizontalScrollBar(DependencyObject obj)
	{
		return (bool?)obj.GetValue(AutoHideHorizontalScrollBarProperty);
	}

	public static void SetAutoHideHorizontalScrollBar(DependencyObject obj, bool? value)
	{
		obj.SetValue(AutoHideHorizontalScrollBarProperty, value);
	}

	private static void OnAutoHideHorizontalScrollBarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		Microsoft.UI.Xaml.Controls.ScrollViewer scrollViewer = d as Microsoft.UI.Xaml.Controls.ScrollViewer;
		if ((object)scrollViewer == null || scrollViewer == null)
		{
			return;
		}
		scrollViewer.Loaded += delegate
		{
			ScrollBar scrollBar = GetScrollBar(scrollViewer, "HorizontalScrollBar");
			Thumb thumb = UIExtensionsInternal.FindChildByName<Thumb>("HorizontalThumb", scrollBar);
			if (thumb != null)
			{
				thumb.Opacity = ((!(bool)e.NewValue) ? 1 : 0);
			}
		};
	}

	private static void ApplyMaskingRoundCorner(Control control)
	{
		if (GetIsMaskingRoundCorner(control) == true)
		{
			control.CornerRadius = new CornerRadius(16.0);
		}
		else
		{
			control.CornerRadius = new CornerRadius(0.0);
		}
	}

	private static void UpdateMaskingRoundElementReference(Control control)
	{
		ScrollContentPresenter scrollContentPresenter = UIExtensionsInternal.FindChildByName<ScrollContentPresenter>("ScrollContentPresenter", control);
		if (!(scrollContentPresenter != null))
		{
			return;
		}
		FrameworkElement maskingElement = GetMaskingRoundElementReference(control);
		if (maskingElement != null)
		{
			UpdateScrollContentPresenterMaxWidth(scrollContentPresenter, maskingElement);
			SizeChangedEventHandler sizeChangedHandler = null;
			sizeChangedHandler = delegate
			{
				UpdateScrollContentPresenterMaxWidth(scrollContentPresenter, maskingElement);
			};
			RoutedEventHandler unloadedHandler = null;
			unloadedHandler = delegate
			{
				maskingElement.SizeChanged -= sizeChangedHandler;
				maskingElement.Unloaded -= unloadedHandler;
			};
			maskingElement.SizeChanged += sizeChangedHandler;
			maskingElement.Unloaded += unloadedHandler;
		}
	}

	private static void UpdateScrollContentPresenterMaxWidth(ScrollContentPresenter scrollContentPresenter, FrameworkElement maskingElement)
	{
		if (double.IsInfinity(maskingElement.MaxWidth) || maskingElement.MaxWidth > maskingElement.Width)
		{
			scrollContentPresenter.MaxWidth = maskingElement.Width;
		}
		else
		{
			scrollContentPresenter.MaxWidth = maskingElement.MaxWidth;
		}
	}
}
