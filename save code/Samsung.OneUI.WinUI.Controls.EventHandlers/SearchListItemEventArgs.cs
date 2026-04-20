using System;

namespace Samsung.OneUI.WinUI.Controls.EventHandlers;

public class SearchListItemEventArgs : EventArgs
{
	public object SelectedItem { get; private set; }

	public int SelectedIndex { get; private set; }

	public SearchListItemEventArgs(object selectedItem, int selectedIndex)
	{
		SelectedItem = selectedItem;
		SelectedIndex = selectedIndex;
	}
}
