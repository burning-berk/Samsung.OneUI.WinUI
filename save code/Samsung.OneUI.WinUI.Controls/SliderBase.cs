using System;
using System.ComponentModel;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.Commom.Interfaces;
using Samsung.OneUI.WinUI.Controls.Inputs.Slider.Base;
using Samsung.OneUI.WinUI.Controls.Selectors;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Controls;

public abstract class SliderBase : Microsoft.UI.Xaml.Controls.Slider, IDialogComponentNegativeOutStroke
{
	private const string STATE_POINTER_NORMAL = "NormalSlider";

	private const string STATE_POINTER_OVER = "PointerOverSlider";

	private const string STATE_POINTER_PRESSED = "PressedSlider";

	private const string STATE_POINTER_DRAGGING = "DraggingSlider";

	private const string SHOCK_AREA_PART_OF_STATE_NAMES = "ShockArea";

	protected const int DEFAULT_SHOCK_AREA = int.MinValue;

	private const string STATE_BEHAVIOR_SHOCK_AREA_SLIDER = "SliderBehaviorState";

	private const string PART_HORIZONTAL_TRACKER_BORDER = "HorizontalTrackBorder";

	private const string PART_HORIZONTAL_DECREASE_BORDER = "HorizontalDecreaseBorder";

	private const string PART_HORIZONTAL_DECREASE_OVERLAY_BORDER = "HorizontalDecreaseOverlayBorder";

	private const string PART_HORIZONTAL_DECREASE_BORDER_BUFFER = "HorizontalDecreaseBorderBuffer";

	private const string PART_VERTICAL_TRACKER_BORDER = "VerticalTrackBorder";

	private const string PART_VERTICAL_DECREASE_BORDER = "VerticalDecreaseBorder";

	private const string PART_VERTICAL_DECREASE_OVERLAY_BORDER = "VerticalDecreaseOverlayBorder";

	private const string PART_VERTICAL_DECREASE_BORDER_BUFFER = "VerticalDecreaseBorderBuffer";

	protected const string HORIZONTAL_DECREASE_RECT = "HorizontalDecreaseRect";

	protected const string VERTICAL_DECREASE_RECT = "VerticalDecreaseRect";

	private const string HEIGHT_PROPERTY = "Height";

	private const string WIDTH_PROPERTY = "Width";

	private const string CORNER_RADIUS_PROPERTY = "(Border.CornerRadius)";

	private const double DURATION_350_MS = 0.35;

	private const double DURATION_50_MS = 0.05;

	private const double PRESSED_CORNER_RADIUS_SLIDER_TYPE1 = 9.0;

	private const double PRESSED_CORNER_RADIUS_SLIDER_TYPE2 = 6.0;

	private const double RELEASED_CORNER_RADIUS_SLIDER_TYPE1 = 6.0;

	private const double RELEASED_CORNER_RADIUS_SLIDER_TYPE2 = 2.0;

	private const double PRESSED_SLIDER_ANIMATION_PATH_INTERPOLATOR_CONTROL_X1 = 0.22;

	private const double PRESSED_SLIDER_ANIMATION_PATH_INTERPOLATOR_CONTROL_Y1 = 0.25;

	private const double PRESSED_SLIDER_ANIMATION_PATH_INTERPOLATOR_CONTROL_X2 = 0.0;

	private const double PRESSED_SLIDER_ANIMATION_PATH_INTERPOLATOR_CONTROL_Y2 = 1.0;

	private const double RELEASED_SLIDER_ANIMATION_PATH_INTERPOLATOR_CONTROL_X1 = 0.22;

	private const double RELEASED_SLIDER_ANIMATION_PATH_INTERPOLATOR_CONTROL_Y1 = 0.22;

	private const double RELEASED_SLIDER_ANIMATION_PATH_INTERPOLATOR_CONTROL_X2 = 0.0;

	private const double RELEASED_SLIDER_ANIMATION_PATH_INTERPOLATOR_CONTROL_Y2 = 1.0;

	protected const double THUMB_SIZE_TYPE1 = 18.0;

	protected const double THUMB_SIZE_TYPE2 = 12.0;

	protected const double THUMB_SIZE_GHOST_TYPE = 12.0;

	private const string INITIAL_STATIC_VALUE = "0";

	private const string FOCUS_MARGIN_OUT_STROKE_RESOURCE_KEY = "OneUIShockAreaOutStrokeMargin";

	protected bool isShockArea;

	protected ISliderStyleSelector sliderStyleSelector;

	private SliderPointerStates pointerState;

	private bool animationStartMoved;

	private bool isPointerOver;

