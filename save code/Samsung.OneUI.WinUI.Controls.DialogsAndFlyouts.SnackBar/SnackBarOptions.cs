using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar;

public class SnackBarOptions
{
	public double? Width { get; set; }

	public FrameworkElement Target { get; set; }

	public double VerticalOffSet { get; set; }

	public double VerticalAppTitleBarOffSet { get; set; }

	public bool HasWidthValue()
	{
		if (Width.HasValue)
		{
			return !double.IsNaN(Width.Value);
		}
		return false;
	}
}
