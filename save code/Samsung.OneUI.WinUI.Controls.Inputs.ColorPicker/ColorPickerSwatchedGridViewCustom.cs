using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerCustomAutomationPeer;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

internal class ColorPickerSwatchedGridViewCustom : GridView
{
	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new ColorPickerSwatchedGridViewCustomAutomationPeer(this);
	}
}
