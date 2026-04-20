using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class NavigationViewItemAutomationPeer : FrameworkElementAutomationPeer
{
	private const string STATUS_KEY = "SS_SELECTED";

	private const string ORDER_KEY = "DREAM_P1SD_OF_P2SD_TBOPT";

	private readonly string _selectedStatus;

	private readonly string _order;

	public NavigationViewItemAutomationPeer(NavigationViewItem owner)
		: base(owner)
	{
		_selectedStatus = "SS_SELECTED".GetLocalized();
		_order = "DREAM_P1SD_OF_P2SD_TBOPT".GetLocalized();
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.Custom;
	}

	protected override string GetLocalizedControlTypeCore()
	{
		return string.Join(", ", GetOrder(), GetStatus());
	}

	protected override int GetPositionInSetCore()
	{
		return 0;
	}

	protected override int GetSizeOfSetCore()
	{
		return 0;
	}

	private string GetOrder()
	{
		if (base.Owner is NavigationViewItem { Parent: ItemsRepeater { ItemsSourceView: var itemsSourceView } } navigationViewItem)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < itemsSourceView.Count; i++)
			{
				if (itemsSourceView.GetAt(i) is NavigationViewItem navigationViewItem2)
				{
					num++;
					if (navigationViewItem2.Equals(navigationViewItem))
					{
						num2 = num;
					}
				}
			}
			return string.Format(_order, num2, num);
		}
		return string.Empty;
	}

	private string GetStatus()
	{
		if (base.Owner is NavigationViewItem { IsSelected: not false })
		{
			return _selectedStatus;
		}
		return string.Empty;
	}
}
