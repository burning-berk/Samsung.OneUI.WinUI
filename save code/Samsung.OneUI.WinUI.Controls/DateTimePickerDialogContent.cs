using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_TimePickerListDialogContentWinRTTypeDetails))]
public sealed class DateTimePickerDialogContent : Page, IComponentConnector
{
	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private DateTimePickerList DateTimePicker;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	public DateTime DateResult => DateTimePicker.GetDateSelected();

	public DateTimePickerDialogContent(DateTime date = default(DateTime), TimePeriod period = TimePeriod.PM, TimeType type = TimeType.MidDay, int rangeDays = -1, DateTime? startRangeDate = null)
	{
		InitializeComponent();
		DateTimePicker.Date = date;
		DateTimePicker.Type = type;
		DateTimePicker.Period = period;
		DateTimePicker.RangeDays = rangeDays;
		DateTimePicker.StartRangeDate = startRangeDate;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/DialogsAndFlyouts/DateTimePickerControl/DateTimePickerDialogContent.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		if (connectionId == 2)
		{
			DateTimePicker = target.As<DateTimePickerList>();
		}
		_contentLoaded = true;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public IComponentConnector GetBindingConnector(int connectionId, object target)
	{
		return null;
	}
}
