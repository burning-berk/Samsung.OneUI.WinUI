using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class FlatButtonStyleSelector : StyleSelector
{
	private const string FLAT_BUTTON_PRIMARY_STYLE = "OneUIFlatButtonPrimaryStyle";

	private const string FLAT_BUTTON_SECONDARY_STYLE = "OneUIFlatButtonSecondaryStyle";

	private const string FLAT_BUTTON_RED_STYLE = "OneUIFlatButtonRedStyle";

	private readonly FlatButtonType _flatButtonType;

	public FlatButtonStyleSelector(FlatButtonType flatButtonType)
	{
		_flatButtonType = flatButtonType;
	}

	public Style SelectStyle()
	{
		return _flatButtonType switch
		{
			FlatButtonType.Primary => "OneUIFlatButtonPrimaryStyle".GetStyle(), 
			FlatButtonType.Secondary => "OneUIFlatButtonSecondaryStyle".GetStyle(), 
			FlatButtonType.Red => "OneUIFlatButtonRedStyle".GetStyle(), 
			_ => "OneUIFlatButtonPrimaryStyle".GetStyle(), 
		};
	}
}
