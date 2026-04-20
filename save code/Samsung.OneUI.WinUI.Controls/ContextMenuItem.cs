using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class ContextMenuItem : ListFlyoutItem
{
	private const string CONTEXT_MENU_ITEM_STYLE = "OneUIContextMenuItem";

	public ContextMenuItem()
	{
		base.Style = "OneUIContextMenuItem".GetStyle();
	}
}
