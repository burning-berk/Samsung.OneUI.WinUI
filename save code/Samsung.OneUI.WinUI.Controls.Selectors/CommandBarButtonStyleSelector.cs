using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class CommandBarButtonStyleSelector : StyleSelector
{
	private const string DEFAULT_STYLE = "OneUICommandBarBackButtonStyle";

	private const string CLOSE_PANE_BUTTON_STYLE = "OneUICommandBarClosePaneButtonStyle";

	private const string OPEN_PANE_BUTTON_STYLE = "OneUICommandBarOpenPaneButtonStyle";

	private readonly CommandBarBackButtonType _commandBarType;

	private readonly bool _isPaneOpen;

	public CommandBarButtonStyleSelector(CommandBarBackButtonType commandBarType, bool isPaneOpen)
	{
		_commandBarType = commandBarType;
		_isPaneOpen = isPaneOpen;
	}

	public Style SelectStyle()
	{
		if (_commandBarType == CommandBarBackButtonType.CollapsibleSideMenu)
		{
			return _isPaneOpen ? "OneUICommandBarClosePaneButtonStyle".GetStyle() : "OneUICommandBarOpenPaneButtonStyle".GetStyle();
		}
		return "OneUICommandBarBackButtonStyle".GetStyle();
	}
}
