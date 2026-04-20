using System;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class ContentButton : Button
{
	private const int DEFAULT_MULTICLICK_DELAY = 250;

	private const string POINTER_OVER_STATE = "PointerOver";

	private const string NORMAL_STATE = "Normal";

	private const string PRESSED_STATE = "Pressed";

	private DispatcherTimer _timer;

	private bool _pressedOnce = true;

	private bool _wasKeyDown;

	public static readonly DependencyProperty ShapeProperty = DependencyProperty.Register("Shape", typeof(ButtonShapeEnum), typeof(ContentButton), new PropertyMetadata(ButtonShapeEnum.Default, OnShapePropertyChanged));

	public static readonly DependencyProperty IsPressAndHoldEnabledProperty = DependencyProperty.Register("IsPressAndHoldEnabled", typeof(bool), typeof(ContentButton), new PropertyMetadata(false, OnIsPressAndHoldEnabledPropertyChanged));

	public static readonly DependencyProperty PressAndHoldIntervalProperty = DependencyProperty.Register("PressAndHoldInterval", typeof(int), typeof(ContentButton), new PropertyMetadata(250, OnPressAndHoldIntervalPropertyChanged));

	public ButtonShapeEnum Shape
	{
		get
		{
			return (ButtonShapeEnum)GetValue(ShapeProperty);
		}
		set
		{
			SetValue(ShapeProperty, value);
		}
	}

	public bool IsPressAndHoldEnabled
	{
		get
		{
			return (bool)GetValue(IsPressAndHoldEnabledProperty);
		}
		set
		{
			SetValue(IsPressAndHoldEnabledProperty, value);
		}
	}

	public int PressAndHoldInterval
	{
		get
		{
			return (int)GetValue(PressAndHoldIntervalProperty);
		}
		set
		{
			SetValue(PressAndHoldIntervalProperty, value);
		}
	}

	public new event EventHandler<RoutedEventArgs> Click;

	private static void OnShapePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ContentButton contentButton)
		{
			contentButton.AdjustContentButtonShape(contentButton.Shape);
		}
	}

	private static void OnIsPressAndHoldEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ContentButton contentButton)
		{
			contentButton._timer?.Stop();
		}
	}

	private static void OnPressAndHoldIntervalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ContentButton contentButton)
		{
			contentButton.UpdatePressAndHoldIntervalProperty();
		}
	}

	public ContentButton()
	{
		base.Loaded += ContentButton_Loaded;
		base.Unloaded += ContentButton_Unloaded;
	}

	private void ContentButton_Loaded(object sender, RoutedEventArgs e)
	{
		_timer = new DispatcherTimer
		{
			Interval = TimeSpan.FromMilliseconds(PressAndHoldInterval)
		};
		_timer.Tick += Timer_Tick;
		base.SizeChanged += ContentButton_SizeChanged;
		base.LosingFocus += ContentButton_LosingFocus;
		base.PreviewKeyDown += ContentButton_PreviewKeyDown;
		base.KeyDown += ContentButton_KeyDown;
		base.PointerExited += ContentButton_PointerExited;
	}

	private void ContentButton_Unloaded(object sender, RoutedEventArgs e)
	{
		if (_timer != null)
		{
			_timer.Tick -= Timer_Tick;
			_timer = null;
		}
		base.SizeChanged -= ContentButton_SizeChanged;
		base.LosingFocus -= ContentButton_LosingFocus;
		base.KeyDown -= ContentButton_KeyDown;
		base.PointerExited -= ContentButton_PointerExited;
		base.PreviewKeyDown -= ContentButton_PreviewKeyDown;
	}

	private void ContentButton_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (!e.KeyStatus.WasKeyDown && ShouldInvokeActions(e.Key))
		{
			_wasKeyDown = true;
		}
	}

	private void ContentButton_KeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (IsPressAndHoldEnabled && e.KeyStatus.WasKeyDown && e.Key == VirtualKey.Space && !_timer.IsEnabled)
		{
			base.PointerExited -= ContentButton_PointerExited;
			InvokeActions();
			StartTimer();
			_pressedOnce = false;
		}
	}

	protected override void OnKeyUp(KeyRoutedEventArgs e)
	{
		if (IsKeyUpFromExternalSource())
		{
			base.OnKeyUp(e);
			return;
		}
		if (!_pressedOnce)
		{
			_pressedOnce = true;
			StopTimer();
			if (ShouldCancelCommand(e.Key))
			{
				CancelCommandExecution(e);
				return;
			}
		}
		else if (ShouldInvokeActions(e.Key))
		{
			InvokeActions(isKeyUP: true);
		}
		base.OnKeyUp(e);
	}

	private void ContentButton_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		StopTimer();
	}

	protected override void OnPointerPressed(PointerRoutedEventArgs e)
	{
		e.Handled = true;
		if (IsPressAndHoldEnabled)
		{
			InvokeActions();
			StartTimer();
		}
		UpdateVisualState("Pressed");
		base.OnPointerPressed(e);
	}

	protected override void OnPointerReleased(PointerRoutedEventArgs e)
	{
		if (!IsPressAndHoldEnabled)
		{
			InvokeActions();
		}
		else
		{
			StopTimer();
		}
		base.OnPointerReleased(e);
		if (base.IsPointerOver)
		{
			UpdateVisualState("PointerOver");
		}
		else
		{
			UpdateVisualState("Normal");
		}
		Focus(FocusState.Pointer);
	}

	private void ContentButton_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		this.RoundResizedContentButton(e, Shape);
	}

	private void ContentButton_LosingFocus(UIElement sender, LosingFocusEventArgs args)
	{
		StopTimer();
	}

	private void Timer_Tick(object sender, object e)
	{
		InvokeActions();
	}

	private bool ShouldCancelCommand(VirtualKey key)
	{
		if (IsPressAndHoldEnabled)
		{
			return key == VirtualKey.Space;
		}
		return false;
	}

	private void CancelCommandExecution(KeyRoutedEventArgs e)
	{
		ICommand command = base.Command;
		base.Command = null;
		base.OnKeyUp(e);
		base.Command = command;
		base.PointerExited += ContentButton_PointerExited;
	}

	private bool IsKeyUpFromExternalSource()
	{
		bool result = !_wasKeyDown;
		_wasKeyDown = false;
		return result;
	}

	private bool ShouldInvokeActions(VirtualKey key)
	{
		if (key != VirtualKey.Space)
		{
			return key == VirtualKey.Enter;
		}
		return true;
	}

	private void InvokeActions(bool isKeyUP = false)
	{
		this.Click?.Invoke(this, new RoutedEventArgs());
		if (!isKeyUP)
		{
			ICommand command = base.Command;
			if (command != null && command.CanExecute(base.CommandParameter))
			{
				base.Command?.Execute(base.CommandParameter);
			}
		}
		base.Flyout?.ShowAt(this);
	}

	private void UpdatePressAndHoldIntervalProperty()
	{
		int num = ((PressAndHoldInterval <= 0) ? 250 : PressAndHoldInterval);
		_timer.Interval = TimeSpan.FromMilliseconds(num);
	}

	private void StartTimer()
	{
		if (IsPressAndHoldEnabled)
		{
			_timer.Start();
		}
	}

	private void StopTimer()
	{
		if (IsPressAndHoldEnabled)
		{
			_timer.Stop();
		}
	}

	private void UpdateVisualState(string state)
	{
		VisualStateManager.GoToState(this, state, useTransitions: true);
	}
}
