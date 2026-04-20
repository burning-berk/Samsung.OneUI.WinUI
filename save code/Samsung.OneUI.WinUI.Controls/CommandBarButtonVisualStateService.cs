using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Samsung.OneUI.WinUI.Controls;

internal class CommandBarButtonVisualStateService
{
	private const string ENABLED_STATE = "Enabled";

	private const string DISABLED_STATE = "Disabled";

	private readonly ButtonBase _buttonBase;

	private long _tokenButtonVisibilityChanged;

	private long _tokenButtonIsEnabledChanged;

	public CommandBarButtonVisualStateService(ButtonBase buttonBase, Grid buttonTextLabelGrid)
	{
		_buttonBase = buttonBase;
		UpdateDisabledVisualState();
	}

	private void ButtonBase_OnIsEnabledPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		UpdateDisabledVisualState();
	}

	private void ButtonBase_OnVisibilityPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		UpdateDisabledVisualState();
	}

	internal void RegisterEvents()
	{
		if (_buttonBase != null)
		{
			_tokenButtonVisibilityChanged = _buttonBase.RegisterPropertyChangedCallback(UIElement.VisibilityProperty, ButtonBase_OnVisibilityPropertyChanged);
			_tokenButtonIsEnabledChanged = _buttonBase.RegisterPropertyChangedCallback(Control.IsEnabledProperty, ButtonBase_OnIsEnabledPropertyChanged);
		}
	}

	internal void UnregisterEvents()
	{
		if (_buttonBase != null)
		{
			_buttonBase.UnregisterPropertyChangedCallback(Control.IsEnabledProperty, _tokenButtonIsEnabledChanged);
			_buttonBase.UnregisterPropertyChangedCallback(UIElement.VisibilityProperty, _tokenButtonVisibilityChanged);
		}
	}

	internal void UpdateDisabledVisualState()
	{
		if (!(_buttonBase == null))
		{
			DispatcherQueue.GetForCurrentThread().TryEnqueue(delegate
			{
				VisualStateManager.GoToState(_buttonBase, _buttonBase.IsEnabled ? "Enabled" : "Disabled", useTransitions: false);
			});
		}
	}
}
