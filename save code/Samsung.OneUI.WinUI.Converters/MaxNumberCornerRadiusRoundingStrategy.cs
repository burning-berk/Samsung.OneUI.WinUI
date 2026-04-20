using System;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Converters;

internal class MaxNumberCornerRadiusRoundingStrategy : ICornerRadiusRoundingStrategyConvertion
{
	double ICornerRadiusRoundingStrategyConvertion.Convert(CornerRadius cornerRadius)
	{
		double topRight = cornerRadius.TopRight;
		double topLeft = cornerRadius.TopLeft;
		double bottomRight = cornerRadius.BottomRight;
		double bottomLeft = cornerRadius.BottomLeft;
		double val = Math.Max(topRight, topLeft);
		double val2 = Math.Max(bottomRight, bottomLeft);
		return Math.Max(val, val2);
	}
}
