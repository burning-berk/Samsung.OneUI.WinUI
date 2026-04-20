using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class DropdownStyleSelector : StyleSelector
{
	private const string DEFAULT_STYLE = "DropdownListDefault";

	private const string SUB_APP_BAR_STYLE = "DropdownListSubAppBar";

	private readonly DropdownListType _dropdownListType;

	public DropdownStyleSelector(DropdownListType dropdownListType)
	{
		_dropdownListType = dropdownListType;
	}

	public Style SelectStyle()
	{
		if (_dropdownListType != DropdownListType.SubAppBar)
		{
			return "DropdownListDefault".GetStyle();
		}
		return "DropdownListSubAppBar".GetStyle();
	}
}
