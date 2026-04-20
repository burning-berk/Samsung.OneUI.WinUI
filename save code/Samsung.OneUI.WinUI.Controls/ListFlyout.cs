using System;
using System.Linq;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Services.Animation.Services.Implementations;
using Samsung.OneUI.WinUI.Services.Popup;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class ListFlyout : MenuFlyout
{
	private const double MORE_OPTIONS_ALIGNMENT_MARGIN = 8.0;

	private const string LIST_FLYOUT_BACKGROUND_RESOURCE = "OneUIListFlyoutPresenterBackground";

	public EventHandler<FlyoutBaseClosingEventArgs> MoreOptionClosing;

	public static readonly DependencyProperty IsCommandBarChildProperty = DependencyProperty.Register("IsCommandBarChild", typeof(bool), typeof(ListFlyout), new PropertyMetadata(false));

	public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(ListFlyout), new PropertyMetadata(0.0, OnHorizontalOffsetPropertyChanged));

	public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register("VerticalOffset", typeof(double), typeof(ListFlyout), new PropertyMetadata(0.0, OnVerticalOffsetPropertyChanged));

	public new static readonly DependencyProperty PlacementProperty = DependencyProperty.Register("Placement", typeof(FlyoutPlacementMode), typeof(ListFlyout), new PropertyMetadata(FlyoutPlacementMode.Bottom));

	public static readonly DependencyProperty IsBlurProperty = DependencyProperty.Register("IsBlur", typeof(bool), typeof(ListFlyout), new PropertyMetadata(true));

	private MenuFlyoutPresenter _contentPresenter;

	private readonly IPopupService _popupService;

	private readonly IFlyoutAnimationService _flyoutAnimation;

	private readonly IMenuFlyoutItemsService _menuFlyoutItemsService;

	private readonly SolidColorBrush _listFlyoutBackground;

	public bool IsCommandBarChild
	{
		get
		{
			return (bool)GetValue(IsCommandBarChildProperty);
		}
		set
		{
			SetValue(IsCommandBarChildProperty, value);
		}
	}

	public double HorizontalOffset
	{
		get
		{
			return (double)GetValue(HorizontalOffsetProperty);
		}
		set
		{
			SetValue(HorizontalOffsetProperty, value);
		}
	}

	public double VerticalOffset
	{
		get
		{
			return (double)GetValue(VerticalOffsetProperty);
		}
		set
		{
			SetValue(VerticalOffsetProperty, value);
		}
	}

	[Obsolete("Placement is deprecated, please use VerticalOffset and HorizontalOffset instead.")]
	public new FlyoutPlacementMode Placement
	{
		get
		{
			return (FlyoutPlacementMode)GetValue(PlacementProperty);
		}
		set
		{
			SetValue(PlacementProperty, value);
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

	public event EventHandler MoreOptionsTabRequested;

	private static void OnHorizontalOffsetPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is ListFlyout listFlyout)
		{
			listFlyout.UpdateListFlyoutPosition();
		}
	}

	private static void OnVerticalOffsetPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is ListFlyout listFlyout)
		{
			listFlyout.UpdateListFlyoutPosition();
		}
	}

	public ListFlyout()
	{
		base.Opening += MenuListFlyout_Opening;
		base.Opened += ListFlyout_Opened;
		base.AreOpenCloseAnimationsEnabled = false;
		base.LightDismissOverlayMode = LightDismissOverlayMode.Off;
		_listFlyoutBackground = ((SolidColorBrush)"OneUIListFlyoutPresenterBackground".GetKey()) ?? new SolidColorBrush(Colors.Transparent);
		base.Placement = FlyoutPlacementMode.Bottom;
		_popupService = new PopupService();
		_flyoutAnimation = new MenuFlyoutAnimationService();
		_menuFlyoutItemsService = new MenuFlyoutItemsService();
	}

	protected override Control CreatePresenter()
	{
		base.MenuFlyoutPresenterStyle = "OneUIListFlyoutPresenter".GetStyle();
		_contentPresenter = base.CreatePresenter() as MenuFlyoutPresenter;
		return _contentPresenter;
	}

	private void MenuListFlyout_Opening(object sender, object e)
	{
		if (sender is ListFlyout listFlyout && listFlyout.Items.Any())
		{
			foreach (ListFlyoutItem item in listFlyout.Items.OfType<ListFlyoutItem>())
			{
				item.KeyDown -= Item_KeyDownAsync;
				item.KeyDown += Item_KeyDownAsync;
			}
		}
		base.Closing += ListFlyout_Closing;
	}

	private void ListFlyout_Opened(object sender, object e)
	{
		_popupService.PreventBackdropPupups(base.XamlRoot, IsBlur);
		_menuFlyoutItemsService.ConfigureItems(base.Items);
		AssignEventToDivider();
		ConfigureSubItems();
		UpdateListFlyoutPosition();
		ContentPresenter contentPresenter = UIExtensionsInternal.FindFirstChildOfType<ContentPresenter>(_contentPresenter);
		if ((object)contentPresenter != null)
		{
			_flyoutAnimation.OpenAnimation(contentPresenter);
		}
		UpdateListFlyoutBlur(_contentPresenter);
	}

	private void ListFlyout_Closing(FlyoutBase sender, FlyoutBaseClosingEventArgs args)
	{
		base.Closing -= ListFlyout_Closing;
		args.Cancel = true;
		_popupService.CloseSubMenuPopups(base.XamlRoot, (Popup item) => item.Child is MenuFlyoutPresenter);
		_popupService.CloseAllPopups(base.XamlRoot, (Popup item) => item.Child is MenuFlyoutPresenter menuFlyoutPresenter && menuFlyoutPresenter == _contentPresenter);
		MoreOptionClosing?.Invoke(this, args);
	}

	private async void Item_KeyDownAsync(object sender, KeyRoutedEventArgs args)
	{
		if (args.Key == VirtualKey.Tab)
		{
			Hide();
			if (this.MoreOptionsTabRequested != null)
			{
				this.MoreOptionsTabRequested(this, new EventArgs());
			}
			else
			{
				await FocusManager.TryFocusAsync(base.Target, FocusState.Keyboard);
			}
		}
	}

	private void Divider_LineLoaded(object sender, RoutedEventArgs e)
	{
		if (sender is ListFlyoutSeparator listFlyoutSeparator)
		{
			double num = GetItemActualWidth() - GetPaddingCompensation(listFlyoutSeparator);
			listFlyoutSeparator.SetSeparatorEndPoint((num > 0.0) ? num : 168.0);
		}
	}

	private void ConfigureSubItems()
	{
		if (base.Items == null || !base.Items.Any())
		{
			return;
		}
		foreach (MenuFlyoutSubItem item in _menuFlyoutItemsService.FindAllMenuFlyoutItemsFromType<MenuFlyoutSubItem>(base.Items))
		{
			MenuFlyoutItemBase menuFlyoutItemBase = item.Items.FirstOrDefault();
			if ((object)menuFlyoutItemBase != null)
			{
				menuFlyoutItemBase.Loaded -= SubItem_Loaded;
				menuFlyoutItemBase.Loaded += SubItem_Loaded;
			}
		}
	}

	private void SubItem_Loaded(object sender, RoutedEventArgs e)
	{
		UpdateLayoutByMousePosition(sender);
	}

	private void UpdateLayoutByMousePosition(object sender)
	{
		Popup popup = _popupService.GetOpenPopups(base.XamlRoot).FirstOrDefault();
		if ((object)popup != null && popup.Child is MenuFlyoutPresenter menuFlyoutPresenter && sender is MenuFlyoutItemBase submenu)
		{
			MenuFlyoutSubItem menuFlyoutSubItem = _menuFlyoutItemsService.FindSubMenuParent(submenu, base.Items);
			if (!(menuFlyoutSubItem == null))
			{
				UpdateListFlyoutBlur(menuFlyoutPresenter);
				_menuFlyoutItemsService.UpdateSubMenuMargin(menuFlyoutPresenter, submenu, menuFlyoutSubItem, 8.0, base.XamlRoot.Content);
			}
		}
	}

	private static double GetPaddingCompensation(ListFlyoutSeparator separator)
	{
		return separator.Padding.Right + separator.Padding.Left + 1.0;
	}

	private void UpdateListFlyoutPosition()
	{
		try
		{
			if (base.XamlRoot == null)
			{
				return;
			}
		}
		catch (InvalidCastException)
		{
			return;
		}
		Point offset = new Point(HorizontalOffset, VerticalOffset);
		_popupService.SetPopupOffsets(_contentPresenter, base.Target, offset);
	}

	private void AssignEventToDivider()
	{
		foreach (ListFlyoutSeparator item in _menuFlyoutItemsService.ListItemDivider(base.Items))
		{
			item.LineLoaded -= Divider_LineLoaded;
			item.LineLoaded += Divider_LineLoaded;
		}
	}

	private double GetItemActualWidth()
	{
		return base.Items.FirstOrDefault((MenuFlyoutItemBase a) => !(a is ListFlyoutSeparator))?.ActualWidth ?? 0.0;
	}

	private void UpdateListFlyoutBlur(MenuFlyoutPresenter menuFlyoutPresenter)
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
