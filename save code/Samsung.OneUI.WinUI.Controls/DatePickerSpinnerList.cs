using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Enums;
using Samsung.OneUI.WinUI.Utils.Extensions;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_DatePickerSpinnerListWinRTTypeDetails))]
public class DatePickerSpinnerList : Control, IDateTimePicker
{
	private const string LIST_DAY_NAME = "PART_ListDay";

	private const string LIST_YEAR_NAME = "PART_ListYear";

	private const string LIST_MONTH_NAME = "PART_ListMonth";

	public static readonly DependencyProperty DayProperty = DependencyProperty.Register("Day", typeof(DatePickerSpinnerListItem), typeof(DatePickerSpinnerList), new PropertyMetadata(new DatePickerSpinnerListItem(DateTime.Now.Day, TypeDate.Day), OnDayPropertyChanged));

	public static readonly DependencyProperty MonthProperty = DependencyProperty.Register("Month", typeof(DatePickerSpinnerListItem), typeof(DatePickerSpinnerList), new PropertyMetadata(new DatePickerSpinnerListItem(1, TypeDate.Month), OnMonthPropertyChanged));

	public static readonly DependencyProperty YearProperty = DependencyProperty.Register("Year", typeof(DatePickerSpinnerListItem), typeof(DatePickerSpinnerList), new PropertyMetadata(new DatePickerSpinnerListItem(DateTime.Today.Year, TypeDate.Year), OnYearPropertyChanged));

