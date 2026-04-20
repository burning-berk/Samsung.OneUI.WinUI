using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class FlatUnderlineButton : FlatButtonBase
{
	private const string UNDERLINE_STYLE = "OneUIFlatButtonUnderlineStyle";

	public FlatUnderlineButton()
	{
		base.Style = "OneUIFlatButtonUnderlineStyle".GetStyle();
	}
}
