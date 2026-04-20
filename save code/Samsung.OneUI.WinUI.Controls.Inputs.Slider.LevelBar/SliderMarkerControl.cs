using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Inputs.Slider.LevelBar;

internal sealed class SliderMarkerControl : Control
{
	private const string STATE_ENABLED = "Enabled";

	private const string STATE_DISABLED = "Disabled";

	private LevelSlider _slider;

	public SliderMarkerControl()
	{
		base.Loaded += SliderMarkerControl_Loaded;
		base.DefaultStyleKey = typeof(SliderMarkerControl);
		base.IsEnabledChanged += SliderMarkerControl_IsEnabledChanged;
	}

	private void SliderMarkerControl_Loaded(object sender, RoutedEventArgs e)
	{
		_slider = UIExtensionsInternal.GetVisualParent<LevelSlider>(this);
		UpdateEnablement();
	}

	private void SliderMarkerControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		UpdateEnablement();
	}

	private void UpdateEnablement()
	{
		if (!(_slider == null))
		{
			VisualStateManager.GoToState(this, _slider.IsEnabled ? "Enabled" : "Disabled", useTransitions: true);
		}
	}
}
