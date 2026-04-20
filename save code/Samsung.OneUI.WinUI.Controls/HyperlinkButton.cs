using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class HyperlinkButton : Microsoft.UI.Xaml.Controls.HyperlinkButton
{
	private const string TEXT_BLOCK_CHILD = "TextBlockChild";

	private TextBlock _textBlockChild;

	public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register("TextTrimming", typeof(TextTrimming), typeof(HyperlinkButton), new PropertyMetadata(TextTrimming.CharacterEllipsis, null));

	public static readonly DependencyProperty IsTextTrimmedProperty = DependencyProperty.Register("IsTextTrimmed", typeof(bool), typeof(HyperlinkButton), new PropertyMetadata(false, null));

	public TextTrimming TextTrimming
	{
		get
		{
			return (TextTrimming)GetValue(TextTrimmingProperty);
		}
		set
		{
			SetValue(TextTrimmingProperty, value);
		}
	}

	public bool IsTextTrimmed
	{
		get
		{
			return (bool)GetValue(IsTextTrimmedProperty);
		}
		private set
		{
			SetValue(IsTextTrimmedProperty, value);
		}
	}

	public HyperlinkButton()
	{
		base.Loaded += delegate
		{
			if (_textBlockChild != null)
			{
				_textBlockChild.IsTextTrimmedChanged += TextBlockChild_IsTextTrimmedChanged;
			}
		};
	}

	private void TextBlockChild_IsTextTrimmedChanged(TextBlock sender, IsTextTrimmedChangedEventArgs args)
	{
		IsTextTrimmed = sender.IsTextTrimmed;
	}

	protected override void OnApplyTemplate()
	{
		_textBlockChild = GetTemplateChild("TextBlockChild") as TextBlock;
		base.OnApplyTemplate();
	}
}
