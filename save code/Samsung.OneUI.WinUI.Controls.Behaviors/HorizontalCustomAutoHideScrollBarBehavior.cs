using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls.Behaviors;

public class HorizontalCustomAutoHideScrollBarBehavior : CustomAutoHideScrollBarBehavior
{
	protected override string GetScrollBarElementName()
	{
		return "HorizontalScrollBar";
	}

	protected override string GetScrollRootGridElementName()
	{
		return "HorizontalRoot";
	}

	protected override double GetScrollableCurrentSize(ScrollViewer scrollViewer)
	{
		return scrollViewer.ScrollableWidth;
	}
}
