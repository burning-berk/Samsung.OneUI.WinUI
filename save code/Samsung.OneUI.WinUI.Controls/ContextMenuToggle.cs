using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class ContextMenuToggle : ListFlyoutToggle
{
	private const string CONTEXT_MENU_ITEM_TOGGLE_STYLE = "OneUIContextMenuToggle";

	public ContextMenuToggle()
	{
		base.Style = "OneUIContextMenuToggle".GetStyle();
	}
}
