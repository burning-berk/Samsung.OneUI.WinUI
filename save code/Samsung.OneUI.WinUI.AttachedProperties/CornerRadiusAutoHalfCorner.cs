using System;
using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Utils.Extensions;

namespace Samsung.OneUI.WinUI.AttachedProperties;

public class CornerRadiusAutoHalfCorner
{
	public static readonly DependencyProperty CornerPointProperty = DependencyProperty.RegisterAttached("CornerPoint", typeof(string), typeof(CornerRadiusAutoHalfCorner), new PropertyMetadata(string.Empty, OnCornerPointChanged));

	public static readonly DependencyProperty CanOverrideProperty = DependencyProperty.RegisterAttached("CanOverride", typeof(bool), typeof(CornerRadiusAutoHalfCorner), new PropertyMetadata(false, OnCanOverrideChanged));

	public static void SetCornerPoint(DependencyObject element, string value)
	{
		element.SetValue(CornerPointProperty, value);
	}

	public static string GetCornerPoint(DependencyObject element)
	{
		return (string)element.GetValue(CornerPointProperty);
	}

	private static void OnCornerPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FrameworkElement frameworkElement)
		{
			frameworkElement.Loaded -= Element_Loaded;
			frameworkElement.Loaded += Element_Loaded;
			frameworkElement.SizeChanged -= ElementSizeChanged;
			frameworkElement.SizeChanged += ElementSizeChanged;
		}
	}

	private static void OnCanOverrideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FrameworkElement dp)
		{
			UpdateCornerRadius(dp);
		}
	}

	public static void SetCanOverride(DependencyObject element, bool value)
	{
		element.SetValue(CanOverrideProperty, value);
	}

	public static bool GetCanOverride(DependencyObject element)
	{
		return (bool)element.GetValue(CanOverrideProperty);
	}

	private static void Element_Loaded(object sender, RoutedEventArgs e)
	{
		if (sender is FrameworkElement dp)
		{
			UpdateCornerRadius(dp);
		}
	}

	private static void ElementSizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (sender is FrameworkElement dp)
		{
			UpdateCornerRadius(dp);
		}
	}

	private static void UpdateCornerRadius(dynamic dp)
	{
		if (GetCanOverride(dp as DependencyObject))
		{
			dynamic val = dp.CornerRadius;
			if (val.TopLeft != 0 || val.TopRight != 0 || val.BottomLeft != 0 || val.BottomRight != 0)
			{
				return;
			}
		}
		string cornerPoint = GetCornerPoint(dp as DependencyObject);
		double num = ResolveDimension(dp.ActualWidth, dp.Width);
		double num2 = ResolveDimension(dp.ActualHeight, dp.Height);
		if (num2 != 0.0 || num != 0.0)
		{
			double uniformRadius = Math.Min(num2, num) / 2.0;
			dp.CornerRadius = new CornerRadius(uniformRadius).SetCorners(cornerPoint);
		}
		static double ResolveDimension(double value, double fallback)
		{
			if (!double.IsNaN(value))
			{
				return value;
			}
			if (!double.IsNaN(fallback))
			{
				return fallback;
			}
			return 0.0;
		}
	}
}
