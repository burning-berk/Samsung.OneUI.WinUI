using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class FlipViewButton : Button
{
	public static readonly DependencyProperty IsBlurProperty = DependencyProperty.Register("IsBlur", typeof(bool), typeof(FlipViewButton), new PropertyMetadata(false));

	public bool IsBlur
	{
		get
		{
			return (bool)GetValue(IsBlurProperty);
		}
		set
		{
			SetValue(IsBlurProperty, value);
		}
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
	}
}
