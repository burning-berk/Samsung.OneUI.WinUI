using System.Globalization;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class TextBadge : BadgeBase
{
	private const string PART_BORDER_TEXT = "TextBlockBadgeText";

	private TextBlock _borderText;

	protected override void OnApplyTemplate()
	{
		_borderText = GetTemplateChild("TextBlockBadgeText") as TextBlock;
		SetTextBadgeString();
		base.OnApplyTemplate();
	}

	private void SetTextBadgeString()
	{
		if (_borderText != null && !string.IsNullOrEmpty(_borderText.Text))
		{
			string text = _borderText.Text;
			string text2 = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(text.ToLower());
			SetValue(AutomationProperties.NameProperty, text2);
			_borderText.Text = text2;
		}
	}
}
