using System;
using System.Text.RegularExpressions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class TimePickerKeyboard : Control
{
	private const string HOUR_TEXTBOX_NAME = "PART_HourTextBox";

	private const string MINUTE_TEXTBOX_NAME = "PART_MinuteTextBox";

	public const int MIN_FULLDAY_HOURS = 0;

	public const int MAX_FULLDAY_HOURS = 24;

	public const int MIN_MINUTES = 0;

	public const int MAX_MINUTES = 60;

	private const string ZERO_TIME = "00";

	private const string MINUTE_TEXT = "SS_MINUTE_ABB_TIMEPICKER/AutomationProperties/Name";

	private const string HOUR_TEXT = "SS_HOUR_TIMEPICKER/AutomationProperties/Name";

	public static readonly DependencyProperty TimeResultProperty = DependencyProperty.Register("TimeResult", typeof(TimeSpan), typeof(TimePickerKeyboard), new PropertyMetadata(default(TimeSpan)));

	public static readonly DependencyProperty HourProperty = DependencyProperty.Register("Hour", typeof(int), typeof(TimePickerKeyboard), new PropertyMetadata(0, OnHourPropertyChanged));

	public static readonly DependencyProperty MinuteProperty = DependencyProperty.Register("Minute", typeof(int), typeof(TimePickerKeyboard), new PropertyMetadata(0, OnMinutePropertyChanged));

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(TimeType), typeof(TimePickerList), new PropertyMetadata(TimeType.MidDay));

	private TextBox hourTextBox;

	private TextBox minuteTextBox;

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

	public int Hour
	{
		get
		{
			return (int)GetValue(HourProperty);
		}
		set
		{
			SetValue(HourProperty, value);
		}
	}

	public int Minute
	{
		get
		{
			return (int)GetValue(MinuteProperty);
		}
		set
		{
			SetValue(MinuteProperty, value);
		}
	}

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

	private static void OnHourPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		TimePickerKeyboard obj = (TimePickerKeyboard)d;
		obj.ValidateHour();
		obj.UpdateTimeResult();
	}

	private static void OnMinutePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		TimePickerKeyboard obj = (TimePickerKeyboard)d;
		obj.ValidateMinute();
		obj.UpdateTimeResult();
	}

	public TimePickerKeyboard()
	{
		base.DefaultStyleKey = typeof(TimePickerKeyboard);
		base.Unloaded += TimePickerKeyboard_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		DisposeTimePicker();
		AddEvents();
	}

	private void AddEvents()
	{
		hourTextBox = (TextBox)GetTemplateChild("PART_HourTextBox");
		if (hourTextBox != null)
		{
			hourTextBox.Text = Hour.ToString("00");
			hourTextBox.TextChanged += HourTextBox_TextChanged;
			hourTextBox.LosingFocus += HourTextBox_LosingFocus;
			hourTextBox.BeforeTextChanging += HourTextBox_BeforeTextChanging;
			string localized = "SS_HOUR_TIMEPICKER/AutomationProperties/Name".GetLocalized();
			if (!string.IsNullOrWhiteSpace(localized))
			{
				AutomationProperties.SetName(hourTextBox, localized);
			}
		}
		minuteTextBox = (TextBox)GetTemplateChild("PART_MinuteTextBox");
		if (minuteTextBox != null)
		{
			minuteTextBox.Text = Minute.ToString("00");
			minuteTextBox.TextChanged += MinuteTextBox_TextChanged;
			minuteTextBox.LosingFocus += MinuteTextBox_LosingFocus;
			minuteTextBox.BeforeTextChanging += MinuteTextBox_BeforeTextChanging;
			string localized2 = "SS_MINUTE_ABB_TIMEPICKER/AutomationProperties/Name".GetLocalized();
			if (!string.IsNullOrWhiteSpace(localized2))
			{
				AutomationProperties.SetName(minuteTextBox, localized2);
			}
		}
	}

	private void DisposeTimePicker()
	{
		if (hourTextBox != null)
		{
			hourTextBox.TextChanged -= HourTextBox_TextChanged;
			hourTextBox.LosingFocus -= HourTextBox_LosingFocus;
			hourTextBox.BeforeTextChanging -= HourTextBox_BeforeTextChanging;
		}
		if (minuteTextBox != null)
		{
			minuteTextBox.TextChanged -= MinuteTextBox_TextChanged;
			minuteTextBox.LosingFocus -= MinuteTextBox_LosingFocus;
			minuteTextBox.BeforeTextChanging -= MinuteTextBox_BeforeTextChanging;
		}
	}

	private void ValidateHour()
	{
		if (Hour < 0)
		{
			Hour = 0;
		}
		else if (Hour > 23)
		{
			Hour = 23;
		}
	}

	private void ValidateMinute()
	{
		if (Minute < 0)
		{
			Minute = 0;
		}
		else if (Minute > 59)
		{
			Minute = 59;
		}
	}

	private bool ValidateHourWithRegularExpressions(string hour)
	{
		if (!string.IsNullOrEmpty(hour))
		{
			return Regex.IsMatch(hour, "^(?:0?[0-9]|1[0-9]|2[0-3])$");
		}
		return false;
	}

	private bool ValidateMinuteWithRegularExpressions(string minute)
	{
		if (!string.IsNullOrEmpty(minute))
		{
			return Regex.IsMatch(minute, "^([0-5]?[0-9])$");
		}
		return false;
	}

	private void UpdateTimeResult()
	{
		TimeResult = new TimeSpan(Hour, Minute, 0);
	}

	private void TimePickerKeyboard_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeTimePicker();
	}

	private void HourTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateHourWithSender(sender);
	}

	private void HourTextBox_LosingFocus(object sender, RoutedEventArgs e)
	{
		UpdateHourWithSender(sender);
		hourTextBox.Text = Hour.ToString("00");
	}

	private void UpdateHourWithSender(object sender)
	{
		string text = ((TextBox)sender).Text;
		if (ValidateHourWithRegularExpressions(text))
		{
			Hour = int.Parse(text);
		}
		else
		{
			Hour = 0;
		}
	}

	private void HourTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
	{
		bool cancel = false;
		if (!string.IsNullOrEmpty(args.NewText))
		{
			cancel = !ValidateHourWithRegularExpressions(args.NewText);
		}
		args.Cancel = cancel;
	}

	private void MinuteTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateMinuteWithSender(sender);
	}

	private void MinuteTextBox_LosingFocus(object sender, RoutedEventArgs e)
	{
		UpdateMinuteWithSender(sender);
		minuteTextBox.Text = Minute.ToString("00");
	}

	private void UpdateMinuteWithSender(object sender)
	{
		string text = ((TextBox)sender).Text;
		if (ValidateMinuteWithRegularExpressions(text))
		{
			Minute = int.Parse(text);
		}
		else
		{
			Minute = 0;
		}
	}

	private void MinuteTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
	{
		bool cancel = false;
		if (!string.IsNullOrEmpty(args.NewText))
		{
			cancel = !ValidateMinuteWithRegularExpressions(args.NewText);
		}
		args.Cancel = cancel;
	}
}
