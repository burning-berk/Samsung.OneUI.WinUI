using System;
using System.Collections.Generic;
using System.Linq;
using Samsung.OneUI.WinUI.Utils.Enums;
using Samsung.OneUI.WinUI.Utils.Extensions;

namespace Samsung.OneUI.WinUI.Controls;

internal class DatePickerDateUpdateService : IDatePickerDateUpdateService
{
	public const int ITEMS_TO_VISIBLE = 13;

	private DateTime _currendDate;

	public void UpdateDate(DateTime currentDateTime, ScrollList listYears, ScrollList listMonths, ScrollList listDays)
	{
		if (!(listYears == null) && !(listMonths == null) && !(listDays == null))
		{
			if (_currendDate == DateTime.MinValue)
			{
				UpdateYear(currentDateTime, listDays, listYears);
				UpdateMonth(currentDateTime, listDays);
				UpdateDay(currentDateTime, listDays);
				_currendDate = currentDateTime;
			}
			else if (currentDateTime.Month != _currendDate.Month)
			{
				UpdateMonth(currentDateTime, listDays);
				_currendDate = currentDateTime;
			}
			else if (currentDateTime.Year != _currendDate.Year)
			{
				UpdateYear(currentDateTime, listDays, listYears);
				_currendDate = currentDateTime;
			}
			else if (currentDateTime.Day != _currendDate.Day)
			{
				UpdateDay(currentDateTime, listDays);
				_currendDate = currentDateTime;
			}
		}
	}

	public IEnumerable<IDatePickerListItem> GetDays(DateTime currentDateTime)
	{
		int num = 6;
		DatePickerSpinnerListItem[] array = new DatePickerSpinnerListItem[13];
		DateTime dateTime = currentDateTime;
		for (int num2 = num - 1; num2 >= 0; num2--)
		{
			DateTime previewDayInMonth = currentDateTime.GetPreviewDayInMonth();
			currentDateTime = previewDayInMonth;
			array[num2] = new DatePickerSpinnerListItem(previewDayInMonth.Day, TypeDate.Day);
		}
		currentDateTime = dateTime;
		array[num] = new DatePickerSpinnerListItem(currentDateTime.Day, TypeDate.Day);
		for (int i = num + 1; i < 13; i++)
		{
			DateTime nextDayInMonth = currentDateTime.GetNextDayInMonth();
			array[i] = new DatePickerSpinnerListItem(nextDayInMonth.Day, TypeDate.Day);
			currentDateTime = nextDayInMonth;
		}
		return array;
	}

	public IEnumerable<IDatePickerListItem> GetYears(int currentYear)
	{
		int num = currentYear - 6;
		int num2 = currentYear + 6;
		List<int> list = new List<int>();
		int year = DateTime.Now.Year;
		if (num < DateTime.MinValue.Year)
		{
			for (int num3 = DateTime.MinValue.Year - num; num3 > 0; num3--)
			{
				list.Add(year - num3 + 1);
			}
			int num4 = DateTime.MinValue.Year;
			while (list.Count < 13)
			{
				list.Add(num4);
				num4++;
			}
		}
		else if (num2 > DateTime.MaxValue.Year)
		{
			_ = DateTime.MaxValue.Year;
			for (int i = num; i <= DateTime.MaxValue.Year; i++)
			{
				list.Add(i);
			}
			int num5 = year;
			while (list.Count < 13)
			{
				list.Add(num5);
				num5++;
			}
		}
		else
		{
			list.AddRange(Enumerable.Range(num, 13));
		}
		return list.Select((int x) => new DatePickerSpinnerListItem(x, TypeDate.Year));
	}

	public IEnumerable<IDatePickerListItem> GetMonths(DateTime currentDateTime)
	{
		DatePickerSpinnerListItem[] array = new DatePickerSpinnerListItem[12];
		DateTime dateTime = currentDateTime;
		int num = 12 / 2;
		for (int num2 = num - 1; num2 >= 0; num2--)
		{
			if (currentDateTime.Date.Year <= DateTime.MinValue.Date.Year && currentDateTime.Date.Month <= DateTime.MinValue.Date.Month)
			{
				currentDateTime = new DateTime(1900, 1, 1);
			}
			DateTime dateTime2 = currentDateTime.AddMonths(-1);
			currentDateTime = dateTime2;
			array[num2] = new DatePickerSpinnerListItem(dateTime2.Month, TypeDate.Month);
		}
		currentDateTime = dateTime;
		array[num] = new DatePickerSpinnerListItem(currentDateTime.Month, TypeDate.Month);
		for (int i = num + 1; i < 12; i++)
		{
			if (currentDateTime.Date.Year >= DateTime.MaxValue.Date.Year && currentDateTime.Date.Month >= DateTime.MaxValue.Date.Month)
			{
				currentDateTime = new DateTime(1900, 12, 31);
			}
			DateTime dateTime3 = currentDateTime.AddMonths(1);
			array[i] = new DatePickerSpinnerListItem(dateTime3.Month, TypeDate.Month);
			currentDateTime = dateTime3;
		}
		return array;
	}

	private void UpdateMonth(DateTime currentDateTime, ScrollList listDays)
	{
		UpdateDay(currentDateTime, listDays);
	}

	private void UpdateDay(DateTime currentDateTime, ScrollList listDays)
	{
		int selectedIndex = listDays.GetSelectedIndex();
		DateTime dateTime = currentDateTime;
		for (int i = selectedIndex; i <= listDays.TimeItemsSource.Count - 1; i++)
		{
			if (listDays.TimeItemsSource.ElementAtOrDefault(i) is DatePickerSpinnerListItem datePickerSpinnerListItem)
			{
				datePickerSpinnerListItem.Value = currentDateTime.Day;
				currentDateTime = currentDateTime.GetNextDayInMonth();
			}
		}
		currentDateTime = dateTime;
		for (int num = selectedIndex - 1; num >= 0; num--)
		{
			if (listDays.TimeItemsSource.ElementAtOrDefault(num) is DatePickerSpinnerListItem datePickerSpinnerListItem2)
			{
				currentDateTime = currentDateTime.GetPreviewDayInMonth();
				datePickerSpinnerListItem2.Value = currentDateTime.Day;
			}
		}
	}

	private void UpdateYear(DateTime currentDateTime, ScrollList listDays, ScrollList listYears)
	{
		int selectedIndex = listYears.GetSelectedIndex();
		DateTime dateTime = currentDateTime;
		for (int i = selectedIndex; i <= listYears.TimeItemsSource.Count - 1; i++)
		{
			if (listYears.TimeItemsSource.ElementAtOrDefault(i) is DatePickerSpinnerListItem datePickerSpinnerListItem)
			{
				datePickerSpinnerListItem.Value = currentDateTime.Year;
				currentDateTime = ((currentDateTime.Year < DateTime.MaxValue.Year) ? currentDateTime.AddYears(1) : DateTime.Now);
			}
		}
		currentDateTime = dateTime;
		for (int num = selectedIndex - 1; num >= 0; num--)
		{
			if (listYears.TimeItemsSource.ElementAtOrDefault(num) is DatePickerSpinnerListItem datePickerSpinnerListItem2)
			{
				if (currentDateTime.Year <= DateTime.MinValue.Year)
				{
					currentDateTime = DateTime.Now.AddYears(1);
				}
				currentDateTime = currentDateTime.AddYears(-1);
				datePickerSpinnerListItem2.Value = currentDateTime.Year;
			}
		}
		UpdateMonth(dateTime, listDays);
	}
}
