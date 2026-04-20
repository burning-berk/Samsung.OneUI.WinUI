using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Utils.Extensions;
using WinRT.Interop;

namespace Samsung.OneUI.WinUI.Controls;

public class OneUIAppTitleBar
{
	private const double ACTIVE_OPACITY = 1.0;

	private const double INACTIVE_OPACITY = 0.36;

	private readonly Window window;

	private readonly Titlebar appTitleBar;

	public OneUIAppTitleBar(Window window, Titlebar appTitleBar)
	{
		this.window = window;
		this.appTitleBar = appTitleBar;
		if (this.window != null)
		{
			this.window.ExtendsContentIntoTitleBar = true;
			this.window.SetTitleBar(this.appTitleBar);
			this.window.Activated += Window_Activated;
		}
	}

	private void Window_Activated(object sender, WindowActivatedEventArgs args)
	{
		if (!(appTitleBar.TitlebarText == null))
		{
			if (args.WindowActivationState == WindowActivationState.Deactivated)
			{
				appTitleBar.TitlebarText.Opacity = 0.36;
			}
			else
			{
				appTitleBar.TitlebarText.Opacity = 1.0;
			}
		}
	}

	public void CustomizeTitleBar(ElementTheme theme)
	{
		if (window != null)
		{
			if (appTitleBar != null)
			{
				appTitleBar.RequestedTheme = theme;
			}
			AppWindow appWindowForCurrentWindow = GetAppWindowForCurrentWindow(window);
			window.SetAdjustBar(theme);
			if (appWindowForCurrentWindow != null && appWindowForCurrentWindow.TitleBar != null)
			{
				appWindowForCurrentWindow.TitleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
			}
			appWindowForCurrentWindow.Customize(theme);
		}
	}

	private AppWindow GetAppWindowForCurrentWindow(Window window)
	{
		return AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(WindowNative.GetWindowHandle(window)));
	}
}
