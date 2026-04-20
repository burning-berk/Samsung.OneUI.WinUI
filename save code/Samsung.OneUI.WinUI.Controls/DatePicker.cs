using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.UI.System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Services.Animation.Services.Implementations;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Globalization;
using Windows.System;
using Windows.System.UserProfile;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

public class DatePicker : CalendarView
{
	private const int DISPLAYED_DAYS_IN_CALENDAR = 42;

	private const string TODAY_DATE = "TodayDate";

	private const string TODAY_DATE_FOCUSED = "TodayDateFocused";

	private const string SUNDAY_OUT_SCOPE_DATE_PREV = "SundayOutOfScopeDatePrev";

	private const string SUNDAY_OUT_SCOPE_DATE_NEXT = "SundayOutOfScopeDateNext";

	private const string SUNDAY_SCOPED_DATE = "SundayScopedDate";

	private const string OUT_SCOPE_DATE_PREV = "OutOfScopeDatePrev";

	private const string OUT_SCOPE_DATE_NEXT = "OutOfScopeDateNext";

	private const string NORMAL_SCOPED_DATE = "NormalScopedDate";

	private const string FOCUS_STATE_UNFOCUSED = "Unfocused";

	private const string FOCUS_STATE_FOCUSED = "Focused";

	private const string PRESSED_STATE = "Pressed";

	private const string FOCUS_STATE_UNFOCUSED_INTERNAL = "UnfocusedInternal";

	private const string PREVIOUS_MONTH_BUTTON = "PreviousMonthButton";

	private const string HEADER_DATE_BUTTON = "HeaderDateButton";

	private const string NEXT_MONTH_BUTTON = "NextMonthButton";

	private const string DAY_OF_WEEK_FORMAT_3 = "{dayofweek.abbreviated(3)}";

	private const string DAY_OF_WEEK_FORMAT_1 = "{dayofweek.abbreviated(1)}";

	private const string DAY_NUMBER_TEXT_HIGHCONTRAST_NAME = "DayNumberTextHighContrast";

	private const string DATE_PICKER_SPINNER_PANEL = "DatePickerSpinnerPanel";

	private const string DATE_PICKER_PANEL = "DatePickerPanel";

	private const string DATE_PICKER_HEADER_TEXT_BLOCK = "HeaderDateTextBlock";

	private const double DATE_PICKER_TEXT_SIZE_SCALE_MAX = 2.25;

	private const double TEXT_SCALE_BREAKPOINT = 1.4;

	private const int SECOND_DAY_OF_THE_WEEK = 2;

	private const int THIRD_DAY_OF_THE_WEEK = 3;

	private const int FOURTH_DAY_OF_THE_WEEK = 4;

	private const int FIFTH_DAY_OF_THE_WEEK = 5;

	private const int SIXTH_DAY_OF_THE_WEEK = 6;

	private const int SEVENTH_DAY_OF_THE_WEEK = 7;

	private const int DATE_PICKER_ITEM_HIGHTLIGHTED_SMALL_SIZE = 28;

	private const int DATE_PICKER_ITEM_HIGHTLIGHTED_MEDIUM_SIZE = 42;

	private const string TURN_VISIBLE_DATE_PICKER = "TurnVisibleDatePicker";

	private const string TURN_VISIBLE_DATE_PICKER_SPINNER = "TurnVisibleDatePickerSpinner";

	private const string DATEPICKERCALENDAR_STYLE = "OneUIDatePickerCalendarSpinnerListStyle";

	private const string HEADER_DATE_BUTTON_ICON_CONTAINER = "IconContainer";

	private const string SUNDAY_COLOR = "OneUIDatePickerSundayTextColorNormal";

	private const string NORMAL_COLOR = "OneUIDatePickerWeekDayTextColorNormal";

	private DispatcherTimer _currentDayTimer;

	private DatePickerSpinnerList _datePickerSpinner;

	private Grid _datePickerPanel;

	private StackPanel _datePickerSpinnerPanel;

	private readonly UISettings _uiSettings = new UISettings();

