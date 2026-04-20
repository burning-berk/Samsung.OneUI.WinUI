using System;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Common;

internal class DpiChangedTo150StateTrigger : DpiChangedStateTriggerBase
{
	protected readonly DpiChangedTo150StateTrigger _instance;

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

	protected override int Scale => 150;

	public static event EventHandler XamlRootChangedEvent;

	public DpiChangedTo150StateTrigger()
	{
		_instance = this;
		XamlRootChangedEvent += OnXamlRootChanged;
	}

	private static void OnXamlRootChangedEvent()
	{
		DpiChangedTo150StateTrigger.XamlRootChangedEvent?.Invoke(null, EventArgs.Empty);
	}

	private void OnXamlRootChanged(object sender, EventArgs e)
	{
		_instance?.CheckDpiStateTrigger(XamlRoot);
	}
}
