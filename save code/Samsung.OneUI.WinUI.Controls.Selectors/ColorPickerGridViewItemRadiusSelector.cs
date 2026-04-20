using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class ColorPickerGridViewItemRadiusSelector : StyleSelector
{
	private int lineItensCount = 10;

	public Style TopLeftItem { get; set; }

	public Style TopRightItem { get; set; }

	public Style BottomLeftItem { get; set; }

	public Style BottomRightItem { get; set; }

	public Style MiddleItem { get; set; }

	protected override Style SelectStyleCore(object item, DependencyObject container)
	{
		ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(container);
		if (itemsControl == null)
		{
			return MiddleItem;
		}
		if (itemsControl is GridView { ItemsPanelRoot: ItemsWrapGrid itemsPanelRoot })
		{
			lineItensCount = itemsPanelRoot.MaximumRowsOrColumns;
		}
		int itemIndex = itemsControl.IndexFromContainer(container);
		return GetStyle(itemsControl, itemIndex);
	}

	private Style GetStyle(ItemsControl itemControl, int itemIndex)
	{
		if (itemIndex == 0)
		{
			return TopLeftItem;
		}
		if (itemIndex == lineItensCount - 1)
		{
			return BottomLeftItem;
		}
		if (itemIndex == itemControl.Items.Count - lineItensCount)
		{
			return TopRightItem;
		}
		if (itemIndex == itemControl.Items.Count - 1)
		{
			return BottomRightItem;
		}
		return MiddleItem;
	}
}
