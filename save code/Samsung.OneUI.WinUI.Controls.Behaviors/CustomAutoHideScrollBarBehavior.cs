using System;
using System.Collections.Generic;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.Xaml.Interactivity;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Behaviors;

public abstract class CustomAutoHideScrollBarBehavior : Behavior<ScrollViewer>
{
	private const int DEFAULT_AUTO_HIDE_INTERVAL_MILLISECONDS = 3000;

	private const double COMPOSITE_SCALE_100_PERCENT = 1.0;

	private const string VISUAL_STATE_NAME_NO_INDICATOR = "NoIndicator";

	public static readonly DependencyProperty IntervalAutoHideProperty = DependencyProperty.Register("Interval", typeof(int), typeof(CustomAutoHideScrollBarBehavior), new PropertyMetadata(3000));

	public int Interval
	{
		get
		{
			return (int)GetValue(IntervalAutoHideProperty);
		}
		set
		{
			SetValue(IntervalAutoHideProperty, value);
		}
	}

	protected abstract string GetScrollBarElementName();

	protected abstract string GetScrollRootGridElementName();

	protected abstract double GetScrollableCurrentSize(ScrollViewer scrollViewer);

	protected override void OnAttached()
	{
		base.OnAttached();
		if (base.AssociatedObject != null)
		{
			base.AssociatedObject.ViewChanged += TargetScrollViewer_ViewChanged;
			base.AssociatedObject.PointerMoved += TargetScrollViewer_PointerMoved;
		}
	}

	protected override void OnDetaching()
	{
		base.OnDetaching();
		if (base.AssociatedObject != null)
		{
			base.AssociatedObject.ViewChanged -= TargetScrollViewer_ViewChanged;
			base.AssociatedObject.PointerMoved -= TargetScrollViewer_PointerMoved;
		}
	}

	private void TargetScrollViewer_PointerMoved(object sender, PointerRoutedEventArgs e)
	{
		StartTimerOnPointerMoved(sender as ScrollViewer);
	}

	private void TargetScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
	{
		StartTimerOnViewChanged(sender as ScrollViewer, !e.IsIntermediate);
	}

	private void StartTimerOnViewChanged(ScrollViewer scrollViewer, bool executeTimer)
	{
		if (scrollViewer == null || GetScrollableCurrentSize(scrollViewer) == 0.0)
		{
			return;
		}
		DispatcherTimer internalTimer = GetInternalTimer(scrollViewer);
		if (!(internalTimer == null))
		{
			internalTimer.Stop();
			if (executeTimer && CanHideScrollBarOnViewChanged(scrollViewer))
			{
				internalTimer.Start();
			}
		}
	}

	private bool CanHideScrollBarOnViewChanged(ScrollViewer scrollViewer)
	{
		Grid startNode = UIExtensionsInternal.FindChildByName<Grid>(GetScrollRootGridElementName(), scrollViewer);
		List<DependencyObject> list = new List<DependencyObject>();
		UIExtensionsInternal.FindChildren(list, startNode);
		foreach (DependencyObject item in list)
		{
			if (item is ButtonBase { IsPointerOver: not false })
			{
				return false;
			}
			if (item is Thumb { RenderTransform: CompositeTransform { ScaleX: 1.0, ScaleY: 1.0 } })
			{
				return false;
			}
		}
		return true;
	}

	private void StartTimerOnPointerMoved(ScrollViewer scrollViewer)
	{
		if (!(scrollViewer == null))
		{
			DispatcherTimer internalTimer = GetInternalTimer(scrollViewer);
			if (!(internalTimer == null))
			{
				internalTimer.Stop();
				internalTimer.Start();
			}
		}
	}

	private DispatcherTimer GetInternalTimer(ScrollViewer scrollViewer)
	{
		return CreateInternalTimer(scrollViewer);
	}

	private DispatcherTimer CreateInternalTimer(ScrollViewer scrollViewer)
	{
		DispatcherTimer dispatcherTimer = new DispatcherTimer();
		dispatcherTimer.Interval = TimeSpan.FromMilliseconds(Interval);
		dispatcherTimer.Tick += async delegate(object? sender, object args)
		{
			await scrollViewer.DispatcherQueue.EnqueueAsync(delegate
			{
				if (UIExtensionsInternal.FindChildByName<ScrollBar>(GetScrollBarElementName(), scrollViewer) != null)
				{
					VisualStateManager.GoToState(scrollViewer, "NoIndicator", useTransitions: true);
				}
				if (sender is DispatcherTimer dispatcherTimer2)
				{
					dispatcherTimer2.Stop();
				}
			});
		};
		return dispatcherTimer;
	}
}
