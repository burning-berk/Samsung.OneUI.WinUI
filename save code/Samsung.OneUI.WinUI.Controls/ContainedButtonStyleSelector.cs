using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

internal class ContainedButtonStyleSelector : StyleSelector
{
	private const string PRIMARY_CONTAINED_BUTTON_STYLE = "OneUIContainedButtonPrimaryStyle";

	private const string SECONDARY_CONTAINED_BUTTON_STYLE = "OneUIContainedButtonSecondaryStyle";

	private readonly ContainedButtonType _containedButtonType;

	public ContainedButtonStyleSelector(ContainedButtonType containedButtonType)
	{
		_containedButtonType = containedButtonType;
	}

	public Style SelectStyle()
	{
		if (_containedButtonType == ContainedButtonType.Primary)
		{
			return "OneUIContainedButtonPrimaryStyle".GetStyle();
		}
		return "OneUIContainedButtonSecondaryStyle".GetStyle();
	}
}
