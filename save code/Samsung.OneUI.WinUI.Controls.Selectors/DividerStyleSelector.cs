using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class DividerStyleSelector : StyleSelector
{
	private const string LINE_STYLE_KEY = "OneUILineDividerStyle";

	private const string DASH_STYLE_KEY = "OneUIDashDividerStyle";

	private readonly DividerType _dividerType;

	public DividerStyleSelector(DividerType dividerType)
	{
		_dividerType = dividerType;
	}

	public Style SelectStyle()
	{
		return _dividerType switch
		{
			DividerType.Dash => "OneUIDashDividerStyle".GetStyle(), 
			DividerType.Line => "OneUILineDividerStyle".GetStyle(), 
			_ => "OneUILineDividerStyle".GetStyle(), 
		};
	}
}
