using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls.Inputs.Slider.Base;

internal readonly struct SliderBorders(Border trackBorder, Border decreaseBorder, Border decreaseOverlayBorder, Border decreaseBorderBuffer)
{
	public Border TrackBorder { get; } = trackBorder;

	public Border DecreaseBorder { get; } = decreaseBorder;

	public Border DecreaseOverlayBorder { get; } = decreaseOverlayBorder;

	public Border DecreaseBorderBuffer { get; } = decreaseBorderBuffer;
}
