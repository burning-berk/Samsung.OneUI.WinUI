using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

internal static class ColorExtension
{
	internal static SolidColorBrush SetOpacity(this Color color, double opacity)
	{
		byte a = color.A;
		byte r = color.R;
		byte g = color.G;
		byte b = color.B;
		opacity = ((opacity > 1.0) ? 1.0 : opacity);
		return new SolidColorBrush(Color.FromArgb((byte)((double)(int)a * opacity), r, g, b));
	}
}
