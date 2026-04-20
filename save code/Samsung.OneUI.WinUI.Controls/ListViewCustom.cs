using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls.DataView.Lists.Custom;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class ListViewCustom : ListView
{
	private const string PART_NOITEMS_DESCRIPTION_TEXTBLOCK = "OneUINoItemsDescriptionTextBlock";

	private const string NO_ITEMS_TEXTBLOCK = "OneUINoItemsTextBlock";

	private const string COUNTER_TEXTBLOCK = "OneUICounterTextBlock";

	private const string COUNTER_TEXT_VISIBLE = "CounterTextVisible";

	private const string NO_ITEMS_TEXT_VISIBLE = "NoItemsTextVisible";

	private const string NO_ITEMS_AND_COUNTER_TEXT_COLLAPSED = "NoItemsAndCounterTextCollapsed";

	private const string VISUAL_STATE_SINGLE_ITEM = "SingleItem";

	private const string VISUAL_STATE_HEAD_ITEM = "HeadItem";

	private const string VISUAL_STATE_BODY_ITEM = "BodyItem";

	private const string VISUAL_STATE_TAIL_ITEM = "TailItem";

	private const string NO_ITEMS_TEXT = "DREAM_NO_ITEMS_NPBODY/text";

	private const string SCROLL_VIEWER = "ScrollViewer";

	private const double DEFAULT_MAX_HEIGHT = double.PositiveInfinity;

	internal TextBlock counterTextBlock;

	internal TextBlock noItemsTextBlock;

	internal TextBlock noItemsDescriptionTextBlock;

	private long _cornerRadiusChangedToken;

	private ScrollViewer _scrollViewer;

	private bool _isMaxHeightDefinedByUser;

	public static readonly DependencyProperty NoItemsTextProperty = DependencyProperty.Register("NoItemsText", typeof(string), typeof(ListViewCustom), new PropertyMetadata(string.Empty, OnNoItemsTextPropertyChanged));

	public static readonly DependencyProperty NoItemsDescriptionProperty = DependencyProperty.Register("NoItemsDescription", typeof(string), typeof(ListViewCustom), new PropertyMetadata(string.Empty, OnNoItemsDescriptionTextPropertyChanged));

	public static readonly DependencyProperty CounterTextProperty = DependencyProperty.Register("CounterText", typeof(string), typeof(ListViewCustom), new PropertyMetadata(string.Empty, OnCounterTextPropertyChanged));

	public string NoItemsText
	{
		get
		{
			return (string)GetValue(NoItemsTextProperty);
		}
		set
		{
			SetValue(NoItemsTextProperty, value);
		}
	}

	public string NoItemsDescription
	{
		get
		{
			return (string)GetValue(NoItemsDescriptionProperty);
		}
		set
		{
			SetValue(NoItemsDescriptionProperty, value);
		}
	}

	public string CounterText
	{
		get
		{
			return (string)GetValue(CounterTextProperty);
		}
		set
		{
			SetValue(CounterTextProperty, value);
		}
	}

	private static void OnNoItemsTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ListViewCustom listViewCustom)
		{
			listViewCustom.UpdateNoItemsText();
		}
	}

	private static void OnNoItemsDescriptionTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ListViewCustom listViewCustom && listViewCustom.noItemsDescriptionTextBlock != null)
		{
			listViewCustom.noItemsDescriptionTextBlock.Text = listViewCustom.NoItemsDescription;
			listViewCustom.ConfigureNoItemTooltip(listViewCustom.noItemsDescriptionTextBlock, listViewCustom.NoItemsDescription);
		}
	}

	private static void OnCounterTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ListViewCustom listViewCustom)
		{
			listViewCustom.ChangeItemsQuantityTextVisibility();
		}
	}

	public ListViewCustom()
	{
		base.DefaultStyleKey = typeof(ListViewCustom);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		noItemsDescriptionTextBlock = GetTemplateChild("OneUINoItemsDescriptionTextBlock") as TextBlock;
		counterTextBlock = GetTemplateChild("OneUICounterTextBlock") as TextBlock;
		noItemsTextBlock = GetTemplateChild("OneUINoItemsTextBlock") as TextBlock;
		_scrollViewer = GetTemplateChild("ScrollViewer") as ScrollViewer;
		_isMaxHeightDefinedByUser = base.MaxHeight != double.PositiveInfinity;
		UpdateMaxHeight();
		RegisterListViewCustomEvent();
	}

	protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
	{
		base.PrepareContainerForItemOverride(element, item);
		if (element is ListViewItem { IsLoaded: false } listViewItem)
		{
			listViewItem.Loaded += ListItem_Loaded;
		}
	}

	protected override void OnItemsChanged(object e)
	{
		base.OnItemsChanged(e);
		LoadItems();
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new ListViewCustomAutomationPeer(this);
	}

	public ListViewItem GetItem(int itemIndex)
	{
		return ContainerFromItem(base.Items.ElementAtOrDefault(itemIndex)) as ListViewItem;
	}

	private void LoadItems()
	{
		UpdateListItems();
		ChangeItemsQuantityTextVisibility();
	}

	private void UpdateListItems()
	{
		if (base.Items == null)
		{
			return;
		}
		UIExtensionsInternal.ExecuteWhenLoaded(this, delegate
		{
			for (int i = 0; i < base.Items.Count; i++)
			{
				if (ContainerFromItem(base.Items[i]) is ListViewItem item)
				{
					UpdateItem(item, base.Items.Count);
				}
			}
		});
	}

	private void UpdateItem(ListViewItem item, int itemsCount)
	{
		int num = IndexFromContainer(item);
		if (num >= 0 && (object)item != null)
		{
			if (itemsCount == 1)
			{
				SetSingleItem(item);
			}
			else if (num == 0)
			{
				SetHeadItem(item);
			}
			else if (num == itemsCount - 1)
			{
				SetTailItem(item);
			}
			else
			{
				SetBodyItem(item);
			}
		}
	}

	private void SetSingleItem(ListViewItem item)
	{
		UIExtensionsInternal.ExecuteWhenLoaded(item, delegate
		{
			VisualStateManager.GoToState(item, "SingleItem", useTransitions: true);
			item.CornerRadius = base.CornerRadius;
		});
	}

	private void SetHeadItem(ListViewItem item)
	{
		UIExtensionsInternal.ExecuteWhenLoaded(item, delegate
		{
			VisualStateManager.GoToState(item, "HeadItem", useTransitions: true);
			item.CornerRadius = new CornerRadius(base.CornerRadius.TopLeft, base.CornerRadius.TopRight, 0.0, 0.0);
		});
	}

	private void SetBodyItem(ListViewItem item)
	{
		UIExtensionsInternal.ExecuteWhenLoaded(item, delegate
		{
			VisualStateManager.GoToState(item, "BodyItem", useTransitions: true);
			item.CornerRadius = new CornerRadius(0.0);
		});
	}

	private void SetTailItem(ListViewItem item)
	{
		UIExtensionsInternal.ExecuteWhenLoaded(item, delegate
		{
			VisualStateManager.GoToState(item, "TailItem", useTransitions: true);
			item.CornerRadius = new CornerRadius(0.0, 0.0, base.CornerRadius.BottomRight, base.CornerRadius.BottomLeft);
		});
	}

	private void ChangeItemsQuantityTextVisibility()
	{
		UIExtensionsInternal.ExecuteWhenLoaded(this, delegate
		{
			if (base.Items.Count == 0 && noItemsDescriptionTextBlock != null)
			{
				SetTextBlockText(noItemsDescriptionTextBlock, NoItemsDescription);
				VisualStateManager.GoToState(this, "NoItemsTextVisible", useTransitions: false);
			}
			else if (!string.IsNullOrWhiteSpace(CounterText) && counterTextBlock != null)
			{
				SetTextBlockText(counterTextBlock, CounterText);
				VisualStateManager.GoToState(this, "CounterTextVisible", useTransitions: false);
			}
			else
			{
				VisualStateManager.GoToState(this, "NoItemsAndCounterTextCollapsed", useTransitions: false);
			}
		});
	}

	private void SetTextBlockText(TextBlock textBlock, string text)
	{
		if (textBlock != null)
		{
			textBlock.Text = text;
		}
	}

	private void UpdateNoItemsText()
	{
		if (!(noItemsTextBlock == null))
		{
			if (string.IsNullOrEmpty(NoItemsText))
			{
				noItemsTextBlock.Text = "DREAM_NO_ITEMS_NPBODY/text".GetLocalized();
			}
			else
			{
				noItemsTextBlock.Text = NoItemsText;
			}
			ConfigureNoItemTooltip(noItemsTextBlock, noItemsTextBlock.Text);
		}
	}

	private void ConfigureNoItemTooltip(TextBlock element, string value)
	{
		if ((object)element != null)
		{
			ToolTip toolTip = ToolTipService.GetToolTip(element) as ToolTip;
			if ((object)toolTip == null)
			{
				toolTip = new ToolTip();
			}
			toolTip.Content = value;
			ToolTipService.SetToolTip(element, toolTip);
		}
	}

	private void UpdateMaxHeight()
	{
		if (!_isMaxHeightDefinedByUser && !(base.XamlRoot == null))
		{
			base.MaxHeight = base.XamlRoot.Size.Height;
		}
	}

	private void OnCornerRadiusPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (sender is ListViewCustom listViewCustom)
		{
			listViewCustom.UpdateListItems();
		}
	}

	private void ListItem_Loaded(object sender, RoutedEventArgs e)
	{
		if (sender is ListViewItem listViewItem && !(base.Items == null))
		{
			listViewItem.Loaded -= ListItem_Loaded;
			UpdateItem(listViewItem, base.Items.Count);
		}
	}

	private void ListViewCustom_Loaded(object sender, RoutedEventArgs e)
	{
		LoadItems();
		UpdateListItems();
		UpdateNoItemsText();
	}

	private void ListViewCustom_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
	{
		UpdateListItems();
	}

	private void ListViewCustom_Unloaded(object sender, RoutedEventArgs e)
	{
		UnRegisterListViewCustomEvent();
	}

	private void ListViewCustom_DragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
	{
		UpdateListItems();
	}

	private void Element_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateMaxHeight();
	}

	private void ListViewCustom_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
	{
		if (!e.IsIntermediate)
		{
			UpdateListItems();
		}
	}

	private void RegisterListViewCustomEvent()
	{
		base.Loaded += ListViewCustom_Loaded;
		base.Unloaded += ListViewCustom_Unloaded;
		base.DragItemsCompleted += ListViewCustom_DragItemsCompleted;
		base.DataContextChanged += ListViewCustom_DataContextChanged;
		if (_scrollViewer != null)
		{
			_scrollViewer.ViewChanged += ListViewCustom_ViewChanged;
		}
		if (base.XamlRoot.Content is FrameworkElement frameworkElement)
		{
			frameworkElement.SizeChanged += Element_SizeChanged;
		}
		_cornerRadiusChangedToken = RegisterPropertyChangedCallback(Control.CornerRadiusProperty, OnCornerRadiusPropertyChanged);
	}

	private void UnRegisterListViewCustomEvent()
	{
		base.Loaded -= ListViewCustom_Loaded;
		base.Unloaded -= ListViewCustom_Unloaded;
		base.DragItemsCompleted -= ListViewCustom_DragItemsCompleted;
		base.DataContextChanged -= ListViewCustom_DataContextChanged;
		if (_scrollViewer != null)
		{
			_scrollViewer.ViewChanged -= ListViewCustom_ViewChanged;
		}
		if (base.XamlRoot.Content is FrameworkElement frameworkElement)
		{
			frameworkElement.SizeChanged -= Element_SizeChanged;
		}
		UnregisterPropertyChangedCallback(Control.CornerRadiusProperty, _cornerRadiusChangedToken);
	}
}
