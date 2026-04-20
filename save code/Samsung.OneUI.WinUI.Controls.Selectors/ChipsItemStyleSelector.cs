using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls.Selectors;

internal class ChipsItemStyleSelector : StyleSelector
{
	public Style NoneStyle { get; set; }

	public Style CancelStyle { get; set; }

	public Style MinusStyle { get; set; }

	public Style TagStyle { get; set; }

	public Style NoneBorderStyle { get; set; }

	public Style CancelBorderStyle { get; set; }

	public Style MinusBorderStyle { get; set; }

	public Style TagBorderStyle { get; set; }

	protected override Style SelectStyleCore(object item, DependencyObject container)
	{
		Style result = NoneStyle;
		if (item is ChipsItem chipsItem)
		{
			bool flag = chipsItem.Type == ChipsItemType.Default;
			switch (chipsItem.Label)
			{
			case ChipsItemTemplate.Cancel:
				result = (flag ? CancelStyle : CancelBorderStyle);
				break;
			case ChipsItemTemplate.Minus:
				result = (flag ? MinusStyle : MinusBorderStyle);
				break;
			case ChipsItemTemplate.Tag:
			case ChipsItemTemplate.Custom:
				result = (flag ? TagStyle : TagBorderStyle);
				break;
			case ChipsItemTemplate.Default:
				if (!flag)
				{
					result = NoneBorderStyle;
				}
				break;
			}
		}
		return result;
	}
}
