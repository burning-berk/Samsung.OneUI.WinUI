using System;
using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

[Obsolete("This style is deprecated, please use property \"IsProgressEnabled\" from Contained, ContainedColored, ContainedBody or ContainedBodyColored. This component will be removed soon.", false)]
internal class ProgressButtonStyleSelector : StyleSelector
{
	private const string PROGRESS_BUTTON_FLAT_STYLE = "OneUIProgressButtonFlatStyle";

	private const string PROGRESS_BUTTON_CONTAINED_STYLE = "OneUIProgressButtonContainedStyle";

	private const string PROGRESS_BUTTON_CONTAINED_COLORED_STYLE = "OneUIProgressButtonContainedColoredStyle";

	private const string PROGRESS_BUTTON_CONTAINED_BODY_STYLE = "OneUIProgressButtonContainedBodyStyle";

	private const string PROGRESS_BUTTON_CONTAINED_BODY_COLORED_STYLE = "OneUIProgressButtonContainedBodyColoredStyle";

	private readonly ProgressButtonType _progressButtonType;

	public ProgressButtonStyleSelector(ProgressButtonType progressButtonType)
	{
		_progressButtonType = progressButtonType;
	}

	public Style SelectStyle()
	{
		return _progressButtonType switch
		{
			ProgressButtonType.Flat => "OneUIProgressButtonFlatStyle".GetStyle(), 
			ProgressButtonType.ContainedColored => "OneUIProgressButtonContainedColoredStyle".GetStyle(), 
			ProgressButtonType.ContainedBody => "OneUIProgressButtonContainedBodyStyle".GetStyle(), 
			ProgressButtonType.ContainedBodyColored => "OneUIProgressButtonContainedBodyColoredStyle".GetStyle(), 
			_ => "OneUIProgressButtonContainedStyle".GetStyle(), 
		};
	}
}
