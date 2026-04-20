using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class TextFieldStyleSelector : StyleSelector
{
	private const string NORMAL_STYLE = "OneUITextFieldStyle";

	private const string SINGLELIST_STYLE = "OneUITextFieldSingleListStyle";

	private readonly TextFieldType _textFieldType;

	public TextFieldStyleSelector(TextFieldType textFieldType)
	{
		_textFieldType = textFieldType;
	}

	public Style SelectStyle()
	{
		if (_textFieldType == TextFieldType.SingleList)
		{
			return "OneUITextFieldSingleListStyle".GetStyle();
		}
		return "OneUITextFieldStyle".GetStyle();
	}
}
