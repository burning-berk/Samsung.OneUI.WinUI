using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls;

internal class ProgressCircleDeterminateModel
{
	public ProgressCircleDeterminateType Type { get; set; }

	public ProgressCircleIndeterminateOrientation Orientation { get; set; }

	public ProgressCircleSize Size { get; set; }

	public double RadiusSize { get; set; }

	public double Thickness { get; set; }

	public Thickness Margin { get; set; }
}
