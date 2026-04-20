using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;

namespace Samsung.OneUI.WinUI.Controls;

internal class DropdownCustomControlAutomationPeer : FrameworkElementAutomationPeer, IInvokeProvider
{
	public DropdownCustomControlAutomationPeer(DropdownCustomControl owner)
		: base(owner)
	{
	}

	protected override string GetClassNameCore()
	{
		return "DropdownCustomControl";
	}

	protected override object GetPatternCore(PatternInterface patternInterface)
	{
		if (patternInterface == PatternInterface.Invoke)
		{
			return this;
		}
		return base.GetPatternCore(patternInterface);
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.Text;
	}

	public void Invoke()
	{
		if (base.Owner is DropdownCustomControl dropdownCustomControl)
		{
			dropdownCustomControl.ExecuteAutomationRequest();
		}
	}
}
