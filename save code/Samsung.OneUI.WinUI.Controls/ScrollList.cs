using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Windows.System;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

public class ScrollList : Control
{
	private const string LISTVIEW_ENTRANCE_ANIMATION_NAME = "PART_ListViewEntranceAnimation";

	private const string LISTVIEW_NAME = "PART_ListView";

	private const int INITIAL_POSITION = 1;

	private const string FOCUS_STATE_UNFOCUSED = "Unfocused";

	private const string FOCUS_STATE_FOCUSED = "Focused";

	private const int LOAD_COMPLETED_TIME = 1000;

	private const int MIN_LIST_COUNT_TO_ROTATE_TWO_ELEMENTS = 10;

	private const int INDEX_TO_ROTATE_LIST = 4;

	private const int HEIGHT_MULTIPLIER_TO_COMPARE_OFFSET = 2;

	public static readonly DependencyProperty TimeItemsSourceProperty = DependencyProperty.Register("TimeItemsSource", typeof(ObservableCollection<object>), typeof(ScrollList), new PropertyMetadata(null, OnTimeItemsSourcePropertyChanged));

	public static readonly DependencyProperty SelectedTimeProperty = DependencyProperty.Register("SelectedTime", typeof(object), typeof(ScrollList), new PropertyMetadata(null, OnSelectedItemItemPropertyChanged));

	public static readonly DependencyProperty InfiniteScrollProperty = DependencyProperty.Register("InfiniteScroll", typeof(bool), typeof(ScrollList), new PropertyMetadata(true));

	private bool _isRotating;

	internal ListView _listView;

	internal ListView _listViewEntranceAnimation;

	private Dictionary<ListView, DispatcherTimer> _dispatcherTimers;

	private DispatcherTimer _loadCompleted;

	private bool _isLoadCompleted;

	private bool _disableScrollViewChanged;

	private bool _isSelectedItemByKeyboardOrMouse;

	private readonly ScrollListAnimationService _scrollListAnimationService;

	private readonly ScrollListListViewHelperService _scrollListListViewHelperService;

	private FrameworkElementAutomationPeer _automationPeer;

	public ObservableCollection<object> TimeItemsSource
	{
		get
		{
			return (ObservableCollection<object>)GetValue(TimeItemsSourceProperty);
		}
		set
		{
			SetValue(TimeItemsSourceProperty, value);
		}
	}

	public object SelectedTime
	{
		get
		{
			return GetValue(SelectedTimeProperty);
		}
		set
		{
			SetValue(SelectedTimeProperty, value);
		}
	}

	public bool InfiniteScroll
	{
		get
		{
			return (bool)GetValue(InfiniteScrollProperty);
		}
		set
		{
			SetValue(InfiniteScrollProperty, value);
		}
	}

	public event EventHandler EntranceAnimationCompleted;

	public event EventHandler ScrollChanged;

