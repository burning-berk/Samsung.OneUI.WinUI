using System.Collections.Generic;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls.DataView.Lists.Custom;

internal class ListViewCustomAutomationPeer : ListViewAutomationPeer
{
	private readonly ListViewCustom _owner;

	public ListViewCustomAutomationPeer(ListViewCustom owner)
		: base(owner)
	{
		_owner = owner;
	}

	protected override IList<AutomationPeer> GetChildrenCore()
	{
		if (_owner.Items.Count > 0)
		{
			IList<AutomationPeer> childrenCore = base.GetChildrenCore();
			AddTextBlockAutomationPeers(childrenCore, _owner.counterTextBlock);
			return childrenCore;
		}
		IList<AutomationPeer> childrenCore2 = base.GetChildrenCore();
		childrenCore2.Clear();
		AddTextBlockAutomationPeers(childrenCore2, _owner.noItemsTextBlock);
		AddTextBlockAutomationPeers(childrenCore2, _owner.noItemsDescriptionTextBlock);
		return childrenCore2;
	}

	private void AddTextBlockAutomationPeers(IList<AutomationPeer> listViewCustomPeers, TextBlock textBlock)
	{
		if (textBlock != null && !string.IsNullOrWhiteSpace(textBlock.Text))
		{
			AutomationPeer automationPeer = FrameworkElementAutomationPeer.CreatePeerForElement(textBlock);
			if (automationPeer != null)
			{
				listViewCustomPeers.Add(automationPeer);
			}
		}
	}
}
