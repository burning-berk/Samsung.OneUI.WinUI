using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls.EventHandlers;

public class UserValueChangedEventArgs : RoutedEventArgs
{
	public double NewValue { get; }

	public double OldValue { get; }

	public UserValueChangedEventArgs(double newValue, double oldValue)
	{
		NewValue = newValue;
		OldValue = oldValue;
	}
}
