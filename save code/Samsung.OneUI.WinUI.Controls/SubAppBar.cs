using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class SubAppBar : Control
{
	private const string SUB_APP_BAR_CONTENT = "SubAppBarContent";

	public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(UIElement), typeof(SubAppBar), new PropertyMetadata(null));

	public UIElement Content
	{
		get
		{
			return (UIElement)GetValue(ContentProperty);
		}
		set
		{
			SetValue(ContentProperty, value);
		}
	}

	protected override void OnApplyTemplate()
	{
		if (GetTemplateChild("SubAppBarContent") is ContentPresenter contentPresenter)
		{
			contentPresenter.Content = Content;
		}
		base.OnApplyTemplate();
	}
}
