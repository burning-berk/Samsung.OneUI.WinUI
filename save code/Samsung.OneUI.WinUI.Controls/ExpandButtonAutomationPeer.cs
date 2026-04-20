using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

internal class ExpandButtonAutomationPeer : ButtonAutomationPeer
{
	public ExpandButtonAutomationPeer(Button owner)
		: base(owner)
	{
		AssignEvents();
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.Button;
	}

	protected override string GetLocalizedControlTypeCore()
	{
		if (base.Owner is ExpandButton expandButton)
		{
			return expandButton.GetLocalizedName();
		}
		return base.GetLocalizedControlTypeCore();
	}

	private void ExpandButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is ExpandButton expandButton)
		{
			string localizedName = expandButton.GetLocalizedName();
			RaiseNotificationEvent(AutomationNotificationKind.Other, AutomationNotificationProcessing.MostRecent, localizedName, localizedName);
		}
	}

	private void AssignEvents()
	{
		if (base.Owner is ExpandButton expandButton)
		{
			expandButton.Click += ExpandButton_Click;
		}
	}
}
