using System;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Common;

internal class DpiChangedTo175StateTrigger : DpiChangedStateTriggerBase
{
	protected readonly DpiChangedTo175StateTrigger _instance;

	private static XamlRoot _xamlRoot;

	public static XamlRoot XamlRoot
	{
		get
		{
			return _xamlRoot;
		}
		set
		{
			_xamlRoot = value;
			OnXamlRootChangedEvent();
		}
	}

	protected override int Scale => 175;

	public static event EventHandler XamlRootChangedEvent;

	public DpiChangedTo175StateTrigger()
	{
		_instance = this;
		XamlRootChangedEvent += OnXamlRootChanged;
	}

	private static void OnXamlRootChangedEvent()
	{
		DpiChangedTo175StateTrigger.XamlRootChangedEvent?.Invoke(null, EventArgs.Empty);
	}

	private void OnXamlRootChanged(object sender, EventArgs e)
	{
		_instance?.CheckDpiStateTrigger(XamlRoot);
	}
}
