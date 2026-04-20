using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerCustomAutomationPeer;

internal class ColorPickerSwatchedGridViewCustomAutomationPeer : GridViewAutomationPeer
{
	public ColorPickerSwatchedGridViewCustomAutomationPeer(GridView owner)
		: base(owner)
	{
	}

	protected override ItemAutomationPeer OnCreateItemAutomationPeer(object item)
	{
		return new ColorPickerSwatchedGridViewItemCustomAutomationPeer(item, this);
	}
}
