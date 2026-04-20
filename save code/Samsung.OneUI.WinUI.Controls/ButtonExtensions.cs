using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Samsung.OneUI.WinUI.Controls;

internal static class ButtonExtensions
{
	private const int DEFAULT_RADIUS_VALUE = 15;

	internal static void RoundResizedContentButton(this ButtonBase button, SizeChangedEventArgs e, ButtonShapeEnum shape)
	{
		if (shape == ButtonShapeEnum.Rounded)
		{
			if (e.NewSize.Width == e.PreviousSize.Width)
			{
				button.Width = e.NewSize.Height;
				button.CornerRadius = new CornerRadius(button.Width / 2.0);
			}
			else
			{
				button.Height = e.NewSize.Width;
				button.CornerRadius = new CornerRadius(button.Height / 2.0);
			}
		}
	}

	internal static void AdjustContentButtonShape(this ButtonBase button, ButtonShapeEnum shape)
	{
		if (shape == ButtonShapeEnum.Rounded)
		{
			button.CornerRadius = new CornerRadius(button.Height / 2.0);
			button.Width = button.Height;
		}
		else
		{
			button.CornerRadius = new CornerRadius(15.0);
		}
	}
}
