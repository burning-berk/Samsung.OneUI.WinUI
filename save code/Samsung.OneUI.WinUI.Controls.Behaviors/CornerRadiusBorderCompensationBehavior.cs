using System.Reflection;
using Microsoft.UI.Xaml;
using Microsoft.Xaml.Interactivity;

namespace Samsung.OneUI.WinUI.Controls.Behaviors;

public class CornerRadiusBorderCompensationBehavior : Behavior<FrameworkElement>
{
	public static readonly DependencyProperty CompensationProperty = DependencyProperty.Register("Compensation", typeof(double), typeof(CornerRadiusBorderCompensationBehavior), new PropertyMetadata(-3.0, OnCompensationChanged));

	public double Compensation
	{
		get
		{
			return (double)GetValue(CompensationProperty);
		}
		set
		{
			SetValue(CompensationProperty, value);
		}
	}

	protected override void OnAttached()
	{
		base.OnAttached();
		base.AssociatedObject.Loaded += OnLoaded;
	}

	protected override void OnDetaching()
	{
		base.AssociatedObject.Loaded -= OnLoaded;
		base.OnDetaching();
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		UpdateCornerRadius();
	}

	private static void OnCompensationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CornerRadiusBorderCompensationBehavior cornerRadiusBorderCompensationBehavior && cornerRadiusBorderCompensationBehavior.AssociatedObject != null)
		{
			cornerRadiusBorderCompensationBehavior.UpdateCornerRadius();
		}
	}

	private void UpdateCornerRadius()
	{
		PropertyInfo property = base.AssociatedObject.GetType().GetProperty("CornerRadius");
		if (property != null && property.PropertyType == typeof(CornerRadius))
		{
			property.SetValue(value: new CornerRadius(((CornerRadius)property.GetValue(base.AssociatedObject)).TopLeft + Compensation), obj: base.AssociatedObject);
		}
	}
}
