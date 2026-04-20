using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Samsung.OneUI.WinUI.Utils.Helpers;

internal static class MouseHelpers
{
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetCursorPos(out Point lpPoint);

	public static Point GetMousePosition()
	{
		GetCursorPos(out var lpPoint);
		return lpPoint;
	}

	public static double GetMousePositionX(double screenRatio = 1.0)
	{
		return Math.Abs((double)GetMousePosition().X / screenRatio);
	}

	public static double GetMousePositionY(double screenRatio = 1.0)
	{
		return Math.Abs((double)GetMousePosition().Y / screenRatio);
	}
}
