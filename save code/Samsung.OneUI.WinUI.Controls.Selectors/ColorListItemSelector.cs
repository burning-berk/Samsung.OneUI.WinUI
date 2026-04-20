using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class ColorListItemSelector : StyleSelector
{
	private readonly SolidColorBrush TRANSPARENT_COLOR = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

	public Style NormalStyle { get; set; }

	public Style EmptyStyle { get; set; }

	protected override Style SelectStyleCore(object item, DependencyObject container)
	{
		if (!(item is ColorInfo colorInfo))
		{
			return EmptyStyle;
		}
		if (!colorInfo.ColorBrush.Color.ToString().Equals(TRANSPARENT_COLOR.Color.ToString()))
		{
			return NormalStyle;
		}
		return EmptyStyle;
	}
}
