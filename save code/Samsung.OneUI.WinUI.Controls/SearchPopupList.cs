using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Controls.EventHandlers;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class SearchPopupList : ListView
{
	private const int RADIUS_DEFAULT_VALUE = 22;

	private const int DELAY_TO_TRIGGER_KEY_UP_OR_DOWN = 10;

	private readonly DispatcherTimer _keyDownTimer;

	private bool _canHandleKeyDown = true;

	public static readonly DependencyProperty HightlightSearchedWordsProperty = DependencyProperty.Register("HighlightSearchedWords", typeof(bool), typeof(SearchPopupList), new PropertyMetadata(true, OnHightlightSearchedWordsChanged));

	public static readonly DependencyProperty TextFilterProperty = DependencyProperty.Register("TextFilter", typeof(string), typeof(SearchPopupList), new PropertyMetadata(string.Empty, OnTextFilterChanged));

	public static readonly DependencyProperty PopupItemsProperty = DependencyProperty.Register("PopupItems", typeof(ObservableCollection<SearchPopupListItem>), typeof(SearchPopupList), new PropertyMetadata(null, OnPopupItemsPropertyChanged));

	public static readonly DependencyProperty IsCornerRadiusAutoAdjustmentEnabledProperty = DependencyProperty.Register("IsCornerRadiusAutoAdjustmentEnabled", typeof(bool), typeof(SearchPopupList), new PropertyMetadata(true, OnIsCornerRadiusAutoAdjustmentEnabledPropertyChanged));

	public bool HighlightSearchedWords
	{
		get
		{
			return (bool)GetValue(HightlightSearchedWordsProperty);
		}
		set
		{
			SetValue(HightlightSearchedWordsProperty, value);
		}
	}

	public string TextFilter
	{
		get
		{
			return (string)GetValue(TextFilterProperty);
		}
		set
		{
			SetValue(TextFilterProperty, value);
		}
	}

	public ObservableCollection<SearchPopupListItem> PopupItems
	{
		get
		{
			return (ObservableCollection<SearchPopupListItem>)GetValue(PopupItemsProperty);
		}
		set
		{
			if (PopupItems != value)
			{
				if (IsItemsPopupNotNull())
				{
					PopupItems.CollectionChanged -= ItemsChanged;
				}
				value.CollectionChanged += ItemsChanged;
				SetValue(PopupItemsProperty, value);
				if (IsCornerRadiusAutoAdjustmentEnabled)
				{
					AdjustItemsCornerRadius();
				}
			}
		}
	}

	public bool IsCornerRadiusAutoAdjustmentEnabled
	{
		get
		{
			return (bool)GetValue(IsCornerRadiusAutoAdjustmentEnabledProperty);
		}
		set
		{
			SetValue(IsCornerRadiusAutoAdjustmentEnabledProperty, value);
		}
	}

	public event EventHandler<SearchListItemEventArgs> ItemRemoved;

	public event EventHandler<SearchListItemEventArgs> ItemChosen;

	public event EventHandler Close;

	public event EventHandler ChildGotFocus;

	private static void OnHightlightSearchedWordsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SearchPopupList searchPopupList)
		{
			searchPopupList.ForceColorHighlightUpdate();
		}
	}

	private static void OnTextFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SearchPopupList searchPopupList)
		{
			searchPopupList.ForceColorHighlightUpdate();
		}
	}

	private static void OnPopupItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SearchPopupList searchPopupList)
		{
			searchPopupList.AddListItemEvents();
			searchPopupList.ItemsSource = searchPopupList.PopupItems;
			searchPopupList.ForceColorHighlightUpdate();
		}
	}

	private static void OnIsCornerRadiusAutoAdjustmentEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		SearchPopupList searchPopupList = d as SearchPopupList;
		bool flag = default(bool);
		int num;
		if ((object)searchPopupList != null)
		{
			object newValue = e.NewValue;
			if (newValue is bool)
			{
				flag = (bool)newValue;
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		else
		{
			num = 0;
		}
		if (((uint)num & (flag ? 1u : 0u)) != 0)
		{
			searchPopupList.AdjustItemsCornerRadius();
		}
	}

	public SearchPopupList()
	{
		base.Loaded += SearchPopupList_Loaded;
		_keyDownTimer = new DispatcherTimer();
		_keyDownTimer.Interval = TimeSpan.FromMilliseconds(10.0);
		_keyDownTimer.Tick += Timer_Tick;
	}

	private void Timer_Tick(object sender, object e)
	{
		_canHandleKeyDown = true;
		_keyDownTimer.Stop();
	}

	private void ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		AddListItemEvents();
		if (IsCornerRadiusAutoAdjustmentEnabled)
		{
			AdjustItemsCornerRadius();
		}
	}

	private void SearchPopupList_Loaded(object sender, RoutedEventArgs e)
	{
		AddListItemEvents();
		ForceColorHighlightUpdate();
	}

	private void ForceColorHighlightUpdate()
	{
		if (PopupItems == null)
		{
			return;
		}
		foreach (SearchPopupListItem listItem in PopupItems)
		{
			UIExtensionsInternal.ExecuteWhenLoaded(listItem, delegate
			{
				FilterTextBlock filterTextBlock = UIExtensionsInternal.FindFirstChildOfType<FilterTextBlock>(listItem);
				if (filterTextBlock != null)
				{
					ShouldHighlightText(filterTextBlock);
				}
			});
		}
	}

	private void ShouldHighlightText(FilterTextBlock itemControl)
	{
		itemControl.SearchedText = string.Empty;
		if (HighlightSearchedWords)
		{
			itemControl.SearchedText = TextFilter;
		}
		else
		{
			itemControl.ClearTextHighlight();
		}
	}

	private void AddListItemEvents()
	{
		if (!IsItemsPopupNotNull())
		{
			return;
		}
		foreach (SearchPopupListItem popupItem in PopupItems)
		{
			popupItem.RemoveRequested -= OnActionRequested;
			popupItem.RemoveRequested += OnActionRequested;
			popupItem.Chosen -= OnItemChosen;
			popupItem.Chosen += OnItemChosen;
			popupItem.ItemGotFocus -= OnItemGotFocus;
			popupItem.ItemGotFocus += OnItemGotFocus;
			popupItem.LostFocus -= OnItemLostFocus;
			popupItem.LostFocus += OnItemLostFocus;
			popupItem.ItemRemoveButtonLostFocus -= OnItemLostFocus;
			popupItem.ItemRemoveButtonLostFocus += OnItemLostFocus;
		}
	}

	private bool HasFocusOnItemsTree(XamlRoot xamlRoot)
	{
		object focusedElement = FocusManager.GetFocusedElement(xamlRoot);
		if (!(focusedElement is SearchPopupListItem) && !(focusedElement is SearchPopupListFooterButton))
		{
			return focusedElement is SearchPopupRemoveButton;
		}
		return true;
	}

	private void OnItemGotFocus(object sender, EventArgs e)
	{
		if (sender is SearchPopupListItem searchPopupListItem && IsItemsPopupNotNull() && base.Items.Count == 1)
		{
			searchPopupListItem.SetRemoveButtonFocusEvent(HandleSingleItemFocusBehaviour);
		}
	}

	private void HandleSingleItemFocusBehaviour(object sender, KeyRoutedEventArgs args)
	{
		if (args.Key == VirtualKey.Tab)
		{
			args.Handled = UIExtensionsInternal.FindFirstChildOfType<SearchPopupListFooterButton>(this)?.Focus(FocusState.Keyboard) ?? false;
		}
	}

	private void OnItemChosen(object sender, Samsung.OneUI.WinUI.Controls.EventHandlers.ItemClickEventArgs e)
	{
		if (sender is SearchPopupListItem searchPopupListItem && IsItemsPopupNotNull())
		{
			Focus(FocusState.Unfocused);
			this.ItemChosen?.Invoke(this, new SearchListItemEventArgs(searchPopupListItem, PopupItems.IndexOf(searchPopupListItem)));
			this.Close?.Invoke(this, new SearchEventArgs(SeachPopupCloseEventType.ItemSelected));
		}
	}

	private void OnActionRequested(object sender, EventArgs e)
	{
		if (sender is SearchPopupListItem searchPopupListItem && IsItemsPopupNotNull())
		{
			this.ItemRemoved?.Invoke(this, new SearchListItemEventArgs(searchPopupListItem, PopupItems.IndexOf(searchPopupListItem)));
			PopupItems.Remove(searchPopupListItem);
		}
	}

	private bool IsItemsPopupNotNull()
	{
		return PopupItems != null;
	}

	private void AdjustItemsCornerRadius()
	{
		if (PopupItems != null)
		{
			if (PopupItems.Count == 1)
			{
				AdjustUniqueItemCornerRadius();
			}
			else
			{
				AdjustMultipleItemsCornerRadius();
			}
		}
	}

	private void AdjustMultipleItemsCornerRadius()
	{
		bool flag = !IsHeaderVisible();
		bool flag2 = !IsFooterVisible();
		foreach (SearchPopupListItem popupItem in PopupItems)
		{
			if (flag && popupItem == PopupItems.FirstOrDefault())
			{
				popupItem.ApplyTopCornerRadius(22.0);
			}
			else if (flag2 && popupItem == PopupItems.LastOrDefault())
			{
				popupItem.ApplyBottomCornerRadius(22.0);
			}
			else
			{
				popupItem.CornerRadius = new CornerRadius(0.0);
			}
		}
	}

	private void AdjustUniqueItemCornerRadius()
	{
		SearchPopupListItem searchPopupListItem = PopupItems.FirstOrDefault();
		if (!(searchPopupListItem == null))
		{
			bool flag = !IsHeaderVisible();
			bool flag2 = !IsFooterVisible();
			if (flag && flag2)
			{
				searchPopupListItem.ApplyCornersRadius(22.0);
			}
			else if (flag)
			{
				searchPopupListItem.ApplyTopCornerRadius(22.0);
			}
			else if (flag2)
			{
				searchPopupListItem.ApplyBottomCornerRadius(22.0);
			}
		}
	}

	private bool IsHeaderVisible()
	{
		if (base.Header is UIElement uIElement)
		{
			return uIElement.Visibility == Visibility.Visible;
		}
		return false;
	}

	private bool IsFooterVisible()
	{
		if (base.Footer is UIElement uIElement)
		{
			return uIElement.Visibility == Visibility.Visible;
		}
		return false;
	}

	protected override void OnKeyDown(KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Escape)
		{
			Focus(FocusState.Unfocused);
			this.Close?.Invoke(this, new SearchEventArgs(SeachPopupCloseEventType.EscapeKey));
		}
		else if ((e.Key == VirtualKey.Up || e.Key == VirtualKey.Down) && _canHandleKeyDown)
		{
			base.OnKeyDown(e);
			_canHandleKeyDown = false;
			_keyDownTimer.Start();
		}
	}

	public void OnItemLostFocus(object sender, RoutedEventArgs e)
	{
		if (!HasFocusOnItemsTree(base.XamlRoot))
		{
			this.Close?.Invoke(this, EventArgs.Empty);
		}
	}
}
