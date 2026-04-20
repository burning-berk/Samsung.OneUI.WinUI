using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls;

internal class ContainedButtonModel
{
	public double MinHeight { get; set; }

	public double MinWidth { get; set; }

	public double FontSize { get; set; }

	public Thickness Margin { get; set; }

	public CornerRadius CornerRadius { get; set; }

	public ProgressCircleSize ProgressCircleSize { get; set; }

	public ContainedButtonSize Size { get; set; }
}
