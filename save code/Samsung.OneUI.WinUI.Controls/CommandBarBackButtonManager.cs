using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls.Selectors;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

internal class CommandBarBackButtonManager
{
	private const string DEFAULT_BACK_BUTTON_TOOLTIP_CONTENT = "DREAM_UP_OPT/Content";

	private const string DREAM_IDLE_TBBODY_DETAILS_VIEW_EXPANDED = "DREAM_IDLE_TBBODY_DETAILS_VIEW_EXPANDED";

	private const string DREAM_IDLE_TBBODY_DETAILS_VIEW_COLLAPSED = "DREAM_IDLE_TBBODY_DETAILS_VIEW_COLLAPSED";

	private const string DREAM_IDLE_TBOPT_EXPAND_DETAILS_VIEW = "DREAM_IDLE_TBOPT_EXPAND_DETAILS_VIEW";

	private const string DREAM_IDLE_TBOPT_COLLAPSE_DETAILS_VIEW = "DREAM_IDLE_TBOPT_COLLAPSE_DETAILS_VIEW";

	private const string CLOSE_PANE_BUTTON_STYLE = "OneUICommandBarClosePaneButtonStyle";

	private const string OPEN_PANE_BUTTON_STYLE = "OneUICommandBarOpenPaneButtonStyle";

	private const string BACK_BUTTON_VISIBLE_STATE = "BackButtonVisible";

	private const string BACK_BUTTON_COLLAPSED_STATE = "BackButtonCollapsed";

	private readonly CommandBar _commandBar;

	private readonly Button _backButton;

	private MultiPane _parentMultiPane;

	private readonly ToolTip _backButtonToolTip = new ToolTip
	{
		IsTabStop = false,
		VerticalOffset = 10.0
	};

	public CommandBarBackButtonManager(Button backButton, CommandBar commandBar)
	{
		_commandBar = commandBar;
		_backButton = backButton;
	}

	public void UpdateStringResourcesForBackButton()
	{
		string empty = string.Empty;
		if (_commandBar.BackButtonType == CommandBarBackButtonType.CollapsibleSideMenu && _parentMultiPane != null)
		{
			_backButtonToolTip.Content = (_parentMultiPane.IsPaneOpen ? "DREAM_IDLE_TBBODY_DETAILS_VIEW_EXPANDED".GetLocalized() : "DREAM_IDLE_TBBODY_DETAILS_VIEW_COLLAPSED".GetLocalized());
			empty = (_parentMultiPane.IsPaneOpen ? "DREAM_IDLE_TBOPT_EXPAND_DETAILS_VIEW".GetLocalized() : "DREAM_IDLE_TBOPT_COLLAPSE_DETAILS_VIEW".GetLocalized());
		}
		else
		{
			_backButtonToolTip.Content = "DREAM_UP_OPT/Content".GetLocalized();
			empty = "DREAM_UP_OPT/Content".GetLocalized();
		}
		_backButton.SetValue(AutomationProperties.NameProperty, empty);
	}

	public void ChangeButtonToExpandedOrCollapsedStyle(bool isPaneOpen)
	{
		if (_backButton != null)
		{
			_backButton.Style = (isPaneOpen ? "OneUICommandBarClosePaneButtonStyle".GetStyle() : "OneUICommandBarOpenPaneButtonStyle".GetStyle());
			UpdateStringResourcesForBackButton();
		}
	}

	public void ApplyBackButtonStyle()
	{
		if (!(_backButton == null) && _commandBar.BackButtonType == CommandBarBackButtonType.CollapsibleSideMenu && _parentMultiPane != null)
		{
			_backButton.Style = new CommandBarButtonStyleSelector(_commandBar.BackButtonType, _parentMultiPane.IsPaneOpen).SelectStyle();
		}
	}

	public void Load()
	{
		AssignBackButtonClickEvent();
		AddEventsOpenCloseMultiPane();
		ApplyBackButtonStyle();
		GoToStateBackButtonVisibility();
		UpdateBackButtonToolTip();
	}

	public void GoToStateBackButtonVisibility()
	{
		VisualStateManager.GoToState(_commandBar, _commandBar.IsBackButtonVisible ? "BackButtonVisible" : "BackButtonCollapsed", useTransitions: false);
	}

	internal void AssignBackButtonClickEvent()
	{
		if (!(_backButton == null))
		{
			_backButton.Click -= OnButtonClick;
			_backButton.Click += OnButtonClick;
		}
	}

	private void UpdateBackButtonToolTip()
	{
		ToolTipService.SetToolTip(_backButton, null);
		UpdateStringResourcesForBackButton();
		ToolTipService.SetToolTip(_backButton, _backButtonToolTip);
	}

	private void OnButtonClick(object sender, RoutedEventArgs e)
	{
		if (_commandBar.BackButtonType == CommandBarBackButtonType.CollapsibleSideMenu)
		{
			HandleBackButtonMultiPane();
		}
		_commandBar.IsOpen = false;
		_commandBar.InvokeBackButtonEvent();
		if (_commandBar.BackButtonCommand != null && _commandBar.BackButtonCommand.CanExecute(_commandBar.BackButtonCommandParameter))
		{
			_commandBar.BackButtonCommand.Execute(_commandBar.BackButtonCommandParameter);
		}
	}

	private void AddEventsOpenCloseMultiPane()
	{
		if (_commandBar.BackButtonType == CommandBarBackButtonType.CollapsibleSideMenu)
		{
			_parentMultiPane = UIExtensionsInternal.GetVisualParent<MultiPane>(_commandBar);
			if (_parentMultiPane != null)
			{
				_parentMultiPane.PaneOpened += ParentMultiPane_PaneOpened;
				_parentMultiPane.PaneClosed += ParentMultiPane_PaneClosed;
			}
		}
	}

	private void ParentMultiPane_PaneClosed(SplitView sender, object args)
	{
		ChangeButtonToExpandedOrCollapsedStyle(_parentMultiPane.IsPaneOpen);
	}

	private void ParentMultiPane_PaneOpened(SplitView sender, object args)
	{
		ChangeButtonToExpandedOrCollapsedStyle(_parentMultiPane.IsPaneOpen);
	}

	private void HandleBackButtonMultiPane()
	{
		if (_parentMultiPane != null)
		{
			_parentMultiPane.OpenPane(_parentMultiPane.IsPaneOpen);
		}
	}
}
