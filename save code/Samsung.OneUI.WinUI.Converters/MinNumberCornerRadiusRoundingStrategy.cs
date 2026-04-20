using System;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Converters;

internal class MinNumberCornerRadiusRoundingStrategy : ICornerRadiusRoundingStrategyConvertion
{
	double ICornerRadiusRoundingStrategyConvertion.Convert(CornerRadius cornerRadius)
	{
		double topRight = cornerRadius.TopRight;
		double topLeft = cornerRadius.TopLeft;
		double bottomRight = cornerRadius.BottomRight;
		double bottomLeft = cornerRadius.BottomLeft;
		double val = Math.Min(topRight, topLeft);
		double val2 = Math.Min(bottomRight, bottomLeft);
		return Math.Min(val, val2);
	}
}
