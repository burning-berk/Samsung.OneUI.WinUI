using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Samsung.OneUI.WinUI.Controls;

public class CardTypeItem
{
	public ImageSource Image { get; set; }

	public Style SvgStyle { get; set; }

	public string Title { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public string ButtonText { get; set; } = string.Empty;

	public EventHandler Click_Event { get; set; }

	internal void ButtonRoutedEvent(object sender, RoutedEventArgs e)
	{
		Click_Event?.Invoke(this, new EventArgs());
	}
}
