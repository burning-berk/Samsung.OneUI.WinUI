using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Utils.Helpers;

internal static class ScreenHelpers
{
	private const double DEFAULT_SCREEN_WIDTH = 1920.0;

	private const double DEFAULT_SCREEN_HEIGHT = 1080.0;

	public static Size GetScreenSize(XamlRoot xamlRoot)
	{
		double width = 0.0;
		double height = 0.0;
		if (xamlRoot == null)
		{
			return new Size(1920.0, 1080.0);
		}
		if (xamlRoot.ContentIslandEnvironment != null)
		{
			DisplayArea fromDisplayId = DisplayArea.GetFromDisplayId(xamlRoot.ContentIslandEnvironment.DisplayId);
			int num = fromDisplayId.WorkArea.Height + fromDisplayId.WorkArea.Y;
			width = (double)fromDisplayId.WorkArea.Width / xamlRoot.RasterizationScale;
			height = (double)num / xamlRoot.RasterizationScale;
		}
		return new Size(width, height);
	}
}