	private Button _headerDateButton;

	private Grid _iconContainer;

	private IDatePickerAnimationService _animationService;

	private Button _previousMonthButton;

	private Button _nextMonthButton;

	private ThemeSettings _themeSettings;

	public static readonly DependencyProperty CurrentDayProperty = DependencyProperty.Register("CurrentDay", typeof(string), typeof(DatePicker), new PropertyMetadata(null));

	public static readonly DependencyProperty ActualMonthAndYearTimeScopeProperty = DependencyProperty.Register("ActualMonthAndYearTimeScope", typeof(string), typeof(DatePicker), new PropertyMetadata(null));

	public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(DatePicker), new PropertyMetadata(DateTime.Today, OnSelectedDatePropertyChanged));

	public static readonly DependencyProperty SundayIndicatorProperty = DependencyProperty.Register("SundayDayIndicator", typeof(int), typeof(DatePicker), new PropertyMetadata(1));

	public DateTime ActualDateTimeScope { get; set; }

	internal string CurrentDay
	{
		get
		{
			return (string)GetValue(CurrentDayProperty);
		}
		set
		{
			SetValue(CurrentDayProperty, value);
		}
	}

	internal string ActualMonthAndYearTimeScope
	{
		get
		{
			return (string)GetValue(ActualMonthAndYearTimeScopeProperty);
		}
		set
		{
			SetValue(ActualMonthAndYearTimeScopeProperty, value);
		}
	}

	public DateTime SelectedDate
	{
		get
		{
			return (DateTime)GetValue(SelectedDateProperty);
		}
		set
		{
			SetValue(SelectedDateProperty, value);
		}
	}

	public int SundayDayIndicator
	{
		get
		{
			return (int)GetValue(SundayIndicatorProperty);
		}
		set
		{
			SetValue(SundayIndicatorProperty, value);
		}
	}

	private static void OnSelectedDatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (!e.NewValue.Equals(e.OldValue))
		{
			((DatePicker)d).UpdateDatePickerWithNewSelectedDate();
		}
	}

	public DatePicker()
	{
		SetLimitDateRange();
		base.DayOfWeekFormat = "{dayofweek.abbreviated(3)}";
		CurrentDay = SelectedDate.Day.ToString();
		ActualDateTimeScope = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
		ActualMonthAndYearTimeScope = GetFormattedMonthAndYear(ActualDateTimeScope);
		base.Loaded += DatePicker_Loaded;
		base.Unloaded += DatePicker_Unloaded;
		base.CalendarViewDayItemChanging += Calendar_CalendarViewDayItemChanging;
		base.PointerWheelChanged += DatePicker_PointerSpinnerChanged;
		_currentDayTimer = new DispatcherTimer();
		_currentDayTimer.Tick += CurrentDayTimer_Tick;
		SetCurrentDayTimerTick();
		SetFirstDayOfWeek();
		base.DefaultStyleKey = typeof(DatePicker);
	}

	protected override void OnApplyTemplate()
	{
		_datePickerPanel = UIExtensionsInternal.FindChildByName<Grid>("DatePickerPanel", this);
		_datePickerSpinnerPanel = UIExtensionsInternal.FindChildByName<StackPanel>("DatePickerSpinnerPanel", this);
		_headerDateButton = UIExtensionsInternal.FindChildByName<Button>("HeaderDateButton", this);
		_previousMonthButton = UIExtensionsInternal.FindChildByName<Button>("PreviousMonthButton", this);
		_nextMonthButton = UIExtensionsInternal.FindChildByName<Button>("NextMonthButton", this);
		AddEvents();
		base.OnApplyTemplate();
		if (_headerDateButton != null)
		{
			UIExtensionsInternal.ExecuteWhenLoaded(_headerDateButton, delegate
			{
				_iconContainer = UIExtensionsInternal.FindChildByName<Grid>("IconContainer", _headerDateButton);
				if (SafetyNullCheckDatePickerPanels() && SafetyNullCheckNavigationMonthButtons())
				{
					InitializeAnimationService();
				}
			});
		}
		MonthNavigation(SelectedDate);
		UpdateWeekDayColors();
	}

	private bool SafetyNullCheckNavigationMonthButtons()
	{
		if (_previousMonthButton != null)
		{
			return _nextMonthButton != null;
		}
		return false;
	}

	private bool SafetyNullCheckDatePickerPanels()
	{
		if (_iconContainer != null && _datePickerPanel != null)
		{
			return _datePickerSpinnerPanel != null;
		}
		return false;
	}

	private void UpdateMonthSize()
	{
		if (_uiSettings.TextScaleFactor > 2.25)
		{
			base.DayOfWeekFormat = "{dayofweek.abbreviated(1)}";
		}
		else
		{
			base.DayOfWeekFormat = "{dayofweek.abbreviated(3)}";
		}
	}

	private void UpdateHeaderSize()
	{
		TextBlock textBlock = UIExtensionsInternal.FindChildByName<TextBlock>("HeaderDateTextBlock", this);
		if (!(textBlock == null))
		{
			textBlock.Margin = ((_uiSettings.TextScaleFactor > 2.25) ? default(Thickness) : new Thickness(8.0, 0.0, 0.0, 0.0));
		}
	}

	private void SetLimitDateRange()
	{
		DateTimeOffset minDate = new DateTimeOffset(DateTimeOffset.MinValue.Date, TimeSpan.Zero);
		DateTimeOffset maxDate = new DateTimeOffset(DateTimeOffset.MaxValue.Date, TimeSpan.Zero);
		if (TimeZoneInfo.Local.BaseUtcOffset < TimeSpan.Zero)
		{
			TimeSpan timeSpan = TimeSpan.Zero - TimeZoneInfo.Local.BaseUtcOffset;
			minDate = minDate.Add(timeSpan);
			maxDate = maxDate.Add(timeSpan);
		}
		base.MinDate = minDate;
		base.MaxDate = maxDate;
	}

	private void AddEvents()
	{
		if (_previousMonthButton != null)
		{
			_previousMonthButton.Click += CalendarPreviousButton_Click;
		}
		if (_nextMonthButton != null)
		{
			_nextMonthButton.Click += CalendarNextButton_Click;
		}
		if (_headerDateButton != null)
		{
			_headerDateButton.Click += HeaderDateButton_Click;
		}
	}

	private void MonthNavigation(DateTime newDateTime)
	{
		ActualDateTimeScope = new DateTime(newDateTime.Year, newDateTime.Month, 1);
		ActualMonthAndYearTimeScope = GetFormattedMonthAndYear(ActualDateTimeScope);
		SetDisplayDate(ActualDateTimeScope.ToUniversalTime());
		int rangeDays = 15;
		UpdateCalendar(rangeDays);
	}

	private void ChangeSelectedDate(DateTime newSelectedDate)
	{
		SelectedDate = newSelectedDate;
	}

	private void UpdateDatePickerWithNewSelectedDate()
	{
		if (!IsDatePickerSpinnerPanelInvisible())
		{
			return;
		}
		DateTime expectedScopeDate = new DateTime(SelectedDate.Date.Year, SelectedDate.Date.Month, 1);
		if (!expectedScopeDate.Equals(ActualDateTimeScope))
		{
			int monthsDifference = GetMonthsDifference(expectedScopeDate, ActualDateTimeScope);
			if (monthsDifference != 0)
			{
				MonthNavigation(ActualDateTimeScope.AddMonths(monthsDifference));
			}
		}
		else
		{
			UpdateCalendar();
		}
	}

	private int GetMonthsDifference(DateTime expectedScopeDate, DateTime actualDateTimeScope)
	{
		if (expectedScopeDate.Year == actualDateTimeScope.Year && expectedScopeDate.Month == actualDateTimeScope.Month)
		{
			return 0;
		}
		int signal = ((!(expectedScopeDate < actualDateTimeScope)) ? 1 : (-1));
		if (expectedScopeDate < actualDateTimeScope)
		{
			DateTime initialDate = new DateTime(expectedScopeDate.Year, expectedScopeDate.Month, 1);
			DateTime finalDate = new DateTime(actualDateTimeScope.Year, actualDateTimeScope.Month, 1);
			return GetMonthsDifference(signal, initialDate, ref finalDate);
		}
		DateTime initialDate2 = new DateTime(actualDateTimeScope.Year, actualDateTimeScope.Month, 1);
		DateTime finalDate2 = new DateTime(expectedScopeDate.Year, expectedScopeDate.Month, 1);
		return GetMonthsDifference(signal, initialDate2, ref finalDate2);
	}

	private int GetMonthsDifference(int signal, DateTime initialDate, ref DateTime finalDate)
	{
		int num = 0;
		while (initialDate < finalDate)
		{
			num++;
			finalDate = finalDate.AddDays(-1.0);
			finalDate = new DateTime(finalDate.Year, finalDate.Month, 1);
		}
		return num * signal;
	}

	private bool CanNavigateToPreviousMonthWithKeyboard(DateTime date)
	{
		if (date.Month == base.MinDate.Month && date.Year == base.MinDate.Year)
		{
			return base.MinDate.Date.Day == 1;
		}
		return true;
	}

	private bool CanNavigateToNextMonthWithKeyboard(DateTime date)
	{
		if (date.Month == base.MaxDate.Month && date.Year == base.MaxDate.Year)
		{
			return base.MaxDate.Date.Day == DateTime.DaysInMonth(base.MaxDate.Year, base.MaxDate.Month);
		}
		return true;
	}

	private void UpdateCalendar(int rangeDays = 1)
	{
		foreach (CalendarViewDayItem visibleDay in GetVisibleDays(ActualDateTimeScope, rangeDays))
		{
			DateTime dateTime = new DateTime(visibleDay.Date.Year, visibleDay.Date.Month, 1);
			bool isDayOutOfScopePrev = dateTime < ActualDateTimeScope;
			bool isDayOutOfScopeNext = dateTime > ActualDateTimeScope;
			SetDayItemScopeProperties(isDayOutOfScopePrev, isDayOutOfScopeNext, visibleDay);
		}
	}

	private void SetDayItemScopeProperties(bool isDayOutOfScopePrev, bool isDayOutOfScopeNext, CalendarViewDayItem item)
	{
		if (item.Date.Date == SelectedDate.Date)
		{
			VisualStateManager.GoToState(item, "TodayDate", useTransitions: true);
		}
		else if (item.Date.DayOfWeek == System.DayOfWeek.Sunday)
		{
			if (isDayOutOfScopePrev)
			{
				VisualStateManager.GoToState(item, "SundayOutOfScopeDatePrev", useTransitions: true);
			}
			else if (isDayOutOfScopeNext)
			{
				VisualStateManager.GoToState(item, "SundayOutOfScopeDateNext", useTransitions: true);
			}
			else
			{
				VisualStateManager.GoToState(item, "SundayScopedDate", useTransitions: true);
			}
		}
		else if (isDayOutOfScopePrev)
		{
			VisualStateManager.GoToState(item, "OutOfScopeDatePrev", useTransitions: true);
		}
		else if (isDayOutOfScopeNext)
		{
			VisualStateManager.GoToState(item, "OutOfScopeDateNext", useTransitions: true);
		}
		else
		{
			VisualStateManager.GoToState(item, "NormalScopedDate", useTransitions: true);
		}
		if (item.FocusState == FocusState.Unfocused)
		{
			VisualStateManager.GoToState(item, "UnfocusedInternal", useTransitions: true);
		}
	}

	private string GetFormattedMonthAndYear(DateTime date)
	{
		CultureInfo cultureInfo = new CultureInfo(GlobalizationPreferences.Languages[0]);
		return date.ToString("y", cultureInfo);
	}

	private IEnumerable GetVisibleDays(DateTime baseDate, int rangeDays = 1)
	{
		DateTime minVisibleDay = GetMinVisibleDay(baseDate);
		if (baseDate.Date.Year == DateTime.MaxValue.Date.Year && baseDate.Date.Month == DateTime.MaxValue.Date.Month)
		{
			return FindDescendants();
		}
		DateTime dt = minVisibleDay.SafeAddDays(42);
		DateTime safeStart = minVisibleDay.SafeAddDays(-rangeDays);
		DateTime safeEnd = dt.SafeAddDays(rangeDays);
		return from x in FindDescendants()
			where x.GetType() == typeof(CalendarViewDayItem) && x.Date.DateTime >= safeStart && x.Date.DateTime < safeEnd
			select x;
	}

	private IEnumerable<CalendarViewDayItem> FindDescendants()
	{
		List<CalendarViewDayItem> list = new List<CalendarViewDayItem>();
		UIExtensionsInternal.FindChildren(list, this);
		return list.OrderByDescending((CalendarViewDayItem it) => it.Date.DateTime);
	}

	private DateTime GetMinVisibleDay(DateTime minDate)
	{
		DateTime dateTime = new DateTime(minDate.Year, minDate.Month, 1, 0, 0, 0);
		if (dateTime.DayOfWeek == System.DayOfWeek.Sunday)
		{
			return dateTime;
		}
		DateTime dateTime2 = dateTime.SafeAddDays(-1);
		int days = (int)(dateTime.DayOfWeek - 1);
		return dateTime2.Subtract(new TimeSpan(days, 0, 0, 0));
	}

	private void CurrentDayTimer_Tick(object sender, object e)
	{
		CurrentDay = DateTime.Today.Day.ToString();
		SetCurrentDayTimerTick();
	}

	private void SetCurrentDayTimerTick()
	{
		if (_currentDayTimer.IsEnabled)
		{
			_currentDayTimer.Stop();
		}
		DateTime dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59, 999);
		_currentDayTimer.Interval = new TimeSpan(dateTime.Ticks - DateTime.Today.Ticks);
		_currentDayTimer.Start();
	}

	private void SetFirstDayOfWeek()
	{
		switch (Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
		{
		case System.DayOfWeek.Friday:
			base.FirstDayOfWeek = Windows.Globalization.DayOfWeek.Friday;
			SundayDayIndicator = 3;
			break;
		case System.DayOfWeek.Monday:
			base.FirstDayOfWeek = Windows.Globalization.DayOfWeek.Monday;
			SundayDayIndicator = 7;
			break;
		case System.DayOfWeek.Saturday:
			base.FirstDayOfWeek = Windows.Globalization.DayOfWeek.Saturday;
			SundayDayIndicator = 2;
			break;
		case System.DayOfWeek.Thursday:
			base.FirstDayOfWeek = Windows.Globalization.DayOfWeek.Thursday;
			SundayDayIndicator = 4;
			break;
		case System.DayOfWeek.Tuesday:
			base.FirstDayOfWeek = Windows.Globalization.DayOfWeek.Tuesday;
			SundayDayIndicator = 6;
			break;
		case System.DayOfWeek.Wednesday:
			base.FirstDayOfWeek = Windows.Globalization.DayOfWeek.Wednesday;
			SundayDayIndicator = 5;
			break;
		default:
			base.FirstDayOfWeek = Windows.Globalization.DayOfWeek.Sunday;
			break;
		}
	}

	private void InitializeAnimationService()
	{
		if (_animationService == null)
		{
			_animationService = new DatePickerAnimationService(_iconContainer, _previousMonthButton, _nextMonthButton, _datePickerPanel, _datePickerSpinnerPanel);
		}
	}

	private void UpdateWeekDayColors()
	{
		for (int i = 1; i <= 7; i++)
		{
			TextBlock textBlock = UIExtensionsInternal.FindChildByName<TextBlock>($"WeekDay{i}", this);
			if (textBlock != null)
			{
				textBlock.Foreground = (Brush)((i == SundayDayIndicator) ? "OneUIDatePickerSundayTextColorNormal" : "OneUIDatePickerWeekDayTextColorNormal").GetKey();
			}
		}
	}

	private void DatePicker_Loaded(object sender, RoutedEventArgs e)
	{
		_themeSettings = ThemeSettings.CreateForWindowId(base.XamlRoot.ContentIslandEnvironment.AppWindowId);
		_themeSettings.Changed += ThemeSettings_Changed;
	}

	private void DatePicker_Unloaded(object sender, RoutedEventArgs e)
	{
		_currentDayTimer.Tick -= CurrentDayTimer_Tick;
		_currentDayTimer = null;
		base.CalendarViewDayItemChanging -= Calendar_CalendarViewDayItemChanging;
		base.PointerWheelChanged -= DatePicker_PointerSpinnerChanged;
	}

	private void ThemeSettings_Changed(ThemeSettings sender, object args)
	{
		UpdateWeekDayColors();
	}

	private void DatePicker_PointerSpinnerChanged(object sender, PointerRoutedEventArgs e)
	{
		if (!(_datePickerPanel != null) || _datePickerPanel.Visibility != Visibility.Collapsed)
		{
			if (e.GetCurrentPoint((UIElement)sender).Properties.MouseWheelDelta > 0)
			{
				MonthNavigation(ActualDateTimeScope.AddMonths(-1));
			}
			else
			{
				MonthNavigation(ActualDateTimeScope.AddMonths(1));
			}
		}
	}

	private void DatePickerSpinner_DateChangedEvent(object sender, EventArgs e)
	{
		if (_datePickerSpinner != null)
		{
			ActualMonthAndYearTimeScope = _datePickerSpinner.Title;
			DateTime dateSelected = _datePickerSpinner.GetDateSelected();
			ChangeSelectedDate(dateSelected);
			if (_datePickerPanel.Visibility == Visibility.Collapsed)
			{
				MonthNavigation(dateSelected);
			}
		}
	}

	private void HeaderDateButton_Click(object sender, RoutedEventArgs e)
	{
		DateTime result;
		DatePickerSpinnerList datePickerSpinnerList = new DatePickerSpinnerList(DateTime.TryParse(SelectedDate.ToString(), out result) ? result : DateTime.Now);
		if (IsDatePickerSpinnerPanelInvisible())
		{
			datePickerSpinnerList.Style = "OneUIDatePickerCalendarSpinnerListStyle".GetStyle();
			if (_animationService != null)
			{
				_animationService.CreateTransitionAnimation(TransitionType.CalendarToSpinner);
			}
			ChangeVisibilityDatePickers("TurnVisibleDatePickerSpinner");
			_datePickerSpinner = datePickerSpinnerList;
			_datePickerSpinnerPanel?.Children.Clear();
			_datePickerSpinnerPanel?.Children.Insert(0, datePickerSpinnerList);
			return;
		}
		DateTime dateSelected = _datePickerSpinner.GetDateSelected();
		ChangeSelectedDate(dateSelected);
		_datePickerSpinnerPanel?.Children.Remove(datePickerSpinnerList);
		_datePickerSpinner = null;
		MonthNavigation(SelectedDate);
		if (_animationService != null)
		{
			_animationService.CreateTransitionAnimation(TransitionType.SpinnerToCalendar);
		}
		ChangeVisibilityDatePickers("TurnVisibleDatePicker");
	}

	public DateTime GetSelectedDateTime()
	{
		if (_datePickerSpinner != null)
		{
			return _datePickerSpinner.GetDateSelected();
		}
		return SelectedDate;
	}

	private bool IsDatePickerSpinnerPanelInvisible()
	{
		if (_datePickerSpinnerPanel != null)
		{
			return _datePickerSpinnerPanel.Visibility == Visibility.Collapsed;
		}
		return false;
	}

	private void ChangeVisibilityDatePickers(string visibility)
	{
		VisualStateManager.GoToState(this, visibility, useTransitions: true);
	}

	private void CalendarNextButton_Click(object sender, RoutedEventArgs e)
	{
		MonthNavigation(ActualDateTimeScope.AddMonths(1));
	}

	private void CalendarPreviousButton_Click(object sender, RoutedEventArgs e)
	{
		MonthNavigation(ActualDateTimeScope.AddMonths(-1));
	}

	private void Calendar_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
	{
		CalendarViewDayItem item = args.Item;
		item.GettingFocus += DayItem_GettingFocus;
		item.LosingFocus += DayItem_LosingFocus;
		item.PointerPressed += DayItem_PointerPressed;
		item.PointerReleased += DayItem_PointerReleased;
		item.PreviewKeyDown += DayItem_PreviewKeyDown;
		Grid grid = UIExtensionsInternal.FindFirstChildOfType<Grid>(item);
		TextBlock textBlock = UIExtensionsInternal.FindFirstChildOfType<TextBlock>(UIExtensionsInternal.FindFirstChildOfType<Grid>(grid));
		if (textBlock != null)
		{
			textBlock.Text = item.Date.Day.ToString();
			textBlock.SizeChanged -= DayNumberTextBlock_SizeChanged;
			textBlock.SizeChanged += DayNumberTextBlock_SizeChanged;
		}
		TextBlock textBlock2 = UIExtensionsInternal.FindChildByName<TextBlock>("DayNumberTextHighContrast", grid);
		if (textBlock2 != null)
		{
			textBlock2.Text = item.Date.Day.ToString();
			textBlock2.SizeChanged -= DayNumberTextBlock_SizeChanged;
			textBlock2.SizeChanged += DayNumberTextBlock_SizeChanged;
		}
		DateTime dateTime = new DateTime(item.Date.Year, item.Date.Month, 1);
		bool isDayOutOfScopePrev = dateTime < ActualDateTimeScope;
		bool isDayOutOfScopeNext = dateTime > ActualDateTimeScope;
		SetDayItemScopeProperties(isDayOutOfScopePrev, isDayOutOfScopeNext, item);
	}

	private void DayNumberTextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (sender is TextBlock { Parent: Grid parent })
		{
			if (TextScaleHelper.EqualOrGreaterThan(1.4))
			{
				parent.Height = 42.0;
				parent.Width = 42.0;
			}
			else
			{
				parent.Height = 28.0;
				parent.Width = 28.0;
			}
			UpdateHeaderSize();
			UpdateMonthSize();
		}
	}

	private void DayItem_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Enter || e.Key == VirtualKey.Space)
		{
			CalendarViewDayItem calendarViewDayItem = sender as CalendarViewDayItem;
			ChangeSelectedDate(calendarViewDayItem.Date.DateTime);
		}
	}

	private void DayItem_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		VisualStateManager.GoToState(sender as CalendarViewDayItem, "Pressed", useTransitions: true);
	}

	private void DayItem_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		CalendarViewDayItem calendarViewDayItem = sender as CalendarViewDayItem;
		ChangeSelectedDate(calendarViewDayItem.Date.DateTime);
	}

	private void DayItem_LosingFocus(UIElement sender, LosingFocusEventArgs args)
	{
		VisualStateManager.GoToState(sender as CalendarViewDayItem, "Unfocused", useTransitions: true);
	}

	private void DayItem_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		CalendarViewDayItem calendarViewDayItem = sender as CalendarViewDayItem;
		DateTime dateTime = new DateTime(calendarViewDayItem.Date.Year, calendarViewDayItem.Date.Month, 1);
		if (args.InputDevice == FocusInputDeviceKind.Keyboard)
		{
			VisualStateManager.GoToState(calendarViewDayItem, (calendarViewDayItem.Date.Date == SelectedDate.Date) ? "TodayDateFocused" : "Focused", useTransitions: true);
			if (dateTime < ActualDateTimeScope && CanNavigateToPreviousMonthWithKeyboard(dateTime))
			{
				MonthNavigation(ActualDateTimeScope.AddMonths(-1));
			}
			else if (dateTime > ActualDateTimeScope && CanNavigateToNextMonthWithKeyboard(dateTime))
			{
				MonthNavigation(ActualDateTimeScope.AddMonths(1));
			}
		}
	}
}
