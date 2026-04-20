using Microsoft.UI.Xaml.Automation.Peers;

namespace Samsung.OneUI.WinUI.Controls.Inputs.DropdownList;

public class DropdownListViewCustomAutomationPeer : ListViewAutomationPeer
{
	public DropdownListViewCustomAutomationPeer(DropdownListViewCustom owner)
		: base(owner)
	{
	}

	protected override ItemAutomationPeer OnCreateItemAutomationPeer(object item)
	{
		return new DropdownListViewItemCustomAutomationPeer(item, this);
	}
}
