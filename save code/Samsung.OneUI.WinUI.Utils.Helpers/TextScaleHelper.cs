using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Utils.Helpers;

public static class TextScaleHelper
{
	private static double _lastScale = new UISettings().TextScaleFactor;

	public static bool IsTextScaleChanged()
	{
		double textScaleFactor = new UISettings().TextScaleFactor;
		bool result = textScaleFactor != _lastScale;
		_lastScale = textScaleFactor;
		return result;
	}

	public static bool EqualOrGreaterThan(double factor)
	{
		return new UISettings().TextScaleFactor >= factor;
	}
}
