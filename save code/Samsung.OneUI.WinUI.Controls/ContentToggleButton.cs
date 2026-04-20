using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Samsung.OneUI.WinUI.Controls;

public class ContentToggleButton : ToggleButton
{
	public static readonly DependencyProperty ShapeProperty = DependencyProperty.Register("Shape", typeof(ButtonShapeEnum), typeof(ContentToggleButton), new PropertyMetadata(ButtonShapeEnum.Default, OnShapePropertyChanged));

	public ButtonShapeEnum Shape
	{
		get
		{
			return (ButtonShapeEnum)GetValue(ShapeProperty);
		}
		set
		{
			SetValue(ShapeProperty, value);
		}
	}

	public ContentToggleButton()
	{
		base.Loaded += ContentToggleButton_Loaded;
		base.Unloaded += ContentToggleButton_Unloaded;
	}

	private void ContentToggleButton_Loaded(object sender, RoutedEventArgs e)
	{
		base.SizeChanged += ContentToggleButton_SizeChanged;
	}

	private void ContentToggleButton_Unloaded(object sender, RoutedEventArgs e)
	{
		base.SizeChanged -= ContentToggleButton_SizeChanged;
	}

	private void ContentToggleButton_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		this.RoundResizedContentButton(e, Shape);
	}

	private static void OnShapePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ContentToggleButton contentToggleButton)
		{
			contentToggleButton.AdjustContentButtonShape(contentToggleButton.Shape);
		}
	}
}
