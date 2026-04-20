using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class EditButtonStyleSelector : StyleSelector
{
	private const string ADD_BUTTON_STYLE = "OneUIEditButtonAdd";

	private const string DELETE_BUTTON_STYLE = "OneUIEditButtonDelete";

	private readonly EditButtonType _editButtonType;

	public EditButtonStyleSelector(EditButtonType editButtonType)
	{
		_editButtonType = editButtonType;
	}

	public Style SelectStyle()
	{
		if (_editButtonType == EditButtonType.Add)
		{
			return "OneUIEditButtonAdd".GetStyle();
		}
		return "OneUIEditButtonDelete".GetStyle();
	}
}
