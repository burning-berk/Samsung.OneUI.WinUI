using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

internal class GoToTopButtonAutomationPeer : ButtonAutomationPeer
{
	private const string GO_TO_TOP_BUTTON_TEXT = "DREAM_GO_TO_TOP_TBOPT/text";

	public GoToTopButtonAutomationPeer(Button owner)
		: base(owner)
	{
		if (base.Owner is GoToTopButton goToTopButton)
		{
			goToTopButton.Click += GoToTopButton_Click;
		}
	}

	private string GetTranslationName()
	{
		return "DREAM_GO_TO_TOP_TBOPT/text".GetLocalized();
	}

	private void GoToTopButton_Click(object sender, RoutedEventArgs e)
	{
		string translationName = GetTranslationName();
		RaiseNotificationEvent(AutomationNotificationKind.Other, AutomationNotificationProcessing.MostRecent, translationName, translationName);
	}

	protected override string GetNameCore()
	{
		return GetTranslationName();
	}
}
