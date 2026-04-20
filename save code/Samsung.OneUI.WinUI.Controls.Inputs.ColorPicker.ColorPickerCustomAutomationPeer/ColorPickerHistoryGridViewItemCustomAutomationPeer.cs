using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerCustomAutomationPeer;

internal class ColorPickerHistoryGridViewItemCustomAutomationPeer : GridViewItemDataAutomationPeer
{
	private const string RECENT_COLOR_STRING_ID = "DREAM_RECENT_COLOR_PD_TBOPT";

	private const string NO_COLOR_STRING_ID = "DREAM_NO_COLOR_SET_TBOPT";

	private const string BUTTON_STRING_ID = "SS_BUTTON_TTS/Text";

	private readonly SolidColorBrush TRANSPARENT_COLOR = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

	private readonly ColorPickerHistoryGridViewCustom _colorPickerGridViewCustom;

	public ColorPickerHistoryGridViewItemCustomAutomationPeer(object item, GridViewAutomationPeer parent)
		: base(item, parent)
	{
		_colorPickerGridViewCustom = (ColorPickerHistoryGridViewCustom)((GridViewAutomationPeer)base.ItemsControlAutomationPeer).Owner;
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.Text;
	}

	protected override string GetNameCore()
	{
		ColorInfo colorInfo = base.Item as ColorInfo;
		int indexOfRecentColor = GetIndexOfRecentColor(colorInfo);
		if (colorInfo == null || ColorsHelpers.AreColorsEqual(colorInfo.ColorBrush, TRANSPARENT_COLOR) || indexOfRecentColor == 0)
		{
			return "DREAM_NO_COLOR_SET_TBOPT".GetLocalized();
		}
		return string.Format("DREAM_RECENT_COLOR_PD_TBOPT".GetLocalized(), indexOfRecentColor) + ", " + "SS_BUTTON_TTS/Text".GetLocalized();
	}

	protected override int GetPositionInSetCore()
	{
		return 0;
	}

	protected override int GetSizeOfSetCore()
	{
		return 0;
	}

	private int GetIndexOfRecentColor(ColorInfo colorInfo)
	{
		for (int i = 0; i < _colorPickerGridViewCustom.Items.Count; i++)
		{
			if (_colorPickerGridViewCustom.Items[i] is ColorInfo colorInfo2 && ColorsHelpers.AreColorsEqual(colorInfo2.ColorBrush, colorInfo.ColorBrush))
			{
				return i + 1;
			}
		}
		return 0;
	}
}
