using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class CenterSliderStyleSelector : ISliderStyleSelector
{
	private const string TYPE_1_STYLE = "OneUICenterSliderType1Style";

	private const string TYPE_2_STYLE = "OneUICenterSliderType2Style";

	public Style SelectStyle(SliderType sliderType)
	{
		if (sliderType == SliderType.Type2)
		{
			return "OneUICenterSliderType2Style".GetStyle();
		}
		return "OneUICenterSliderType1Style".GetStyle();
	}
}
