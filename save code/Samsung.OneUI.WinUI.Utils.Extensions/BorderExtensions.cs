using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

internal static class BorderExtensions
{
	public static void ApplyAlignmentBounds(this Border border, FrameworkElement elementSpace, Samsung.OneUI.WinUI.Controls.ContentDialog dialog)
	{
		if (!(dialog.XamlRoot == null))
		{
			double num = border.Margin.Left;
			double num2 = border.Margin.Bottom;
			double right = border.Margin.Right;
			double top = border.Margin.Top;
			if (num < 0.0 || num2 < 0.0)
			{
				num = ((num < 0.0) ? 0.0 : num);
				num2 = ((num2 < 0.0) ? 0.0 : num2);
				border.Margin = new Thickness(num, 0.0, 0.0, num2);
			}
			if (right < 0.0 || top < 0.0)
			{
				num = ((right < 0.0) ? (num + right) : num);
				num2 = ((top < 0.0) ? (num2 + top) : num2);
				border.Margin = new Thickness(num, 0.0, 0.0, num2);
			}
		}
	}
}
