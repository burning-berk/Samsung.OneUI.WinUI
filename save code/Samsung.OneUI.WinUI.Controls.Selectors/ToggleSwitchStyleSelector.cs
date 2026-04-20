using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Controls.Inputs.ToggleSwitch;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class ToggleSwitchStyleSelector
{
	private const string DEFAULT_STYLE = "OneUIToggleSwitchStyle";

	private const string DEFAULT_RIGHT_STYLE = "OneUIToggleSwitchRightStyle";

	private const string MAIN_STYLE = "OneUIMainToggleSwitchStyle";

	private const string MAIN_RIGHT_STYLE = "OneUIMainToggleSwitchRightStyle";

	private readonly ToggleSwitchType _toggleSwitchType;

	public ToggleSwitchStyleSelector(ToggleSwitchType type)
	{
		_toggleSwitchType = type;
	}

	public Style SelectStyle()
	{
		return _toggleSwitchType switch
		{
			ToggleSwitchType.DefaultRight => "OneUIToggleSwitchRightStyle".GetStyle(), 
			ToggleSwitchType.Main => "OneUIMainToggleSwitchStyle".GetStyle(), 
			ToggleSwitchType.MainRight => "OneUIMainToggleSwitchRightStyle".GetStyle(), 
			_ => "OneUIToggleSwitchStyle".GetStyle(), 
		};
	}
}
