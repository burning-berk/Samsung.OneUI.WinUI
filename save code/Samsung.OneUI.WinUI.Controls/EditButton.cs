using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls.Selectors;

namespace Samsung.OneUI.WinUI.Controls;

public class EditButton : Button
{
	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(EditButtonType), typeof(EditButton), new PropertyMetadata(EditButtonType.Add, OnTypeChanged));

	public EditButtonType Type
	{
		get
		{
			return (EditButtonType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is EditButton editButton)
		{
			editButton.Style = new EditButtonStyleSelector(editButton.Type).SelectStyle();
		}
	}
}
