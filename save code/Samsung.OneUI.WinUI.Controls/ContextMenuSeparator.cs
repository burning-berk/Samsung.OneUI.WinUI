using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class ContextMenuSeparator : ListFlyoutSeparator
{
	private const string CONTEXT_MENU_SEPARATOR_STYLE = "OneUIContextMenuSeparator";

	public ContextMenuSeparator()
	{
		base.Style = "OneUIContextMenuSeparator".GetStyle();
	}
}
