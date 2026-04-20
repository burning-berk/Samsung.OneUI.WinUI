using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Samsung.OneUI.WinUI.Controls.EventHandlers;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class BufferSlider : SliderBase
{
	private Border _horizontalDecreaseBorderBuffer;

	private Border _horizontalDecreaseBorder;

	private Border _horizontalTrackBorder;

	private Thumb _horizontalThumb;

	private Border _verticalDecreaseBorderBuffer;

	private Border _verticalDecreaseBorder;

	private Border _verticalTrackBorder;

	private Thumb _verticalThumb;

	public static readonly DependencyProperty BufferProperty = DependencyProperty.Register("Buffer", typeof(double), typeof(BufferSlider), new PropertyMetadata(0.0, OnBufferPropertyChanged));

	private const string HORIZONTAL_DECREASE_BORDER_BUFFER = "HorizontalDecreaseBorderBuffer";

	private const string HORIZONTAL_DECREASE_BORDER = "HorizontalDecreaseBorder";

	private const string HORIZONTAL_TRACK_BORDER = "HorizontalTrackBorder";

	private const string HORIZONTAL_THUMB = "HorizontalThumb";

	private const string VERTICAL_DECREASE_BORDER_BUFFER = "VerticalDecreaseBorderBuffer";

	private const string VERTICAL_DECREASE_BORDER = "VerticalDecreaseBorder";

	private const string VERTICAL_TRACK_BORDER = "VerticalTrackBorder";

	private const string VERTICAL_THUMB = "VerticalThumb";

	private const string VIEW_STATE_BUFFER_BEHAVIOR = "BufferSliderBehaviorState";

	public double Buffer
	{
		get
		{
			return (double)GetValue(BufferProperty);
		}
		set
		{
			SetValue(BufferProperty, value);
		}
	}

	public event EventHandler<UserValueChangedEventArgs> UserValueChanged;

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		SetBehaviorSliderType();
		InitElementsInstances();
	}

	private void InitElementsInstances()
	{
		_horizontalDecreaseBorderBuffer = GetTemplateChild("HorizontalDecreaseBorderBuffer") as Border;
		_horizontalDecreaseBorder = GetTemplateChild("HorizontalDecreaseBorder") as Border;
		_horizontalTrackBorder = GetTemplateChild("HorizontalTrackBorder") as Border;
		_horizontalThumb = GetTemplateChild("HorizontalThumb") as Thumb;
		_verticalDecreaseBorderBuffer = GetTemplateChild("VerticalDecreaseBorderBuffer") as Border;
		_verticalDecreaseBorder = GetTemplateChild("VerticalDecreaseBorder") as Border;
		_verticalTrackBorder = GetTemplateChild("VerticalTrackBorder") as Border;
		_verticalThumb = GetTemplateChild("VerticalThumb") as Thumb;
		InitInternalElementsEvents();
	}

	private void InitInternalElementsEvents()
	{
		if (_horizontalDecreaseBorder != null)
		{
			_horizontalDecreaseBorder.SizeChanged += DecreaseBorderSizeChanged;
		}
		if (_verticalDecreaseBorder != null)
		{
			_verticalDecreaseBorder.SizeChanged += DecreaseBorderSizeChanged;
		}
	}

	private void DecreaseBorderSizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateBufferVisualSize();
	}

	private void UpdateBufferVisualSize()
	{
		if (base.Orientation == Orientation.Horizontal)
		{
			UpdateHorizontalBufferVisualSize();
		}
		else
		{
			UpdateVerticalBufferVisualSize();
		}
		UpdatePointerState();
	}

	private void UpdateHorizontalBufferVisualSize()
	{
		if (_horizontalDecreaseBorderBuffer == null || _horizontalTrackBorder == null)
		{
			return;
		}
		if (Buffer == 0.0 || base.Maximum == 0.0)
		{
			_horizontalDecreaseBorderBuffer.Width = 0.0;
			_horizontalDecreaseBorderBuffer.Visibility = Visibility.Collapsed;
			return;
		}
		double num = 0.0;
		if (_horizontalThumb != null)
		{
			num = _horizontalThumb.ActualWidth;
		}
		if (_horizontalTrackBorder.ActualWidth >= num)
		{
			_horizontalDecreaseBorderBuffer.MaxWidth = _horizontalTrackBorder.ActualWidth;
			_horizontalDecreaseBorderBuffer.Width = (base.Value + Buffer) / base.Maximum * (_horizontalDecreaseBorderBuffer.MaxWidth - num) + num;
			_horizontalDecreaseBorderBuffer.Visibility = Visibility.Visible;
		}
	}

	private void UpdateVerticalBufferVisualSize()
	{
		if (_verticalDecreaseBorderBuffer == null || _verticalTrackBorder == null)
		{
			return;
		}
		if (Buffer == 0.0 || base.Maximum == 0.0)
		{
			_verticalDecreaseBorderBuffer.Height = 0.0;
			_verticalDecreaseBorderBuffer.Visibility = Visibility.Collapsed;
			return;
		}
		double num = 0.0;
		if (_verticalThumb != null)
		{
			num = _verticalThumb.ActualHeight;
		}
		if (_verticalTrackBorder.ActualHeight >= num)
		{
			_verticalDecreaseBorderBuffer.MaxHeight = _verticalTrackBorder.ActualHeight;
			_verticalDecreaseBorderBuffer.Height = (base.Value + Buffer) / base.Maximum * (_verticalDecreaseBorderBuffer.MaxHeight - num) + num;
			_verticalDecreaseBorderBuffer.Visibility = Visibility.Visible;
		}
	}

	protected override void SetBehaviorSliderType()
	{
		VisualStateManager.GoToState(this, "BufferSliderBehaviorState", useTransitions: true);
	}

	protected override bool IsShockArea()
	{
		return false;
	}

	protected override void InitEvents()
	{
		base.ValueChanged += BufferSlider_ValueChanged;
		base.Unloaded += BufferSlider_Unloaded;
	}

	public override void RefreshLayout()
	{
		UpdateLayout();
		UpdateBufferVisualSize();
	}

	private static void OnBufferPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		(d as BufferSlider)?.UpdateBufferVisualSize();
	}

	private void BufferSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
	{
		UpdateBufferVisualSize();
		this.UserValueChanged?.Invoke(sender, new UserValueChangedEventArgs(e.NewValue, e.OldValue));
	}

	protected override void SetDefaultStyleKey()
	{
		base.DefaultStyleKey = typeof(BufferSlider);
	}

	private void BufferSlider_Unloaded(object sender, RoutedEventArgs e)
	{
		if (_horizontalDecreaseBorder != null)
		{
			_horizontalDecreaseBorder.SizeChanged -= DecreaseBorderSizeChanged;
		}
		if (_verticalDecreaseBorder != null)
		{
			_verticalDecreaseBorder.SizeChanged -= DecreaseBorderSizeChanged;
		}
	}
}
