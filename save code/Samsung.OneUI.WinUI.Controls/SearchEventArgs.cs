using System;

namespace Samsung.OneUI.WinUI.Controls;

internal class SearchEventArgs : EventArgs
{
	public SeachPopupCloseEventType Type { get; set; }

	public SearchEventArgs(SeachPopupCloseEventType closedType)
	{
		Type = closedType;
	}
}
