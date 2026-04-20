using System;

namespace Samsung.OneUI.WinUI.Controls.EventHandlers;

public class SliderRangeBaseValueChangedEventArgs : EventArgs
{
	public double OldValue { get; private set; }

	public double NewValue { get; private set; }

	internal SliderRangeBaseValueChangedEventArgs(double oldValue, double newValue)
	{
		OldValue = oldValue;
		NewValue = newValue;
	}
}
