using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

internal static class ControlExtensions
{
	public static void ApplyTopCornerRadius(this Control control, double cornerRadiusValue)
	{
		control.CornerRadius = new CornerRadius(cornerRadiusValue, cornerRadiusValue, 0.0, 0.0);
	}

	public static void ApplyBottomCornerRadius(this Control control, double cornerRadiusValue)
	{
		control.CornerRadius = new CornerRadius(0.0, 0.0, cornerRadiusValue, cornerRadiusValue);
	}

	public static void ApplyCornersRadius(this Control control, double cornerRadiusValue)
	{
		control.CornerRadius = new CornerRadius(cornerRadiusValue);
	}
}
