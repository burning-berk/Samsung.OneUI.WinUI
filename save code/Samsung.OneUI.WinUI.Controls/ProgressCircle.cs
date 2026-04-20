using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public abstract class ProgressCircle : Control
{
	protected Grid colorGrid;

	protected readonly long tokenOnFlowDirectionPropertyChanged;

	internal IDictionary<ProgressCircleIndeterminateOrientation, string> _progressCircleTextAlingmentDictionary = new Dictionary<ProgressCircleIndeterminateOrientation, string>
	{
		{
			ProgressCircleIndeterminateOrientation.Vertical,
			"TextVerticalAlignment"
		},
		{
			ProgressCircleIndeterminateOrientation.Horizontal,
			"TextHorizontalAlignment"
		}
	};

	public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(ProgressCircleSize), typeof(ProgressCircle), new PropertyMetadata(ProgressCircleSize.Small, OnSizePropertyChangedInternal));

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ProgressCircle), new PropertyMetadata(null));

	public ProgressCircleSize Size
	{
		get
		{
			return (ProgressCircleSize)GetValue(SizeProperty);
		}
		set
		{
			SetValue(SizeProperty, value);
		}
	}

	public string Text
	{
		get
		{
			return (string)GetValue(TextProperty);
		}
		set
		{
			SetValue(TextProperty, value);
		}
	}

	private static void OnSizePropertyChangedInternal(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ProgressCircle progressCircle)
		{
			progressCircle.OnSizePropertyChanged(d, e);
		}
	}

	protected abstract void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);

	protected internal void ForceLeftToRightFlowDirection()
	{
		if (base.FlowDirection != FlowDirection.LeftToRight)
		{
			base.FlowDirection = FlowDirection.LeftToRight;
		}
	}

	protected ProgressCircle()
	{
		base.FlowDirection = FlowDirection.LeftToRight;
		base.Unloaded += ProgressCircle_Unloaded;
		tokenOnFlowDirectionPropertyChanged = RegisterPropertyChangedCallback(FrameworkElement.FlowDirectionProperty, OnFlowDirectionPropertyChanged);
	}

	private void ProgressCircle_Unloaded(object sender, RoutedEventArgs e)
	{
		UnregisterPropertyChangedCallback(FrameworkElement.FlowDirectionProperty, tokenOnFlowDirectionPropertyChanged);
	}

	private void OnFlowDirectionPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		ForceLeftToRightFlowDirection();
	}
}
