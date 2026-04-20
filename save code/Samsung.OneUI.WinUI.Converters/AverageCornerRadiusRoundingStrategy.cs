using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Converters;

internal class AverageCornerRadiusRoundingStrategy : ICornerRadiusRoundingStrategyConvertion
{
	double ICornerRadiusRoundingStrategyConvertion.Convert(CornerRadius cornerRadius)
	{
		int num = 4;
		double topRight = cornerRadius.TopRight;
		double topLeft = cornerRadius.TopLeft;
		double bottomRight = cornerRadius.BottomRight;
		double bottomLeft = cornerRadius.BottomLeft;
		return (topLeft + topRight + bottomRight + bottomLeft) / (double)num;
	}
}
