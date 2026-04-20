using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerCustomAutomationPeer;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_Inputs_ColorPicker_ColorPickerHistoryGridViewCustomWinRTTypeDetails))]
public sealed class ColorPickerHistoryGridViewCustom : GridView
{
	public ColorPickerHistoryGridViewCustom()
	{
		base.DefaultStyleKey = typeof(ColorPickerHistoryGridViewCustom);
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new ColorPickerHistoryGridViewCustomAutomationPeer(this);
	}
}
