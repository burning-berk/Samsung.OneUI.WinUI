using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Inputs.DropdownList;

public class DropdownListViewItemCustomAutomationPeer : ListViewItemDataAutomationPeer
{
	private const string SELECTED_ID = "SS_SELECTED";

	private const string ITEM_ORDER_ID = "DREAM_P1SD_OF_P2SD_TBOPT";

	private readonly DropdownListViewCustom _dropDownListViewCustom;

	public DropdownListViewItemCustomAutomationPeer(object item, ListViewBaseAutomationPeer parent)
		: base(item, parent)
	{
		_dropDownListViewCustom = (DropdownListViewCustom)((ListViewAutomationPeer)base.ItemsControlAutomationPeer).Owner;
	}

	protected override string GetLocalizedControlTypeCore()
	{
		ListViewItem listViewItem = _dropDownListViewCustom.ContainerFromItem(base.Item) as ListViewItem;
		string value = ((listViewItem == null) ? string.Empty : (listViewItem.Content?.ToString() ?? string.Empty));
		string value2 = string.Format(GetTranslation("DREAM_P1SD_OF_P2SD_TBOPT"), _dropDownListViewCustom.Items.IndexOf(base.Item) + 1, _dropDownListViewCustom.Items.Count);
		return $"{value}, {value2}, {SelectedText()}".Trim(',');
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.ListItem;
	}

	private string GetTranslation(string resourceKey)
	{
		return resourceKey.GetLocalized();
	}

	private string SelectedText()
	{
		if (!_dropDownListViewCustom.SelectedItems.Contains(base.Item))
		{
			return string.Empty;
		}
		return GetTranslation("SS_SELECTED");
	}
}
