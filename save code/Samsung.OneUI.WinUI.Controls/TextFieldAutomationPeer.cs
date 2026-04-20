using Microsoft.UI.Xaml.Automation.Peers;

namespace Samsung.OneUI.WinUI.Controls;

internal class TextFieldAutomationPeer : FrameworkElementAutomationPeer
{
	private readonly TextField _owner;

	public TextFieldAutomationPeer(TextField owner)
		: base(owner)
	{
		_owner = owner;
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.Text;
	}

	protected override string GetNameCore()
	{
		return string.Empty;
	}

	protected override string GetHelpTextCore()
	{
		return string.Empty;
	}

	protected override string GetLocalizedControlTypeCore()
	{
		return _owner._textFieldNarratorText ?? "";
	}
}
