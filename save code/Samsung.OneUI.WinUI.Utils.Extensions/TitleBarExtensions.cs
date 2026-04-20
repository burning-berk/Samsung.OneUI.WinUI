using System;
using System.Runtime.InteropServices;
using CommunityToolkit.WinUI.Helpers;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.UI;
using WinRT.Interop;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

public static class TitleBarExtensions
{
	private struct MARGINS
	{
		public int cxLeftWidth;

		public int cxRightWidth;

		public int cyTopHeight;

		public int cyBottomHeight;
	}

	private const string LIGHT_THEME_SOURCE = "ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Light/Colors.xaml";

	private const string DARK_THEME_SOURCE = "ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Dark/Colors.xaml";

	private const double OPACITY_INACTIVE = 0.36;

	private const string APP_TITLE_BAR_BACKGROUND_COLOR = "OneUIColorBackground";

	private const string APP_TITLE_BAR_FOREGROUND_COLOR = "OneUIColorTitlebarOnSurfaceHighest";

	private const string APP_TITLE_BAR_BUTTON_HOVER_COLOR = "OneUIColorTitlebarHoveredOnSurface";

	private const string APP_TITLE_BAR_BUTTON_BACKGROUND_COLOR = "OneUIColorBackground";

	private const string APP_TITLE_BAR_BUTTON_PRESSED_COLOR = "OneUIColorTitlebarPressedOnSurface";

	private const string APP_TITLE_BAR_BUTTON_PRESSED_FOREGROUND_COLOR = "OneUIColorTitlebarOnSurfaceHighest";

	internal static void Customize(this AppWindow appWindow, ElementTheme requestedTheme)
	{
		if (AppWindowTitleBar.IsCustomizationSupported() && appWindow != null && appWindow.TitleBar != null)
		{
			bool flag = requestedTheme == ElementTheme.Light;
			AppWindowTitleBar titleBar = appWindow.TitleBar;
			ResourceDictionary resourceDictionary = new ResourceDictionary();
			resourceDictionary.ThemeDictionaries.Add("Light", new ResourceDictionary
			{
				Source = new Uri("ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Light/Colors.xaml")
			});
			resourceDictionary.ThemeDictionaries.Add("Dark", new ResourceDictionary
			{
				Source = new Uri("ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Dark/Colors.xaml")
			});
			Func<string, Color> func = (string source) => (Color)((ResourceDictionary)resourceDictionary.ThemeDictionaries[$"{requestedTheme}"])[source];
			titleBar.BackgroundColor = func("OneUIColorBackground");
			titleBar.ButtonBackgroundColor = func("OneUIColorBackground");
			titleBar.ButtonInactiveBackgroundColor = func("OneUIColorBackground");
			titleBar.InactiveBackgroundColor = func("OneUIColorBackground");
			titleBar.ForegroundColor = BlendWithOpacity(func("OneUIColorTitlebarOnSurfaceHighest"), func("OneUIColorTitlebarOnSurfaceHighest").ToHsl().A, Colors.Transparent);
			titleBar.ButtonHoverForegroundColor = BlendWithOpacity(func("OneUIColorTitlebarOnSurfaceHighest"), func("OneUIColorTitlebarOnSurfaceHighest").ToHsl().A, func("OneUIColorBackground"));
			titleBar.ButtonForegroundColor = BlendWithOpacity(func("OneUIColorTitlebarOnSurfaceHighest"), func("OneUIColorTitlebarOnSurfaceHighest").ToHsl().A, func("OneUIColorBackground"));
			titleBar.ButtonHoverBackgroundColor = BlendWithOpacity(func("OneUIColorTitlebarHoveredOnSurface"), func("OneUIColorTitlebarHoveredOnSurface").ToHsl().A, func("OneUIColorBackground"));
			titleBar.ButtonPressedBackgroundColor = BlendWithOpacity(func("OneUIColorTitlebarPressedOnSurface"), func("OneUIColorTitlebarPressedOnSurface").ToHsl().A, func("OneUIColorBackground"));
			titleBar.ButtonPressedForegroundColor = BlendWithOpacity(func("OneUIColorTitlebarOnSurfaceHighest"), func("OneUIColorTitlebarOnSurfaceHighest").ToHsl().A, func("OneUIColorBackground"));
			titleBar.InactiveForegroundColor = BlendWithOpacity(func("OneUIColorTitlebarOnSurfaceHighest"), 0.36, flag ? Colors.Transparent : func("OneUIColorBackground"));
			titleBar.ButtonInactiveForegroundColor = BlendWithOpacity(func("OneUIColorTitlebarOnSurfaceHighest"), 0.36, flag ? Colors.Transparent : func("OneUIColorBackground"));
		}
	}

	public static Color BlendWithOpacity(Color foreground, double opacity, Color background)
	{
		byte r = (byte)Math.Round((1.0 - opacity) * (double)(int)background.R + opacity * (double)(int)foreground.R);
		byte g = (byte)Math.Round((1.0 - opacity) * (double)(int)background.G + opacity * (double)(int)foreground.G);
		byte b = (byte)Math.Round((1.0 - opacity) * (double)(int)background.B + opacity * (double)(int)foreground.B);
		return Color.FromArgb(byte.MaxValue, r, g, b);
	}

	[DllImport("dwmapi")]
	private static extern nint DwmExtendFrameIntoClientArea(nint hWnd, ref MARGINS pMarInset);

	[DllImport("UXTheme.dll")]
	private static extern bool ShouldSystemUseDarkMode();

	public static void SetAdjustBar(this Window window, ElementTheme requestedTheme)
	{
		if (window != null && requestedTheme != ElementTheme.Dark)
		{
			nint handle = WindowNative.GetWindowHandle(window);
			MARGINS margins = new MARGINS
			{
				cxLeftWidth = 0,
				cxRightWidth = 0,
				cyBottomHeight = 0,
				cyTopHeight = 2
			};
			window.Activated += delegate
			{
				DwmExtendFrameIntoClientArea(handle, ref margins);
			};
		}
	}
}
