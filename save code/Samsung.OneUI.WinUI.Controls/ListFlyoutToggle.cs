using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class ListFlyoutToggle : ToggleMenuFlyoutItem
{
	private const string PART_STYLE_TOGGLE = "OneUIListFlyoutToggle";

	private const string CONTENT_TOOLTIP_NAME = "ContentTooltip";

	public ListFlyoutToggle()
	{
		base.Style = "OneUIListFlyoutToggle".GetStyle();
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		ConfigureItemTooltip();
	}

	private void ConfigureItemTooltip()
	{
		if (GetTemplateChild("ContentTooltip") is ToolTip value)
		{
			ToolTipService.SetToolTip(this, value);
		}
	}
}
