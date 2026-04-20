using System;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;

namespace Samsung.OneUI.WinUI.Controls;

internal class CustomSliderAutomationPeer : SliderAutomationPeer
{
	private DispatcherTimer _dispatcherTimerToNarrator;

	private readonly string _componentName;

	private const string VALUE_CHANGED_ACTIVITY_DESCRIPTION = "Value changed to";

	private const int INTERNAL_INTERVAL_VALUE_CHANGED_DELAY_IN_MILLISECONDS = 500;

	public CustomSliderAutomationPeer(Microsoft.UI.Xaml.Controls.Slider owner, string componentName)
		: base(owner)
	{
		_componentName = componentName;
		AssignEvents();
	}

	private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
	{
		if (sender is Microsoft.UI.Xaml.Controls.Slider slider)
		{
			StartTimerToNarrateValueChanged(slider);
		}
	}

	private void Slider_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		StopTimerToAvoidNarrateValueChanged(sender);
	}

	private void Slider_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		if (sender is Microsoft.UI.Xaml.Controls.Slider slider)
		{
			StartTimerToNarrateValueChanged(slider);
		}
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.Custom;
	}

	protected override string GetNameCore()
	{
		return GetSliderValue(base.Owner as Microsoft.UI.Xaml.Controls.Slider);
	}

	protected override string GetLocalizedControlTypeCore()
	{
		return _componentName;
	}

	private void AssignEvents()
	{
		if (base.Owner is Microsoft.UI.Xaml.Controls.Slider slider)
		{
			slider.ValueChanged += Slider_ValueChanged;
			slider.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(Slider_PointerPressed), handledEventsToo: true);
			slider.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(Slider_PointerReleased), handledEventsToo: true);
		}
	}

	private void RaiseNarratorNotification(Microsoft.UI.Xaml.Controls.Slider slider, double newValue)
	{
		string sliderValue = GetSliderValue(slider, newValue);
		string activityId = "Value changed to " + sliderValue;
		RaiseNotificationEvent(AutomationNotificationKind.Other, AutomationNotificationProcessing.MostRecent, sliderValue, activityId);
	}

	private string GetSliderValue(Microsoft.UI.Xaml.Controls.Slider slider)
	{
		if (slider == null)
		{
			return string.Empty;
		}
		double value = slider.Value;
		return GetSliderValue(slider, value);
	}

	private string GetSliderValue(Microsoft.UI.Xaml.Controls.Slider slider, double sliderValue)
	{
		string result = sliderValue.ToString();
		if (slider == null)
		{
			return string.Empty;
		}
		if (slider.ThumbToolTipValueConverter != null)
		{
			object obj = slider.ThumbToolTipValueConverter.Convert(sliderValue, typeof(double), null, null);
			if (obj != null)
			{
				result = obj.ToString();
			}
		}
		return result;
	}

	private DispatcherTimer GetInternalTimer(Microsoft.UI.Xaml.Controls.Slider slider)
	{
		if (_dispatcherTimerToNarrator == null)
		{
			_dispatcherTimerToNarrator = CreateInternalTimer(slider);
		}
		return _dispatcherTimerToNarrator;
	}

	private DispatcherTimer CreateInternalTimer(Microsoft.UI.Xaml.Controls.Slider slider)
	{
		DispatcherTimer dispatcherTimer = new DispatcherTimer();
		dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500.0);
		dispatcherTimer.Tick += async delegate(object? sender, object args)
		{
			await slider.DispatcherQueue.EnqueueAsync(delegate
			{
				RaiseNarratorNotification(slider, slider.Value);
				if (sender is DispatcherTimer dispatcherTimer2)
				{
					dispatcherTimer2.Stop();
				}
			});
		};
		return dispatcherTimer;
	}

	private void StartTimerToNarrateValueChanged(Microsoft.UI.Xaml.Controls.Slider slider)
	{
		DispatcherTimer internalTimer = GetInternalTimer(slider);
		if (!(internalTimer == null))
		{
			internalTimer.Stop();
			internalTimer.Start();
		}
	}

	private void StopTimerToAvoidNarrateValueChanged(object sender)
	{
		if (sender is Microsoft.UI.Xaml.Controls.Slider slider)
		{
			DispatcherTimer internalTimer = GetInternalTimer(slider);
			if (!(internalTimer == null))
			{
				internalTimer.Stop();
			}
		}
	}
}
