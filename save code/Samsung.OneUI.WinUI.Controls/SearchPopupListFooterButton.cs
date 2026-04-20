using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class SearchPopupListFooterButton : Button
{
	public SearchPopupListFooterButton()
	{
		base.LostFocus += SearchPopupListFooterButtonLostFocus;
	}

	private void SearchPopupListFooterButtonLostFocus(object sender, RoutedEventArgs e)
	{
		SearchPopupList visualParent = UIExtensionsInternal.GetVisualParent<SearchPopupList>(this);
		if (visualParent != null)
		{
			visualParent.OnItemLostFocus(sender, e);
		}
	}
}
