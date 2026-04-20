using System.Numerics;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Hosting;

namespace Samsung.OneUI.WinUI.AttachedProperties;

public class BackdropBlurExtension
{
	private const string GAUSSIAN_BLUR_EFFECT_NAME = "Blur";

	private const string GAUSSIAN_BLUR_EFFECT_SOURCE_NAME = "source";

	public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(BackdropBlurExtension), new PropertyMetadata(false, OnIsEnabledChanged));

	public static readonly DependencyProperty BlurAmountProperty = DependencyProperty.RegisterAttached("BlurAmount", typeof(double), typeof(BackdropBlurExtension), new PropertyMetadata(12.0, OnBlurAmountChanged));

	public static bool GetIsEnabled(DependencyObject obj)
	{
		return (bool)obj.GetValue(IsEnabledProperty);
	}

	public static void SetIsEnabled(DependencyObject obj, bool value)
	{
		obj.SetValue(IsEnabledProperty, value);
	}

	public static double GetBlurAmount(DependencyObject obj)
	{
		return (double)obj.GetValue(BlurAmountProperty);
	}

	public static void SetBlurAmount(DependencyObject obj, double value)
	{
		obj.SetValue(BlurAmountProperty, value);
	}

	private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FrameworkElement element)
		{
			UnRegisterEvents(element);
			if ((bool)e.NewValue)
			{
				SetupBlur(element);
				RegisterEvents(element);
			}
			else
			{
				ClearBlur(element);
			}
		}
	}

	private static void OnBlurAmountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FrameworkElement frameworkElement && GetIsEnabled(frameworkElement))
		{
			ClearBlur(frameworkElement);
			SetupBlur(frameworkElement);
		}
	}

	private static void Element_Loaded(object sender, RoutedEventArgs e)
	{
		SetupBlur((FrameworkElement)sender);
	}

	private static void Element_Unloaded(object sender, RoutedEventArgs e)
	{
		ClearBlur((FrameworkElement)sender);
	}

	private static void Element_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		FrameworkElement frameworkElement = (FrameworkElement)sender;
		if (ElementCompositionPreview.GetElementChildVisual(frameworkElement) is SpriteVisual spriteVisual)
		{
			spriteVisual.Size = new Vector2((float)frameworkElement.ActualWidth, (float)frameworkElement.ActualHeight);
		}
	}

	private static void Element_ActualThemeChanged(FrameworkElement sender, object args)
	{
		if (GetIsEnabled(sender))
		{
			ClearBlur(sender);
			SetupBlur(sender);
		}
	}

	private static void SetupBlur(FrameworkElement element)
	{
		if (!(element.ActualWidth <= 0.0) && !(element.ActualHeight <= 0.0))
		{
			Compositor compositor = ElementCompositionPreview.GetElementVisual(element).Compositor;
			GaussianBlurEffect graphicsEffect = new GaussianBlurEffect
			{
				Name = "Blur",
				BlurAmount = (float)GetBlurAmount(element),
				BorderMode = EffectBorderMode.Hard,
				Optimization = EffectOptimization.Balanced,
				Source = new CompositionEffectSourceParameter("source")
			};
			CompositionEffectBrush compositionEffectBrush = compositor.CreateEffectFactory(graphicsEffect).CreateBrush();
			compositionEffectBrush.SetSourceParameter("source", compositor.CreateBackdropBrush());
			SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
			spriteVisual.Brush = compositionEffectBrush;
			spriteVisual.Size = new Vector2((float)element.ActualWidth, (float)element.ActualHeight);
			ElementCompositionPreview.SetElementChildVisual(element, spriteVisual);
		}
	}

	private static void ClearBlur(FrameworkElement element)
	{
		ElementCompositionPreview.SetElementChildVisual(element, null);
	}

	private static void UnRegisterEvents(FrameworkElement element)
	{
		element.Loaded -= Element_Loaded;
		element.Unloaded -= Element_Unloaded;
		element.SizeChanged -= Element_SizeChanged;
		element.ActualThemeChanged -= Element_ActualThemeChanged;
	}

	private static void RegisterEvents(FrameworkElement element)
	{
		element.Loaded += Element_Loaded;
		element.Unloaded += Element_Unloaded;
		element.SizeChanged += Element_SizeChanged;
		element.ActualThemeChanged += Element_ActualThemeChanged;
	}
}
