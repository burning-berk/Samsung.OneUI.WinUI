using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI;
using Microsoft.UI.System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Controls;

public class ProgressCircleIndeterminate : ProgressCircle
{
	private const string PART_ROOT_GRID_NAME = "PART_RootGrid";

	private const string PART_TEXT_NAME = "PART_text";

	private const string PART_STORYBOARD_NAME = "RotateAnimation";

	private const string PART_ELLIPSEPOINT = "PART_EllipsePoint";

	private const string PART_ELLIPSE01 = "PART_Ellipse01";

	private const string PART_ELLIPSE03 = "PART_Ellipse02";

	private const string PART_ELLIPSE04 = "PART_Ellipse03";

	private const string ELLIPSE_INDETERMINATE_KEY = "OneUIProgressCircleIndeterminateEllipse";

	private const string VARIANT_ELLIPSE_INDETERMINATE_KEY = "OneUIProgressCircleIndeterminateVariantEllipse";

	private const double ELLIPSE_BASE_SIZE = 4.5;

	private const double ELLIPE_BASE_MIN_OFFSET = 1.5;

	private const double ELLIPE_BASE_MAX_OFFSET = 6.5;

	private const double ELLIPE_BASE_DISPLACEMENT = 5.0;

	private const double ELLIPE_BASE_DISPLACEMENT_REVERSE = -4.0;

	private const string TEXT_VERTICAL_ALINGMENT_STATE = "TextVerticalAlignment";

	private const string TEXT_HORIZONTAL_ALINGMENT_STATE = "TextHorizontalAlignment";

	private const string PART_TEXT_FONTSIZE_MS = "OneUISizeMS";

	private const string PART_TEXT_FONTSIZE_SM = "OneUISizeSM";

	private const string PART_TEXT_FONTSIZE_XS = "OneUISizeXS";

	private const int GRIDSIZE_XL = 90;

	private const int GRIDSIZE_LG = 60;

	private const int GRIDSIZE_MD = 48;

	private const int GRIDSIZE_SM = 24;

	private const int GRIDSIZE_ST = 16;

	private Grid _rootGrid;

	private TextBlock _text;

	private Storyboard _rotateAnimation;

	private Ellipse _ellipsePoint;

	private Ellipse _ellipse01;

	private Ellipse _ellipse02;

	private Ellipse _ellipse03;

	private Brush _elipseIndeterminateBrushDefault = (SolidColorBrush)"OneUIProgressCircleIndeterminateEllipse".GetKey();

	private Brush _variantElipseIndeterminateBrushDefault = (SolidColorBrush)"OneUIProgressCircleIndeterminateVariantEllipse".GetKey();

	private ThemeSettings _themeSettings;

	private long _visibilityPropertyRegisterToken;

	private readonly List<ProgressCircleIndeterminateModel> _progressCircleIndeterminateModels = new List<ProgressCircleIndeterminateModel>
	{
		new ProgressCircleIndeterminateModel
		{
			Size = ProgressCircleSize.XLarge,
			Orientation = ProgressCircleIndeterminateOrientation.Vertical,
			Scale = 3.75,
			GridSize = 90.0
		},
		new ProgressCircleIndeterminateModel
		{
			Size = ProgressCircleSize.Large,
			Orientation = ProgressCircleIndeterminateOrientation.Vertical,
			Scale = 2.5,
			GridSize = 60.0
		},
		new ProgressCircleIndeterminateModel
		{
			Size = ProgressCircleSize.Medium,
			Orientation = ProgressCircleIndeterminateOrientation.Vertical,
			Scale = 2.0,
			GridSize = 48.0
		},
		new ProgressCircleIndeterminateModel
		{
			Size = ProgressCircleSize.Small,
			Orientation = ProgressCircleIndeterminateOrientation.Horizontal,
			Scale = 1.0,
			GridSize = 24.0
		},
		new ProgressCircleIndeterminateModel
		{
			Size = ProgressCircleSize.SmallTitle,
			Orientation = ProgressCircleIndeterminateOrientation.Horizontal,
			Scale = 0.67,
			GridSize = 16.0
		}
	};

