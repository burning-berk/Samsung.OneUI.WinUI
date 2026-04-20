using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Services.Animation.Services.Implementations;
using Samsung.OneUI.WinUI.Services.Popup;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Controls;

public class ContextMenu : MenuFlyout
{
	private const double CONTEXT_MENU_ALIGNMENT_MARGIN = 6.0;

	private const string CONTEXT_MENU_MIN_HEIGHT = "OneUIContextMenuMinHeight";

	private const string CONTEXT_MENU_STYLE_PRESENTER = "OneUIContextMenuPresenter";

	private const string CONTEXT_MENU_ITEM_DEFAULT_PADDING = "OneUIContextMenuItemPadding";

	private const double SINGLE_ITEM_MIN_HEIGHT = 40.0;

	private const double FIRST_ITEM_TOP_PADDING = 11.0;

	private const double LAST_ITEM_BOTTOM_PADDING = 13.0;

	private const double FIRST_AND_LAST_ITEM_MIN_HEIGHT = 36.0;

	private const string SUBITEM_ICON_NAME = "SubItemChevron";

	private const double CONTEXT_MENU_MIN_WIDTH = 132.0;

	private const double CONTEXT_MENU_MAX_WIDTH = 292.0;

	private const string LIST_FLYOUT_BACKGROUND_RESOURCE = "OneUIListFlyoutPresenterBackground";

