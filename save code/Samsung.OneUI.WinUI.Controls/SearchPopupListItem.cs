using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Controls.EventHandlers;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class SearchPopupListItem : ListViewItem
{
	private const string ELEMENT_NAME_REMOVE_BUTTON = "RemoveButton";

	private const string ELEMENT_NAME_REMOVE_BUTTON_TOOLTIP = "RemoveButtonTooltip";

	private const string CONTAINS_IMAGE_STATE = "ContainsImage";

	private const string CONTAINS_ICON_STYLE_STATE = "ContainsIconStyle";

	private const string CONTAINS_IMAGE_AND_ICON_STYLE_STATE = "ContainsImageAndIconStyle";

	private const string DOES_NOT_CONTAIN_IMAGE_AND_ICON_STYLE_STATE = "DoesNotContainsImageAndIconStyle";

	private const string FILTER_TEXTBLOCK = "ItemText";

	private const string LIST_ITEM_NORMAL_STATE = "Normal";

	private const string LIST_ITEM_HOVER_STATE = "PointerOver";

	private SearchPopupRemoveButton _removeButton;

	private ToolTip _removeButtonTooltip;

	private bool _isRemoveButtonClicked;

	private FilterTextBlock _filterTextBlock;

	public static readonly DependencyProperty RemoveButtonTooltipMarginProperty = DependencyProperty.Register("RemoveButtonTooltipMargin", typeof(Thickness), typeof(SearchPopupListItem), new PropertyMetadata(new Thickness(0.0)));

	public static readonly DependencyProperty RemoveButtonTooltipVerticalOffsetProperty = DependencyProperty.Register("RemoveButtonTooltipVerticalOffset", typeof(double), typeof(SearchPopupListItem), new PropertyMetadata(0));

	public static readonly DependencyProperty RemoveButtonTooltipContentProperty = DependencyProperty.Register("RemoveButtonTooltipContent", typeof(string), typeof(SearchPopupListItem), new PropertyMetadata(string.Empty, OnRemoveButtonTooltipContentPropertyChanged));

	public static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(SearchPopupListItem), new PropertyMetadata(null));

	public static readonly DependencyProperty RemoveButtonVisibilityProperty = DependencyProperty.Register("RemoveButtonVisibility", typeof(Visibility), typeof(SearchPopupListItem), new PropertyMetadata(Visibility.Collapsed));

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SearchPopupListItem), new PropertyMetadata(string.Empty, OnTextPropertyChanged));

	public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(SearchPopupListItem), new PropertyMetadata(null, OnImagePropertyChanged));

	public static readonly DependencyProperty IconSvgStyleProperty = DependencyProperty.Register("IconSvgStyle", typeof(Style), typeof(SearchPopupListItem), new PropertyMetadata(null, OnIconSvgStylePropertyChanged));

	public Thickness RemoveButtonTooltipMargin
	{
		get
		{
			return (Thickness)GetValue(RemoveButtonTooltipMarginProperty);
		}
		set
		{
			SetValue(RemoveButtonTooltipMarginProperty, value);
		}
	}

	public double RemoveButtonTooltipVerticalOffset
	{
		get
		{
			return (double)GetValue(RemoveButtonTooltipVerticalOffsetProperty);
		}
		set
		{
			SetValue(RemoveButtonTooltipVerticalOffsetProperty, value);
		}
	}

	public string RemoveButtonTooltipContent
	{
		get
		{
			return (string)GetValue(RemoveButtonTooltipContentProperty);
		}
		set
		{
			SetValue(RemoveButtonTooltipContentProperty, value);
		}
	}

	public int Id
	{
		get
		{
			return (int)GetValue(IdProperty);
		}
		set
		{
			SetValue(IdProperty, value);
		}
	}

	public Visibility RemoveButtonVisibility
	{
		get
		{
			return (Visibility)GetValue(RemoveButtonVisibilityProperty);
		}
		set
		{
			SetValue(RemoveButtonVisibilityProperty, value);
		}
	}

	public string Text
	{
		get
		{
			return (string)GetValue(TextProperty);
		}
		set
		{
			SetValue(TextProperty, value);
		}
	}

	public ImageSource Image
	{
		get
		{
			return (ImageSource)GetValue(ImageProperty);
		}
		set
		{
			SetValue(ImageProperty, value);
		}
	}

	public Style IconSvgStyle
	{
		get
		{
			return (Style)GetValue(IconSvgStyleProperty);
		}
		set
		{
			SetValue(IconSvgStyleProperty, value);
		}
	}

	public event EventHandler RemoveRequested;

	public event EventHandler ItemGotFocus;

	public event RoutedEventHandler ItemRemoveButtonLostFocus;

	public event EventHandler<Samsung.OneUI.WinUI.Controls.EventHandlers.ItemClickEventArgs> Chosen;

	private static void OnRemoveButtonTooltipContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SearchPopupListItem searchPopupListItem)
		{
			searchPopupListItem.UpdateRemoveButtonTooltipStatesVisibility();
		}
	}

	private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SearchPopupListItem searchPopupListItem)
		{
			searchPopupListItem.SetAccessibilityName();
		}
	}

	private static void OnImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SearchPopupListItem searchPopupListItem)
		{
			searchPopupListItem.UpdateIconStatesVisibility();
		}
	}

	private static void OnIconSvgStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SearchPopupListItem searchPopupListItem)
		{
			searchPopupListItem.UpdateIconStatesVisibility();
		}
	}

	public SearchPopupListItem()
	{
		base.GotFocus += SearchPopupListItemGotFocus;
		base.LostFocus += SearchPopupListItem_LostFocus;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_removeButton = GetTemplateChild("RemoveButton") as SearchPopupRemoveButton;
		_filterTextBlock = GetTemplateChild("ItemText") as FilterTextBlock;
		UpdateIconStatesVisibility();
		InitElementsInstance();
		UpdateRemoveButtonTooltipStatesVisibility();
		RegisterEventsForCloseButton();
		SetAccessibilityName();
	}

	private void SetAccessibilityName()
	{
		if (!string.IsNullOrEmpty(Text))
		{
			SetValue(AutomationProperties.NameProperty, Text);
		}
	}

	protected override void OnTapped(TappedRoutedEventArgs e)
	{
		if (!_isRemoveButtonClicked)
		{
			base.OnTapped(e);
			this.Chosen?.Invoke(this, new Samsung.OneUI.WinUI.Controls.EventHandlers.ItemClickEventArgs
			{
				ClickedItem = this
			});
		}
		_isRemoveButtonClicked = false;
	}

	protected override void OnKeyDown(KeyRoutedEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.Key == VirtualKey.Enter)
		{
			this.Chosen?.Invoke(this, new Samsung.OneUI.WinUI.Controls.EventHandlers.ItemClickEventArgs
			{
				ClickedItem = this
			});
		}
	}

	private void UpdateRemoveButtonTooltipStatesVisibility()
	{
		_removeButtonTooltip = GetTemplateChild("RemoveButtonTooltip") as ToolTip;
		if (_removeButtonTooltip != null)
		{
			_removeButtonTooltip.Visibility = (string.IsNullOrEmpty(RemoveButtonTooltipContent) ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	private void InitElementsInstance()
	{
		_removeButton = GetTemplateChild("RemoveButton") as SearchPopupRemoveButton;
	}

	private void RegisterEventsForCloseButton()
	{
		if (_removeButton != null)
		{
			_removeButton.Click += RemoveButtonClick;
			_removeButton.LostFocus += RemoveButtonLostFocus;
			_removeButton.PointerEntered += RemoveButtonPointerEntered;
			_removeButton.PointerExited += RemoveButtonPointerExited;
		}
	}

	private void RemoveButtonPointerExited(object sender, PointerRoutedEventArgs e)
	{
		VisualStateManager.GoToState(this, "PointerOver", useTransitions: false);
	}

	private void RemoveButtonPointerEntered(object sender, PointerRoutedEventArgs e)
	{
		VisualStateManager.GoToState(this, "Normal", useTransitions: false);
	}

	private void RemoveButtonLostFocus(object sender, RoutedEventArgs e)
	{
		this.ItemRemoveButtonLostFocus?.Invoke(this, null);
	}

	private void SearchPopupListItemGotFocus(object sender, RoutedEventArgs e)
	{
		this.ItemGotFocus?.Invoke(this, new EventArgs());
		_filterTextBlock?.SetFocusTrimmedTextTooltip(isOpen: true);
	}

	private void SearchPopupListItem_LostFocus(object sender, RoutedEventArgs e)
	{
		_filterTextBlock?.SetFocusTrimmedTextTooltip(isOpen: false);
	}

	private void RemoveButtonClick(object sender, RoutedEventArgs e)
	{
		_isRemoveButtonClicked = true;
		this.RemoveRequested?.Invoke(this, new EventArgs());
	}

	private void UpdateIconStatesVisibility()
	{
		if (Image == null && IconSvgStyle == null)
		{
			VisualStateManager.GoToState(this, "DoesNotContainsImageAndIconStyle", useTransitions: false);
		}
		else
		{
			HandleIconVisibility();
		}
	}

	private void HandleIconVisibility()
	{
		if (Image != null && IconSvgStyle != null)
		{
			VisualStateManager.GoToState(this, "ContainsImageAndIconStyle", useTransitions: false);
		}
		else if (Image != null && IconSvgStyle == null)
		{
			VisualStateManager.GoToState(this, "ContainsImage", useTransitions: false);
		}
		else
		{
			VisualStateManager.GoToState(this, "ContainsIconStyle", useTransitions: false);
		}
	}

	public void SetRemoveButtonFocusEvent(Action<object, KeyRoutedEventArgs> eventHandler)
	{
		if (_removeButton != null)
		{
			_removeButton.KeyDown += delegate(object sender, KeyRoutedEventArgs args)
			{
				eventHandler(sender, args);
			};
		}
	}
}
