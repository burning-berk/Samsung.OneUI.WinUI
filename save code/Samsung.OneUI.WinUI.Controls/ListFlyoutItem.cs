using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls.EventHandlers;

namespace Samsung.OneUI.WinUI.Controls;

public class ListFlyoutItem : MenuFlyoutItem
{
	private const string NOTIFICATION_BADGE_ROOT_GRID = "NotificationBadgeRootGrid";

	private const string NOTIFICATION_BADGE_CONTENT_CONTROL = "ListFlyoutItemNotificationBadge";

	private const string CONTENT_TOOLTIP_NAME = "ContentTooltip";

	private const string ITEM_TEXTBLOCK = "TextBlock";

	private const string LAYOUTROOT_GRID = "LayoutRoot";

	private const string BADGE_AREA_BORDER = "BadgeAreaBorder";

	private Grid _notificationBadgeRootGrid;

	private ContentControl _notificationBadgeContentControl;

	private TextBlock _itemTextBlock;

	private Grid _layoutRoot;

	private Border _badgeAreaBorder;

	public static readonly DependencyProperty NotificationBadgeProperty = DependencyProperty.Register("NotificationBadge", typeof(BadgeBase), typeof(ListFlyoutItem), new PropertyMetadata(null, OnNotificationBadgeChanged));

	public ICommandBarItemOverflowable CommandBarItemOverflowable { get; private set; }

	public BadgeBase NotificationBadge
	{
		get
		{
			return (BadgeBase)GetValue(NotificationBadgeProperty);
		}
		set
		{
			SetValue(NotificationBadgeProperty, value);
		}
	}

	private static void OnNotificationBadgeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is ListFlyoutItem listFlyoutItem)
		{
			listFlyoutItem.UpdateNotificationBadge(e.NewValue);
		}
	}

	public ListFlyoutItem()
	{
	}

	public ListFlyoutItem(ICommandBarItemOverflowable commandBarItemOverflowable)
	{
		if (commandBarItemOverflowable != null)
		{
			CommandBarItemOverflowable = commandBarItemOverflowable;
			base.Click += delegate
			{
				CommandBarItemOverflowable.PerformButtonClick();
			};
		}
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_notificationBadgeRootGrid = GetTemplateChild("NotificationBadgeRootGrid") as Grid;
		_notificationBadgeContentControl = GetTemplateChild("ListFlyoutItemNotificationBadge") as ContentControl;
		_itemTextBlock = GetTemplateChild("TextBlock") as TextBlock;
		_layoutRoot = GetTemplateChild("LayoutRoot") as Grid;
		_badgeAreaBorder = GetTemplateChild("BadgeAreaBorder") as Border;
		_notificationBadgeRootGrid.Loaded += _notificationBadgeRootGrid_Loaded;
		UpdateNotificationBadge(NotificationBadge);
		SetItemTextBlockMaxWidth();
		ConfigureItemTooltip();
	}

	private void _notificationBadgeRootGrid_Loaded(object sender, RoutedEventArgs e)
	{
		SetItemTextBlockMaxWidth();
	}

	private void SetItemTextBlockMaxWidth()
	{
		if (_itemTextBlock != null)
		{
			_itemTextBlock.MaxWidth = GetItemTextBlockMaxWidth();
		}
	}

	private double GetItemTextBlockMaxWidth()
	{
		double result = 0.0;
		if (_layoutRoot != null && _badgeAreaBorder != null)
		{
			double num = _layoutRoot.Padding.Left + _layoutRoot.Padding.Right;
			double num2 = ((_layoutRoot.ActualWidth == 0.0) ? _layoutRoot.MaxWidth : _layoutRoot.ActualWidth);
			result = Math.Max(0.0, num2 - (_badgeAreaBorder.ActualWidth + num));
		}
		return result;
	}

	private void UpdateNotificationBadge(object newValue)
	{
		if (!(newValue is DotBadge))
		{
			if (!(newValue is NumberBadge numberBadge))
			{
				if (!(newValue is TextBadge))
				{
					if (newValue is AlertBadge)
					{
						AdjustMarginNotificationBadge(new Thickness(16.0, 0.0, 4.0, 0.0), new Thickness(0.0));
					}
				}
				else
				{
					AdjustMarginNotificationBadge(new Thickness(16.0, 0.0, 4.0, 0.0), new Thickness(0.0));
				}
			}
			else
			{
				AdjustMarginNotificationBadge(new Thickness(16.0, 0.0, 4.0, 0.0), new Thickness(0.0));
				numberBadge.ValueChanged -= NumberBadge_ValueChanged;
				numberBadge.ValueChanged += NumberBadge_ValueChanged;
			}
		}
		else
		{
			AdjustMarginNotificationBadge(new Thickness(3.0, 0.0, 0.0, 0.0), new Thickness(0.0, 6.0, 0.0, 0.0));
		}
	}

	private void NumberBadge_ValueChanged(object sender, NumberBadgeValueChangedEventArgs e)
	{
		if (sender is NumberBadge)
		{
			AdjustMarginNotificationBadge(new Thickness(16.0, 0.0, 4.0, 0.0), new Thickness(0.0));
		}
	}

	private void AdjustMarginNotificationBadge(Thickness rootGridMargin, Thickness contentControlMargin)
	{
		if (_notificationBadgeRootGrid != null && _notificationBadgeContentControl != null)
		{
			_notificationBadgeRootGrid.Margin = rootGridMargin;
			_notificationBadgeContentControl.VerticalAlignment = VerticalAlignment.Top;
			_notificationBadgeContentControl.Margin = contentControlMargin;
		}
	}

	private void ConfigureItemTooltip()
	{
		if (GetTemplateChild("ContentTooltip") is ToolTip value)
		{
			ToolTipService.SetToolTip(this, value);
		}
	}
}
