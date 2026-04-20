using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace Samsung.OneUI.WinUI.Services;

internal class UIElementClickService : IDisposable
{
	private UIElement _uiElement;

	private bool _isPointerInside;

	private bool _isPointerPressed;

	public RoutedEventHandler Clicked;

	public UIElementClickService(UIElement uiElement)
	{
		_uiElement = uiElement;
		if (_uiElement != null)
		{
			_uiElement.PointerPressed += UIElement_PointerPressed;
			_uiElement.PointerReleased += UIElement_PointerReleased;
			_uiElement.PointerEntered += UIElement_PointerEntered;
			_uiElement.PointerExited += UIElement_PointerExited;
		}
	}

	private void UIElement_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		_isPointerPressed = true;
	}

	private void UIElement_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		if (_isPointerPressed && _isPointerInside)
		{
			Clicked?.Invoke(sender, e);
		}
		_isPointerPressed = false;
	}

	private void UIElement_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		_isPointerInside = true;
	}

	private void UIElement_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		_isPointerInside = false;
		_isPointerPressed = false;
	}

	public void Dispose()
	{
		if (_uiElement != null)
		{
			_uiElement.PointerPressed -= UIElement_PointerPressed;
			_uiElement.PointerReleased -= UIElement_PointerReleased;
			_uiElement.PointerEntered -= UIElement_PointerEntered;
			_uiElement.PointerExited -= UIElement_PointerExited;
			_uiElement = null;
		}
	}
}
