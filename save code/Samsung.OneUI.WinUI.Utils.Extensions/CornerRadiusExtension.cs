using System;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

internal static class CornerRadiusExtension
{
	internal static CornerRadius SetCorners(this CornerRadius cornerRadius, string corner)
	{
		double topLeft = 0.0;
		double topRight = 0.0;
		double bottomRight = 0.0;
		double bottomLeft = 0.0;
		string[] array = corner.Split('|', StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			switch (array[i].Trim().ToLower())
			{
			case "topleft":
				topLeft = cornerRadius.TopLeft;
				continue;
			case "topright":
				topRight = cornerRadius.TopRight;
				continue;
			case "bottomright":
				bottomRight = cornerRadius.BottomRight;
				continue;
			case "bottomleft":
				bottomLeft = cornerRadius.BottomLeft;
				continue;
			}
			topLeft = cornerRadius.TopLeft;
			topRight = cornerRadius.TopRight;
			bottomRight = cornerRadius.BottomRight;
			bottomLeft = cornerRadius.BottomLeft;
		}
		return new CornerRadius(topLeft, topRight, bottomRight, bottomLeft);
	}
}
