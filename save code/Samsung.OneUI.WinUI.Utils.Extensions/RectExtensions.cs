using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Controls;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

internal static class RectExtensions
{
	public static double GetAlignmentHorizontal(this Size rect, FrameworkElement elementHandler, ContentDialog dialog)
	{
		double result = 0.0;
		if (elementHandler != null && dialog.XamlRoot != null)
		{
			result = elementHandler.TransformToVisual(dialog.XamlRoot.Content).TransformPoint(new Point(0f, 0f)).X;
		}
		return result;
	}

	public static double GetAlignmentVertical(this Size rect, FrameworkElement elementHandler, ContentDialog dialog)
	{
		double result = 0.0;
		if (elementHandler != null && dialog.XamlRoot != null)
		{
			double height = rect.Height;
			double y = elementHandler.TransformToVisual(dialog.XamlRoot.Content).TransformPoint(new Point(0f, 0f)).Y;
			result = height - y - elementHandler.ActualHeight;
		}
		return result;
	}
}
