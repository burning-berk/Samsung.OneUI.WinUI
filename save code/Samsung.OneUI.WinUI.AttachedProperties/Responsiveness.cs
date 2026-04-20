using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.AttachedProperties.Enums;

namespace Samsung.OneUI.WinUI.AttachedProperties;

public class Responsiveness
{
	public static readonly DependencyProperty IsFlexibleSpacingProperty = DependencyProperty.RegisterAttached("IsFlexibleSpacing", typeof(bool?), typeof(Responsiveness), new PropertyMetadata(null));

	public static readonly DependencyProperty FlexibleSpacingTypeProperty = DependencyProperty.RegisterAttached("FlexibleSpacingType", typeof(FlexibleSpacingType), typeof(Responsiveness), new PropertyMetadata(FlexibleSpacingType.Wide));

	public static void SetIsFlexibleSpacing(UIElement element, bool? value)
	{
		element.SetValue(IsFlexibleSpacingProperty, value);
	}

	public static bool? GetIsFlexibleSpacing(UIElement element)
	{
		return (bool?)element.GetValue(IsFlexibleSpacingProperty);
	}

	public static void SetFlexibleSpacingType(UIElement element, FlexibleSpacingType value)
	{
		element.SetValue(FlexibleSpacingTypeProperty, value);
	}

	public static FlexibleSpacingType GetFlexibleSpacingType(UIElement element)
	{
		return (FlexibleSpacingType)element.GetValue(FlexibleSpacingTypeProperty);
	}
}
