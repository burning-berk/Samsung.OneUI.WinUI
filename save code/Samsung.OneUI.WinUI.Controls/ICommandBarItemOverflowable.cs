using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls;

public interface ICommandBarItemOverflowable
{
	void PerformButtonClick();

	string GetButtonLabel();

	Visibility GetButtonLabelVisibility();
}
