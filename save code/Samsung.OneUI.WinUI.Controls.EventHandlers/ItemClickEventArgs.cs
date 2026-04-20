using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls.EventHandlers;

public sealed class ItemClickEventArgs : RoutedEventArgs
{
	public object ClickedItem { get; set; }
}
