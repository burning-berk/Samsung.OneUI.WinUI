using System;
using System.Collections.Generic;

namespace Samsung.OneUI.WinUI.Controls;

internal interface IDatePickerDateUpdateService
{
	void UpdateDate(DateTime currentDateTime, ScrollList listYears, ScrollList listMonths, ScrollList listDays);

	IEnumerable<IDatePickerListItem> GetDays(DateTime currentDateTime);

	IEnumerable<IDatePickerListItem> GetYears(int currentYear);

	IEnumerable<IDatePickerListItem> GetMonths(DateTime currentDateTime);
}
