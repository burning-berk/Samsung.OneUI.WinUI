using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class PeriodStyleSelector : StyleSelector
{
	private const int FIRST_ITEM_INDEX = 0;

	public Style NormalStyle { get; set; }

	public Style HiddenStyle { get; set; }

	protected override Style SelectStyleCore(object item, DependencyObject container)
	{
		Style result = NormalStyle;
		ListView listView = ItemsControl.ItemsControlFromItemContainer(container) as ListView;
		if (listView == null)
		{
			return result;
		}
		int num = listView.IndexFromContainer(container);
		if (num == 0 || num == listView.Items.Count - 1)
		{
			result = HiddenStyle;
		}
		return result;
	}
}