	public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DatePickerSpinnerList), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty EnabledEntranceAnimationProperty = DependencyProperty.Register("EnabledEntranceAnimation", typeof(bool), typeof(DatePickerSpinnerList), new PropertyMetadata(true));

	private DatePickerSpinnerListAnimationService _datePickerSpinnerListAnimationService;

	private readonly DatePickerDateUpdateService _datePickerSpinnerUpdateDateService;

	private ScrollList _listYears;

	private ScrollList _listMonths;

	private ScrollList _listDays;

	public DatePickerSpinnerListItem Day
	{
		get
		{
			return (DatePickerSpinnerListItem)GetValue(DayProperty);
		}
		set
		{
			SetValue(DayProperty, value);
		}
	}

	public DatePickerSpinnerListItem Month
	{
		get
		{
			return (DatePickerSpinnerListItem)GetValue(MonthProperty);
		}
		set
		{
			SetValue(MonthProperty, value);
		}
	}

	public DatePickerSpinnerListItem Year
	{
		get
		{
			return (DatePickerSpinnerListItem)GetValue(YearProperty);
		}
		set
		{
			SetValue(YearProperty, value);
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

	public bool EnabledEntranceAnimation
	{
		get
		{
			return (bool)GetValue(EnabledEntranceAnimationProperty);
		}
		set
		{
			SetValue(EnabledEntranceAnimationProperty, value);
		}
	}

	private static void OnDayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		_ = (DatePickerSpinnerList)d;
	}

	private static void OnMonthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		_ = (DatePickerSpinnerList)d;
	}

	private static void OnYearPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		_ = (DatePickerSpinnerList)d;
	}

	public DatePickerSpinnerList(DateTime initialDateTime)
	{
		base.DefaultStyleKey = typeof(DatePickerSpinnerList);
		_datePickerSpinnerUpdateDateService = new DatePickerDateUpdateService();
		base.Unloaded += DatePickerSpinnerListUnloaded;
		Year = new DatePickerSpinnerListItem(initialDateTime.Year, TypeDate.Year);
		Month = new DatePickerSpinnerListItem(initialDateTime.Month, TypeDate.Month);
		Day = new DatePickerSpinnerListItem(initialDateTime.Day, TypeDate.Day);
	}

	protected override void OnApplyTemplate()
	{
		DisposeDatePickerSpinnerList();
		_listYears = GetTemplateChild("PART_ListYear") as ScrollList;
		_listMonths = GetTemplateChild("PART_ListMonth") as ScrollList;
		_listDays = GetTemplateChild("PART_ListDay") as ScrollList;
		if (_listDays != null && _listMonths != null && _listYears != null)
		{
			_datePickerSpinnerListAnimationService = new DatePickerSpinnerListAnimationService(this);
			DateTime dateSelected = GetDateSelected();
			LoadDatePickerScrolls(dateSelected, _listYears, _listMonths, _listDays);
			SetDateFormatSpinnerUI(dateSelected, _listYears, _listMonths, _listDays);
		}
		base.OnApplyTemplate();
	}

	public DateTime GetDateSelected()
	{
		try
		{
			int value = Month.Value;
			int value2 = Day.Value;
			int value3 = Year.Value;
			int num = DateTime.DaysInMonth(value3, value);
			if (value2 > num)
			{
				return new DateTime(value3, value, num);
			}
			return new DateTime(value3, value, value2);
		}
		catch
		{
			return DateTime.Now;
		}
	}

	public async void ShowEntranceAnimation()
	{
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			_datePickerSpinnerListAnimationService?.StartEntranceAnimation();
		});
	}

	public async void PrepareEntranceAnimation()
	{
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			_datePickerSpinnerListAnimationService?.PrepareEntranceAnimation();
		});
	}

	private void UpdateTitle(DateTime dateTime)
	{
		Title = dateTime.ToString("y", CultureInfo.CurrentCulture);
	}

	private void DisposeDatePickerSpinnerList()
	{
		if (_listDays != null)
		{
			_listDays.Loaded -= ListDaysLoaded;
		}
		_listDays = null;
		_listMonths = null;
		_listYears = null;
	}

	private void LoadDatePickerScrolls(DateTime dateTime, ScrollList _listYears, ScrollList _listMonths, ScrollList _listDays)
	{
		LoadDefaultData(dateTime, _listYears, _listMonths, _listDays);
		_listDays.Loaded += ListDaysLoaded;
		_listMonths.ScrollChanged += ListScrollChanged;
		_listYears.ScrollChanged += ListScrollChanged;
		_listDays.ScrollChanged += ListScrollChanged;
	}

	private void LoadDefaultData(DateTime dateTime, ScrollList _listYears, ScrollList _listMonths, ScrollList _listDays)
	{
		IEnumerable<IDatePickerListItem> days = _datePickerSpinnerUpdateDateService.GetDays(dateTime);
		_listDays.TimeItemsSource = new ObservableCollection<object>(days);
		_listDays.SelectedTime = new DatePickerSpinnerListItem(dateTime.Day, TypeDate.Day);
		IEnumerable<IDatePickerListItem> years = _datePickerSpinnerUpdateDateService.GetYears(dateTime.Year);
		_listYears.TimeItemsSource = new ObservableCollection<object>(years);
		_listYears.SelectedTime = new DatePickerSpinnerListItem(dateTime.Year, TypeDate.Year);
		IEnumerable<IDatePickerListItem> months = _datePickerSpinnerUpdateDateService.GetMonths(dateTime);
		_listMonths.TimeItemsSource = new ObservableCollection<object>(months);
		_listMonths.SelectedTime = new DatePickerSpinnerListItem(dateTime.Month, TypeDate.Month);
	}

	private void SetDateFormatSpinnerUI(DateTime dateTime, ScrollList listYears, ScrollList listMonths, ScrollList listDays)
	{
		int num = 2;
		int positionDate = dateTime.GetPositionDate(TypeDate.Year);
		int positionDate2 = dateTime.GetPositionDate(TypeDate.Month);
		int num2 = (listDays.TabIndex = dateTime.GetPositionDate(TypeDate.Day));
		listMonths.TabIndex = positionDate2;
		listYears.TabIndex = positionDate;
		if (listDays.Parent is Grid grid)
		{
			grid.ColumnDefinitions[num2 * num].Width = new GridLength(listDays.Width, GridUnitType.Pixel);
			grid.ColumnDefinitions[positionDate2 * num].Width = new GridLength(listMonths.Width, GridUnitType.Pixel);
			grid.ColumnDefinitions[positionDate * num].Width = new GridLength(listYears.Width, GridUnitType.Pixel);
		}
		Grid.SetColumn(listDays, num2 * num);
		Grid.SetColumn(listMonths, positionDate2 * num);
		Grid.SetColumn(listYears, positionDate * num);
	}

	private void UpdateDate(DateTime dateTime)
	{
		_datePickerSpinnerUpdateDateService.UpdateDate(dateTime, _listYears, _listMonths, _listDays);
		UpdateTitle(dateTime);
	}

	private void ListScrollChanged(object sender, EventArgs e)
	{
		UpdateDate(GetDateSelected());
	}

	private void ListDaysLoaded(object sender, RoutedEventArgs e)
	{
		if (EnabledEntranceAnimation)
		{
			ShowEntranceAnimation();
		}
	}

	private void DatePickerSpinnerListUnloaded(object sender, RoutedEventArgs e)
	{
		DisposeDatePickerSpinnerList();
	}
}
