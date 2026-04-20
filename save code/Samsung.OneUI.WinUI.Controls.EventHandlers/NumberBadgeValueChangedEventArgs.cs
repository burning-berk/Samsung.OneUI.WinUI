using System;

namespace Samsung.OneUI.WinUI.Controls.EventHandlers;

public class NumberBadgeValueChangedEventArgs : EventArgs
{
	public int OldValue { get; private set; }

	public int NewValue { get; private set; }

	internal NumberBadgeValueChangedEventArgs(int oldValue, int newValue)
	{
		OldValue = oldValue;
		NewValue = newValue;
	}
}
