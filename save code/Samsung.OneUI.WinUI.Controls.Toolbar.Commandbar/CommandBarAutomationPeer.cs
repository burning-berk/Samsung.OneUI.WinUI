using Microsoft.UI.Xaml.Automation.Peers;

namespace Samsung.OneUI.WinUI.Controls.Toolbar.Commandbar;

internal class CommandBarAutomationPeer : FrameworkElementAutomationPeer
{
	public CommandBarAutomationPeer(CommandBar owner)
		: base(owner)
	{
	}

	protected override object GetPatternCore(PatternInterface patternInterface)
	{
		if (patternInterface == PatternInterface.ExpandCollapse)
		{
			return null;
		}
		return base.GetPatternCore(patternInterface);
	}
}
