using System;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls.Primitives;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerCustomAutomationPeer;

internal class ColorPickerSliderCustomAutomationPeer : ColorPickerSliderAutomationPeer
{
	private const string PERCENT_STRING_ID = "DREAM_PD_PERCENT_TBOPT";

	private const int INTERVAL_VALUE_CHANGED_DELAY_IN_MILISECONDS = 500;

	private readonly ColorPickerSliderCustom _colorPickerSliderCustom;

	private DispatcherTimer _dispatcherTimer;

	private string _sliderValueText = string.Empty;

	private bool _isReadyToNarrate;

	public ColorPickerSliderCustomAutomationPeer(ColorPickerSliderCustom owner)
		: base(owner)
	{
		if (base.Owner is ColorPickerSliderCustom colorPickerSliderCustom)
		{
			_colorPickerSliderCustom = colorPickerSliderCustom;
			_colorPickerSliderCustom.ValueChanged += ColorPickerSliderCustom_ValueChanged;
			InitializeDispatcherTimer();
		}
	}

	private void ColorPickerSliderCustom_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
	{
		if (_isReadyToNarrate)
		{
			_sliderValueText = Math.Round(e.NewValue, 0).ToString();
			_dispatcherTimer.Stop();
			_dispatcherTimer.Start();
		}
		else
		{
			_isReadyToNarrate = true;
		}
	}

	protected override object GetPatternCore(PatternInterface patternInterface)
	{
		if (patternInterface == PatternInterface.RangeValue || patternInterface == PatternInterface.Value)
		{
			return null;
		}
		return base.GetPatternCore(patternInterface);
	}

	protected override string GetNameCore()
	{
		return AutomationProperties.GetName(_colorPickerSliderCustom);
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.Custom;
	}

	protected override string GetLocalizedControlTypeCore()
	{
		return "Slider, " + string.Format(ResourceExtensions.GetLocalized("DREAM_PD_PERCENT_TBOPT"), Math.Round(_colorPickerSliderCustom.Value, 0));
	}

	private void InitializeDispatcherTimer()
	{
		_dispatcherTimer = new DispatcherTimer
		{
			Interval = TimeSpan.FromMilliseconds(500.0)
		};
		_dispatcherTimer.Tick += async delegate(object? sender, object args)
		{
			await _colorPickerSliderCustom.DispatcherQueue.EnqueueAsync(delegate
			{
				RaiseNotificationEvent(AutomationNotificationKind.Other, AutomationNotificationProcessing.MostRecent, _sliderValueText, Guid.NewGuid().ToString());
				if (sender is DispatcherTimer dispatcherTimer)
				{
					dispatcherTimer.Stop();
				}
			});
		};
	}
}