	public new static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(ProgressCircleIndeterminate), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), OnForegroundPropertyChanged));

	public static readonly DependencyProperty PointForegroundProperty = DependencyProperty.Register("PointForeground", typeof(Brush), typeof(ProgressCircleIndeterminate), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), OnPointForegroundPropertyChanged));

	internal static readonly DependencyProperty EllipseDiameterProperty = DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(ProgressCircleIndeterminate), new PropertyMetadata(0.0));

	internal static readonly DependencyProperty EllipseDisplacementPositionProperty = DependencyProperty.Register("EllipseDisplacementPosition", typeof(double), typeof(ProgressCircleIndeterminate), new PropertyMetadata(0.0));

	internal static readonly DependencyProperty EllipseNegativeDisplacementProperty = DependencyProperty.Register("EllipseNegativeDisplacement", typeof(double), typeof(ProgressCircleIndeterminate), new PropertyMetadata(0.0));

	internal static readonly DependencyProperty EllipseMinOffsetProperty = DependencyProperty.Register("EllipseMinOffset", typeof(double), typeof(ProgressCircleIndeterminate), new PropertyMetadata(0.0));

	internal static readonly DependencyProperty EllipseMaxOffsetProperty = DependencyProperty.Register("EllipseMaxOffset", typeof(double), typeof(ProgressCircleIndeterminate), new PropertyMetadata(0.0));

	public new Brush Foreground
	{
		get
		{
			return (Brush)GetValue(ForegroundProperty);
		}
		set
		{
			SetValue(ForegroundProperty, value);
		}
	}

	public Brush PointForeground
	{
		get
		{
			return (Brush)GetValue(PointForegroundProperty);
		}
		set
		{
			SetValue(PointForegroundProperty, value);
		}
	}

	internal double EllipseDiameter
	{
		get
		{
			return (double)GetValue(EllipseDiameterProperty);
		}
		set
		{
			SetValue(EllipseDiameterProperty, value);
		}
	}

	internal double EllipseDisplacementPosition
	{
		get
		{
			return (double)GetValue(EllipseDisplacementPositionProperty);
		}
		set
		{
			SetValue(EllipseDisplacementPositionProperty, value);
		}
	}

	internal double EllipseNegativeDisplacement
	{
		get
		{
			return (double)GetValue(EllipseNegativeDisplacementProperty);
		}
		set
		{
			SetValue(EllipseNegativeDisplacementProperty, value);
		}
	}

	internal double EllipseMinOffset
	{
		get
		{
			return (double)GetValue(EllipseMinOffsetProperty);
		}
		set
		{
			SetValue(EllipseMinOffsetProperty, value);
		}
	}

	internal double EllipseMaxOffset
	{
		get
		{
			return (double)GetValue(EllipseMaxOffsetProperty);
		}
		set
		{
			SetValue(EllipseMaxOffsetProperty, value);
		}
	}

	public ProgressCircleIndeterminate()
	{
		base.DefaultStyleKey = typeof(ProgressCircleIndeterminate);
		base.Loaded += ProgressCircleIndeterminate_Loaded;
		base.Unloaded += ProgressCircleIndeterminate_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_rootGrid = (Grid)GetTemplateChild("PART_RootGrid");
		_text = (TextBlock)GetTemplateChild("PART_text");
		_ellipsePoint = (Ellipse)GetTemplateChild("PART_EllipsePoint");
		_ellipse01 = (Ellipse)GetTemplateChild("PART_Ellipse01");
		_ellipse02 = (Ellipse)GetTemplateChild("PART_Ellipse02");
		_ellipse03 = (Ellipse)GetTemplateChild("PART_Ellipse03");
		TrySetInitialForegroundColors();
		RegisterPropertyChangedCallback(UIElement.VisibilityProperty, OnVisibilityPropertyChanged);
		UpdateProgressCircleLayout();
		UpdateCircleScale();
		_rotateAnimation = (Storyboard)GetTemplateChild("RotateAnimation");
		BeginAnimation();
	}

	protected override void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		UpdateCircleScale();
		UpdateProgressCircleLayout();
	}

	private void ProgressCircleIndeterminate_Loaded(object sender, RoutedEventArgs e)
	{
		if (_themeSettings == null && base.XamlRoot != null && base.XamlRoot.ContentIslandEnvironment != null)
		{
			Microsoft.UI.WindowId appWindowId = base.XamlRoot.ContentIslandEnvironment.AppWindowId;
			_themeSettings = ThemeSettings.CreateForWindowId(appWindowId);
			if (_themeSettings != null)
			{
				_themeSettings.Changed += ThemeSettings_Changed;
			}
		}
		BeginAnimation();
		_visibilityPropertyRegisterToken = RegisterPropertyChangedCallback(UIElement.VisibilityProperty, OnVisibilityPropertyChanged);
	}

	private void ProgressCircleIndeterminate_Unloaded(object sender, RoutedEventArgs e)
	{
		UnregisterPropertyChangedCallback(UIElement.VisibilityProperty, _visibilityPropertyRegisterToken);
		_rotateAnimation?.Stop();
	}

	private void ThemeSettings_Changed(ThemeSettings sender, object args)
	{
		UpdateEllipseBrush(Foreground, "OneUIProgressCircleIndeterminateEllipse", UpdateEllipsePointColor);
		UpdateEllipseBrush(PointForeground, "OneUIProgressCircleIndeterminateVariantEllipse", UpdateVariantEllipsePointColor);
	}

	private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ProgressCircleIndeterminate progressCircleIndeterminate)
		{
			progressCircleIndeterminate._elipseIndeterminateBrushDefault = (Brush)e.NewValue;
			progressCircleIndeterminate.UpdateEllipsePointColor(progressCircleIndeterminate._elipseIndeterminateBrushDefault);
		}
	}

	private static void OnPointForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ProgressCircleIndeterminate progressCircleIndeterminate)
		{
			progressCircleIndeterminate._variantElipseIndeterminateBrushDefault = (Brush)e.NewValue;
			progressCircleIndeterminate.UpdateVariantEllipsePointColor(progressCircleIndeterminate._variantElipseIndeterminateBrushDefault);
		}
	}

	private static void OnVisibilityPropertyChanged(DependencyObject d, DependencyProperty dp)
	{
		if (d is ProgressCircleIndeterminate progressCircleIndeterminate)
		{
			if (progressCircleIndeterminate.Visibility == Visibility.Collapsed)
			{
				progressCircleIndeterminate._rotateAnimation?.Stop();
			}
			else
			{
				progressCircleIndeterminate._rotateAnimation?.Begin();
			}
		}
	}

	private void DoVerticalTextAlignment()
	{
		VisualStateManager.GoToState(this, "TextVerticalAlignment", useTransitions: true);
	}

	private void DoHorizontalTextAlignment()
	{
		VisualStateManager.GoToState(this, "TextHorizontalAlignment", useTransitions: true);
	}

	private void UpdateCircleScale()
	{
		ProgressCircleIndeterminateModel progressCircleIndeterminateModel = _progressCircleIndeterminateModels.FirstOrDefault((ProgressCircleIndeterminateModel x) => x.Size == base.Size);
		if (progressCircleIndeterminateModel != null)
		{
			SetTextAlignment(progressCircleIndeterminateModel.Orientation);
			EllipseDiameter = 4.5 * progressCircleIndeterminateModel.Scale;
			EllipseMinOffset = 1.5 * progressCircleIndeterminateModel.Scale;
			EllipseMaxOffset = 6.5 * progressCircleIndeterminateModel.Scale;
			EllipseDisplacementPosition = 5.0 * progressCircleIndeterminateModel.Scale;
			EllipseNegativeDisplacement = -4.0 * progressCircleIndeterminateModel.Scale;
		}
	}

	private void SetTextAlignment(ProgressCircleIndeterminateOrientation orientation)
	{
		if (_progressCircleTextAlingmentDictionary.TryGetValue(orientation, out var value))
		{
			VisualStateManager.GoToState(this, value, useTransitions: true);
		}
	}

	private void UpdateProgressCircleLayout()
	{
		ProgressCircleIndeterminateModel progressCircleIndeterminateModel = _progressCircleIndeterminateModels.FirstOrDefault((ProgressCircleIndeterminateModel x) => x.Size == base.Size);
		if (progressCircleIndeterminateModel != null)
		{
			UpdateRootGridSize(progressCircleIndeterminateModel);
			UpdateFontSizeMessage(progressCircleIndeterminateModel);
		}
	}

	private void UpdateRootGridSize(ProgressCircleIndeterminateModel progressDefinition)
	{
		if (!(_rootGrid == null))
		{
			_rootGrid.Width = progressDefinition.GridSize;
			_rootGrid.Height = progressDefinition.GridSize;
		}
	}

	private void UpdateFontSizeMessage(ProgressCircleIndeterminateModel progressDefinition)
	{
		if (!(_text == null))
		{
			double gridSize = progressDefinition.GridSize;
			double num = ((gridSize == 90.0) ? ((double)"OneUISizeMS".GetKey()) : ((gridSize == 60.0) ? ((double)"OneUISizeMS".GetKey()) : ((gridSize == 48.0) ? ((double)"OneUISizeSM".GetKey()) : ((gridSize == 24.0) ? ((double)"OneUISizeXS".GetKey()) : ((gridSize != 16.0) ? ((double)"OneUISizeMS".GetKey()) : ((double)"OneUISizeXS".GetKey()))))));
			double fontSize = num;
			_text.FontSize = fontSize;
		}
	}

	private void BeginAnimation()
	{
		_rotateAnimation?.Begin();
	}

	private void TrySetInitialForegroundColors()
	{
		if (!IsSameSolidColorBrush(_variantElipseIndeterminateBrushDefault, new SolidColorBrush(Colors.Transparent)))
		{
			UpdateVariantEllipsePointColor(_variantElipseIndeterminateBrushDefault);
		}
		if (!IsSameSolidColorBrush(_elipseIndeterminateBrushDefault, new SolidColorBrush(Colors.Transparent)))
		{
			UpdateEllipsePointColor(_elipseIndeterminateBrushDefault);
		}
	}

	private void UpdateEllipsePointColor(Brush colorBrush)
	{
		if (_ellipse01 != null && _ellipse02 != null && _ellipse03 != null)
		{
			Ellipse ellipse = _ellipse01;
			Ellipse ellipse2 = _ellipse02;
			Brush brush = (_ellipse03.Fill = colorBrush);
			Brush fill = (ellipse2.Fill = brush);
			ellipse.Fill = fill;
		}
	}

	private void UpdateVariantEllipsePointColor(Brush colorBrush)
	{
		if (_ellipsePoint != null)
		{
			_ellipsePoint.Fill = colorBrush;
		}
	}

	private bool IsSameSolidColorBrush(Brush brush1, Brush brush2)
	{
		if (brush1 is SolidColorBrush solidColorBrush && brush2 is SolidColorBrush solidColorBrush2)
		{
			return solidColorBrush.Color == solidColorBrush2.Color;
		}
		return false;
	}

	private void UpdateEllipseBrush(Brush brush, string resourceKey, Action<Brush> updateAction)
	{
		Color color = ((SolidColorBrush)brush).Color;
		if (Colors.Transparent.Equals(color))
		{
			Brush obj = (Brush)resourceKey.GetKey();
			updateAction(obj);
		}
	}
}
