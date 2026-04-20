using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Controls.Inputs.CheckBox;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class CheckBoxStyleSelector : StyleSelector
{
	private const string DEFAULT_STYLE = "OneUICheckBoxStyle";

	private const string WHITE_STYLE = "OneUICheckBoxWhiteStyle";

	private const string THUMBNAIL_STYLE = "OneUICheckBoxThumbnailStyle";

	private const string MULTIPLE_SELECTION_STYLE = "OneUICheckBoxMultipleSelectionStyle";

	private const string GHOST_STYLE = "OneUICheckBoxGhostStyle";

	private readonly CheckBoxType _checkboxType;

	public CheckBoxStyleSelector(CheckBoxType checkboxType)
	{
		_checkboxType = checkboxType;
	}

	public Style SelectStyle()
	{
		return _checkboxType switch
		{
			CheckBoxType.Thumbnail => "OneUICheckBoxThumbnailStyle".GetStyle(), 
			CheckBoxType.White => "OneUICheckBoxWhiteStyle".GetStyle(), 
			CheckBoxType.MultipleSelection => "OneUICheckBoxMultipleSelectionStyle".GetStyle(), 
			CheckBoxType.Ghost => "OneUICheckBoxGhostStyle".GetStyle(), 
			_ => "OneUICheckBoxStyle".GetStyle(), 
		};
	}
}