	protected Rectangle _horizontalDecreaseRect;

	protected double _horizontalDecreaseRectMaxWidth;

	protected double _horizontalDecreaseRectMinWidth;

	protected Rectangle _verticalDecreaseRect;

	protected double _verticalDecreaseRectMaxHeight;

	protected double _verticalDecreaseRectMinHeight;

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(SliderType), typeof(Slider), new PropertyMetadata(SliderType.Type1, OnTypePropertyChanged));

	internal static readonly DependencyProperty TextValueProperty = DependencyProperty.Register("TextValue", typeof(string), typeof(SliderBase), new PropertyMetadata("0"));

	public static readonly DependencyProperty TextValueVisibilityProperty = DependencyProperty.Register("TextValueVisibility", typeof(Visibility), typeof(SliderBase), new PropertyMetadata(Visibility.Collapsed, OnTextValueVisibilityChanged));

	private SliderPointerStates SliderPointerState
	{
		get
		{
			return pointerState;
		}
		set
		{
			pointerState = value;
			UpdatePointerState();
		}
	}

	internal bool IsPointerPressed { get; private set; }

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool IsThumbToolTipEnabled
	{
		get
		{
			return base.IsThumbToolTipEnabled;
		}
		set
		{
			base.IsThumbToolTipEnabled = false;
		}
	}

	public SliderType Type
	{
		get
		{
			return (SliderType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	internal string TextValue
	{
		get
		{
			return (string)GetValue(TextValueProperty);
		}
		set
		{
			SetValue(TextValueProperty, value);
		}
	}

	public Visibility TextValueVisibility
	{
		get
		{
			return (Visibility)GetValue(TextValueVisibilityProperty);
		}
		set
		{
			SetValue(TextValueVisibilityProperty, value);
		}
	}

	private static void OnTextValueVisibilityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is SliderBase sliderBase)
		{
			sliderBase.UpdateFixedTooltipTextValue();
		}
	}

	protected SliderBase()
	{
		Init();
		ApplyStyleBySelector();
	}

	protected SliderBase(bool applyStyleUsingSelectorStyles)
	{
		Init();
		if (applyStyleUsingSelectorStyles)
		{
			ApplyStyleBySelector();
		}
	}

	protected abstract void InitEvents();

	protected abstract void SetDefaultStyleKey();

	public abstract void RefreshLayout();

	private void Init()
	{
		SetDefaultStyleKey();
		InitCommonSliderEvents();
	}

	private void InitCommonSliderEvents()
	{
		InitEvents();
		base.IsEnabledChanged += OnIsEnabledChanged;
		base.PointerEntered += OnSliderPointerEntered;
		base.PointerExited += OnSliderPointerExited;
		AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(OnSliderPointerReleased), handledEventsToo: true);
		AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(OnSliderPointerPressed), handledEventsToo: true);
		base.ValueChanged += SliderBase_ValueChanged;
	}

	private void SliderBase_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
	{
		if (IsPointerPressed)
		{
			SliderPointerState = SliderPointerStates.Dragging;
			if (!animationStartMoved && !SliderType.Ghost.Equals(Type))
			{
				animationStartMoved = true;
				CreateHorizontalPressedAnimation();
				CreateVerticalPressedAnimation();
			}
		}
	}

	private void OnSliderPointerPressed(object sender, PointerRoutedEventArgs e)
	{
		if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
		{
			if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
			{
				UpdateSliderPointerToPressed();
			}
		}
		else
		{
			UpdateSliderPointerToPressed();
		}
	}

	private void UpdateSliderPointerToPressed()
	{
		IsPointerPressed = true;
		if (isPointerOver)
		{
			SliderPointerState = SliderPointerStates.Pressed;
		}
	}

	private void OnSliderPointerEntered(object sender, PointerRoutedEventArgs e)
	{
		isPointerOver = true;
		if (!IsPointerPressed)
		{
			SliderPointerState = SliderPointerStates.Over;
		}
	}

	private void OnSliderPointerExited(object sender, PointerRoutedEventArgs e)
	{
		isPointerOver = false;
		if (!IsPointerPressed)
		{
			SliderPointerState = SliderPointerStates.Normal;
		}
	}

	private void OnSliderPointerReleased(object sender, PointerRoutedEventArgs e)
	{
		IsPointerPressed = false;
		SliderPointerState = (isPointerOver ? SliderPointerStates.Over : SliderPointerStates.Normal);
		if (animationStartMoved && !SliderType.Ghost.Equals(Type))
		{
			CreateHorizontalReleaseAnimation();
			CreateVerticalReleaseAnimation();
			animationStartMoved = false;
		}
	}

	private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (base.IsEnabled)
		{
			VisualStateManager.GoToState(this, "NormalSlider", useTransitions: true);
			UpdatePointerState();
		}
	}

	private static void OnTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SliderBase sliderBase)
		{
			sliderBase.Style = sliderBase.GetSliderStyleSelectorInstance().SelectStyle(sliderBase.Type);
			sliderBase.RefreshLayout();
		}
	}

	private void ApplyStyleBySelector()
	{
		base.Style = GetSliderStyleSelectorInstance().SelectStyle(Type);
	}

	private void UpdateFixedTooltipTextValue()
	{
		if (TextValueVisibility == Visibility.Visible)
		{
			TextValue = base.Value.ToString();
		}
	}

	protected void UpdateDecreaseRectVariables()
	{
		_horizontalDecreaseRect = GetTemplateChild("HorizontalDecreaseRect") as Rectangle;
		_verticalDecreaseRect = GetTemplateChild("VerticalDecreaseRect") as Rectangle;
		if (_horizontalDecreaseRect != null)
		{
			_horizontalDecreaseRectMaxWidth = _horizontalDecreaseRect.MaxWidth;
			_horizontalDecreaseRectMinWidth = _horizontalDecreaseRect.MinWidth;
		}
		if (_verticalDecreaseRect != null)
		{
			_verticalDecreaseRectMaxHeight = _verticalDecreaseRect.MaxHeight;
			_verticalDecreaseRectMinHeight = _verticalDecreaseRect.MinHeight;
		}
	}

	public Thickness GetFocusVisualMargin()
	{
		object key = "OneUIShockAreaOutStrokeMargin".GetKey();
		if (key is Thickness)
		{
			return (Thickness)key;
		}
		return default(Thickness);
	}

	public virtual ISliderStyleSelector GetSliderStyleSelectorInstance()
	{
		if (sliderStyleSelector == null)
		{
			sliderStyleSelector = new SliderStyleSelector();
		}
		return sliderStyleSelector;
	}

	internal virtual void CreateHorizontalPressedAnimation()
	{
		Border border = GetTemplateChild("HorizontalTrackBorder") as Border;
		Border border2 = GetTemplateChild("HorizontalDecreaseBorder") as Border;
		Border border3 = GetTemplateChild("HorizontalDecreaseOverlayBorder") as Border;
		Border border4 = GetTemplateChild("HorizontalDecreaseBorderBuffer") as Border;
		FadeInHeightAnimation(border);
		FadeInHeightAnimation(border4);
		FadeInHeightAnimation(border2, border3);
		TimeSpan animationTime = TimeSpan.FromSeconds(0.05);
		SliderBorders sliderBorders = new SliderBorders(border, border2, border3, border4);
		if (SliderType.Type1.Equals(Type))
		{
			HorizontalSliderCornerRadiusAnimation(9.0, sliderBorders, animationTime);
		}
		else if (SliderType.Type2.Equals(Type))
		{
			HorizontalSliderCornerRadiusAnimation(6.0, sliderBorders, animationTime);
		}
	}

	internal virtual void CreateVerticalPressedAnimation()
	{
		Border border = GetTemplateChild("VerticalTrackBorder") as Border;
		Border border2 = GetTemplateChild("VerticalDecreaseBorder") as Border;
		Border border3 = GetTemplateChild("VerticalDecreaseOverlayBorder") as Border;
		Border border4 = GetTemplateChild("VerticalDecreaseBorderBuffer") as Border;
		FadeInWidthAnimation(border);
		FadeInWidthAnimation(border4);
		FadeInWidthAnimation(border2, border3);
		TimeSpan animationTime = TimeSpan.FromSeconds(0.05);
		SliderBorders sliderBorders = new SliderBorders(border, border2, border3, border4);
		if (SliderType.Type1.Equals(Type))
		{
			VerticalSliderCornerRadiusAnimation(9.0, sliderBorders, animationTime);
		}
		else if (SliderType.Type2.Equals(Type))
		{
			VerticalSliderCornerRadiusAnimation(6.0, sliderBorders, animationTime);
		}
	}

	internal virtual void CreateHorizontalReleaseAnimation()
	{
		Border border = GetTemplateChild("HorizontalTrackBorder") as Border;
		Border border2 = GetTemplateChild("HorizontalDecreaseBorder") as Border;
		Border border3 = GetTemplateChild("HorizontalDecreaseOverlayBorder") as Border;
		Border border4 = GetTemplateChild("HorizontalDecreaseBorderBuffer") as Border;
		FadeOutHeightAnimation(border);
		FadeOutHeightAnimation(border4);
		FadeOutHeightAnimation(border2, border3);
		TimeSpan animationTime = TimeSpan.FromSeconds(0.05);
		SliderBorders sliderBorders = new SliderBorders(border, border2, border3, border4);
		if (SliderType.Type1.Equals(Type))
		{
			HorizontalSliderCornerRadiusAnimation(6.0, sliderBorders, animationTime);
		}
		else if (SliderType.Type2.Equals(Type))
		{
			HorizontalSliderCornerRadiusAnimation(2.0, sliderBorders, animationTime);
		}
	}

	internal virtual void CreateVerticalReleaseAnimation()
	{
		Border border = GetTemplateChild("VerticalTrackBorder") as Border;
		Border border2 = GetTemplateChild("VerticalDecreaseBorder") as Border;
		Border border3 = GetTemplateChild("VerticalDecreaseOverlayBorder") as Border;
		Border border4 = GetTemplateChild("VerticalDecreaseBorderBuffer") as Border;
		FadeOutWidthAnimation(border);
		FadeOutWidthAnimation(border4);
		FadeOutWidthAnimation(border2, border3);
		TimeSpan animationTime = TimeSpan.FromSeconds(0.05);
		SliderBorders sliderBorders = new SliderBorders(border, border2, border3, border4);
		if (SliderType.Type1.Equals(Type))
		{
			VerticalSliderCornerRadiusAnimation(6.0, sliderBorders, animationTime);
		}
		else if (SliderType.Type2.Equals(Type))
		{
			VerticalSliderCornerRadiusAnimation(2.0, sliderBorders, animationTime);
		}
	}

	internal virtual void HorizontalSliderCornerRadiusAnimation(double cornerRadius, SliderBorders sliderBorders, TimeSpan animationTime)
	{
		CornerRadiusAnimation(sliderBorders.TrackBorder, new CornerRadius(cornerRadius), animationTime);
		CornerRadiusAnimation(sliderBorders.DecreaseBorderBuffer, new CornerRadius(cornerRadius), animationTime);
		CornerRadiusAnimation(sliderBorders.DecreaseBorder, new CornerRadius(cornerRadius, 0.0, 0.0, cornerRadius), animationTime);
		CornerRadiusAnimation(sliderBorders.DecreaseOverlayBorder, new CornerRadius(cornerRadius, 0.0, 0.0, cornerRadius), animationTime);
	}

	internal virtual void VerticalSliderCornerRadiusAnimation(double cornerRadius, SliderBorders sliderBorders, TimeSpan animationTime)
	{
		CornerRadiusAnimation(sliderBorders.TrackBorder, new CornerRadius(cornerRadius), animationTime);
		CornerRadiusAnimation(sliderBorders.DecreaseBorderBuffer, new CornerRadius(cornerRadius), animationTime);
		CornerRadiusAnimation(sliderBorders.DecreaseBorder, new CornerRadius(0.0, 0.0, cornerRadius, cornerRadius), animationTime);
		CornerRadiusAnimation(sliderBorders.DecreaseOverlayBorder, new CornerRadius(0.0, 0.0, cornerRadius, cornerRadius), animationTime);
	}

	protected void FadeInHeightAnimation(Border border, Border overlayBorder = null)
	{
		if (border != null)
		{
			KeySpline pathInterpolator = CreateKeySpline(0.22, 0.25, 0.0, 1.0);
			CreateAnimationWithPathInterpolator(border.MaxHeight, border, "Height", TimeSpan.FromSeconds(0.35), pathInterpolator);
		}
		if (overlayBorder != null)
		{
			KeySpline pathInterpolator2 = CreateKeySpline(0.22, 0.25, 0.0, 1.0);
			CreateAnimationWithPathInterpolator(overlayBorder.MaxHeight, overlayBorder, "Height", TimeSpan.FromSeconds(0.35), pathInterpolator2);
		}
	}

	protected void FadeOutHeightAnimation(Border border, Border overlayBorder = null)
	{
		if (border != null)
		{
			KeySpline pathInterpolator = CreateKeySpline(0.22, 0.22, 0.0, 1.0);
			CreateAnimationWithPathInterpolator(border.MinHeight, border, "Height", TimeSpan.FromSeconds(0.35), pathInterpolator);
		}
		if (overlayBorder != null)
		{
			KeySpline pathInterpolator2 = CreateKeySpline(0.22, 0.22, 0.0, 1.0);
			CreateAnimationWithPathInterpolator(overlayBorder.MinHeight, overlayBorder, "Height", TimeSpan.FromSeconds(0.35), pathInterpolator2);
		}
	}

	protected void FadeInWidthAnimation(Border border, Border overlayBorder = null)
	{
		if (border != null)
		{
			KeySpline pathInterpolator = CreateKeySpline(0.22, 0.25, 0.0, 1.0);
			CreateAnimationWithPathInterpolator(border.MaxWidth, border, "Width", TimeSpan.FromSeconds(0.35), pathInterpolator);
		}
		if (overlayBorder != null)
		{
			KeySpline pathInterpolator2 = CreateKeySpline(0.22, 0.25, 0.0, 1.0);
			CreateAnimationWithPathInterpolator(overlayBorder.MaxWidth, overlayBorder, "Width", TimeSpan.FromSeconds(0.35), pathInterpolator2);
		}
	}

	protected void FadeOutWidthAnimation(Border border, Border overlayBorder = null)
	{
		if (border != null)
		{
			KeySpline pathInterpolator = CreateKeySpline(0.22, 0.22, 0.0, 1.0);
			CreateAnimationWithPathInterpolator(border.MinWidth, border, "Width", TimeSpan.FromSeconds(0.35), pathInterpolator);
		}
		if (overlayBorder != null)
		{
			KeySpline pathInterpolator2 = CreateKeySpline(0.22, 0.22, 0.0, 1.0);
			CreateAnimationWithPathInterpolator(overlayBorder.MinWidth, overlayBorder, "Width", TimeSpan.FromSeconds(0.35), pathInterpolator2);
		}
	}

	private KeySpline CreateKeySpline(double x1, double y1, double x2, double y2)
	{
		return new KeySpline
		{
			ControlPoint1 = new Point(x1, y1),
			ControlPoint2 = new Point(x2, y2)
		};
	}

	protected void CornerRadiusAnimation(DependencyObject target, CornerRadius toValue, TimeSpan duration)
	{
		if (target != null)
		{
			Storyboard storyboard = new Storyboard();
			ObjectAnimationUsingKeyFrames objectAnimationUsingKeyFrames = new ObjectAnimationUsingKeyFrames();
			Storyboard.SetTarget(objectAnimationUsingKeyFrames, target);
			Storyboard.SetTargetProperty(objectAnimationUsingKeyFrames, "(Border.CornerRadius)");
			DiscreteObjectKeyFrame item = new DiscreteObjectKeyFrame
			{
				KeyTime = KeyTime.FromTimeSpan(duration),
				Value = toValue
			};
			objectAnimationUsingKeyFrames.KeyFrames.Add(item);
			storyboard.Children.Add(objectAnimationUsingKeyFrames);
			storyboard.SafeBegin();
		}
	}

	private void CreateAnimationWithPathInterpolator(double to, DependencyObject target, string property, TimeSpan duration, KeySpline pathInterpolator)
	{
		if (target != null)
		{
			Storyboard storyboard = new Storyboard();
			DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames
			{
				EnableDependentAnimation = true
			};
			Storyboard.SetTarget(doubleAnimationUsingKeyFrames, target);
			Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, property);
			SplineDoubleKeyFrame item = new SplineDoubleKeyFrame
			{
				KeyTime = KeyTime.FromTimeSpan(duration),
				KeySpline = pathInterpolator,
				Value = to
			};
			doubleAnimationUsingKeyFrames.KeyFrames.Add(item);
			storyboard.Children.Add(doubleAnimationUsingKeyFrames);
			storyboard.SafeBegin();
		}
	}

	protected void UpdatePointerState()
	{
		if (base.IsEnabled)
		{
			string empty = string.Empty;
			empty = SliderPointerState switch
			{
				SliderPointerStates.Normal => empty + "NormalSlider", 
				SliderPointerStates.Over => empty + "PointerOverSlider", 
				SliderPointerStates.Pressed => empty + "PressedSlider", 
				SliderPointerStates.Dragging => empty + "DraggingSlider", 
				_ => empty + "NormalSlider", 
			} + (IsShockArea() ? "ShockArea" : string.Empty);
			VisualStateManager.GoToState(this, empty, useTransitions: true);
		}
	}

	protected virtual bool IsShockArea()
	{
		if (!SliderType.Ghost.Equals(Type))
		{
			return isShockArea;
		}
		return false;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		SetBehaviorSliderType();
		UpdateDecreaseRectVariables();
	}

	protected virtual void SetBehaviorSliderType()
	{
		VisualStateManager.GoToState(this, "SliderBehaviorState", useTransitions: true);
	}

	protected override void OnValueChanged(double oldValue, double newValue)
	{
		base.OnValueChanged(oldValue, newValue);
		UpdateFixedTooltipTextValue();
	}
}
