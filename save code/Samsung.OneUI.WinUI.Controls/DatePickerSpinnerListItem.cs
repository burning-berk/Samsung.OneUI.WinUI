using System;
using System.Globalization;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Enums;
using Windows.System.UserProfile;

namespace Samsung.OneUI.WinUI.Controls;

public class DatePickerSpinnerListItem : ListViewItem, ICloneable, IDatePickerListItem
{
	private const string VIETNAMESE_LANGUAGE = "vi";

	public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(DatePickerSpinnerListItem), new PropertyMetadata(0, OnValuePropertyChanged));

	public static readonly DependencyProperty FormattedValueProperty = DependencyProperty.Register("FormattedValue", typeof(string), typeof(DatePickerSpinnerListItem), new PropertyMetadata(""));

	public TypeDate TypeDate { get; private set; }

	public int Value
	{
		get
		{
			return (int)GetValue(ValueProperty);
		}
		set
		{
			SetValue(ValueProperty, value);
		}
	}

	public string FormattedValue
	{
		get
		{
			return (string)GetValue(FormattedValueProperty);
		}
		set
		{
			SetValue(FormattedValueProperty, value);
		}
	}

	private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DatePickerSpinnerListItem datePickerSpinnerListItem)
		{
			switch (datePickerSpinnerListItem.TypeDate)
			{
			case TypeDate.Day:
				datePickerSpinnerListItem.FormattedValue = datePickerSpinnerListItem.Value.ToString("00");
				break;
			case TypeDate.Month:
				datePickerSpinnerListItem.FormattedValue = GetShortMonthName(datePickerSpinnerListItem.Value);
				break;
			case TypeDate.Year:
				datePickerSpinnerListItem.FormattedValue = datePickerSpinnerListItem.Value.ToString();
				break;
			default:
				datePickerSpinnerListItem.FormattedValue = datePickerSpinnerListItem.Value.ToString();
				break;
			}
		}
	}

	private static string GetShortMonthName(int month)
	{
		CultureInfo cultureInfo = new CultureInfo(GlobalizationPreferences.Languages[0]);
		string text = cultureInfo.DateTimeFormat.AbbreviatedMonthNames[month - 1].ToUpper();
		if (cultureInfo.Name == "vi")
		{
			return $"T{month:00}";
		}
		if (text.Length <= 3)
		{
			return text;
		}
		return text.Substring(0, 3);
	}

	public DatePickerSpinnerListItem(int value, TypeDate typeDate)
	{
		base.DefaultStyleKey = typeof(DatePickerSpinnerListItem);
		TypeDate = typeDate;
		Value = value;
	}

	public object Clone()
	{
		return new DatePickerSpinnerListItem(Value, TypeDate);
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as DatePickerSpinnerListItem);
	}

	public bool Equals(DatePickerSpinnerListItem other)
	{
		if (other != null)
		{
			return Value == other.Value;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return new { Value }.GetHashCode();
	}
}
