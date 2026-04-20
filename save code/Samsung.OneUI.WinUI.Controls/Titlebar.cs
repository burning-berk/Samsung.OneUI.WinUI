using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class Titlebar : Control
{
	public TextBlock TitlebarText;

	private Thickness _textTitleBarMarginWithoutBackButton = new Thickness(16.0, 0.0, 0.0, 0.0);

	private const string PART_TITLE_TEXT = "TitlebarText";

	private const double ACTIVE_OPACITY = 1.0;

	private const double INACTIVE_OPACITY = 0.36;

	public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Titlebar), new PropertyMetadata(string.Empty, OnChangeText));

	public string Title
	{
		get
		{
			return (string)GetValue(TitleProperty);
		}
		set
		{
			SetValue(TitleProperty, value);
		}
	}

	private static void OnChangeText(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Titlebar titlebar)
		{
			titlebar.UpdateTitle();
		}
	}

	protected override void OnApplyTemplate()
	{
		TitlebarText = GetTemplateChild("TitlebarText") as TextBlock;
		TitlebarText.Margin = _textTitleBarMarginWithoutBackButton;
		UpdateTitle();
		base.OnApplyTemplate();
	}

	private void UpdateTitle()
	{
		if (TitlebarText != null)
		{
			TitlebarText.Text = Title;
		}
	}
}
