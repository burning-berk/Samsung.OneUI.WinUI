using System;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls.EventHandlers;

public class OneUIContentDialogClosingEventArgs
{
	public Func<ContentDialogClosingDeferral> GetDeferral;

	public bool Cancel { get; set; }

	public ContentDialogResult Result { get; set; }

	public OneUIContentDialogClosingEventArgs(ContentDialogClosingEventArgs originalArgs)
	{
		Cancel = originalArgs.Cancel;
		Result = originalArgs.Result;
		GetDeferral = () => originalArgs.GetDeferral();
	}
}
