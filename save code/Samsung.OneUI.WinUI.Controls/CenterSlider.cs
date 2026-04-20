using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Controls.Inputs.Slider.Base;
using Samsung.OneUI.WinUI.Controls.Selectors;

namespace Samsung.OneUI.WinUI.Controls;

public class CenterSlider : SliderBase
{
	private const string INCREASE_DECREASE_BORDER_HORIZONTAL = "HorizontalDecreaseBorder";

	private const string INCREASE_DECREASE_BORDER_VERTICAL = "VerticalDecreaseBorder";

	private const string CENTER_CIRCLE_HORIZONTAL = "HorizontalCenterCircle";

	private const string CENTER_CIRCLE_VERTICAL = "VerticalCenterCircle";

	private const string FOCUS_BORDER = "FocusBorder";

	private double rangeDelta;

	private readonly ScaleTransform scaleX = new ScaleTransform();

	private readonly ScaleTransform scaleY = new ScaleTransform();

	private Border centerCircleHorizontal;

	private Border centerCircleVertical;

	private CustomSliderAutomationPeer _customAutomatinoPeer;

	private ContentControl _focusBorder;

	private readonly Thickness _horizontalCenterSliderFocusBorderMargin = new Thickness(-4.0, 0.0, -4.0, 0.0);

	private readonly Thickness _verticalCenterSliderFocusBorderMargin = new Thickness(0.0, -4.0, 0.0, -4.0);

	public new Orientation Orientation
	{
		get
		{
			return (Orientation)GetValue(Microsoft.UI.Xaml.Controls.Slider.OrientationProperty);
		}
		set
		{
			SetValue(Microsoft.UI.Xaml.Controls.Slider.OrientationProperty, value);
			UpdateBar();
			UpdateFocusBorderMargin();
		}
	}

	public override ISliderStyleSelector GetSliderStyleSelectorInstance()
	{
		if (sliderStyleSelector == null)
		{
			sliderStyleSelector = new CenterSliderStyleSelector();
		}
		return sliderStyleSelector;
	}

	private void SetElementScales()
	{
		if (GetTemplateChild("HorizontalCenterCircle") is Border border)
		{
			centerCircleHorizontal = border;
		}
		if (GetTemplateChild("VerticalCenterCircle") is Border border2)
		{
			centerCircleVertical = border2;
		}
		if (GetTemplateChild("HorizontalDecreaseBorder") is Border border3)
		{
			border3.RenderTransform = scaleX;
		}
		if (GetTemplateChild("VerticalDecreaseBorder") is Border border4)
		{
			border4.RenderTransform = scaleY;
		}
	}

	private void UpdateRangeDelta()
	{
		rangeDelta = (base.Maximum - base.Minimum) / 2.0;
	}

	private void UpdateBar()
	{
		if (rangeDelta != 0.0)
		{
			if (Orientation == Orientation.Horizontal && scaleX != null)
			{
				scaleX.ScaleX = base.Value / rangeDelta;
			}
			else if (Orientation == Orientation.Vertical && scaleY != null)
			{
				scaleY.ScaleY = base.Value / rangeDelta * -1.0;
			}
		}
	}

	private void UpdateFocusBorderMargin()
	{
		if (!(_focusBorder == null))
		{
			_focusBorder.Margin = ((Orientation == Orientation.Horizontal) ? _horizontalCenterSliderFocusBorderMargin : _verticalCenterSliderFocusBorderMargin);
		}
	}

	protected override void SetDefaultStyleKey()
	{
		base.DefaultStyleKey = typeof(CenterSlider);
	}

	public override void RefreshLayout()
	{
		UpdateLayout();
	}

	protected override void InitEvents()
	{
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_focusBorder = GetTemplateChild("FocusBorder") as ContentControl;
		SetElementScales();
		UpdateRangeDelta();
		UpdateBar();
		UpdateFocusBorderMargin();
	}

	protected override void OnValueChanged(double oldValue, double newValue)
	{
		base.OnValueChanged(oldValue, newValue);
		UpdateBar();
	}

	protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
	{
		base.OnMinimumChanged(oldMinimum, newMinimum);
		UpdateRangeDelta();
	}

	protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
	{
		base.OnMaximumChanged(oldMaximum, newMaximum);
		UpdateRangeDelta();
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		if (_customAutomatinoPeer == null)
		{
			_customAutomatinoPeer = new CustomSliderAutomationPeer(this, "CenterSlider");
		}
		return _customAutomatinoPeer;
	}

	internal override void CreateHorizontalPressedAnimation()
	{
		base.CreateHorizontalPressedAnimation();
		FadeInHeightAnimation(centerCircleHorizontal);
		FadeInWidthAnimation(centerCircleHorizontal);
	}

	internal override void CreateHorizontalReleaseAnimation()
	{
		base.CreateHorizontalReleaseAnimation();
		FadeOutHeightAnimation(centerCircleHorizontal);
		FadeOutWidthAnimation(centerCircleHorizontal);
	}

	internal override void CreateVerticalPressedAnimation()
	{
		base.CreateVerticalPressedAnimation();
		FadeInWidthAnimation(centerCircleVertical);
		FadeInHeightAnimation(centerCircleVertical);
	}

	internal override void CreateVerticalReleaseAnimation()
	{
		base.CreateVerticalReleaseAnimation();
		FadeOutWidthAnimation(centerCircleVertical);
		FadeOutHeightAnimation(centerCircleVertical);
	}

	internal override void HorizontalSliderCornerRadiusAnimation(double cornerRadius, SliderBorders sliderBorders, TimeSpan animationTime)
	{
		CornerRadiusAnimation(sliderBorders.TrackBorder, new CornerRadius(cornerRadius), animationTime);
		CornerRadiusAnimation(sliderBorders.DecreaseBorder, new CornerRadius(0.0, cornerRadius, cornerRadius, 0.0), animationTime);
		CornerRadiusAnimation(centerCircleHorizontal, new CornerRadius(cornerRadius), animationTime);
	}

	internal override void VerticalSliderCornerRadiusAnimation(double cornerRadius, SliderBorders sliderBorders, TimeSpan animationTime)
	{
		base.VerticalSliderCornerRadiusAnimation(cornerRadius, sliderBorders, animationTime);
		CornerRadiusAnimation(centerCircleVertical, new CornerRadius(cornerRadius), animationTime);
	}
}
