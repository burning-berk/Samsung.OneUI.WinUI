using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Controls.Selectors;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("This component is deprecated, please use property \"IsProgressEnabled\" from Contained, ContainedColored, ContainedBody or ContainedBodyColored. This component will be removed soon.", false)]
public class ProgressButton : Button
{
	private const string PROGRESS_ENABLED_VISUAL_STATE = "ProgressEnabled";

	private const string PROGRESS_DISABLED_VISUAL_STATE = "ProgressDisabled";

	public static readonly DependencyProperty IsProgressEnabledProperty = DependencyProperty.Register("IsProgressEnabled", typeof(bool), typeof(ProgressButton), new PropertyMetadata(false, OnIsProgressEnabledChanged));

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(ProgressButtonType), typeof(ProgressButton), new PropertyMetadata(ProgressButtonType.Flat, OnTypePropertyChanged));

	public bool IsProgressEnabled
	{
		get
		{
			return (bool)GetValue(IsProgressEnabledProperty);
		}
		set
		{
			SetValue(IsProgressEnabledProperty, value);
		}
	}

	public ProgressButtonType Type
	{
		get
		{
			return (ProgressButtonType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	private static void OnTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ProgressButton progressButton)
		{
			progressButton.Style = new ProgressButtonStyleSelector(progressButton.Type).SelectStyle();
		}
	}

	public ProgressButton()
	{
		base.Style = new ProgressButtonStyleSelector(Type).SelectStyle();
		base.IsEnabledChanged += ProgressButton_IsEnabledChanged;
	}

	private static void OnIsProgressEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ProgressButton progressButton)
		{
			progressButton.ProgressVisualStateChange();
		}
	}

	private void ProgressButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		ProgressVisualStateChange();
	}

	private void ProgressVisualStateChange(bool useTransition = false)
	{
		if (base.IsEnabled && IsProgressEnabled)
		{
			VisualStateManager.GoToState(this, "ProgressEnabled", useTransition);
			base.IsTabStop = false;
			base.ProcessKeyboardAccelerators += ProgressButton_ProcessKeyboardAccelerators;
		}
		else
		{
			VisualStateManager.GoToState(this, "ProgressDisabled", useTransition);
			base.ProcessKeyboardAccelerators -= ProgressButton_ProcessKeyboardAccelerators;
			base.IsTabStop = true;
		}
	}

	private void ProgressButton_ProcessKeyboardAccelerators(UIElement sender, ProcessKeyboardAcceleratorEventArgs args)
	{
		if (args.Key == VirtualKey.Enter || args.Key == VirtualKey.Space)
		{
			args.Handled = true;
		}
	}
}
