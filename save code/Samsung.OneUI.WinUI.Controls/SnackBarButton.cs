using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class SnackBarButton : FlatButtonBase
{
	private const string TEXT_BLOCK_CONTENT = "PART_Text";

	private TextBlock _textContent;

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(SnackBarButtonType), typeof(SnackBarButton), new PropertyMetadata(SnackBarButtonType.Secondary, OnTypeChanged));

	public SnackBarButtonType Type
	{
		get
		{
			return (SnackBarButtonType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public SnackBarButton()
	{
		base.Style = new SnackBarButtonStyleSelector(Type).SelectStyle();
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_textContent = GetTemplateChild("PART_Text") as TextBlock;
	}

	private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SnackBarButton snackBarButton)
		{
			snackBarButton.Style = new SnackBarButtonStyleSelector(snackBarButton.Type).SelectStyle();
		}
	}
}
