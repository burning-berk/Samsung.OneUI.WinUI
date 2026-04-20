using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace Samsung.OneUI.WinUI.Controls;

internal class DropdownListVisualStateService
{
	private const string STATE_NORMAL = "Normal";

	private const string STATE_POINTER_OVER = "PointerOver";

	private const string STATE_PRESSED = "Pressed";

	private const string STATE_DISABLED = "Disabled";

	private const string STATE_FOCUSED = "Focused";

	private const string STATE_UNFOCUSED = "Unfocused";

	private bool _isPointerPressed;

	private bool _isPointerOver;

	private Control _control;

	private FrameworkElement _rootElement;

	public EventHandler<PointerRoutedEventArgs> PointerPressed;

	public EventHandler<PointerRoutedEventArgs> PointerEntered;

	public EventHandler<PointerRoutedEventArgs> PointerReleased;

	public EventHandler<PointerRoutedEventArgs> PointerExited;

	public EventHandler<GettingFocusEventArgs> GettingFocus;

	public void AddConfiguration(Control control, FrameworkElement rootElement)
	{
		DisposeEvent();
		_control = control;
		_rootElement = rootElement;
		if (!(_rootElement == null) && !(_control == null))
		{
			_rootElement.PointerPressed += OnPointerPressed;
			_rootElement.PointerEntered += OnPointerEntered;
			_rootElement.PointerExited += OnPointerExited;
			_rootElement.PointerReleased += OnPointerReleased;
			_rootElement.GettingFocus += OnGettingFocus;
			_rootElement.LosingFocus += OnLosingFocus;
			_control.IsEnabledChanged += OnIsEnabledChanged;
		}
	}

	private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		UpdateVisualState();
	}

	private void OnGettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		if (args.InputDevice == FocusInputDeviceKind.Keyboard)
		{
			VisualStateManager.GoToState(_control, "Focused", useTransitions: true);
			GettingFocus?.Invoke(sender, args);
		}
	}

	private void OnLosingFocus(UIElement sender, LosingFocusEventArgs args)
	{
		if (args.InputDevice == FocusInputDeviceKind.Keyboard || args.InputDevice == FocusInputDeviceKind.Mouse)
		{
			VisualStateManager.GoToState(_control, "Unfocused", useTransitions: true);
		}
	}

	private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
	{
		_isPointerOver = true;
		UpdateVisualState();
		PointerEntered?.Invoke(sender, e);
	}

	private void OnPointerExited(object sender, PointerRoutedEventArgs e)
	{
		_isPointerOver = false;
		_isPointerPressed = false;
		UpdateVisualState();
		PointerExited(sender, e);
	}

	private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
	{
		_isPointerPressed = true;
		UpdateVisualState();
		PointerPressed?.Invoke(sender, e);
	}

	private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
	{
		_isPointerPressed = false;
		UpdateVisualState();
		PointerReleased?.Invoke(sender, e);
	}

	private void UpdateVisualState()
	{
		if (!_control.IsEnabled)
		{
			VisualStateManager.GoToState(_control, "Disabled", useTransitions: false);
		}
		else if (_isPointerPressed)
		{
			VisualStateManager.GoToState(_control, "Pressed", useTransitions: false);
		}
		else if (_isPointerOver)
		{
			VisualStateManager.GoToState(_control, "PointerOver", useTransitions: false);
		}
		else
		{
			VisualStateManager.GoToState(_control, "Normal", useTransitions: false);
		}
	}

	private void DisposeEvent()
	{
		if (!(_rootElement == null) && !(_control == null))
		{
			_rootElement.PointerPressed -= OnPointerPressed;
			_rootElement.PointerEntered -= OnPointerEntered;
			_rootElement.PointerExited -= OnPointerExited;
			_rootElement.PointerReleased -= OnPointerReleased;
			_rootElement.GettingFocus -= OnGettingFocus;
			_rootElement.LosingFocus -= OnLosingFocus;
			_control.IsEnabledChanged -= OnIsEnabledChanged;
		}
	}
}
