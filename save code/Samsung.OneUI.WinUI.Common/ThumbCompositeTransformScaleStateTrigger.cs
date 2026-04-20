using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;

namespace Samsung.OneUI.WinUI.Common;

internal class ThumbCompositeTransformScaleStateTrigger : StateTriggerBase
{
	private const double SCALE_100_PERCENT = 1.0;

	private Thumb thumb;

	private CompositeTransform compositeTransform;

	private long tokenScaleYCallbackRegistered;

	private long tokenScaleXCallbackRegistered;

	public static readonly DependencyProperty ThumbReferenceProperty = DependencyProperty.Register("ThumbReference", typeof(Thumb), typeof(ThumbCompositeTransformScaleStateTrigger), new PropertyMetadata(null, OnThumbPropertyChanged));

	public Thumb ThumbReference
	{
		get
		{
			return (Thumb)GetValue(ThumbReferenceProperty);
		}
		set
		{
			SetValue(ThumbReferenceProperty, value);
		}
	}

	private static void OnThumbPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ThumbCompositeTransformScaleStateTrigger thumbCompositeTransformScaleStateTrigger)
		{
			thumbCompositeTransformScaleStateTrigger.StartScaleMonitor(e.NewValue as Thumb);
		}
	}

	private void StartScaleMonitor(Thumb thumbReference)
	{
		if (!(thumbReference == null))
		{
			thumb = thumbReference;
			compositeTransform = thumb.RenderTransform as CompositeTransform;
			RegisterScalePropertyChangedCallbacks();
		}
	}

	private void OnScaleXChanged(DependencyObject sender, DependencyProperty dp)
	{
		ExecuteStateTriggerRule(sender);
	}

	private void OnScaleYChanged(DependencyObject sender, DependencyProperty dp)
	{
		ExecuteStateTriggerRule(sender);
	}

	private void ExecuteStateTriggerRule(DependencyObject sender)
	{
		if (sender is CompositeTransform { ScaleX: 1.0, ScaleY: 1.0 })
		{
			SetActive(IsActive: true);
		}
		else
		{
			SetActive(IsActive: false);
		}
	}

	private void RegisterScalePropertyChangedCallbacks()
	{
		thumb.Unloaded += ThumbUnloaded;
		tokenScaleYCallbackRegistered = compositeTransform.RegisterPropertyChangedCallback(CompositeTransform.ScaleYProperty, OnScaleYChanged);
		tokenScaleXCallbackRegistered = compositeTransform.RegisterPropertyChangedCallback(CompositeTransform.ScaleXProperty, OnScaleXChanged);
	}

	private void ThumbUnloaded(object sender, RoutedEventArgs e)
	{
		UnregisterScalePropertyChangedCallbacks();
	}

	private void UnregisterScalePropertyChangedCallbacks()
	{
		if (tokenScaleYCallbackRegistered > 0)
		{
			compositeTransform.UnregisterPropertyChangedCallback(CompositeTransform.ScaleYProperty, tokenScaleYCallbackRegistered);
		}
		if (tokenScaleXCallbackRegistered > 0)
		{
			compositeTransform.UnregisterPropertyChangedCallback(CompositeTransform.ScaleXProperty, tokenScaleXCallbackRegistered);
		}
	}
}
