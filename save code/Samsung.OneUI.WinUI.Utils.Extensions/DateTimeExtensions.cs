using System;
using System.Globalization;
using Samsung.OneUI.WinUI.Utils.Enums;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

internal static class DateTimeExtensions
{
	private const int MAX_YEAR_VALUE_CALENDAR_VIEW = 100;

	public static string GetFormattedMonthDay(this DateTime dateTime)
	{
		string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(dateTime.Month);
		string text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(monthName.ToLower());
		string shortDatePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
		if (shortDatePattern.IndexOf("M") < shortDatePattern.IndexOf("y"))
		{
			return text + " " + dateTime.Year;
		}
		return dateTime.Year + " " + text;
	}

	public static DateTime SafeAddDays(this DateTime dt, int days)
	{
		if (days == 0)
		{
			return dt;
		}
		if (days > 0)
		{
			double totalDays = (DateTime.MaxValue - dt).TotalDays;
			if (!((double)days <= totalDays))
			{
				return DateTime.MaxValue;
			}
			return dt.AddDays(days);
		}
		double totalDays2 = (dt - DateTime.MinValue).TotalDays;
		if (!((double)(-days) <= totalDays2))
		{
			return DateTime.MinValue;
		}
		return dt.AddDays(days);
	}

	public static DateTime GetNextDayInMonth(this DateTime currentDateTime)
	{
		if (currentDateTime.Date >= DateTime.MaxValue.Date)
		{
			currentDateTime = new DateTime(1900, 12, 31);
		}
		currentDateTime = ((currentDateTime.AddDays(1.0).Month == currentDateTime.Month) ? currentDateTime.AddDays(1.0) : new DateTime(currentDateTime.Year, currentDateTime.Month, 1));
		return currentDateTime;
	}

	public static DateTime GetPreviewDayInMonth(this DateTime currentDateTime)
	{
		if (currentDateTime <= DateTime.MinValue)
		{
			currentDateTime = new DateTime(1900, 1, 1);
		}
		if (currentDateTime.AddDays(-1.0).Month != currentDateTime.Month)
		{
			int day = DateTime.DaysInMonth(currentDateTime.Year, currentDateTime.Month);
			currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, day);
		}
		else
		{
			currentDateTime = currentDateTime.AddDays(-1.0);
		}
		return currentDateTime;
	}

	public static int GetPositionDate(this DateTime date, TypeDate typeDate)
	{
		string stringType = GetStringType(typeDate);
		string[] array = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.Split('/', '-', '.', ' ');
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Contains(stringType))
			{
				return i;
			}
		}
		return 0;
	}

	private static string GetStringType(TypeDate typeDate)
	{
		return typeDate switch
		{
			TypeDate.Day => "d", 
			TypeDate.Month => "M", 
			TypeDate.Year => "y", 
			_ => "", 
		};
	}
}
