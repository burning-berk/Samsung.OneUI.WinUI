using System;
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Utils.Helpers;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls.BGBlurControl;

[WinRTRuntimeClassName("Windows.Foundation.IClosable")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_BGBlurControl_WindowAcrylicSessionWinRTTypeDetails))]
public sealed class WindowAcrylicSession : IDisposable
{
	private const string FALLBACK_COLOR = "#4D4D52";

	private const int FALLBACK_COLOR_ALPHA_LIGHT = 8;

	private const int FALLBACK_COLOR_ALPHA_DARK = 12;

	private readonly BGBlur _bG_BlurLayer;

	private readonly DesktopAcrylicController _controller;

	private readonly SystemBackdropConfiguration _config;

	private WindowAcrylicSession(DesktopAcrylicController controller, SystemBackdropConfiguration config, BGBlur bG_BlurLayer)
	{
		_controller = controller;
		_config = config;
		_bG_BlurLayer = bG_BlurLayer;
		UpdateFallbackColor();
		UpdateConfigTheme();
		RegisterEvents();
	}

	public static WindowAcrylicSession? TryWindowApplyAcrylic(Window window, BGBlur bG_BlurLayer)
	{
		if (!DesktopAcrylicController.IsSupported())
		{
			return null;
		}
		SystemBackdropConfiguration systemBackdropConfiguration = new SystemBackdropConfiguration
		{
			IsInputActive = true
		};
		DesktopAcrylicController desktopAcrylicController = new DesktopAcrylicController();
		desktopAcrylicController.Kind = DesktopAcrylicKind.Base;
		desktopAcrylicController.TintOpacity = 0f;
		desktopAcrylicController.LuminosityOpacity = 0f;
		desktopAcrylicController.AddSystemBackdropTarget(CastExtensions.As<ICompositionSupportsSystemBackdrop>(window));
		desktopAcrylicController.SetSystemBackdropConfiguration(systemBackdropConfiguration);
		return new WindowAcrylicSession(desktopAcrylicController, systemBackdropConfiguration, bG_BlurLayer);
	}

	public void UpdateWindowActivation(WindowActivatedEventArgs args)
	{
		if (!(args == null) && !(_config == null))
		{
			_config.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
		}
	}

	public void Dispose()
	{
		UnregisterEvents();
		_controller.Dispose();
		GC.SuppressFinalize(this);
	}

	private void RegisterEvents()
	{
		if (_bG_BlurLayer != null)
		{
			_bG_BlurLayer.ActualThemeChanged += OnThemeChanged;
		}
	}

	private void UnregisterEvents()
	{
		if (_bG_BlurLayer != null)
		{
			_bG_BlurLayer.ActualThemeChanged -= OnThemeChanged;
		}
	}

	private void OnThemeChanged(FrameworkElement sender, object args)
	{
		UpdateFallbackColor();
		UpdateConfigTheme();
	}

	private void UpdateFallbackColor()
	{
		if (!(_bG_BlurLayer == null) && !(_controller == null))
		{
			if (_bG_BlurLayer.ActualTheme == ElementTheme.Dark)
			{
				_controller.TintColor = ColorsHelpers.ConvertHexToColor("#4D4D52", 12.0);
			}
			else
			{
				_controller.TintColor = ColorsHelpers.ConvertHexToColor("#4D4D52", 8.0);
			}
		}
	}

	private void UpdateConfigTheme()
	{
		if (!(_bG_BlurLayer == null))
		{
			switch (_bG_BlurLayer.ActualTheme)
			{
			case ElementTheme.Dark:
				_config.Theme = SystemBackdropTheme.Dark;
				break;
			case ElementTheme.Light:
				_config.Theme = SystemBackdropTheme.Light;
				break;
			case ElementTheme.Default:
				_config.Theme = SystemBackdropTheme.Default;
				break;
			}
		}
	}
}
