using Microsoft.UI.Xaml.Automation.Peers;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerCustomAutomationPeer;

internal class ColorPickerSwatchedGridViewItemCustomAutomationPeer : GridViewItemDataAutomationPeer
{
	private const string NO_COLOR_STRING_ID = "DREAM_NO_COLOR_SET_TBOPT";

	private const string BUTTON_STRING_ID = "SS_BUTTON_TTS/Text";

	public ColorPickerSwatchedGridViewItemCustomAutomationPeer(object item, GridViewAutomationPeer parent)
		: base(item, parent)
	{
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.Text;
	}

	protected override object GetPatternCore(PatternInterface patternInterface)
	{
		if (patternInterface == PatternInterface.SelectionItem)
		{
			return null;
		}
		return base.GetPatternCore(patternInterface);
	}

	protected override string GetNameCore()
	{
		if (!(base.Item is ColorInfo colorInfo))
		{
			return "DREAM_NO_COLOR_SET_TBOPT".GetLocalized();
		}
		return string.Format(colorInfo.Name) + ", " + "SS_BUTTON_TTS/Text".GetLocalized();
	}

	protected override int GetPositionInSetCore()
	{
		return 0;
	}

	protected override int GetSizeOfSetCore()
	{
		return 0;
	}
}