	private static void OnTimeItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((ScrollList)d).UpdateListViewAndOrder();
	}

	private static void OnSelectedItemItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ScrollList scrollList)
		{
			scrollList.UpdateSelectedTime();
		}
	}

	public ScrollList()
	{
		base.DefaultStyleKey = typeof(ScrollList);
		base.Unloaded += TimeList_Unloaded;
		base.Loaded += ScrollList_Loaded;
		_scrollListAnimationService = new ScrollListAnimationService(this);
		_scrollListListViewHelperService = new ScrollListListViewHelperService();
	}

	protected override void OnApplyTemplate()
	{
		DisposeTimeList();
		_listView = GetTemplateChild("PART_ListView") as ListView;
		if (_listView != null)
		{
			_listView.ItemClick += ListView_ItemClick;
			_listView.IsItemClickEnabled = true;
			ConfigureListView();
			CreateScrollDispatcherTimer(_listView);
			_listView.SelectionChanged += OnListViewSelectionChanged;
			_listView.Loaded += ListView_Loaded;
		}
		_listViewEntranceAnimation = GetTemplateChild("PART_ListViewEntranceAnimation") as ListView;
		if (_listViewEntranceAnimation != null)
		{
			ConfigureListViewEntranceAnimation();
		}
		base.GotFocus += ScrollList_GotFocus;
		base.LosingFocus += ScrollList_LosingFocus;
		base.OnApplyTemplate();
	}

	private void ScrollList_GotFocus(object sender, RoutedEventArgs e)
	{
		if (base.FocusState == FocusState.Keyboard)
		{
			VisualStateManager.GoToState(this, "Focused", useTransitions: false);
		}
	}

	private void ScrollList_LosingFocus(UIElement sender, LosingFocusEventArgs args)
	{
		VisualStateManager.GoToState(this, "Unfocused", useTransitions: false);
	}

	private void ListView_ItemClick(object sender, ItemClickEventArgs e)
	{
		_isSelectedItemByKeyboardOrMouse = true;
	}

	protected override void OnPreviewKeyDown(KeyRoutedEventArgs e)
	{
		if (_listView != null && (e.Key == VirtualKey.Down || e.Key == VirtualKey.Up))
		{
			_isSelectedItemByKeyboardOrMouse = true;
			if (e.Key == VirtualKey.Down)
			{
				_listView.SelectedIndex = _scrollListListViewHelperService.IncreaseIndexOrSetFirst(_listView);
			}
			else if (e.Key == VirtualKey.Up)
			{
				_listView.SelectedIndex = _scrollListListViewHelperService.DecreaseIndexOrSetLast(_listView);
			}
			DatePickerSpinnerListItem datePickerSpinnerListItem = _listView.SelectedItem as DatePickerSpinnerListItem;
			string displayString = ((datePickerSpinnerListItem == null) ? string.Empty : (datePickerSpinnerListItem.FormattedValue + ", " + AutomationProperties.GetLocalizedControlType(this)).Trim(','));
			_automationPeer.RaiseNotificationEvent(AutomationNotificationKind.Other, AutomationNotificationProcessing.MostRecent, displayString, _listView.ToString());
		}
		base.OnPreviewKeyDown(e);
	}

	internal object GetSelectedItem()
	{
		return _listView?.SelectedItem;
	}

	public int GetSelectedIndex()
	{
		if (!(_listView != null))
		{
			return -1;
		}
		return _listView.SelectedIndex;
	}

	internal virtual void ConfigureListViewEntranceAnimation()
	{
		if (IsAnimationEnabled())
		{
			_scrollListAnimationService.ConfigureListViewEntranceAnimation();
		}
	}

	internal virtual void StopScrollAnimation()
	{
		if (IsAnimationEnabled())
		{
			_scrollListAnimationService.StopScrollAnimation();
		}
	}

	internal virtual void StartScrollAnimation()
	{
		if (IsAnimationEnabled())
		{
			_scrollListAnimationService.StartScrollAnimation();
		}
	}

	internal void OnEntranceAnimationCompleted()
	{
		this.EntranceAnimationCompleted?.Invoke(this, EventArgs.Empty);
	}

	internal virtual void EntranceAnimation(double verticalOffSet, TimeSpan duration, Action onCompleted = null)
	{
		if (IsAnimationEnabled())
		{
			_scrollListAnimationService.EntranceAnimation(verticalOffSet, duration, onCompleted);
		}
	}

	internal virtual void StopEntranceAnimation()
	{
		if (IsAnimationEnabled())
		{
			_scrollListAnimationService.StopEntranceAnimation();
		}
	}

	internal virtual void PrepareEntranceAnimation()
	{
		if (IsAnimationEnabled())
		{
			_scrollListAnimationService.PrepareEntranceAnimation();
		}
	}

	private void AnimationStop()
	{
		if (!(_listView == null) && !(_listViewEntranceAnimation == null))
		{
			_listViewEntranceAnimation.Visibility = Visibility.Collapsed;
			_listViewEntranceAnimation.Opacity = 0.0;
			_listView.Visibility = Visibility.Visible;
			_listView.Opacity = 1.0;
		}
	}

	protected internal bool IsAnimationEnabled()
	{
		UISettings uISettings = new UISettings();
		if (!uISettings.AnimationsEnabled)
		{
			AnimationStop();
		}
		return uISettings.AnimationsEnabled;
	}

	private void ConfigureListView()
	{
		if (_listView != null)
		{
			TimeItemsSource = UpdateListViewItemPositionFirstTime(TimeItemsSource, SelectedTime);
			UpdateListView();
		}
	}

	private void UpdateListView()
	{
		if (_listView != null)
		{
			UpdateTimeItemsSource();
			UpdateSelectedTime();
		}
	}

	private void UpdateListViewAndOrder()
	{
		if (_listView != null)
		{
			UpdateListView();
			UpdateListViewItemPositionFirstTime((ObservableCollection<object>)_listView.ItemsSource, _listView.SelectedItem);
		}
	}

	private void UpdateTimeItemsSource()
	{
		if (_listView != null)
		{
			_listView.ItemsSource = TimeItemsSource;
		}
	}

	private void UpdateSelectedTime()
	{
		if (_listView != null)
		{
			if (SelectedTime == null && _listView.Items.Count > 0)
			{
				_listView.SelectedIndex = 0;
			}
			else if (SelectedTime != null)
			{
				_listView.SelectedItem = SelectedTime;
			}
		}
	}

	internal async Task CenterListViewItem()
	{
		if (_listView.SelectedItem != null)
		{
			object item = _listView.SelectedItem;
			FrameworkElement frameworkElement = (FrameworkElement)_listView.ContainerFromItem(item);
			if (frameworkElement == null)
			{
				_listView.ScrollIntoView(item);
			}
			while (frameworkElement == null)
			{
				await Task.Delay(50);
				frameworkElement = (FrameworkElement)_listView.ContainerFromItem(item);
			}
			ChangeView(_listView, frameworkElement);
		}
	}

	private void ChangeView(ListView listView, FrameworkElement listViewItem)
	{
		if (listViewItem != null)
		{
			double listViewItemOffset = _scrollListListViewHelperService.GetListViewItemOffset(listViewItem, listView);
			ScrollViewer scrollViewer = listView.GetScrollViewer();
			if (scrollViewer != null)
			{
				double value = scrollViewer.VerticalOffset + listViewItemOffset;
				scrollViewer.ChangeView(null, value, null);
			}
		}
	}

	private void HighlightSelectedValue(ListView listView)
	{
		List<ListViewItem> list = new List<ListViewItem>();
		ItemCollection items = listView.Items;
		for (int i = 0; i < items.Count; i++)
		{
			_scrollListListViewHelperService.AddVisibleItemsToList(_listView, list, i);
			if (list.Count >= 4)
			{
				break;
			}
		}
		if (list.Count > 2)
		{
			int index = 1;
			if ((Math.Abs(_scrollListListViewHelperService.GetListViewItemOffset(list[2], listView)) != 0.0 || listView.SelectedIndex > 2) && Math.Abs(_scrollListListViewHelperService.GetListViewItemOffset(list[2], listView)) < Math.Abs(_scrollListListViewHelperService.GetListViewItemOffset(list[1], listView)))
			{
				index = 2;
			}
			int selectedIndex = listView.IndexFromContainer(list[index]);
			listView.SelectedIndex = selectedIndex;
		}
	}

	private void CreateScrollDispatcherTimer(ListView listView)
	{
		_dispatcherTimers = new Dictionary<ListView, DispatcherTimer>();
		DispatcherTimer dispatcherTimer = new DispatcherTimer();
		dispatcherTimer.Tick += async delegate
		{
			dispatcherTimer.Stop();
			_disableScrollViewChanged = true;
			await CenterListViewItem();
			StopScrollAnimation();
			_disableScrollViewChanged = false;
			_isSelectedItemByKeyboardOrMouse = false;
		};
		dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100.0);
		_dispatcherTimers.Add(listView, dispatcherTimer);
	}

	private void DisposeTimeList()
	{
		if (!(_listView != null))
		{
			return;
		}
		_listView.SelectionChanged -= OnListViewSelectionChanged;
		_listView.Loaded -= ListView_Loaded;
		ScrollViewer scrollViewer = _listView.GetScrollViewer();
		if (scrollViewer != null)
		{
			scrollViewer.ViewChanged -= delegate
			{
				ScrollViewer_ViewChanged(scrollViewer, _listView);
			};
		}
		_dispatcherTimers = null;
	}

	private void ScrollList_Loaded(object sender, RoutedEventArgs e)
	{
		ScrollList scrollList = sender as ScrollList;
		AutomationProperties.GetLocalizedControlType(scrollList);
		_automationPeer = new FrameworkElementAutomationPeer(scrollList);
	}

	private void TimeList_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeTimeList();
		base.GotFocus -= ScrollList_GotFocus;
		base.LosingFocus -= ScrollList_LosingFocus;
		if (_loadCompleted != null)
		{
			_loadCompleted.Tick -= LoadCompletedTimer_Tick;
			_loadCompleted = null;
		}
		_automationPeer = null;
	}

	private void ScrollViewer_ViewChanged(ScrollViewer scrollViewer, ListView listView)
	{
		if (!_disableScrollViewChanged)
		{
			StartScrollAnimation();
			UpdateListViewItemPosition(scrollViewer, listView);
			if (!_isSelectedItemByKeyboardOrMouse && _isLoadCompleted)
			{
				HighlightSelectedValue(listView);
			}
			_dispatcherTimers[listView].Start();
		}
		else
		{
			_disableScrollViewChanged = false;
		}
	}

	private void UpdateListViewItemPosition(ScrollViewer scrollViewer, ListView listView)
	{
		if (!InfiniteScroll)
		{
			return;
		}
		ObservableCollection<object> observableCollection = (ObservableCollection<object>)listView.ItemsSource;
		if (observableCollection != null)
		{
			int count = observableCollection.Count;
			int selectedIndex = listView.SelectedIndex;
			List<object> list = observableCollection.ToList();
			if (HasToRotateFromFinalToStart(scrollViewer, count, selectedIndex))
			{
				RotateFromFinalToStart(observableCollection, count, list);
			}
			else if (HasToRotateFromStartToFinal(scrollViewer, count, selectedIndex))
			{
				RotateFromStartToFinal(observableCollection, count, list);
			}
		}
	}

	private void RotateCollectionValues(int insertIndex, object item, int removeIndex, ObservableCollection<object> collection)
	{
		if (_isRotating)
		{
			return;
		}
		try
		{
			_isRotating = true;
			collection.Insert(insertIndex, item);
			collection.RemoveAt(removeIndex);
		}
		finally
		{
			_isRotating = false;
		}
	}

	private void RotateFromFinalToStart(ObservableCollection<object> collection, int listCount, List<object> list)
	{
		RotateCollectionValues(0, list.LastOrDefault(), listCount, collection);
		_disableScrollViewChanged = true;
	}

	private void RotateFromStartToFinal(ObservableCollection<object> collection, int listCount, List<object> list)
	{
		RotateCollectionValues(listCount, list.FirstOrDefault(), 0, collection);
		_disableScrollViewChanged = true;
	}

	private bool HasToRotate(int index, int listCount)
	{
		if (index > 4)
		{
			return listCount <= index + 4;
		}
		return true;
	}

	private bool HasToRotateFromFinalToStart(ScrollViewer scrollViewer, int listCount, int index)
	{
		if ((listCount >= 10) ? HasToRotate(index, listCount) : (index <= 1))
		{
			return scrollViewer.VerticalOffset <= scrollViewer.ActualHeight * 2.0;
		}
		return false;
	}

	private bool HasToRotateFromStartToFinal(ScrollViewer scrollViewer, int listCount, int index)
	{
		if ((listCount >= 10) ? HasToRotate(index, listCount) : (index == listCount - 2))
		{
			return scrollViewer.VerticalOffset >= scrollViewer.ExtentHeight - scrollViewer.ActualHeight * 2.0;
		}
		return false;
	}

	internal ObservableCollection<object> UpdateListViewItemPositionFirstTime(ObservableCollection<object> collection, object element)
	{
		if (!InfiniteScroll || collection == null || element == null)
		{
			return collection;
		}
		int count = collection.Count;
		int num = collection.IndexOf(element);
		if (num > 1)
		{
			for (int num2 = num - 1; num2 >= 2; num2--)
			{
				RotateCollectionValues(count, collection[0], 0, collection);
			}
		}
		else if (num < 1)
		{
			int num3 = ((count >= 10) ? 1 : (num + 1));
			for (int i = num; i <= num3; i++)
			{
				RotateCollectionValues(0, collection[count - 1], count, collection);
			}
		}
		else if (collection.Count > 1)
		{
			RotateCollectionValues(0, collection[count - 1], count, collection);
		}
		return collection;
	}

	private async void OnListViewSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
	{
		if (selectionChangedEventArgs.AddedItems.Count() > 0)
		{
			SelectedTime = _listView.SelectedItem;
			await CenterListViewItem();
		}
		else
		{
			_listView.SelectedItem = SelectedTime;
		}
		if (IsSelectionChangingWithoutScrollInASmallList())
		{
			UpdateListViewItemPosition(_listView.GetScrollViewer(), _listView);
			_dispatcherTimers[_listView].Start();
		}
		this.ScrollChanged?.Invoke(this, null);
	}

	private bool IsSelectionChangingWithoutScrollInASmallList()
	{
		if (_listView.ItemsSource is ObservableCollection<object> observableCollection)
		{
			if (_listView.SelectedIndex > 1)
			{
				return _listView.SelectedIndex == observableCollection.Count - 2;
			}
			return true;
		}
		return false;
	}

	private void ListView_Loaded(object sender, RoutedEventArgs e)
	{
		ListView senderListView = sender as ListView;
		if (!(senderListView != null))
		{
			return;
		}
		_disableScrollViewChanged = true;
		ScrollViewer scrollViewer = senderListView.GetScrollViewer();
		if (scrollViewer != null)
		{
			StartScrolling(senderListView, scrollViewer);
			scrollViewer.ViewChanged += delegate
			{
				ScrollViewer_ViewChanged(scrollViewer, senderListView);
			};
		}
		StartDispatcherTimerLoad();
	}

	private void StartScrolling(ListView listView, ScrollViewer scrollViewer)
	{
		if (listView != null && scrollViewer != null && listView.SelectedItem != null)
		{
			FrameworkElement frameworkElement = (FrameworkElement)listView.ContainerFromItem(listView.SelectedItem);
			if (frameworkElement != null)
			{
				double listViewItemOffset = _scrollListListViewHelperService.GetListViewItemOffset(frameworkElement, listView);
				scrollViewer.ScrollToVerticalOffset(listViewItemOffset);
			}
		}
	}

	private void LoadCompletedTimer_Tick(object sender, object e)
	{
		_loadCompleted.Stop();
		_isLoadCompleted = true;
	}

	internal void StartDispatcherTimerLoad()
	{
		_isLoadCompleted = false;
		if (_loadCompleted == null)
		{
			_loadCompleted = new DispatcherTimer();
			_loadCompleted.Tick += LoadCompletedTimer_Tick;
			_loadCompleted.Interval = TimeSpan.FromMilliseconds(1000.0);
		}
		_loadCompleted.Start();
	}
}
