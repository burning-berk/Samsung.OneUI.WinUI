using System.Collections.Generic;
using Microsoft.UI;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

[MarkupExtensionReturnType(ReturnType = typeof(Color))]
internal class OverlayColorsToSolidColorBrushExtension : MarkupExtension
{
	public IList<SolidColorBrush> ColorList { get; set; } = new List<SolidColorBrush>();

	protected override object ProvideValue()
	{
		if (ColorList == null || ColorList.Count == 0)
		{
			return Colors.Transparent;
		}
		Color color = ColorList[0].Color;
		for (int i = 1; i < ColorList.Count; i++)
		{
			color = OverlayColors(color, ColorList[i].Color);
		}
		return color;
	}

	private Color OverlayColors(Color background, Color foreground)
	{
		double num = (float)(int)background.A / 255f;
		double num2 = (float)(int)foreground.A / 255f;
		double num3 = num2 + num * (1.0 - num2);
		if (num3 <= 0.0)
		{
			return Colors.Transparent;
		}
		double num4 = ((double)((float)(int)foreground.R / 255f) * num2 + (double)((float)(int)background.R / 255f) * num * (1.0 - num2)) / num3;
		double num5 = ((double)((float)(int)foreground.G / 255f) * num2 + (double)((float)(int)background.G / 255f) * num * (1.0 - num2)) / num3;
		double num6 = ((double)((float)(int)foreground.B / 255f) * num2 + (double)((float)(int)background.B / 255f) * num * (1.0 - num2)) / num3;
		return Color.FromArgb((byte)(num3 * 255.0 + 0.5), (byte)(num4 * 255.0 + 0.5), (byte)(num5 * 255.0 + 0.5), (byte)(num6 * 255.0 + 0.5));
	}
}
