using System;
using System.Drawing;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Utils.Helpers;

internal static class ColorsHelpers
{
	public static SolidColorBrush ConvertColorHex(string colorString)
	{
		try
		{
			System.Drawing.Color color = (System.Drawing.Color)new ColorConverter().ConvertFromString(colorString);
			return new SolidColorBrush(Windows.UI.Color.FromArgb(color.A, color.R, color.G, color.B));
		}
		catch (ArgumentException)
		{
			return new SolidColorBrush();
		}
	}

	public static SolidColorBrush CreateFromChannels(int alpha, int red, int green, int blue)
	{
		byte a = (byte)Math.Clamp(alpha, 0, 255);
		byte r = (byte)Math.Clamp(red, 0, 255);
		byte g = (byte)Math.Clamp(green, 0, 255);
		byte b = (byte)Math.Clamp(blue, 0, 255);
		return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
	}

	public static bool AreColorsEqual(SolidColorBrush firstSolidColorBrush, SolidColorBrush secondSolidColorBrush)
	{
		if (firstSolidColorBrush == null || secondSolidColorBrush == null)
		{
			return false;
		}
		return firstSolidColorBrush.Color.ToString().Equals(secondSolidColorBrush.Color.ToString());
	}

	public static Windows.UI.Color ConvertHexToColor(string hex, double alpha = 100.0)
	{
		hex = hex.Replace("#", string.Empty);
		byte a = (byte)Math.Round(alpha / 100.0 * 255.0);
		byte b = 0;
		byte b2 = 0;
		byte b3 = 0;
		if (hex.Length == 8)
		{
			a = Convert.ToByte(hex.Substring(0, 2), 16);
			b = Convert.ToByte(hex.Substring(2, 2), 16);
			b2 = Convert.ToByte(hex.Substring(4, 2), 16);
			b3 = Convert.ToByte(hex.Substring(6, 2), 16);
		}
		else
		{
			if (hex.Length != 6)
			{
				throw new ArgumentException("Invalid color format. Use #RRGGBB or #AARRGGBB.");
			}
			b = Convert.ToByte(hex.Substring(0, 2), 16);
			b2 = Convert.ToByte(hex.Substring(2, 2), 16);
			b3 = Convert.ToByte(hex.Substring(4, 2), 16);
		}
		return Windows.UI.Color.FromArgb(a, b, b2, b3);
	}
}
