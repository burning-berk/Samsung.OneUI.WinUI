using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.Media;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.AttachedProperties;

public class ElevationCorner
{
	public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(double), typeof(ElevationCorner), new PropertyMetadata(0, OnCornerRadiusPropertyChanged));

	public static double GetCornerRadius(DependencyObject obj)
	{
		return (double)obj.GetValue(CornerRadiusProperty);
	}

	public static void SetCornerRadius(DependencyObject obj, double value)
	{
		obj.SetValue(CornerRadiusProperty, value);
	}

	private static void OnCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (e.NewValue is double cornerRadius && d is FrameworkElement frameworkElement)
		{
			if (!frameworkElement.IsLoaded)
			{
				frameworkElement.Loaded += FrameworkElement_Loaded;
			}
			ApplyCornerRadiusToElevation(cornerRadius, frameworkElement);
		}
	}

	private static void FrameworkElement_Loaded(object sender, RoutedEventArgs e)
	{
		if (sender is FrameworkElement frameworkElement)
		{
			frameworkElement.Loaded -= FrameworkElement_Loaded;
			ApplyCornerRadiusToElevation(GetCornerRadius(frameworkElement), frameworkElement);
		}
	}

	private static void ApplyCornerRadiusToElevation(double cornerRadius, FrameworkElement frameworkElement)
	{
		if (Effects.GetShadow(frameworkElement) is AttachedCardShadow attachedCardShadow)
		{
			attachedCardShadow.CornerRadius = cornerRadius;
		}
	}
}
