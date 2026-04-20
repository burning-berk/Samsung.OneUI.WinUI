using System;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

namespace Samsung.OneUI.WinUI.Controls;

internal class OverlayPopupService
{
	private readonly XamlRoot _xamlRoot;

	private readonly SolidColorBrush _background;

	private readonly double _opacity;

	private readonly Popup _overlayPopup;

	public EventHandler<PointerRoutedEventArgs> CloseOverlayPopupEvent;

	public OverlayPopupService(XamlRoot xamlRoot, SolidColorBrush background = null, double opacity = 0.0)
	{
		_xamlRoot = xamlRoot;
		_background = background ?? new SolidColorBrush(Colors.Transparent);
		_opacity = opacity;
		_overlayPopup = new Popup();
		BuildOverlayPopup();
	}

	private void BuildOverlayPopup()
	{
		if (!(_xamlRoot == null) && !(_xamlRoot.Content == null))
		{
			Grid grid = new Grid();
			grid.Background = _background;
			grid.Opacity = _opacity;
			_overlayPopup.Child = grid;
			_overlayPopup.XamlRoot = _xamlRoot;
		}
	}

	private void UpdateChildSize()
	{
		if (_overlayPopup.Child is Grid grid)
		{
			grid.Width = _xamlRoot.Content.ActualSize.X;
			grid.Height = _xamlRoot.Content.ActualSize.Y;
		}
	}

	internal void OpenOverlayPopup()
	{
		if (_overlayPopup.Child != null)
		{
			AssignPopupEvents();
			UpdateChildSize();
			_overlayPopup.IsLightDismissEnabled = true;
			_overlayPopup.IsOpen = true;
		}
	}

	internal void CloseOverlayPopup()
	{
		_overlayPopup.IsOpen = false;
	}

	internal bool IsOverlayPopupOpen()
	{
		return _overlayPopup.IsOpen;
	}

	private void AssignPopupEvents()
	{
		_overlayPopup.Closed += OverlayPopup_Closed;
		_overlayPopup.Child.PointerPressed += OverlayGrid_PointerPressed;
	}

	private void UnassignPopupEvents()
	{
		_overlayPopup.Child.PointerPressed -= OverlayGrid_PointerPressed;
		_overlayPopup.Closed -= OverlayPopup_Closed;
	}

	private void OverlayGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		_overlayPopup.IsOpen = false;
	}

	private void OverlayPopup_Closed(object sender, object e)
	{
		UnassignPopupEvents();
		CloseOverlayPopupEvent?.Invoke(this, null);
	}
}
