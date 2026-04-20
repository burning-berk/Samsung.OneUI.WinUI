using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class SliderStyleSelector : ISliderStyleSelector
{
	private const string TYPE_1_STYLE = "OneUISliderType1Style";

	private const string TYPE_2_STYLE = "OneUISliderType2Style";

	private const string GHOST_TYPE_STYLE = "OneUISliderGhostTypeStyle";

	public Style SelectStyle(SliderType sliderType)
	{
		return sliderType switch
		{
			SliderType.Type2 => "OneUISliderType2Style".GetStyle(), 
			SliderType.Ghost => "OneUISliderGhostTypeStyle".GetStyle(), 
			_ => "OneUISliderType1Style".GetStyle(), 
		};
	}
}
