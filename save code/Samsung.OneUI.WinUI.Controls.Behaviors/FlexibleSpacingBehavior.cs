using Microsoft.UI.Dispatching;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.Xaml.Interactivity;
using Samsung.OneUI.WinUI.AttachedProperties;
using Samsung.OneUI.WinUI.AttachedProperties.Enums;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Behaviors;

public class FlexibleSpacingBehavior : Behavior<FrameworkElement>
{
	public const string BREAKPOINT_SMALL_KEY = "OneUIBreakpointSmall";

	public const string BREAKPOINT_MEDIUM_KEY = "OneUIBreakpointMedium";

	public const string BREAKPOINT_LARGE_KEY = "OneUIBreakpointLarge";

	public const string BREAKPOINT_HUGE_KEY = "OneUIBreakpointHuge";

	private AppWindow _appWindow;

	private readonly double _breakpointSmall = (double)"OneUIBreakpointSmall".GetKey();

	private readonly double _breakpointMedium = (double)"OneUIBreakpointMedium".GetKey();

	private readonly double _breakpointLarge = (double)"OneUIBreakpointLarge".GetKey();

	private readonly double _breakpointHuge = (double)"OneUIBreakpointHuge".GetKey();

	public static readonly DependencyProperty IsFlexibleSpacingProperty = DependencyProperty.Register("IsFlexibleSpacing", typeof(bool), typeof(FlexibleSpacingBehavior), new PropertyMetadata(false, OnPropertyChanged));

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(FlexibleSpacingType), typeof(FlexibleSpacingBehavior), new PropertyMetadata(FlexibleSpacingType.Wide, OnPropertyChanged));

	public static readonly DependencyProperty MarginTinyProperty = DependencyProperty.Register("MarginTiny", typeof(Thickness), typeof(FlexibleSpacingBehavior), new PropertyMetadata(new Thickness(12.0, 0.0, 12.0, 0.0), OnPropertyChanged));

	public static readonly DependencyProperty MarginSmallProperty = DependencyProperty.Register("MarginSmall", typeof(Thickness), typeof(FlexibleSpacingBehavior), new PropertyMetadata(new Thickness(24.0, 0.0, 24.0, 0.0), OnPropertyChanged));

	public static readonly DependencyProperty MarginMediumProperty = DependencyProperty.Register("MarginMedium", typeof(Thickness), typeof(FlexibleSpacingBehavior), new PropertyMetadata(new Thickness(24.0, 0.0, 24.0, 0.0), OnPropertyChanged));

	public static readonly DependencyProperty MarginLargeProperty = DependencyProperty.Register("MarginLarge", typeof(Thickness), typeof(FlexibleSpacingBehavior), new PropertyMetadata(new Thickness(288.0, 0.0, 288.0, 0.0), OnPropertyChanged));

	public static readonly DependencyProperty MarginHugeProperty = DependencyProperty.Register("MarginHuge", typeof(Thickness), typeof(FlexibleSpacingBehavior), new PropertyMetadata(new Thickness(288.0, 0.0, 288.0, 0.0), OnPropertyChanged));

	public static readonly DependencyProperty MarginOffProperty = DependencyProperty.Register("MarginOff", typeof(Thickness), typeof(FlexibleSpacingBehavior), new PropertyMetadata(new Thickness(0.0), OnPropertyChanged));

	public static readonly DependencyProperty FlexibleSpacingTargetContentProperty = DependencyProperty.Register("FlexibleSpacingTargetContent", typeof(FrameworkElement), typeof(FlexibleSpacingBehavior), new PropertyMetadata(null, OnPropertyChanged));

	public bool IsFlexibleSpacing
	{
		get
		{
			return (bool)GetValue(IsFlexibleSpacingProperty);
		}
		set
		{
			SetValue(IsFlexibleSpacingProperty, value);
		}
	}

	public FlexibleSpacingType Type
	{
		get
		{
			return (FlexibleSpacingType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public Thickness MarginTiny
	{
		get
		{
			return (Thickness)GetValue(MarginTinyProperty);
		}
		set
		{
			SetValue(MarginTinyProperty, value);
		}
	}

	public Thickness MarginSmall
	{
		get
		{
			return (Thickness)GetValue(MarginSmallProperty);
		}
		set
		{
			SetValue(MarginSmallProperty, value);
		}
	}

	public Thickness MarginMedium
	{
		get
		{
			return (Thickness)GetValue(MarginMediumProperty);
		}
		set
		{
			SetValue(MarginMediumProperty, value);
		}
	}

	public Thickness MarginLarge
	{
		get
		{
			return (Thickness)GetValue(MarginLargeProperty);
		}
		set
		{
			SetValue(MarginLargeProperty, value);
		}
	}

	public Thickness MarginHuge
	{
		get
		{
			return (Thickness)GetValue(MarginHugeProperty);
		}
		set
		{
			SetValue(MarginHugeProperty, value);
		}
	}

	public Thickness MarginOff
	{
		get
		{
			return (Thickness)GetValue(MarginOffProperty);
		}
		set
		{
			SetValue(MarginOffProperty, value);
		}
	}

	public FrameworkElement FlexibleSpacingTargetContent
	{
		get
		{
			return (FrameworkElement)GetValue(FlexibleSpacingTargetContentProperty);
		}
		set
		{
			SetValue(FlexibleSpacingTargetContentProperty, value);
		}
	}

	public static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
	{
		if (d is FlexibleSpacingBehavior flexibleSpacingBehavior)
		{
			flexibleSpacingBehavior.ApplyFlexibleSpacing();
		}
	}

	protected override void OnAttached()
	{
		base.OnAttached();
		if (!(base.AssociatedObject == null))
		{
			base.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, delegate
			{
				ConfigureWindowWithFlexibleSpacing();
			});
			base.AssociatedObject.Unloaded += AssociatedObject_Unloaded;
		}
	}

	protected override void OnDetaching()
	{
		base.OnDetaching();
		if (base.AssociatedObject != null)
		{
			base.AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
		}
	}

	private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
	{
		if (_appWindow != null)
		{
			_appWindow.Changed -= AppWindow_Changed;
		}
	}

	private void AppWindow_Changed(AppWindow sender, AppWindowChangedEventArgs args)
	{
		if (args.DidSizeChange)
		{
			ApplyFlexibleSpacing();
		}
	}

	private void ConfigureWindowWithFlexibleSpacing()
	{
		CreateAppWindow();
		ApplyFlexibleSpacing();
	}

	private void CreateAppWindow()
	{
		if (!(base.AssociatedObject == null) && !(base.AssociatedObject.XamlRoot == null) && !(base.AssociatedObject.XamlRoot.ContentIslandEnvironment == null))
		{
			_appWindow = AppWindow.GetFromWindowId(base.AssociatedObject.XamlRoot.ContentIslandEnvironment.AppWindowId);
			_appWindow.Changed += AppWindow_Changed;
		}
	}

	private double GetEffectiveScreenWidth()
	{
		int num = ((_appWindow != null) ? _appWindow.ClientSize.Width : 0);
		if (base.AssociatedObject == null || base.AssociatedObject.XamlRoot == null)
		{
			return num;
		}
		return (double)num / base.AssociatedObject.XamlRoot.RasterizationScale;
	}

	private void ApplyFlexibleSpacing()
	{
		if (FlexibleSpacingTargetContent == null || _appWindow == null)
		{
			return;
		}
		if (IsFlexibleSpacing)
		{
			double effectiveScreenWidth = GetEffectiveScreenWidth();
			Thickness marginFromWindowWidth = GetMarginFromWindowWidth(effectiveScreenWidth);
			if (ScrollViewer.GetMaskingRoundElementReference(FlexibleSpacingTargetContent) == null)
			{
				UpdateMaxWidthBasedOnMargin(marginFromWindowWidth);
			}
			SetTargetContentMargin(marginFromWindowWidth);
		}
		else
		{
			SetTargetContentMargin(MarginOff);
		}
	}

	private void SetTargetContentMargin(Thickness margin)
	{
		FlexibleSpacingTargetContent.Margin = margin;
	}

	private Thickness GetMarginFromWindowWidth(double windowWidth)
	{
		if (Type == FlexibleSpacingType.Wide)
		{
			if (windowWidth >= _breakpointHuge)
			{
				return MarginHuge;
			}
			if (windowWidth >= _breakpointLarge)
			{
				return MarginLarge;
			}
			if (windowWidth >= _breakpointMedium)
			{
				return MarginMedium;
			}
		}
		if (windowWidth >= _breakpointSmall)
		{
			return MarginSmall;
		}
		return MarginTiny;
	}

	private void UpdateMaxWidthBasedOnMargin(Thickness margin)
	{
		FlexibleSpacingTargetContent.MaxWidth = double.PositiveInfinity;
		if (Type == FlexibleSpacingType.Wide)
		{
			if (margin == MarginHuge || margin == MarginLarge)
			{
				FlexibleSpacingTargetContent.MaxWidth = 2016.0;
			}
			else
			{
				FlexibleSpacingTargetContent.MaxWidth = 1344.0;
			}
		}
	}
}
