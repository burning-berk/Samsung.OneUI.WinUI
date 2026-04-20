using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerCustomAutomationPeer;

internal class ColorPickerOptionCustomButtonAutomationPeer : ToggleButtonAutomationPeer
{
	private const string SELECTED_STRING_ID = "SS_SELECTED";

	private readonly ColorPickerOptionCustomButton _colorPickerOptionCustomButton;

	public ColorPickerOptionCustomButtonAutomationPeer(ColorPickerOptionCustomButton owner)
		: base(owner)
	{
		if (base.Owner is ColorPickerOptionCustomButton colorPickerOptionCustomButton)
		{
			_colorPickerOptionCustomButton = colorPickerOptionCustomButton;
			_colorPickerOptionCustomButton.Click += ColorPickerOptionCustomButton_Click;
		}
	}

	private void ColorPickerOptionCustomButton_Click(object sender, RoutedEventArgs e)
	{
		_colorPickerOptionCustomButton.IsChecked = true;
		RaiseNotificationEvent(AutomationNotificationKind.ActionCompleted, AutomationNotificationProcessing.MostRecent, $"{_colorPickerOptionCustomButton.Content}, Tab, {GetSelectedText()}", Guid.NewGuid().ToString());
	}

	protected override string GetLocalizedControlTypeCore()
	{
		return ", Tab, " + GetSelectedText();
	}

	protected override object GetPatternCore(PatternInterface patternInterface)
	{
		if (patternInterface == PatternInterface.Toggle)
		{
			return null;
		}
		return base.GetPatternCore(patternInterface);
	}

	private string GetTranslation(string resourceKey)
	{
		return resourceKey.GetLocalized();
	}

	private string GetSelectedText()
	{
		if (_colorPickerOptionCustomButton == null)
		{
			return string.Empty;
		}
		if (!_colorPickerOptionCustomButton.IsChecked.HasValue)
		{
			return string.Empty;
		}
		if (!_colorPickerOptionCustomButton.IsChecked.Value)
		{
			return string.Empty;
		}
		return GetTranslation("SS_SELECTED");
	}
}
