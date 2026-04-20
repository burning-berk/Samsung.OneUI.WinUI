using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls.Behaviors;

public class VerticalCustomAutoHideScrollBarBehavior : CustomAutoHideScrollBarBehavior
{
	protected override string GetScrollBarElementName()
	{
		return "VerticalScrollBar";
	}

	protected override string GetScrollRootGridElementName()
	{
		return "VerticalRoot";
	}

	protected override double GetScrollableCurrentSize(ScrollViewer scrollViewer)
	{
		return scrollViewer.ScrollableHeight;
	}
}
