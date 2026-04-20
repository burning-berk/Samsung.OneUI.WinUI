using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

internal class BuilderProperties
{
	public ScrollViewer scrollViewer { get; private set; }

	public BuilderProperties(ScrollViewer scrollViewer)
	{
		this.scrollViewer = scrollViewer;
	}
}
