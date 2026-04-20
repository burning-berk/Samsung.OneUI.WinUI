using Microsoft.UI.Xaml;
using Windows.System.Profile;

namespace Samsung.OneUI.WinUI.Common;

internal abstract class DpiChangedStateTriggerBase : StateTriggerBase
{
	private const int MULTIPLIER_FACTOR = 100;

	private const int WIN11_BUILD_VERSION = 22000;

	private const int SHIFTING_VALUE = 16;

	private readonly AnalyticsVersionInfo _osVersionInfo = AnalyticsInfo.VersionInfo;

	private bool _isOSVersionExpected;

	public static readonly DependencyProperty OsVersionExpectedProperty = DependencyProperty.Register("OsVersionExpected", typeof(OSVersionType), typeof(DpiChangedStateTriggerBase), new PropertyMetadata(OSVersionType.Win11, null));

	protected abstract int Scale { get; }

	public OSVersionType OsVersionExpected
	{
		get
		{
			return (OSVersionType)GetValue(OsVersionExpectedProperty);
		}
		set
		{
			SetValue(OsVersionExpectedProperty, value);
		}
	}

	public void CheckDpiStateTrigger(XamlRoot xamlRoot)
	{
		CheckOsVersionExpected();
		UpdateStateTriggers(xamlRoot);
		if (xamlRoot != null)
		{
			xamlRoot.Changed += XamlRoot_Changed;
		}
	}

	public void UpdateStateTriggers(XamlRoot xamlRoot)
	{
		bool active = xamlRoot != null && (double)Scale == xamlRoot.RasterizationScale * 100.0 && _isOSVersionExpected;
		SetActive(active);
	}

	private void XamlRoot_Changed(XamlRoot sender, XamlRootChangedEventArgs args)
	{
		UpdateStateTriggers(sender);
	}

	private void CheckOsVersionExpected()
	{
		ulong build = (ulong.Parse(_osVersionInfo.DeviceFamilyVersion) & 0xFFFF0000u) >> 16;
		_isOSVersionExpected = IsExpectedWin11Version(build) || IsExpectedWin10Version(build) || IsVersionFlexible();
	}

	private bool IsExpectedWin11Version(ulong build)
	{
		if (build >= 22000)
		{
			return OsVersionExpected == OSVersionType.Win11;
		}
		return false;
	}

	private bool IsExpectedWin10Version(ulong build)
	{
		if (build < 22000)
		{
			return OsVersionExpected == OSVersionType.Win10;
		}
		return false;
	}

	private bool IsVersionFlexible()
	{
		return OsVersionExpected == OSVersionType.Both;
	}
}
