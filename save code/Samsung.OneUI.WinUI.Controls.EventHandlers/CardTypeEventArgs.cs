using System;

namespace Samsung.OneUI.WinUI.Controls.EventHandlers;

public class CardTypeEventArgs : EventArgs
{
	public object SelectedItem { get; private set; }

	public int SelectedIndex { get; private set; }

	public CardTypeEventArgs(object selectedItem, int selectedIndex)
	{
		SelectedItem = selectedItem;
		SelectedIndex = selectedIndex;
	}
}
