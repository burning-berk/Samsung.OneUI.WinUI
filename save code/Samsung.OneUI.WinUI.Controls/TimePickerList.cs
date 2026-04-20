using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class TimePickerList : Control
{
	private const string LIST_HOUR_NAME = "PART_ListHours";

	private const string LIST_MINUTE_NAME = "PART_ListMinutes";

	private const string LIST_PERIOD_NAME = "PART_ListPeriod";

	private const string COLON_NAME = "PART_Colon";

	private const string ROOT_STACK_PANEL = "ROOT_StackPanel";

	private const string DEFAULT_HOUR = "1";

	private const string DEFAULT_MINUTE = "00";

	private const string MINUTE_FORMAT = "00";

	private const int FULL_DAY_HOUR_WIDTH = 100;

	private const int FULL_DAY_MINUTE_WIDTH = 100;

	private const int FULL_DAY_COLON_WIDTH = 14;

	private const int FULL_DAY_ROOT_STACK_PANEL_WIDTH = 214;

	private const int MID_DAY_HOUR_WIDTH = 95;

	private const int MID_DAY_MINUTE_WIDTH = 95;

	private const int MID_DAY_COLON_WIDTH = 14;

	private const int MID_DAY_ROOT_STACK_PANEL_WIDTH = 300;

	public const int MIN_FULLDAY_HOURS = 0;

	public const int MAX_FULLDAY_HOURS = 24;

	public const int MIN_MIDDAY_HOURS = 1;

	public const int MAX_MIDDAY_HOURS = 12;

	public const int MIN_MINUTES = 0;

	public const int MAX_MINUTES = 60;

	private const string MINUTE_TEXT = "SS_MINUTE_ABB/AutomationProperties/LocalizedControlType";

	private const string HOUR_TEXT = "SS_HOUR/AutomationProperties/LocalizedControlType";

	private const string PERIOD_TEXT = "SS_PERIOD/AutomationProperties/LocalizedControlType";

	private const string STRING_TRANSLATION_AM = "DREAM_CLOCK_BODY_AM_ABB/Text";

	private const string STRING_TRANSLATION_PM = "DREAM_CLOCK_BODY_PM_ABB/Text";

	private const int AM_INDEX = 1;

	private const int PM_INDEX = 2;

	private const string DATETIME_FORMAT = "tt";

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(TimeType), typeof(TimePickerList), new PropertyMetadata(TimeType.FullDay, OnTypePropertyChanged));

	public static readonly DependencyProperty PeriodProperty = DependencyProperty.Register("Period", typeof(TimePeriod), typeof(TimePickerList), new PropertyMetadata(TimePeriod.PM, OnTimerPeriodPropertyChanged));

	public static readonly DependencyProperty SelectedPeriodProperty = DependencyProperty.Register("SelectedPeriod", typeof(string), typeof(TimePickerList), new PropertyMetadata(null, OnSelectedPeriodPropertyChanged));

	public static readonly DependencyProperty HourProperty = DependencyProperty.Register("Hour", typeof(string), typeof(TimePickerList), new PropertyMetadata("1", OnHourPropertyChanged));

	public static readonly DependencyProperty MinuteProperty = DependencyProperty.Register("Minute", typeof(string), typeof(TimePickerList), new PropertyMetadata("00", OnMinutePropertyChanged));

	public static readonly DependencyProperty TimeResultProperty = DependencyProperty.Register("TimeResult", typeof(TimeSpan), typeof(TimePickerList), new PropertyMetadata(default(TimeSpan)));

	internal ScrollList ListHours;

	internal ScrollList ListMinutes;

	private List<string> _itemsPeriod;

	internal PeriodScrollList ListPeriod;

	private readonly TimePickerListAnimationService _timePickerListAnimationService;

	public TimeType Type
	{
		get
		{
			return (TimeType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public TimePeriod Period
	{
		get
		{
			return (TimePeriod)GetValue(PeriodProperty);
		}
		set
		{
			SetValue(PeriodProperty, value);
		}
	}

	internal string SelectedPeriod
	{
		get
		{
			return (string)GetValue(SelectedPeriodProperty);
		}
		set
		{
			SetValue(SelectedPeriodProperty, value);
		}
	}

	public string Hour
	{
		get
		{
			return (string)GetValue(HourProperty);
		}
		set
		{
			SetValue(HourProperty, value);
		}
	}

	public string Minute
	{
		get
		{
			return (string)GetValue(MinuteProperty);
		}
		set
		{
			SetValue(MinuteProperty, value);
		}
	}

	public TimeSpan TimeResult
	{
		get
		{
			return (TimeSpan)GetValue(TimeResultProperty);
		}
		private set
		{
			SetValue(TimeResultProperty, value);
		}
	}

	internal int hourInt => int.Parse(Hour);

	internal int minuteInt => int.Parse(Minute);

	private static void OnTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((TimePickerList)d).UpdateTimeType();
	}

	private static void OnTimerPeriodPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		TimePickerList obj = (TimePickerList)d;
		obj.UpdateSelectedPeriod();
		obj.UpdateTimeResult();
	}

	private static void OnSelectedPeriodPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TimePickerList timePickerList)
		{
			timePickerList.UpdatePeriod();
		}
	}

	private static void OnHourPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((TimePickerList)d).UpdateHour();
	}

	private static void OnMinutePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((TimePickerList)d).UpdateMinute();
	}

	public TimePickerList()
	{
		base.DefaultStyleKey = typeof(TimePickerList);
		base.Unloaded += TimePickerList_Unloaded;
		_timePickerListAnimationService = new TimePickerListAnimationService(this);
	}

	protected override void OnApplyTemplate()
	{
		double periodVerticalOffSet = _timePickerListAnimationService.GetPeriodVerticalOffSet();
		TimerPickerBindTemplateChild(periodVerticalOffSet);
		EnableEntranceAnimation();
		base.OnApplyTemplate();
	}

	internal void TimerPickerBindTemplateChild(double periodAnimationVerticalOffSet)
	{
		DisposeTimePickerList();
		ListHours = GetTemplateChild("PART_ListHours") as ScrollList;
		if (ListHours != null)
		{
			UpdateItemsSourceHours();
			UpdateListHourSelectedTime();
			string localized = "SS_HOUR/AutomationProperties/LocalizedControlType".GetLocalized();
			if (!string.IsNullOrWhiteSpace(localized))
			{
				AutomationProperties.SetLocalizedControlType(ListHours, localized);
			}
		}
		ListMinutes = GetTemplateChild("PART_ListMinutes") as ScrollList;
		if (ListMinutes != null)
		{
			ListMinutes.TimeItemsSource = new ObservableCollection<object>(GetListMinutes());
			UpdateListMinuteSelectedTime();
			string localized2 = "SS_MINUTE_ABB/AutomationProperties/LocalizedControlType".GetLocalized();
			if (!string.IsNullOrWhiteSpace(localized2))
			{
				AutomationProperties.SetLocalizedControlType(ListMinutes, localized2);
			}
		}
		ListPeriod = GetTemplateChild("PART_ListPeriod") as PeriodScrollList;
		if (ListPeriod != null)
		{
			ListPeriod.VerticalOffSetAnimation = periodAnimationVerticalOffSet;
			string localized3 = "SS_PERIOD/AutomationProperties/LocalizedControlType".GetLocalized();
			if (!string.IsNullOrWhiteSpace(localized3))
			{
				AutomationProperties.SetLocalizedControlType(ListPeriod, localized3);
			}
			_itemsPeriod = GetListPeriod();
			ListPeriod.TimeItemsSource = new ObservableCollection<object>(_itemsPeriod);
			UpdateSelectedPeriod();
			UpdateTimeType();
		}
	}

	private void UpdateHour()
	{
		if (!IsValidHourByTimerType())
		{
			UpdateHourByTimerType();
		}
		UpdateTimeResult();
		UpdateListHourSelectedTime();
	}

	private void UpdateMinute()
	{
		if (!IsValidMinuteByTimerType())
		{
			UpdateMinuteByTimerType();
		}
		UpdateTimeResult();
		UpdateListMinuteSelectedTime();
	}

	private void UpdateTimeType()
	{
		if (ListPeriod != null)
		{
			ListPeriod.Visibility = ((!TimeType.MidDay.Equals(Type)) ? Visibility.Collapsed : Visibility.Visible);
			UpdateControlsWidth(ListPeriod.Visibility == Visibility.Visible);
			UpdateListPeriodSelectedTime();
			UpdateItemsSourceHours();
		}
		UpdateHour();
		UpdateMinute();
	}

	private void UpdateControlsWidth(bool isListPeriodVisible)
	{
		TextBlock textBlock = GetTemplateChild("PART_Colon") as TextBlock;
		StackPanel stackPanel = GetTemplateChild("ROOT_StackPanel") as StackPanel;
		if (textBlock != null && stackPanel != null)
		{
			textBlock.Width = (isListPeriodVisible ? 14 : 14);
			stackPanel.Width = (isListPeriodVisible ? 300 : 214);
		}
		if (ListHours != null && ListMinutes != null)
		{
			ListHours.Width = (isListPeriodVisible ? 95 : 100);
			ListMinutes.Width = (isListPeriodVisible ? 95 : 100);
		}
	}

	private void UpdateListHourSelectedTime()
	{
		if (ListHours != null)
		{
			ListHours.SelectedTime = Hour;
		}
	}

	private void UpdateListMinuteSelectedTime()
	{
		if (ListMinutes != null)
		{
			ListMinutes.SelectedTime = Minute;
		}
	}

	private void UpdateListPeriodSelectedTime()
	{
		if (ListPeriod != null)
		{
			ListPeriod.SelectedTime = SelectedPeriod;
		}
	}

	private void UpdateSelectedPeriod()
	{
		SelectedPeriod = GetSelectedPeriodByTimePeriod(Period);
	}

	private void UpdatePeriod()
	{
		if (SelectedPeriod != null)
		{
			Period = GetSelectedPeriodByText(SelectedPeriod);
		}
	}

	private void UpdateHourByTimerType()
	{
		if (TimeType.MidDay.Equals(Type))
		{
			SetMinMaxHour();
		}
		else if (hourInt < 0)
		{
			Hour = 0.ToString();
		}
		else if (hourInt > 23)
		{
			Hour = 23.ToString();
		}
	}

	private void SetMinMaxHour()
	{
		if (hourInt < 1)
		{
			Hour = 1.ToString();
		}
		else if (hourInt > 12)
		{
			if (hourInt > 23)
			{
				Hour = 12.ToString();
				return;
			}
			Hour = DateTime.ParseExact(Hour + ":00", "HH:mm", CultureInfo.CurrentCulture).ToString("hh");
			Hour = hourInt.ToString();
		}
	}

	private void UpdateMinuteByTimerType()
	{
		if (minuteInt < 0)
		{
			Minute = 0.ToString("00");
		}
		else if (minuteInt > 59)
		{
			Minute = 59.ToString("00");
		}
		else if (minuteInt >= 0 && minuteInt < 10)
		{
			Minute = minuteInt.ToString("00");
		}
	}

	private bool IsValidHourByTimerType()
	{
		bool result = true;
		if (TimeType.MidDay.Equals(Type))
		{
			if (hourInt < 1 || hourInt > 12)
			{
				result = false;
			}
		}
		else if (hourInt < 0 || hourInt > 23)
		{
			result = false;
		}
		return result;
	}

	private bool IsValidMinuteByTimerType()
	{
		bool result = true;
		if (minuteInt < 0)
		{
			result = false;
		}
		else if (minuteInt > 59)
		{
			result = false;
		}
		else if (minuteInt >= 0 && minuteInt < 10)
		{
			result = false;
		}
		return result;
	}

	private void UpdateTimeResult()
	{
		int timeHour = GetTimeHour(hourInt);
		TimeResult = new TimeSpan(timeHour, minuteInt, 0);
	}

	internal int GetTimeHour(int timeHour)
	{
		if (TimeType.MidDay.Equals(Type))
		{
			if (TimePeriod.AM.Equals(Period) && hourInt == 12)
			{
				timeHour = 0;
			}
			else if (TimePeriod.PM.Equals(Period) && hourInt != 12)
			{
				timeHour += 12;
			}
		}
		return timeHour;
	}

	private void UpdateItemsSourceHours()
	{
		if (ListHours != null)
		{
			ListHours.TimeItemsSource = new ObservableCollection<object>(GetListHours());
		}
	}

	protected List<string> GetListPeriodUsingCultureInfoApproach()
	{
		TimeSpan timespan = new TimeSpan(4, 1, 0);
		TimeSpan timespan2 = new TimeSpan(20, 1, 0);
		string item = ConvertTimeSpanToAMorPM(timespan);
		string item2 = ConvertTimeSpanToAMorPM(timespan2);
		return new List<string>
		{
			string.Empty,
			item,
			item2,
			string.Empty
		};
	}

	protected List<string> GetListPeriod(bool useTranslationStringsApproach = true)
	{
		if (useTranslationStringsApproach)
		{
			string localized = "DREAM_CLOCK_BODY_AM_ABB/Text".GetLocalized();
			string localized2 = "DREAM_CLOCK_BODY_PM_ABB/Text".GetLocalized();
			return new List<string>
			{
				string.Empty,
				localized,
				localized2,
				string.Empty
			};
		}
		return GetListPeriodUsingCultureInfoApproach();
	}

	protected List<string> GetListMinutes()
	{
		return new List<string>(from x in Enumerable.Range(0, 60)
			select x.ToString("00"));
	}

	protected List<string> GetListHours()
	{
		int start = 0;
		int count = 24;
		if (TimeType.MidDay.Equals(Type))
		{
			start = 1;
			count = 12;
		}
		return new List<string>(from s in Enumerable.Range(start, count)
			select s.ToString());
	}

	private string GetSelectedPeriodByTimePeriod(TimePeriod period)
	{
		string result = null;
		if (_itemsPeriod != null && 1 < _itemsPeriod.Count && 2 < _itemsPeriod.Count)
		{
			result = ((!TimePeriod.AM.Equals(period)) ? _itemsPeriod[2] : _itemsPeriod[1]);
		}
		return result;
	}

	private TimePeriod GetSelectedPeriodByText(string period)
	{
		TimePeriod result = TimePeriod.PM;
		if (_itemsPeriod != null && 1 < _itemsPeriod.Count && 2 < _itemsPeriod.Count && _itemsPeriod[1].Equals(period))
		{
			result = TimePeriod.AM;
		}
		return result;
	}

	private string ConvertTimeSpanToAMorPM(TimeSpan timespan)
	{
		return DateTime.Today.Add(timespan).ToString("tt", CultureInfo.CurrentCulture);
	}

	private void EnableEntranceAnimation()
	{
		if (ListHours != null && ListMinutes != null && ListPeriod != null)
		{
			_timePickerListAnimationService?.StartEntranceAnimation();
		}
	}

	private void DisposeTimePickerList()
	{
		ListHours = null;
		ListMinutes = null;
		ListPeriod = null;
	}

	private void TimePickerList_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeTimePickerList();
	}
}
