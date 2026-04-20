using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

public interface ISliderStyleSelector
{
	Style SelectStyle(SliderType sliderType);
}
