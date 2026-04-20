using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Samsung.OneUI.WinUI.Controls;

internal class IconButtonTooltipVisibilityService
{
	private readonly Grid _buttonTextLabelGrid;

	private readonly ICommandBarItemOverflowable _commandBarItemOverflowable;

	private readonly ButtonBase _buttonBase;

	private long _tokenButtonTextLabelGridOnVisibilityChanged;

	internal Visibility LabelVisibility => _commandBarItemOverflowable?.GetButtonLabelVisibility() ?? Visibility.Visible;

	internal string Label => _commandBarItemOverflowable?.GetButtonLabel();

	public IconButtonTooltipVisibilityService(ICommandBarItemOverflowable commandBarItemOverflowable, ButtonBase buttonBase, Grid buttonTextLabelGrid)
	{
		_commandBarItemOverflowable = commandBarItemOverflowable;
		_buttonBase = buttonBase;
		_buttonTextLabelGrid = buttonTextLabelGrid;
		UpdateTooltipVisibility();
	}

	private void ButtonTextLabelGrid_OnVisibilityPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		UpdateTooltipVisibility();
	}

	internal void RegisterEvents()
	{
		if (_buttonTextLabelGrid != null)
		{
			_tokenButtonTextLabelGridOnVisibilityChanged = _buttonTextLabelGrid.RegisterPropertyChangedCallback(UIElement.VisibilityProperty, ButtonTextLabelGrid_OnVisibilityPropertyChanged);
		}
	}

	internal void UnregisterEvents()
	{
		if (_buttonTextLabelGrid != null)
		{
			_buttonTextLabelGrid.UnregisterPropertyChangedCallback(UIElement.VisibilityProperty, _tokenButtonTextLabelGridOnVisibilityChanged);
		}
	}

	internal void UpdateTooltipVisibility()
	{
		if (!(_buttonBase == null))
		{
			ToolTip toolTip = ToolTipService.GetToolTip(_buttonBase) as ToolTip;
			if (!(toolTip == null) && !(_buttonTextLabelGrid == null))
			{
				toolTip.Visibility = ((_buttonTextLabelGrid.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible);
			}
		}
	}
}
