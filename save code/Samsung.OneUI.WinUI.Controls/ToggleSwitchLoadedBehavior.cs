using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.Xaml.Interactivity;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

internal class ToggleSwitchLoadedBehavior : Behavior<Microsoft.UI.Xaml.Controls.ToggleSwitch>
{
	private const string SWITCH_KNOB_BOUNDS = "SwitchKnobBounds";

	private const string OVERLAY_SWITCH_KNOB_BOUNDS = "Overlay_SwitchKnobBounds";

	private const string SWITCH_KNOB = "SwitchKnob";

	private const string OUTER_BORDER_ELEMENT_NAME = "OuterBorder";

	private Rectangle _switchKnobBounds;

	private Rectangle _overlaySwitchKnobBounds;

	private Rectangle _outerBorder;

	private TranslateTransform _translateTransform;

	private Grid _switchKnob;

	private Microsoft.UI.Xaml.Controls.ToggleSwitch _toggleSwitch;

	protected override void OnAttached()
	{
		base.OnAttached();
		if (base.AssociatedObject != null)
		{
			base.AssociatedObject.Loaded += OnLoaded;
		}
	}

	protected override void OnDetaching()
	{
		base.OnDetaching();
		if (base.AssociatedObject != null)
		{
			base.AssociatedObject.Loaded -= OnLoaded;
		}
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		_toggleSwitch = sender as Microsoft.UI.Xaml.Controls.ToggleSwitch;
		if (_toggleSwitch != null)
		{
			FindThumbs();
			_toggleSwitch.ManipulationDelta -= ToggleSwitch_ManipulationDelta;
			_toggleSwitch.ManipulationDelta += ToggleSwitch_ManipulationDelta;
		}
	}

	private void FindThumbs()
	{
		_switchKnobBounds = UIExtensionsInternal.FindChildByName<Rectangle>("SwitchKnobBounds", _toggleSwitch);
		_overlaySwitchKnobBounds = UIExtensionsInternal.FindChildByName<Rectangle>("Overlay_SwitchKnobBounds", _toggleSwitch);
		_outerBorder = UIExtensionsInternal.FindChildByName<Rectangle>("OuterBorder", _toggleSwitch);
		_switchKnob = UIExtensionsInternal.FindChildByName<Grid>("SwitchKnob", _toggleSwitch);
		_translateTransform = _switchKnob?.RenderTransform as TranslateTransform;
	}

	private void ToggleSwitch_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
	{
		if (_switchKnob.Width > 0.0)
		{
			double num = _translateTransform.X / _switchKnob.Width;
			_switchKnobBounds.Opacity = num;
			_outerBorder.Opacity = Math.Abs(num - 1.0);
		}
	}
}
