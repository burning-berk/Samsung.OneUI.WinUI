using System.Linq;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class RadioButtons : Microsoft.UI.Xaml.Controls.RadioButtons
{
	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		if (!base.Items.Any())
		{
			return;
		}
		foreach (object item in base.Items.Where((object i) => !(i is RadioButton)))
		{
			RadioButton oneUIRadioButton = new RadioButton
			{
				Content = item
			};
			ReplaceItemToOneUIRadioButtonItem(item, oneUIRadioButton);
		}
	}

	private void ReplaceItemToOneUIRadioButtonItem(object item, RadioButton oneUIRadioButton)
	{
		int index = base.Items.IndexOf(item);
		base.Items.Remove(item);
		base.Items.Insert(index, oneUIRadioButton);
	}
}
