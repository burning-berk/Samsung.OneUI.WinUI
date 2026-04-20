using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Samsung.OneUI.WinUI.Controls.Inputs.DropdownList;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class DropdownListViewCustom : ListView
{
	private const string DROPDOWN_LIST_TITLE = "ListTitle";

	private const string DROPDOWN_LIST_TITLE_CONTAINER = "ListTitleContainer";

	private const string TEXT = "Text";

	private TextBlock _listTitle;

	private Grid _listTitleContainer;

	public DropdownListViewCustom()
	{
		base.DefaultStyleKey = typeof(DropdownListViewCustom);
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new DropdownListViewCustomAutomationPeer(this);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_listTitleContainer = GetTemplateChild("ListTitleContainer") as Grid;
		_listTitle = GetTemplateChild("ListTitle") as TextBlock;
		if (_listTitle != null)
		{
			_listTitle.IsTextTrimmedChanged += Title_IsTextTrimmedChanged;
		}
	}

	private void Title_IsTextTrimmedChanged(TextBlock sender, IsTextTrimmedChangedEventArgs args)
	{
		UpdateTooltip();
	}

	internal void UpdateListTitleText(string content)
	{
		if (!(_listTitle == null) && !string.IsNullOrEmpty(content))
		{
			_listTitle.Text = content;
		}
	}

	internal void UpdateListTitleVisibility(Visibility ListTitleVisibility)
	{
		if (!(_listTitle == null) && !string.IsNullOrEmpty(_listTitle.Text) && !(_listTitleContainer == null))
		{
			_listTitleContainer.Visibility = ListTitleVisibility;
		}
	}

	internal bool ValidateShowListTitle(Visibility ListTitleVisibility)
	{
		if (IsListTitleValid() && IsListTitleContainerValid())
		{
			return IsListTitleVisible();
		}
		return false;
	}

	private bool IsListTitleValid()
	{
		if (_listTitle != null)
		{
			return !string.IsNullOrEmpty(_listTitle.Text);
		}
		return false;
	}

	private bool IsListTitleContainerValid()
	{
		return _listTitleContainer != null;
	}

	private bool IsListTitleVisible()
	{
		return _listTitleContainer.Visibility != Visibility.Collapsed;
	}

	private void UpdateTooltip()
	{
		if (!(_listTitle == null))
		{
			if (_listTitle.IsTextTrimmed)
			{
				ToolTip toolTip = new ToolTip();
				Binding binding = new Binding
				{
					Path = new PropertyPath("Text"),
					Source = _listTitle,
					Mode = BindingMode.OneWay
				};
				toolTip.SetBinding(ContentControl.ContentProperty, binding);
				ToolTipService.SetToolTip(_listTitle, toolTip);
			}
			else
			{
				ToolTipService.SetToolTip(_listTitle, null);
			}
		}
	}
}
