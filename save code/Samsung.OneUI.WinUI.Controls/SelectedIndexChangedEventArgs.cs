using System;

namespace Samsung.OneUI.WinUI.Controls;

public class SelectedIndexChangedEventArgs : EventArgs
{
	public int OldIndex { get; }

	public int NewIndex { get; }

	public SelectedIndexChangedEventArgs(int oldIndex, int newIndex)
	{
		OldIndex = oldIndex;
		NewIndex = newIndex;
	}
}
