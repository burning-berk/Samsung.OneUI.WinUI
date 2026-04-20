using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class DateTimePickerList : TimePickerList
{
	private const string LIST_DAY_MONTH_NAME = "PART_ListDayAndMonth";

	private const string TITLE_NAME = "PART_Title";

	private const string DATE_FORMAT = "ddd, MMM d";

	private const string DAY_TEXT = "WS_DAY_ABB/AutomationProperties/LocalizedControlType";

	private const string MAIN_STACKPANEL = "MainStackPanel";

	private const int MAIN_STACKPANEL_WIDTH_MEDIUM = 392;

	private const int MID_DAY_HOUR_AND_MINUTE_WIDTH_MEDIUM = 60;

	private const int MID_PERIOD_WIDTH_MEDIUM = 60;

	private const int MID_DAY_AND_MONTH_WIDTH_MEDIUM = 204;

	private const int MAIN_STACKPANEL_WIDTH_SMALL = 312;

	private const int MID_DAY_HOUR_AND_MINUTE_WIDTH_SMALL = 48;

	private const int MID_PERIOD_WIDTH_SMALL = 48;

	private const int MID_DAY_AND_MONTH_WIDTH_SMALL = 160;

	public const int MIN_DAY = 1;

	public const int MAX_DAY = 31;

	public const int MIN_MONTH = 1;

	public const int MAX_MONTH = 12;

	private const int MIN_RANGE_DAYS = 7;

	private const int DEFAULT_RANGE_DAYS = -1;

	private const int PRE_LOAD_INTERVAL_DAYS = 10;

	private const double TEXT_SCALE_BREAKPOINT = 1.75;

	public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(DateTime), typeof(DateTimePickerList), new PropertyMetadata(DateTime.Today));

	public static readonly DependencyProperty StartRangeDateProperty = DependencyProperty.Register("StartRangeDate", typeof(DateTime?), typeof(DateTimePickerList), new PropertyMetadata(null));

	public static readonly DependencyProperty DayMonthYearProperty = DependencyProperty.Register("DayAndMonth", typeof(string), typeof(DateTimePickerList), new PropertyMetadata(DateTime.Today.ToString("ddd, MMM d"), OnDayAndMonthPropertyChanged));

	public static readonly DependencyProperty RangeDaysProperty = DependencyProperty.Register("RangeDays", typeof(int), typeof(DateTimePickerList), new PropertyMetadata(-1));

	public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DateTimePickerList), new PropertyMetadata(string.Empty));

	internal ScrollList ListDayMonthYear;

	private List<string> _listDates;

	private DateTime _startDate;

	private DateTime _endDate;

	private TextBlock _titleTextBlock;

	private readonly DateTimePickerListAnimationService _dateTimePickerListAnimationService;

	public DateTime Date
	{
		get
		{
			return (DateTime)GetValue(DateProperty);
		}
		set
		{
			SetValue(DateProperty, value);
		}
	}

	public DateTime? StartRangeDate
	{
		get
		{
			return (DateTime?)GetValue(StartRangeDateProperty);
		}
		set
		{
			SetValue(StartRangeDateProperty, value);
		}
	}

	internal string DayAndMonth
	{
		get
		{
			return (string)GetValue(DayMonthYearProperty);
		}
		set
		{
			SetValue(DayMonthYearProperty, value);
		}
	}

	public int RangeDays
	{
		get
		{
			return (int)GetValue(RangeDaysProperty);
		}
		set
		{
			SetValue(RangeDaysProperty, value);
		}
	}

	internal string Title
	{
		get
		{
			return (string)GetValue(TitleProperty);
		}
		set
		{
			SetValue(TitleProperty, value);
		}
	}

	private static void OnDayAndMonthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((DateTimePickerList)d).UpdateTitle();
	}

	public DateTimePickerList()
	{
		base.DefaultStyleKey = typeof(DateTimePickerList);
		base.Unloaded += DateTimePickerList_Unloaded;
		base.Loaded += DateTimePickerList_Loaded;
		_dateTimePickerListAnimationService = new DateTimePickerListAnimationService(this);
		_dateTimePickerListAnimationService.StoppedEntranceAnimationEvent += DateTimePickerListAnimationService_StoppedEntranceAnimationEvent;
	}

	protected override async void OnApplyTemplate()
	{
		DateTimePickerBindTemplateChild();
		await _dateTimePickerListAnimationService.StartEntranceAnimationAsync();
	}

	public DateTime GetDateSelected()
	{
		int year = Date.Year;
		if (IsRangeDaysEnabled() && !string.IsNullOrWhiteSpace(Title))
		{
			year = int.Parse(Title);
		}
		int timeHour = GetTimeHour(base.hourInt);
		DateTime result = DateTime.Now;
		if (DateTime.TryParseExact(DayAndMonth, "ddd, MMM d", CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
		{
			return new DateTime(year, result.Month, result.Day, timeHour, base.minuteInt, 0);
		}
		return Date;
	}

	private void DateTimePickerBindTemplateChild()
	{
		double periodVerticalOffSet = _dateTimePickerListAnimationService.GetPeriodVerticalOffSet();
		TimerPickerBindTemplateChild(periodVerticalOffSet);
		DisposeDateTimePickerList();
		ListDayMonthYear = GetTemplateChild("PART_ListDayAndMonth") as ScrollList;
		UpdateListDayMonthYear();
		_titleTextBlock = GetTemplateChild("PART_Title") as TextBlock;
		UpdateTitleTextBlock();
		base.Hour = Date.Hour.ToString();
		base.Minute = Date.Minute.ToString();
		StackPanel mainStackPanel = (StackPanel)GetTemplateChild("MainStackPanel");
		base.DispatcherQueue.TryEnqueue(delegate
		{
			if (ListDayMonthYear != null)
			{
				ListDayMonthYear.Width = (TextScaleHelper.EqualOrGreaterThan(1.75) ? 204 : 160);
			}
			if (ListPeriod != null)
			{
				ListPeriod.Width = (TextScaleHelper.EqualOrGreaterThan(1.75) ? 60 : 48);
				if (ListPeriod.Visibility == Visibility.Collapsed)
				{
					UpdateListHoursMinutesWidth(72, 90);
				}
				else
				{
					UpdateListHoursMinutesWidth(48, 60);
				}
			}
			if (mainStackPanel != null)
			{
				mainStackPanel.Width = (TextScaleHelper.EqualOrGreaterThan(1.75) ? 392 : 312);
			}
		});
	}

	private void UpdateListHoursMinutesWidth(int smallWidth, int mediumWidth)
	{
		if (ListHours != null)
		{
			ListHours.Width = (TextScaleHelper.EqualOrGreaterThan(1.75) ? mediumWidth : smallWidth);
		}
		if (ListMinutes != null)
		{
			ListMinutes.Width = (TextScaleHelper.EqualOrGreaterThan(1.75) ? mediumWidth : smallWidth);
		}
	}

	private void LoadAllElementsListDayMonthYearAfterPreLoad()
	{
		if (ListDayMonthYear == null)
		{
			return;
		}
		_listDates = GetListDayMonthYear();
		int num = _listDates.IndexOf(Date.ToString("ddd, MMM d"));
		int num2 = ListDayMonthYear.TimeItemsSource.IndexOf(Date.ToString("ddd, MMM d"));
		int num3 = num2;
		for (int i = num; i < _listDates.Count; i++)
		{
			if (num3 < ListDayMonthYear.TimeItemsSource.Count && ListDayMonthYear.TimeItemsSource[num3].ToString() != _listDates[i])
			{
				ListDayMonthYear.TimeItemsSource[num3] = _listDates[i];
			}
			else if (!ListDayMonthYear.TimeItemsSource.Contains(_listDates[i]))
			{
				ListDayMonthYear.TimeItemsSource.Add(_listDates[i]);
			}
			num3++;
		}
		int num4 = _listDates.Count - num;
		int num5 = num2 + num4;
		while (ListDayMonthYear.TimeItemsSource.Count > num5)
		{
			ListDayMonthYear.TimeItemsSource.RemoveAt(ListDayMonthYear.TimeItemsSource.Count - 1);
		}
		for (int j = 0; j < num; j++)
		{
			if (!ListDayMonthYear.TimeItemsSource.Contains(_listDates[j]))
			{
				ListDayMonthYear.TimeItemsSource.Add(_listDates[j]);
			}
		}
	}

	private void UpdateTitleTextBlock()
	{
		if (_titleTextBlock != null)
		{
			_titleTextBlock.Visibility = ((!IsRangeDaysEnabled()) ? Visibility.Collapsed : Visibility.Visible);
			UpdateTitle();
		}
	}

	private void UpdateListDayMonthYear()
	{
		if (!(ListDayMonthYear == null))
		{
			UpdateItemsSourceDayMonthYear();
			if (IsRangeDaysEnabled())
			{
				Date = GetValidDate(Date);
			}
			ListDayMonthYear.SelectedTime = Date.ToString("ddd, MMM d");
			string localized = "WS_DAY_ABB/AutomationProperties/LocalizedControlType".GetLocalized();
			if (!string.IsNullOrWhiteSpace(localized))
			{
				AutomationProperties.SetLocalizedControlType(ListDayMonthYear, localized);
			}
		}
	}

	private static string GetShortMonthName(int month)
	{
		return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
	}

	private DateTime GetValidDate(DateTime selectedDateDay)
	{
		if (selectedDateDay < _startDate)
		{
			return new DateTime(_startDate.Year, _startDate.Month, _startDate.Day, Date.Hour, Date.Minute, Date.Second, DateTimeKind.Local);
		}
		if (selectedDateDay > _endDate)
		{
			return new DateTime(_endDate.Year, _endDate.Month, _endDate.Day, Date.Hour, Date.Minute, Date.Second, DateTimeKind.Local);
		}
		return Date;
	}

	private bool IsRangeDaysEnabled()
	{
		return RangeDays > 0;
	}

	private void UpdateItemsSourceDayMonthYear()
	{
		if (!(ListDayMonthYear == null))
		{
			_listDates = GetListDayMonthYear(10);
			ListDayMonthYear.TimeItemsSource = new ObservableCollection<object>(_listDates);
		}
	}

	private void UpdateTitle()
	{
		if (IsRangeDaysEnabled() && _listDates != null)
		{
			int num = _listDates.IndexOf(DayAndMonth);
			Title = _startDate.AddDays(num).Year.ToString();
		}
	}

	private List<string> GetListDayMonthYear(int interval = 0)
	{
		if (IsRangeDaysEnabled())
		{
			return GetRangeDaysList();
		}
		return GenerateListDayMonthYear(interval);
	}

	private List<string> GetRangeDaysList()
	{
		RangeDays = GetValidRangeDays();
		_startDate = GetValidStartRangeDate();
		_endDate = _startDate.AddDays(RangeDays);
		List<string> list = new List<string>();
		DateTime dateTime = _startDate;
		while (dateTime <= _endDate)
		{
			list.Add(dateTime.ToString("ddd, MMM d"));
			dateTime = dateTime.AddDays(1.0);
		}
		return list;
	}

	private int GetValidRangeDays()
	{
		if (RangeDays < 7)
		{
			return 7;
		}
		return RangeDays;
	}

	private List<string> GenerateListDayMonthYear(int interval)
	{
		int year = Date.Year;
		_startDate = new DateTime(year, 1, 1);
		_endDate = new DateTime(year, 12, 31);
		if (interval != 0)
		{
			return PreLoadListDayMonthYearIntervalElements(interval);
		}
		int num = (int)(_endDate - _startDate).TotalDays + 1;
		List<string> list = new List<string>(num);
		for (int i = 0; i < num; i++)
		{
			list.Add(_startDate.AddDays(i).ToString("ddd, MMM d"));
		}
		return list;
	}

	private List<string> PreLoadListDayMonthYearIntervalElements(int interval)
	{
		List<string> list = new List<string>();
		int year = Date.Year;
		_startDate = Date.AddDays(-interval);
		_endDate = Date.AddDays(interval);
		if (_startDate.Year != year)
		{
			_startDate = new DateTime(year, _startDate.Month, _startDate.Day);
		}
		if (_endDate.Year != year)
		{
			_endDate = new DateTime(year, _endDate.Month, _endDate.Day);
		}
		list.Add(Date.ToString("ddd, MMM d"));
		for (int num = interval - 1; num >= 0; num--)
		{
			list.Add(_endDate.AddDays(-num).ToString("ddd, MMM d"));
		}
		for (int i = 1; i < interval; i++)
		{
			list.Add(_startDate.AddDays(i).ToString("ddd, MMM d"));
		}
		return list;
	}

	private DateTime GetValidStartRangeDate()
	{
		DateTime dateTime = new DateTime(Date.Year, Date.Month, Date.Day);
		if (!StartRangeDate.HasValue)
		{
			return dateTime;
		}
		DateTime value = StartRangeDate.Value.AddDays(RangeDays);
		DateTime dateTime2 = dateTime;
		DateTime? startRangeDate = StartRangeDate;
		if (startRangeDate.HasValue && dateTime2 >= startRangeDate.GetValueOrDefault() && StartRangeDate <= value)
		{
			return StartRangeDate.Value;
		}
		return dateTime;
	}

	private void DateTimePickerListAnimationService_StoppedEntranceAnimationEvent(object sender, EventArgs e)
	{
		LoadAllElementsListDayMonthYearAfterPreLoad();
	}

	private void DisposeDateTimePickerList()
	{
		ListDayMonthYear = null;
		_listDates = null;
	}

	private async void DateTimePickerList_Loaded(object sender, RoutedEventArgs e)
	{
		await (ListDayMonthYear?.CenterListViewItem());
		await (ListHours?.CenterListViewItem());
		await (ListMinutes?.CenterListViewItem());
		DpiChangedTo125StateTrigger.XamlRoot = base.XamlRoot;
		DpiChangedTo175StateTrigger.XamlRoot = base.XamlRoot;
	}

	private void DateTimePickerList_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeDateTimePickerList();
	}
}
