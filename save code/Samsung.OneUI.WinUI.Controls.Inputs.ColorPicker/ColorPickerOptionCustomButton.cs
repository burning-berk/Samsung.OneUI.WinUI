using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls.Primitives;
using Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerCustomAutomationPeer;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_Inputs_ColorPicker_ColorPickerOptionCustomButtonWinRTTypeDetails))]
public sealed class ColorPickerOptionCustomButton : ToggleButton
{
	public ColorPickerOptionCustomButton()
	{
		base.DefaultStyleKey = typeof(ColorPickerOptionCustomButton);
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new ColorPickerOptionCustomButtonAutomationPeer(this);
	}
}