	public static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double?), typeof(ContextMenu), new PropertyMetadata(null, OnWidthPropertyChanged));

	public static readonly DependencyProperty IsMultilineItemProperty = DependencyProperty.Register("IsMultilineItem", typeof(bool), typeof(ContextMenu), new PropertyMetadata(false));

	public static readonly DependencyProperty IsBlurProperty = DependencyProperty.Register("IsBlur", typeof(bool), typeof(ContextMenu), new PropertyMetadata(false));

	private double _currentSetWidth;

	private bool _firstTimeClosing = true;

	private MenuFlyoutPresenter _contentPresenter;

	private readonly IPopupService _popupService;

	private readonly IFlyoutAnimationService _flyoutAnimation;

	private readonly IMenuFlyoutItemsService _menuFlyoutItemsService;

	private readonly SolidColorBrush _listFlyoutBackground;

	public double? Width
	{
		get
		{
			return (double?)GetValue(WidthProperty);
		}
		set
		{
			SetValue(WidthProperty, value);
		}
	}

	public bool IsMultilineItem
	{
		get
		{
			return (bool)GetValue(IsMultilineItemProperty);
		}
		set
		{
			SetValue(IsMultilineItemProperty, value);
		}
	}

	public bool IsBlur
	{
		get
		{
			return (bool)GetValue(IsBlurProperty);
		}
		set
		{
			SetValue(IsBlurProperty, value);
		}
	}

	private static void OnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ContextMenu contextMenu)
		{
			contextMenu.UpdateContextMenuWidth();
		}
	}

	public ContextMenu()
	{
		base.Opening += ContextMenu_Opening;
		base.Opened += ContextMenu_Opened;
		base.Closed += ContextMenu_Closed;
		base.Closing += ContextMenu_Closing;
		base.ShouldConstrainToRootBounds = true;
		base.AreOpenCloseAnimationsEnabled = false;
		base.LightDismissOverlayMode = LightDismissOverlayMode.Off;
		_listFlyoutBackground = ((SolidColorBrush)"OneUIListFlyoutPresenterBackground".GetKey()) ?? new SolidColorBrush(Colors.Transparent);
		_popupService = new PopupService();
		_flyoutAnimation = new MenuFlyoutAnimationService();
		_menuFlyoutItemsService = new MenuFlyoutItemsService();
	}

	private void ContextMenu_Opening(object sender, object e)
	{
		UpdateLayoutToOpening();
	}

	protected override Control CreatePresenter()
	{
		base.MenuFlyoutPresenterStyle = "OneUIContextMenuPresenter".GetStyle();
		_contentPresenter = base.CreatePresenter() as MenuFlyoutPresenter;
		return _contentPresenter;
	}

	private void ContextMenu_Opened(object sender, object e)
	{
		_popupService.PreventBackdropPupups(base.XamlRoot, IsBlur);
		_menuFlyoutItemsService.ConfigureItems(base.Items);
		_menuFlyoutItemsService.SetIsMultilineItem(base.Items, IsMultilineItem);
		UpdateContextMenuWidth();
		AssignEventToDivider();
		ConfigureSubItems();
		UpdateLayoutByItemOrder(base.Items);
		UpdateContextMenuBlur(_contentPresenter);
		OpenAnimation();
	}

	private void ContextMenu_Closing(FlyoutBase sender, FlyoutBaseClosingEventArgs args)
	{
		args.Cancel = true;
		if (_firstTimeClosing && _contentPresenter != null)
		{
			_firstTimeClosing = false;
			_popupService.CloseSubMenuPopups(base.XamlRoot, (Popup item) => item.Child is MenuFlyoutPresenter);
			_popupService.CloseAllPopups(base.XamlRoot, (Popup item) => item.Child is MenuFlyoutPresenter menuFlyoutPresenter && menuFlyoutPresenter == _contentPresenter);
			_firstTimeClosing = true;
		}
	}

	private void ContextMenu_Closed(object sender, object e)
	{
		if (_contentPresenter != null)
		{
			_contentPresenter.Margin = new Thickness(0.0);
		}
	}

	private void Divider_LineLoaded(object sender, RoutedEventArgs e)
	{
		if (sender is ListFlyoutSeparator listFlyoutSeparator)
		{
			listFlyoutSeparator.SetSeparatorEndPoint(_currentSetWidth - 26.0);
		}
	}

	private void SubItem_Loaded(object sender, RoutedEventArgs e)
	{
		_menuFlyoutItemsService.SetIsMultilineItem(base.Items, IsMultilineItem);
		UpdateLayoutByMousePosition(sender);
	}

	private void ConfigureSubItems()
	{
		if (base.Items == null || !base.Items.Any())
		{
			return;
		}
		foreach (MenuFlyoutSubItem item in _menuFlyoutItemsService.FindAllMenuFlyoutItemsFromType<MenuFlyoutSubItem>(base.Items))
		{
			SetDefaultFontIconSize(item);
			MenuFlyoutItemBase menuFlyoutItemBase = item.Items.FirstOrDefault();
			if ((object)menuFlyoutItemBase != null)
			{
				menuFlyoutItemBase.Loaded -= SubItem_Loaded;
				menuFlyoutItemBase.Loaded += SubItem_Loaded;
			}
		}
		foreach (ContextMenuToggle item2 in _menuFlyoutItemsService.FindAllMenuFlyoutItemsFromType<ContextMenuToggle>(base.Items))
		{
			SetDefaultContentControlIconStyle(item2);
		}
	}

	private void SetDefaultFontIconSize(MenuFlyoutItemBase item)
	{
		FontIcon fontIcon = UIExtensionsInternal.FindChildByName<FontIcon>("SubItemChevron", item);
		if ((object)fontIcon != null)
		{
			double height = (fontIcon.Width = 16.0);
			fontIcon.Height = height;
		}
	}

	private void SetDefaultContentControlIconStyle(MenuFlyoutItemBase item)
	{
		ContentControl contentControl = UIExtensionsInternal.FindChildByName<ContentControl>("CheckGlyph", item);
		if ((object)contentControl != null)
		{
			contentControl.Margin = new Thickness(7.0, 0.0, 0.0, 0.0);
			contentControl.Style = "ContextMenuGlyphIcon".GetStyle();
		}
	}

	private void UpdateLayoutByMousePosition(object sender)
	{
		Popup popup = _popupService.GetOpenPopups(base.XamlRoot).FirstOrDefault();
		if ((object)popup != null && popup.Child is MenuFlyoutPresenter menuFlyoutPresenter && sender is MenuFlyoutItemBase submenu)
		{
			MenuFlyoutSubItem menuFlyoutSubItem = _menuFlyoutItemsService.FindSubMenuParent(submenu, base.Items);
			if (!(menuFlyoutSubItem == null))
			{
				UpdateContextMenuBlur(menuFlyoutPresenter);
				_menuFlyoutItemsService.UpdateSubMenuMargin(menuFlyoutPresenter, submenu, menuFlyoutSubItem, 6.0, base.XamlRoot.Content);
				UpdateLayoutByItemOrder(menuFlyoutSubItem.Items);
			}
		}
	}

	private void OpenAnimation()
	{
		ContentPresenter contentPresenter = UIExtensionsInternal.FindFirstChildOfType<ContentPresenter>(_contentPresenter);
		if ((object)contentPresenter != null)
		{
			UpdateOpeningSideAnimation(contentPresenter);
			_flyoutAnimation.OpenAnimation(contentPresenter);
		}
	}

	private void UpdateOpeningSideAnimation(FrameworkElement element)
	{
		Windows.Foundation.Size screenSize = ScreenHelpers.GetScreenSize(base.XamlRoot);
		screenSize.Width = UpdateByRasterizationScale(screenSize.Width);
		screenSize.Height = UpdateByRasterizationScale(screenSize.Height);
		System.Drawing.Point mousePosition = MouseHelpers.GetMousePosition();
		double num = (double)mousePosition.X + UpdateByRasterizationScale(element.ActualWidth);
		double num2 = (double)mousePosition.Y + UpdateByRasterizationScale(element.ActualHeight);
		int num3 = 1;
		int num4 = 0;
		int num5 = 1;
		int num6 = 0;
		int num7 = ((num > screenSize.Width) ? num3 : num4);
		int num8 = ((num2 > screenSize.Height) ? num5 : num6);
		element.RenderTransformOrigin = new Windows.Foundation.Point(num7, num8);
	}

	private double UpdateByRasterizationScale(double value)
	{
		return value * base.XamlRoot.RasterizationScale;
	}

	private void UpdateLayoutByItemOrder(IList<MenuFlyoutItemBase> contextMenuItems)
	{
		if (contextMenuItems == null || !contextMenuItems.Any())
		{
			return;
		}
		IEnumerable<MenuFlyoutItemBase> source = contextMenuItems.Where((MenuFlyoutItemBase item) => item.Visibility == Visibility.Visible);
		if (!source.Any())
		{
			return;
		}
		double? num = "OneUIContextMenuMinHeight".GetKey() as double?;
		Thickness? thickness = "OneUIContextMenuItemPadding".GetKey() as Thickness?;
		if (!thickness.HasValue || !num.HasValue)
		{
			return;
		}
		if (source.Count() == 1)
		{
			MenuFlyoutItemBase menuFlyoutItemBase = source.First();
			if ((object)menuFlyoutItemBase != null)
			{
				Thickness thickness2 = new Thickness(menuFlyoutItemBase.Padding.Left, 11.0, menuFlyoutItemBase.Padding.Right, 13.0);
				ApplyMinHeightAndPaddingToItems(menuFlyoutItemBase, 40.0, thickness2);
				return;
			}
		}
		MenuFlyoutItemBase menuFlyoutItemBase2 = source.First();
		if ((object)menuFlyoutItemBase2 != null)
		{
			Thickness thickness3 = new Thickness(menuFlyoutItemBase2.Padding.Left, 11.0, menuFlyoutItemBase2.Padding.Right, thickness.Value.Bottom);
			ApplyMinHeightAndPaddingToItems(menuFlyoutItemBase2, 36.0, thickness3);
		}
		MenuFlyoutItemBase menuFlyoutItemBase3 = source.Last();
		if ((object)menuFlyoutItemBase3 != null)
		{
			Thickness thickness4 = new Thickness(menuFlyoutItemBase3.Padding.Left, thickness.Value.Top, menuFlyoutItemBase3.Padding.Right, 13.0);
			ApplyMinHeightAndPaddingToItems(menuFlyoutItemBase3, 36.0, thickness4);
		}
		foreach (MenuFlyoutItemBase item in from middleItem in source.Skip(1).Take(source.Count() - 2)
			where !(middleItem is MenuFlyoutSeparator)
			select middleItem)
		{
			ApplyMinHeightAndPaddingToItems(item, num.Value, thickness.Value);
		}
	}

	private void ApplyMinHeightAndPaddingToItems(MenuFlyoutItemBase item, double minHeight, Thickness thickness)
	{
		item.Padding = thickness;
		item.MinHeight = minHeight;
	}

	private void UpdateContextMenuWidth()
	{
		double prevailingWidth = GetPrevailingWidth();
		SetItemsWidth(prevailingWidth);
		_currentSetWidth = prevailingWidth;
	}

	private void UpdateLayoutToOpening()
	{
		double prevailingWidth = GetPrevailingWidth();
		double itemsWidth = ((prevailingWidth == 292.0) ? (prevailingWidth - 1.0) : (prevailingWidth + 1.0));
		SetItemsWidth(itemsWidth);
	}

	private double GetPrevailingWidth()
	{
		if (!Width.HasValue)
		{
			return GetPrevailingWidthWithinRange(GetItemsMaxActualWidth());
		}
		return GetPrevailingWidthWithinRange(Width.Value);
	}

	private double GetPrevailingWidthWithinRange(double width)
	{
		if (width < 132.0)
		{
			return 132.0;
		}
		if (width > 292.0)
		{
			return 292.0;
		}
		return width;
	}

	private void SetItemsWidth(double width)
	{
		foreach (MenuFlyoutItemBase item in base.Items.Where((MenuFlyoutItemBase a) => !(a is ListFlyoutSeparator)))
		{
			item.UpdateLayout();
			item.Width = width;
		}
	}

	private double GetItemsMaxActualWidth()
	{
		if (base.Items != null && base.Items.Any())
		{
			return base.Items.Max((MenuFlyoutItemBase item) => item.ActualWidth);
		}
		return 0.0;
	}

	private void AssignEventToDivider()
	{
		IEnumerable<MenuFlyoutItemBase> source = _menuFlyoutItemsService.ListItemDivider(base.Items);
		if (source.Any())
		{
			source.OfType<ListFlyoutSeparator>().ToList().ForEach(delegate(ListFlyoutSeparator divider)
			{
				divider.LineLoaded += Divider_LineLoaded;
			});
		}
	}

	private void UpdateContextMenuBlur(MenuFlyoutPresenter menuFlyoutPresenter)
	{
		if (!(menuFlyoutPresenter == null))
		{
			ScrollViewer scrollViewer = UIExtensionsInternal.FindFirstChildOfType<ScrollViewer>(menuFlyoutPresenter);
			if ((object)scrollViewer != null)
			{
				scrollViewer.Background = (IsBlur ? new SolidColorBrush(Colors.Transparent) : (((SolidColorBrush)"OneUIListFlyoutPresenterBackground".GetKey()) ?? new SolidColorBrush(Colors.Transparent)));
			}
		}
	}
}
