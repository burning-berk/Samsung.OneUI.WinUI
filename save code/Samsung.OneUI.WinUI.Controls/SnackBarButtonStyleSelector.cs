using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class SnackBarButtonStyleSelector : StyleSelector
{
	private const string FLAT_BUTTON_SECONDARY_STYLE = "OneUISnackBarButtonSecondaryStyle";

	private const string FLAT_BUTTON_RED_STYLE = "OneUISnackBarButtonRedStyle";

	private readonly SnackBarButtonType _flatButtonType;

	public SnackBarButtonStyleSelector(SnackBarButtonType flatButtonType)
	{
		_flatButtonType = flatButtonType;
	}

	public Style SelectStyle()
	{
		if (_flatButtonType != SnackBarButtonType.Red)
		{
			return "OneUISnackBarButtonSecondaryStyle".GetStyle();
		}
		return "OneUISnackBarButtonRedStyle".GetStyle();
	}
}
