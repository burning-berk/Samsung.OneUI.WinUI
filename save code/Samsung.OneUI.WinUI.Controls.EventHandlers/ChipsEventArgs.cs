using System;

namespace Samsung.OneUI.WinUI.Controls.EventHandlers;

public class ChipsEventArgs : EventArgs
{
	public ChipsItemAction Action { get; private set; }

	public object SelectedItem { get; private set; }

	public int SelectedIndex { get; private set; }

	public ChipsEventArgs(ChipsItemAction action, object selectedItem, int selectedIndex)
	{
		Action = action;
		SelectedItem = selectedItem;
		SelectedIndex = selectedIndex;
	}
}
